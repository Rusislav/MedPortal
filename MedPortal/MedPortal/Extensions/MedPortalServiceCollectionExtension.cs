using MedPortal.Core.Contracts;
using MedPortal.Core.Services;
using MedPortal.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MedPortal.Extensions
{
    public static class MedPortalServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartProductService, CartProductService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
