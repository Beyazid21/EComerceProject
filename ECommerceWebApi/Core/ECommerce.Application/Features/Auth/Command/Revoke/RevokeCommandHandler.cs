using ECommerce.Application.Bases;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;

        public RevokeCommandHandler(AuthRules authRules,UserManager<User> userManager,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            User? user=await userManager.FindByEmailAsync(request.Email);

           await authRules.EmailAddressShuldBeValid(user);

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
