using ECommerce.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotBeExpiryException:BaseException
    {
        public RefreshTokenShouldNotBeExpiryException():base("Sessiyanızın müddəti bitib, xahiş edirik yenidən daxil olun")
        {
            
        }
    }
}
