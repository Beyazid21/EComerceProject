using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommandRequest>

    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithName("Başlıq");//em[ty meselen title:"" bu emptidir bu hissenin umimiyyetle olmamasi ise nulldir
            RuleFor(x => x.Description).NotEmpty().WithName("Açıqlama");
            RuleFor(x => x.BrandId).GreaterThan(0).WithName("Marka");
            RuleFor(x => x.Price).GreaterThan(0).WithName("Qiymət");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithName("Endirim(%)");
            RuleFor(x => x.CategoryIds).NotEmpty().Must(c => c.Any()).WithName("Kategoriyalar");
        }
    }
}
