# Caching

A soft layer that makes caching ready to use in your application, that support both InMemory and Redis Caching. its uses the power of Abstraction to allow you change the caching mechanism without changing your application 

The Directory contians three projects:
1. The Abstraction project: used to inject the caching in your service, its a common layer.
2. InMemory Caching
3. Redis Caching 


* Add InMemory Caching using DI.
```C#
services.AddMemoryCaching(options =>
{
    options.DefaultTimeout = 60; //In seconds
    options.DefaultSlidingTimeout = 60;
    options.InstanceName = "YourAppName_";
});
```

OR Add Redis Caching using DI. 
```C#
services.AddRedisCaching(options =>
{
    options.DefaultTimeout = 60;
    options.DefaultSlidingTimeout = 60;
    options.InstanceName = "YourAppName_";
    options.ConnectionString = "localhost:5002";
});
```

* Inject ``` ICache ``` Interface to your service
```C#
private readonly ICache _cache;

public SomeService(ICache cache)
{
    _cache = cache;
}
```

* And Cache!
```C#
public async Task<ActionResult<CacheEntity<string>>> SomeMethod()
{
    return await _cache.GetOrCreateAsync("myKey", async () =>
    {
        // code to execute if the cached data is not found - to evalute it and cache it 
        // return data;
    });
}
```
you can also use ``` _cache.SetAsync ``` ``` _cache.GetAsync ``` ``` _cache.DeleteAsync ```


**Remember: you can customize the code to match your needs**
