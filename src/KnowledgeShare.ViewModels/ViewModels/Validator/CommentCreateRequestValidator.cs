using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KnowledgeShare.ViewModels.Content;

namespace KnowledgeShare.ViewModels.ViewModels.Validator
{
    public class CommentCreateRequestValidator : AbstractValidator<CommentCreateRequest>
    {
        public CommentCreateRequestValidator()
        {
            RuleFor(x => x.KnowledgeBaseId)
                .GreaterThan(0)
                .WithMessage("knowledge base id không được để trống");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content không được để trống");
        }
    }
}
