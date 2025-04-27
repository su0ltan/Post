using FluentValidation;
using Post.Common.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Validation.PostValidation
{
    public class PostValidation : AbstractValidator<AddPostDto>
    {

        public PostValidation()
        {
            RuleFor(p => p.description)
                .NotEmpty().WithMessage("Description must not be empty")
                .Length(5, 280).WithMessage("The Description length should be between 5 to 280 character");

            RuleFor(p => p.topic)
                .NotEmpty().WithMessage("Topic must not be empty")
                .Length(5, 100).WithMessage("The Topic length should be between 5 to 100 character");

        }
    }
}
