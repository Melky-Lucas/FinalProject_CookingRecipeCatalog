namespace Application.Interfaces
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
