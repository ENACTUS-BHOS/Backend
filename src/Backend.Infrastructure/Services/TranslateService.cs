using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Backend.Infrastructure.Services;

public static class TranslateService
{
    public static async Task<string> Translate(string text)
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://translate-plus.p.rapidapi.com/translate"),
            Headers =
            {
                { "x-rapidapi-key", "c5f6e00840msh0aa83e10941ab87p17fde6jsn16172a7ccbd8" },
                { "x-rapidapi-host", "translate-plus.p.rapidapi.com" },
            },
            Content = new StringContent("{\"text\":\"" + text + "\",\"source\":\"az\",\"target\":\"en\"}")
            {
                Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
            }
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var translation = JsonConvert.DeserializeObject<TranslateResponse>(body);

            return translation.translations.translation;
        }
    }
}

public class TranslateResponse
{
    public Translation translations { get; set; }
}

public class Translation
{
    public string translation { get; set; }
}