using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KnowledgeShare.API.ViewModels;

namespace KnowledgeShare.ViewModels.ViewModels.Validator
{
    public class UserVmValidator : AbstractValidator<UserVm>
    {
        public UserVmValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id value is required").MaximumLength(50).WithMessage("Role id cannot over limit 50 characters");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Role name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Role name is required");

            RuleFor(x => x.Email)
                  .NotEmpty().WithMessage("Email không được để trống")
                  .EmailAddress().WithMessage("Email format is not match");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Role name is required");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Role name is required");
        }
    }
}
