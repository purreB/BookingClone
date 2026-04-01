using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using BookingClone.Tests.Infrastructure;
using Xunit;

namespace BookingClone.Tests.Auth;

public sealed class AuthLifecycleTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthLifecycleTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_DeleteGuest_Login_ReturnsUnauthorized()
    {
        var email = $"guest-{Guid.NewGuid():N}@example.com";
        const string password = "Password1";

        var registerResponse = await _client.PostAsJsonAsync(
            "/api/auth/register",
            new
            {
                fullName = "Guest User",
                email,
                password,
                role = "Guest"
            });

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var registerBody = await registerResponse.Content.ReadFromJsonAsync<JsonElement>();
        var userId = registerBody.GetProperty("userId").GetGuid();

        var deleteResponse = await _client.DeleteAsync($"/api/user/guest/{userId}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var loginResponse = await _client.PostAsJsonAsync(
            "/api/auth/login",
            new
            {
                email,
                password
            });

        Assert.Equal(HttpStatusCode.Unauthorized, loginResponse.StatusCode);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsProblemDetailsUnauthorized()
    {
        var loginResponse = await _client.PostAsJsonAsync(
            "/api/auth/login",
            new
            {
                email = "missing-user@example.com",
                password = "Password1"
            });

        Assert.Equal(HttpStatusCode.Unauthorized, loginResponse.StatusCode);
        Assert.NotNull(loginResponse.Content.Headers.ContentType);
        Assert.Equal("application/problem+json", loginResponse.Content.Headers.ContentType!.MediaType);

        var body = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("Unauthorized.", body.GetProperty("title").GetString());
        Assert.Equal(401, body.GetProperty("status").GetInt32());
        Assert.Equal("Invalid credentials.", body.GetProperty("detail").GetString());
    }

    [Fact]
    public async Task GetGuestById_MissingGuest_ReturnsProblemDetailsNotFound()
    {
        var response = await _client.GetAsync($"/api/user/guest/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType!.MediaType);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("Guest not found.", body.GetProperty("title").GetString());
        Assert.Equal(404, body.GetProperty("status").GetInt32());
    }
}
