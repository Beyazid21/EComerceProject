﻿using ECommerce.Application.Bases;
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

namespace ECommerce.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
    {
        private readonly AuthRules authRules;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public RegisterCommandHandler(AuthRules authRules , UserManager<User> userManager,RoleManager<Role> roleManager,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor):base(unitOfWork,httpContextAccessor)
        {
            this.authRules = authRules;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
           await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));
            User user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
              

            };
            user.UserName=request.Email;
            user.SecurityStamp=Guid.NewGuid().ToString();
            IdentityResult result = await userManager.CreateAsync(user, request.Password);


            if (!await roleManager.RoleExistsAsync("user"))
            {
                await roleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER",

                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });

                await userManager.AddToRoleAsync(user, "user");
            }
            return Unit.Value;
        }
    }
}
