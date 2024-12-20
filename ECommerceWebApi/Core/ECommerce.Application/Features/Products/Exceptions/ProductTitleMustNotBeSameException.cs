using ECommerce.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Exceptions
{
    public class ProductTitleMustNotBeSameException:BaseException
    {
        public ProductTitleMustNotBeSameException():base("Məhsul başlığı artıq var")
        {
            
        }
    }
}
