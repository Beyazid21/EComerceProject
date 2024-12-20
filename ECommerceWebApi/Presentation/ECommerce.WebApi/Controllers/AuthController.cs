using ECommerce.Application.Features.Auth.Command.Login;
using ECommerce.Application.Features.Auth.Command.RefreshToken;
using ECommerce.Application.Features.Auth.Command.Register;
using ECommerce.Application.Features.Auth.Command.Revoke;
using ECommerce.Application.Features.Auth.Command.RevokeAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            await mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
         var respone= await mediator.Send(request);
            return Ok(respone);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
        {
            var respone = await mediator.Send(request);
            return Ok(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Revoke(RevokeCommandRequest request)
        {
           await mediator.Send(request);
            return Ok("Secdiyiniz kisi revoke oldu");
        }

        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            await mediator.Send(new RevokeAllCommandRequest());
            return Ok("Hami revoke oldu");
        }
    }
}
