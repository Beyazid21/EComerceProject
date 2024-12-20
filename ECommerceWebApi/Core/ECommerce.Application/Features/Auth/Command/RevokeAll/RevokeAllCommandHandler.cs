using ECommerce.Application.Bases;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.RevokeAll
{
    public class RevokeAllCommandHanler:BaseHandler,IRequestHandler<RevokeAllCommandRequest,Unit>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;

        public RevokeAllCommandHanler(AuthRules authRules, UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
        {
            var users=await userManager.Users.ToListAsync(cancellationToken);
            foreach(var user in users)
            {
                user.RefreshToken = null;
                await userManager.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}
