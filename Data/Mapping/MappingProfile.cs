using Application.DTOs;
using AutoMapper;
using Core.Models;


namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Recipe_Categories.Select(rc => rc.Category.Name)));

            CreateMap<CreateRecipeDTO, Recipe>()
                .ForMember(dest => dest.Recipe_Categories, opt => opt.MapFrom(src => src.Category_Ids.Select(id => new Recipe_Category { CategoryId = id })));

            CreateMap<UpdateRecipeDTO, Recipe>();
            CreateMap<Recipe, Recipe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CookingSteps, opt => opt.Ignore())
                .ForMember(dest => dest.Recipe_Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Recipe_Ingredients, opt => opt.Ignore())
                .ForMember(dest => dest.Tips, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.IngredientCategory.Name));

            CreateMap<CreateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.IngredientCategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<UpdateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.IngredientCategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<RecipeCategory, RecipeCategoryDTO>();
            CreateMap<CreateRecipeCategoryDTO, RecipeCategory>();
            CreateMap<UpdateRecipeCategoryDTO, RecipeCategory>();

            CreateMap<Recipe_Ingredient, Recipe_IngredientDTO>();
            CreateMap<CreateRecipe_IngredientDTO, Recipe_Ingredient>();
            CreateMap<UpdateRecipe_IngredientDTO, Recipe_Ingredient>();

            CreateMap<CookingStep, RecipeCookingStepDTO>();
            CreateMap<CreateRecipeStepDTO, CookingStep>();
            CreateMap<UpdateRecipeStepDTO, CookingStep>();

            CreateMap<Tip, RecipeTipDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<CreateRecipeTipDTO, Tip>();
            CreateMap<UpdateTipDTO, Tip>();

            CreateMap<MeasureUnit, MeasureUnitDTO>();
            CreateMap<CreateMeasureUnitDTO, MeasureUnit>();
            CreateMap<UpdateMeasureUnitDTO, MeasureUnit>();

            CreateMap<IngredientCategory, IngredientCategoryDTO>();
            CreateMap<CreateIngredientCategoryDTO, IngredientCategory>();
            CreateMap<UpdateIngredientCategoryDTO, IngredientCategory>();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<Role, RoleDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();
        }
    }
}