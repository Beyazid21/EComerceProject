using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common.Entities
{
   public class ProductCategory:EntityBase
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Product Product { get; set; }
    }
}
