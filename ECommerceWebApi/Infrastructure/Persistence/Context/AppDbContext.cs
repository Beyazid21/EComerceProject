using ECommerce.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class AppDbContext:IdentityDbContext<User,Role,Guid>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<Brand> Brands { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Detail> Details { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//her bir table uchun etdiyimiz konfiqurasiyalirin taninmasi uchun
        }
    }
}
