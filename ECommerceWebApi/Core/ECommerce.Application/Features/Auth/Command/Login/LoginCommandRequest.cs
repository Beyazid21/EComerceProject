using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Auth.Command.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        [DefaultValue("beyazimustafayev55@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("azerbaycan2004")]
        public string Password { get; set; }    
    }
}
