using System.Text.Json;

namespace LibHouse.API.Extensions.Common
{
    public static class GenericExtensions
    {
        public static string ToJson<T>(this T entity) 
            => JsonSerializer.Serialize(entity);
    }
}