using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandValidation:AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidation()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x => x.AccessToken).NotEmpty();

        }
    }
}
