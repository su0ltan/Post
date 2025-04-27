
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Post.Application.CustomException;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Application.Mappers;
using Post.Application.Repositories;
using Post.Application.Validation;
using Post.Common.DTOs.Reply;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Application.Implementation
{
    public class ReplyService : IReplyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAppLogger<Reply> _logger;
        private readonly UserManager<User> _userManager;

        public ReplyService(
            IUnitOfWork uow,
            IAppLogger<Reply> logger,
            UserManager<User> userManager)
        {
            _uow = uow;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(AddReplyDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Parameter {Param} was null", nameof(dto));
                throw new BadRequestException($"{nameof(dto)} cannot be null");
            }

            if (dto.UserId == Guid.Empty)
            {
                _logger.LogWarning("Parameter {Param} was Guid.Empty", nameof(dto.UserId));
                throw new BadRequestException($"{nameof(dto.UserId)} cannot be empty");
            }

            var userIdString = dto.UserId.ToString();
            var user = await _userManager.FindByIdAsync(userIdString);
            if (user is null)
            {
                _logger.LogWarning("User not found: {UserId}", dto.UserId);
                throw new BadRequestException($"User '{dto.UserId}' not found");
            }

            _logger.LogInformation("Creating reply for user {UserId}", dto.UserId);

            var validation = await new ReplyValidation().ValidateAsync(dto);
            if (!validation.IsValid)
            {
                _logger.LogWarning(
                    "Validation failed for Reply. Errors: {Errors}",
                    string.Join("; ", validation.Errors.Select(e => e.ErrorMessage))
                );
                throw new BadRequestException("Invalid reply data", validation);
            }

            var entity = ReplyMapper.Map(dto);
            await _uow.Repository<Reply>().AddAsync(entity);
            await _uow.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReplyDto>> GetUserRepliesAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Parameter {Param} was Guid.Empty", nameof(userId));
                throw new BadRequestException($"{nameof(userId)} cannot be empty");
            }

            _logger.LogInformation("Retrieving replies for user {UserId}", userId);

            var replies = await _uow.ReplyRepository.GetUserReplies(userId)
                              ?? new List<Reply>();

            var dtoList = replies.Select(ReplyMapper.Map).ToList();
            if (!dtoList.Any())
                _logger.LogInformation("No replies found for user {UserId}", userId);

            return dtoList;
        }

        public async Task<bool> RemoveAsync(Guid replyId)
        {
            if (replyId == Guid.Empty)
            {
                _logger.LogWarning("Parameter {Param} was Guid.Empty", nameof(replyId));
                throw new BadRequestException($"{nameof(replyId)} cannot be empty");
            }

            var entity = await _uow.Repository<Reply>().GetByIdAsync(replyId);
            if (entity is null)
            {
                _logger.LogWarning("Reply not found: {ReplyId}", replyId);
                throw new BadRequestException($"Reply '{replyId}' not found");
            }

            _logger.LogInformation("Deleting reply {ReplyId}", replyId);
            await _uow.Repository<Reply>().DeleteAsync(entity);
            await _uow.SaveChangesAsync();

            return true;
        }

        public Task<bool> UpdateAsync(ReplyDto dto)
        {
            throw new NotImplementedException();
       }
    }
    
}
