using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common.Entities
{
    public class User:IdentityUser<Guid>//burada guid primary keyin tipidir hechne yazmasaq default olaraq int olur
    {
        public string FullName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
