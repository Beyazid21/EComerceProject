using ECommerce.Application.Bases;
using ECommerce.Application.Beheviors;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Features.Products.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services) 
        { 
        var assembly=Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            //services.AddTransient<ProductRules>();//her rule ucun bunu istifade etmemek uchun  AddRulesFromAssemlyContaining-ni istifade edrik
            services.AddRulesFromAssemlyContaining(assembly,typeof(BaseRules));
            services.AddTransient < ExceptionMiddleWare>() ;
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture=new CultureInfo("az");
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }

        private static IServiceCollection  AddRulesFromAssemlyContaining(this IServiceCollection services,Assembly assembly,Type type)
        {
            //assembly.GetTypes().Where(t=>t.IsSubclassOf(type)-burada gedir assemlyde olan butun klasslara baxir ve Where(t=>t.IsSubclassOf(type) && type != t) bununla baserulesden miras alanlari ve ozu olmasin tapir ver transient ederek elave edir
            var types =assembly.GetTypes().Where(t=>t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types) 
            {

                services.AddTransient(item);
            }

            return services;
            
        }

    }
}
