using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandValidation:AbstractValidator<RevokeCommandRequest>
    {
        public RevokeCommandValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
