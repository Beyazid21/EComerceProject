using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.Register
{
    public class RegisterCommandValidation:AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x=>x.FullName).NotEmpty().MaximumLength(40).MinimumLength(3).WithName("Ad ,Soyad");
            RuleFor(x=>x.Email).NotEmpty().EmailAddress().MaximumLength(60).MinimumLength(7).WithName("Email ünvan");
            RuleFor(x=>x.Password).NotEmpty().MinimumLength(6).WithName("Şifrə");
            RuleFor(x=>x.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(x=>x.Password).WithName("Şifrəni Təsfiqlə");
          
        }
    }
}
