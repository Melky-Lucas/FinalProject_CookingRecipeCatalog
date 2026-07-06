namespace Core.Interfaces
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
