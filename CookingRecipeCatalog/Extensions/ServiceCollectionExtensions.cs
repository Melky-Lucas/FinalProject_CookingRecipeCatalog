using Application.Contract;
using Application.DTOs.Validators;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using FluentValidation;
using Infrastructure.Auth;
using Infrastructure.Context;
using Infrastructure.Mapping;
using Infrastructure.PasswordHasher;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateRecipeDTOValidator>();
            services.AddScoped<IRecipeService, RecipeService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IApplicationValidator, ApplicationValidator>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RecipeCatalogDBContext>(o =>
                o.UseSqlServer(config.GetConnectionString("Database")));

            services.AddScoped<RecipeCatalogDBContext>();

            // Repositories
            services.AddScoped<IRecipeCategoryRepository, RecipeCategoryRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IObjectMapper, AutoMapperAdapter>();
            services.AddTransient<IPasswordHasher, PasswordHasherAdapter>();
            services.AddSingleton<ITokenGenerator, TokenGeneratorAdapter>();


            services.AddAutoMapper(cfg =>
                cfg.AddProfile<MappingProfile>()
            );

            return services;
        }
    }
}
