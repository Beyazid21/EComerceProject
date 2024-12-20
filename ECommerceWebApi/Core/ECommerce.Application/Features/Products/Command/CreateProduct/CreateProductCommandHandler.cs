using ECommerce.Application.Features.Products.Rules;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductRules productRules;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,ProductRules productRules)
        {
            _unitOfWork = unitOfWork;
            this.productRules = productRules;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);
            var products=await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            await productRules.ProductTitleMustNotBeSame(products,request.Title);
            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

            if (await _unitOfWork.SaveAsync() > 0)
            {


                foreach (var item in request.CategoryIds)
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.ProductId = product.Id;
                    productCategory.CategoryId = item;
                    await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(productCategory);
                }

                await _unitOfWork.SaveAsync();
            }

            return Unit.Value;

        }
    }
}
