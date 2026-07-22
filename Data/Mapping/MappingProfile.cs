using Application.DTOs;
using AutoMapper;
using Core.Models;


namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeDTO, Recipe>().ReverseMap();
            CreateMap<CreateRecipeDTO, Recipe>();
            CreateMap<UpdateRecipeDTO, Recipe>();

            CreateMap<IngredientDTO, Ingredient>().ReverseMap();
            CreateMap<CreateIngredientDTO, Ingredient>();
            CreateMap<UpdateIngredientDTO, Ingredient>();

            CreateMap<RecipeCategoryDTO, RecipeCategory>().ReverseMap();
            CreateMap<CreateRecipeCategoryDTO, RecipeCategory>();
            CreateMap<UpdateRecipeCategoryDTO, RecipeCategory>();

            CreateMap<Recipe_IngredientDTO, Recipe_Ingredient>().ReverseMap();
            CreateMap<CreateRecipe_IngredientDTO, Recipe_Ingredient>();
            CreateMap<UpdateRecipe_IngredientDTO, Recipe_Ingredient>();

            CreateMap<RecipeCookingStepDTO, CookingStep>().ReverseMap();
            CreateMap<CreateRecipeCookingStepDTO, CookingStep>();
            CreateMap<UpdateCookingStepDTO, CookingStep>();

            CreateMap<RecipeTipDTO, Tip>().ReverseMap();
            CreateMap<CreateRecipeTipDTO, Tip>();
            CreateMap<UpdateTipDTO, Tip>();

            CreateMap<MeasureUnitDTO, MeasureUnit>().ReverseMap();
            CreateMap<CreateMeasureUnitDTO, MeasureUnit>();
            CreateMap<UpdateMeasureUnitDTO, MeasureUnit>();

            CreateMap<IngredientCategoryDTO, IngredientCategory>().ReverseMap();
            CreateMap<CreateIngredientCategoryDTO, IngredientCategory>();
            CreateMap<UpdateIngredientCategoryDTO, IngredientCategory>();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.PasswordHash))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new Role { Name = src.RoleName }));

            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => new Password { PasswordHash = src.Password }));

            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();
        }
    }
}