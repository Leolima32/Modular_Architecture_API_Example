﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Products.Core.Abstractions;
using Module.Products.Infrastructure.Persistence;
using Module.Products.Infrastructure.Repositories;
using Shared.Infrastructure.Extensions;

namespace Module.Products.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<ProductDbContext>(config)
                .AddScoped<IProductDbContext>(provider => provider.GetService<ProductDbContext>())
                .AddTransient<IProductRepository, ProductRepository>();
                   
            var context = services.BuildServiceProvider().GetService<ProductDbContext>();
            context.Database.Migrate();
            return services;
        }
    }
}
