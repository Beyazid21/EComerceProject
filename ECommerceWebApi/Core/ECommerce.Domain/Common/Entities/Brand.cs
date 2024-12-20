using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common.Entities
{
    public class Brand:EntityBase
    {
        public Brand(string name)
        {
            Name = name;
        }
        public Brand()
        {
            
        }

        public string Name { get; set; }

        List<Product> Products { get; set; }

    }
}
