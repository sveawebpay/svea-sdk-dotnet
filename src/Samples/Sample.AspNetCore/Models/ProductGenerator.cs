using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Sample.AspNetCore.Data;

namespace Sample.AspNetCore.Models
{
    public static class ProductGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new StoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreDbContext>>());
            if (context.Products.Any())
                return;

            context.Products.AddRange(
                new Product
                {
                    ProductId = 1,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 511 Slim Fit",
                    Reference = "Ref1",
                    Price = 899
                }, new Product
                {
                    ProductId = 2,
                    Class = "Identifier1",
                    Type = "PRODUCT",
                    Name = "Levis 501 Jeans",
                    Reference = "Ref2",
                    Price = 1190
                });

            context.SaveChanges();
        }
    }
}