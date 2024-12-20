using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Common.Entities
{
    public class Detail:EntityBase
    {
        public Detail()
        {
            
        }

        public Detail(string title, string description, int categoryId, Category category)
        {
            Title = title;
            Description = description;
            CategoryId = categoryId;
            Category = category;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
