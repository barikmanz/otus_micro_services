namespace UserService.Infrastructure.SeedWork.JsonSerializers
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T value);
        dynamic Deserialize(string value, Type type);
        T Deserialize<T>(string value);
    }
}