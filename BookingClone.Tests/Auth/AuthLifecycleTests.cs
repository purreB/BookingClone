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
}
