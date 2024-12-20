using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.UnitOfWorks;
using ECommerce.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositores;
using Persistence.UnitOfWorks;


namespace Persistence
{
    public static class Registration
    {
        public static void AddPersistance(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric=false;
                opt.Password.RequiredLength = 2;
                opt.Password.RequireLowercase=false;
                opt.Password.RequireUppercase=false;
                opt.Password.RequireDigit=false;
                opt.SignIn.RequireConfirmedEmail=false;


            }).
                AddRoles<Role>().
                AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
