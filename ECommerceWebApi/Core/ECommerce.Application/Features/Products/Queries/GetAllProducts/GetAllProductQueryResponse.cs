using ECommerce.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

       

        public decimal Price { get; set; }
        public decimal Discount
        {
            get; set;
        }
    }
}
