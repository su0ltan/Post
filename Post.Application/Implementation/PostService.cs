using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Post.Application.CustomException;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Application.Mappers;
using Post.Application.Repositories;
using Post.Application.Validation;
using Post.Application.Validation.PostValidation;
using Post.Common.DTOs.Post;
using Post.Common.Models;
using Post.Domain.Entities;

namespace Post.Application.Implementation
{
    public class PostService : IPostService
    {
        private  readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<Domain.Entities.Post> _logger;
        private readonly UserManager<User> _userManager;

        public PostService(IUnitOfWork unitOfWork, IAppLogger<Domain.Entities.Post> logger, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
            
        }
        public async Task<bool> AddAsync(AddPostDto dto)
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
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", dto.UserId);
                throw new BadRequestException($"User '{dto.UserId}' not found");
            }

            _logger.LogInformation("Creating post for user {UserId}", dto.UserId);

            var validator = new PostValidation();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning(
                    "Validation failed for Post. Errors: {Errors}",
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
                );
                throw new BadRequestException("Invalid post data", validationResult);
            }

            var entity = PostMapper.Map(dto);
            await _unitOfWork.Repository<Domain.Entities.Post>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<PostDto>> GetLatestPosts(QueryParameters parameters)
        {

            _logger.LogInformation("Retrieving lataest posts");


            // retrieving
            var posts = await _unitOfWork.PostRepository.GetLatestPosts(parameters);

           
            if(posts == null || !posts.Any())
            {
                _logger.LogWarning("No Posts Founded");
                return new List<PostDto>();
            }

            // Mapping the domain entity
            var dtoLists = posts.Select(PostMapper.Map).ToList();

            return dtoLists;
        }

        public async Task<List<PostDto>> GetUserPostsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Parameter {1} was Guid.Empty", nameof(userId));
                throw new BadRequestException($"{nameof(userId)} cannot be empty");
            }

            var userIdString = userId.ToString();
            var user = await _userManager.FindByIdAsync(userIdString);
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                throw new BadRequestException($"User '{userId}' not found");
            }

            _logger.LogInformation("Retrieving posts for user {UserId}", userId);

            var posts = await _unitOfWork.PostRepository.GetUserPosts(userId)
                             ?? new List<Domain.Entities.Post>();

            var dtoList = posts.Select(PostMapper.Map).ToList();
            if (!dtoList.Any())
                _logger.LogInformation("No posts found for user {UserId}", userId);

            return dtoList;
        }


        public Task<bool> RemoveAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PostDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
