namespace CLUK.Skeleton.API.Tests.Integration;

public static class ControllerTestsExtensions
{
    private static readonly Uri LocalUri = new Uri("http://localhost");

    public static async Task<T?> ToContent<T>(this HttpResponseMessage response)
    {
        string result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(result, Constants.JsonSerializerOptions);
    }

    public static string AsValidUri(this string endPointUri)
    {
        return new Uri(LocalUri, endPointUri).ToString();
    }
}