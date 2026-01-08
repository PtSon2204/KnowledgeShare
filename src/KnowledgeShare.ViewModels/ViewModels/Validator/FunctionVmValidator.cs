using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace KnowledgeShare.ViewModels.ViewModels.Validator
{
    public class FunctionVmValidator : AbstractValidator<FunctionVm>
    {
        public FunctionVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name không được để trống");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url không được để trống");

            RuleFor(x => x.SortOrder)
                .NotEmpty().WithMessage("SortOrder không được để trống");

            RuleFor(x => x.ParentId)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
