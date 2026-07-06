using AutoMapper;
using Core.DTOs;
using Core.Models;


namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<CreateRecipeDTO, Recipe>();
            CreateMap<UpdateRecipeDTO, Recipe>();
        }
    }
}