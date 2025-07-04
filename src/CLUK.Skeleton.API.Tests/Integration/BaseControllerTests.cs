namespace CLUK.Skeleton.API.Tests.Integration;

public class BaseControllerTests() : IntegrationBaseTest()
{
    protected Task<T?> Can_verify_and_get_content<T>(HttpResponseMessage response)
    {
        response.Content.Should().NotBeNull();
        return response.ToContent<T>();
    }

    protected void Can_verify_response_received_with_http_status_code(HttpResponseMessage response, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(httpStatusCode);
    }
}