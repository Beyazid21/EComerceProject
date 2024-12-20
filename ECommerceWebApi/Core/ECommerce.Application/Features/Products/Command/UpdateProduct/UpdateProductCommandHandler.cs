using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest,Unit>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => !x.IsDeleted && x.Id == request.Id);
            product.Title = request.Title;
            product.Price = request.Price;
            product.Description = request.Description;
            product.BrandId = request.BrandId;
            product.Discount = request.Discount;
            var productcategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(x => x.ProductId == request.Id);
            await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productcategories);
            foreach (var item in request.CategoryIds)
            {

                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new() { CategoryId = item, ProductId = product.Id });
            }
            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
