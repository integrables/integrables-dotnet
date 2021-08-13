namespace Store.Core.Caching
{
    public class CacheEntity<T>
    {
        public T Data { get; set; }
        public bool IsFromCache { get; set; }
    }
}