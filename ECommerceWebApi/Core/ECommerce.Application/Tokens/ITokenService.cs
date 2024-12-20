using ECommerce.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

    }
}
