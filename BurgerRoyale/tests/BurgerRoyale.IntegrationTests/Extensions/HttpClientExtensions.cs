using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BurgerRoyale.IntegrationTests.Extensions;

internal static class HttpClientExtensions
{
    public static T DeserializeTo<T>(this HttpResponseMessage httpResponseMessage, string content)
    {
        JObject jsonObject = JObject.Parse(content);
        var dataObject = jsonObject["data"];
        return JsonConvert.DeserializeObject<T>(dataObject.ToString());
    }
}