using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Exceptions
{
    public class ExceptionMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpcontext, RequestDelegate next)
        {
        try
            {
                await next(httpcontext);    
            }

        catch(Exception ex)  
            {
            
                await HandleException(httpcontext, ex);
            
            }
               
        }

        private static  Task HandleException(HttpContext httpContext,Exception exception) 
        {
        
            int statuscode=GetStatusCode(exception);
            httpContext.Response.ContentType= "application/json";
            httpContext.Response.StatusCode = statuscode;
            if(exception.GetType()==typeof(ValidationException))
            {
                return httpContext.Response.WriteAsync(new ExceptionModel
                {
                    Errors=((ValidationException)exception).Errors.Select(x=>x.ErrorMessage),
                    StatusCode= StatusCodes.Status400BadRequest

                }.ToString());
            }
            List<string> errors = new()
            {
                exception.Message,
                exception.InnerException?.ToString() ??"Boshdur",


            };

            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors=errors,
                StatusCode=statuscode
            }.ToString());
        
        }

        private static int GetStatusCode(Exception exception)
        {
            switch (exception)
            {
                case BadRequestException:
                    return StatusCodes.Status400BadRequest;

                // Digər növ istisnalar üçün nümunələr əlavə edə bilərsiniz
                case NotFoundException:
                    return StatusCodes.Status404NotFound;
                case ValidationException:
                    return StatusCodes.Status422UnprocessableEntity;


                default:
                    return StatusCodes.Status500InternalServerError; // Varsayılan status kodu
            }
        }
    }
}
