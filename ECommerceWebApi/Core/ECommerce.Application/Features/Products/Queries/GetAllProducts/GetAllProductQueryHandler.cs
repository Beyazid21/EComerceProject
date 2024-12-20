using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var values = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            return values.Select(x => new GetAllProductQueryResponse
            {
                Description = x.Description,
                Discount = x.Discount,
                Price = x.Price - ((x.Price * x.Discount) / 100),
                Title = x.Title,

            }).ToList();


        }
    }
}
