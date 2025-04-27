using FluentValidation;
using Post.Common.DTOs.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Validation
{
    public class ReplyValidation : AbstractValidator<AddReplyDto>
    {
        public ReplyValidation()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("User Id should not be empty");
            RuleFor(r => r.PostId)
               .NotEmpty().WithMessage("Post Id should not be empty");
            RuleFor(r => r.replyContent)
               .NotEmpty().WithMessage("Reply content should not be empty")
               .Length(5, 280).WithMessage("The reply should be between 5 to 280 character");
        }
    }
}
