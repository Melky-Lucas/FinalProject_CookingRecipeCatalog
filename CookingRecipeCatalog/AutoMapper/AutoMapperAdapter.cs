using AutoMapper;
using Core.Interfaces;

namespace WebAPI.AutoMapper 
{
    public class AutoMapperAdapter : IObjectMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}