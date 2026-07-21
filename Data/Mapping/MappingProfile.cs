using Application.DTOs;
using AutoMapper;
using Core.Models;


namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<CreateRecipeDTO, Recipe>();
            CreateMap<UpdateRecipeDTO, Recipe>();

            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<CreateIngredientDTO, Ingredient>();
            CreateMap<UpdateIngredientDTO, Ingredient>();

            CreateMap<RecipeCategoryDTO, RecipeCategory>();
            CreateMap<CreateRecipeCategoryDTO, RecipeCategory>();
            CreateMap<UpdateRecipeCategoryDTO, RecipeCategory>();

            CreateMap<Recipe_IngredientDTO, Recipe_Ingredient>();
            CreateMap<CreateRecipe_IngredientDTO, Recipe_Ingredient>();
            CreateMap<UpdateRecipe_IngredientDTO, Recipe_Ingredient>();

            CreateMap<RecipeCookingStepDTO, CookingStep>();
            CreateMap<CreateRecipeCookingStepDTO, CookingStep>();
            CreateMap<UpdateCookingStepDTO, CookingStep>();

            CreateMap<RecipeTipDTO, Tip>();
            CreateMap<CreateRecipeTipDTO, Tip>();
            CreateMap<UpdateTipDTO, Tip>();

            CreateMap<MeasureUnitDTO, MeasureUnit>();
            CreateMap<CreateMeasureUnitDTO, MeasureUnit>();
            CreateMap<UpdateMeasureUnitDTO, MeasureUnit>();

            CreateMap<IngredientCategoryDTO, IngredientCategory>();
            CreateMap<CreateIngredientCategoryDTO, IngredientCategory>();
            CreateMap<UpdateIngredientCategoryDTO, IngredientCategory>();

            CreateMap<string, Password>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src));

            CreateMap<string, Role>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleName));
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<RoleDTO, Role>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();
        }
    }
}