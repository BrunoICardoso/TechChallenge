using System.Text.Json;

namespace BurgerRoyale.IntegrationTests.Extensions
{
    internal static class HttpClientExtensions
    {
        public static T DeserializeTo<T>(this HttpResponseMessage httpResponseMessage)
        {
            var contentStr = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrEmpty(contentStr))
                return default(T);

            return JsonSerializer.Deserialize<T>(httpResponseMessage.Content.ReadAsStream());
        }
    }
}