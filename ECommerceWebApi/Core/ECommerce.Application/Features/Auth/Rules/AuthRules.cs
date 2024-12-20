using ECommerce.Application.Bases;
using ECommerce.Application.Features.Auth.Exceptions;
using ECommerce.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            if (user is not null)
            {
                throw new UserAlradyExistException();
               

            }
            return Task.CompletedTask;
        }

        public Task EmailOrPasswordShouldNotBeInvalid(User? user,bool chekcpassword)
        {
            if ( user is null || !chekcpassword)
            {
               throw new EmailOrPasswordShouldNotBeInvalidException();
            }

            return Task.CompletedTask;
        }

        public Task RefreshTokenShouldNotBeExpiry(DateTime? refreshTokenExpiryTime)
        {
            if (refreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RefreshTokenShouldNotBeExpiryException();
            }

            return Task.CompletedTask;
        }

        public Task EmailAddressShuldBeValid(User? user) 
        {
            if (user is not null) 
            {
                throw EmailAddressShuldBeValidException();
            }

            return Task.CompletedTask;
        }


    }

    
}
