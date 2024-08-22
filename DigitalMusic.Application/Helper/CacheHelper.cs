using System.Text.Json;
using DigitalMusic.Application.Helper.Interface;
using Microsoft.VisualBasic.CompilerServices;
using StackExchange.Redis;

namespace DigitalMusic.Application.Helper;

public class CacheHelper : ICacheHelper
{
    private IDatabase _cacheDB;
    public CacheHelper()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDB = redis.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _cacheDB.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return _cacheDB.StringSet(key, JsonSerializer.Serialize(value), expireTime);
    }

    public object RemoveData(string key)
    {
        var exist = _cacheDB.KeyExists(key);
        if (exist)
        {
            return _cacheDB.KeyDelete(key);
        }

        return false;
    }
}