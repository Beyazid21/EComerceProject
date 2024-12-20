using ECommerce.Application.Bases;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Application.Tokens;
using ECommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler:BaseHandler,IRequestHandler<LoginCommandRequest, LoginCommandResponse> 
    {
        private readonly IConfiguration configuration;
        private readonly ITokenService tokenService;
        private readonly AuthRules authRules;
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public LoginCommandHandler(IConfiguration configuration,ITokenService tokenService, AuthRules authRules,RoleManager<Role> roleManager, UserManager<User> userManager,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor):base(unitOfWork,httpContextAccessor)
        {
            this.configuration = configuration;
            this.tokenService = tokenService;
            this.authRules = authRules;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
           User? user=await userManager.FindByEmailAsync(request.Email);

            bool chekckPaswword=await userManager.CheckPasswordAsync(user,request.Password);    
            await authRules.EmailOrPasswordShouldNotBeInvalid(user,chekckPaswword);

            IList<string> roles=await userManager.GetRolesAsync(user);
            JwtSecurityToken token=await tokenService.CreateToken(user, roles);
            string refreshToken = tokenService.GenerateRefreshToken();
           _= int.TryParse(configuration["JWT:RefresjTokenValidityInDays"],out int refresjTokenValidityInDays);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime=DateTime.Now.AddDays(refresjTokenValidityInDays);

          await  userManager.UpdateAsync(user);
            string _token=new JwtSecurityTokenHandler().WriteToken(token);
            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccesToken", _token);

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
            };
        }
    }
}
