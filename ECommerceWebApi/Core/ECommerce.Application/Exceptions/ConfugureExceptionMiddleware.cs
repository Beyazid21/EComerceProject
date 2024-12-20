using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Exceptions
{
    public static class ConfugureExceptionMiddleware
    {
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleWare>();

        }
    }
}
