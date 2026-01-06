using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace KnowledgeShare.ViewModels.ViewModels.Validator
{
    public class RegisterVmValidator : AbstractValidator<LoginVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Định dạng email không hợp lệ");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 kí tự");
        }
    }
}
