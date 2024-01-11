using System.Net.Http.Headers;

namespace BurgerRoyale.IntegrationTests.Helpers;

public static class StringContentHelper
{
    public static StringContent Create(string content)
    {
        return new StringContent(content, System.Text.Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json; charset=utf-8").MediaType!);
    }
}