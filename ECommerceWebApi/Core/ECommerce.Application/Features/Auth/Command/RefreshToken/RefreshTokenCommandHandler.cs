using ECommerce.Application.Bases;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Application.Tokens;
using ECommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly AuthRules authRules;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;

        public RefreshTokenCommandHandler(AuthRules authRules,ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
            this.authRules = authRules;
            this.tokenService = tokenService;
            this.userManager = userManager;
        }




        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {



            ClaimsPrincipal? principal =tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            string email=principal.FindFirstValue(ClaimTypes.Email);

            User? user =await userManager.FindByEmailAsync(email);
            IList<string> roles =await userManager.GetRolesAsync(user);
           await authRules.RefreshTokenShouldNotBeExpiry(user.RefreshTokenExpiryTime);
            JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);


            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
           


         }



    } }
