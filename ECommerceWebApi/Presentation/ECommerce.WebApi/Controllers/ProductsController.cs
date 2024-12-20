using ECommerce.Application.Features.Products.Command.CreateProduct;
using ECommerce.Application.Features.Products.Command.DeleteProduct;
using ECommerce.Application.Features.Products.Command.UpdateProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProduct()
        {
            var values = await mediator.Send(new GetAllProductQueryRequest());
            return Ok(values);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
             await mediator.Send(request);
            return Ok("Yeni product elave olundu");

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok(" product yenilendi");

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
        {
            await mediator.Send(request);
            return Ok(" product yenilendi");

        }
    }
}
