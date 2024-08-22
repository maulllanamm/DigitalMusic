namespace DigitalMusic.Application.Helper.Interface;

public interface ICacheHelper
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    object RemoveData(string key);
}