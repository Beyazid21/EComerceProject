using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common.Entities
{
    public class Category:EntityBase
    {
        public Category()
        {
            
        }
        public Category(int parentid, string name, int priorty)
        {
            ParentId = parentid;
            Name = name;
            Priorty = priorty;
        }

      
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priorty { get; set; }

        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
