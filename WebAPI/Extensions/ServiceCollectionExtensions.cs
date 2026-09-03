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
            services.AddTransient<IApplicationValidator, ApplicationValidator>();

            // Services
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeCategoryService, RecipeCategoryService>();
            services.AddScoped<IRecipe_IngredientService, Recipe_IngredientService>();
            services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<ICookingStepService, CookingStepService>();
            services.AddScoped<IMeasureUnitService, MeasureUnitService>();
            services.AddScoped<ITipService, TipService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Postgres");

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (!string.IsNullOrEmpty(databaseUrl))
            {
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                int port = databaseUri.Port == -1 ? 5432 : databaseUri.Port;

                connectionString = $"Host={databaseUri.Host};Port={port};Database={databaseUri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True";
            }

            // DBContext
            services.AddDbContext<RecipeCatalogDBContext>(o =>
                o.UseNpgsql(connectionString)
                 .UseLowerCaseNamingConvention()

            );

            services.AddScoped<RecipeCatalogDBContext>();

            // Repositories
            services.AddScoped<IRecipeCategoryRepository, RecipeCategoryRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeCategoryRepository, RecipeCategoryRepository>();
            services.AddScoped<IRecipe_IngredientRepository, Recipe_IngredientRepository>();
            services.AddScoped<IIngredientCategoryRepository, IngredientCategoryRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<ICookingStepRepository, CookingStepRepository>();
            services.AddScoped<IMeasureUnitRepository, MeasureUnitRepository>();
            services.AddScoped<ITipRepository, TipRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Adapters
            services.AddScoped<IObjectMapper, AutoMapperAdapter>();
            services.AddTransient<IPasswordHasher, PasswordHasherAdapter>();
            services.AddSingleton<ITokenGenerator, TokenGeneratorAdapter>();


            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.LicenseKey = config.GetSection("AutoMapper")?.GetSection("Key")?.Value;
            });

            return services;
        }
    }
}
