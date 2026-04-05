using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using BookingClone.Tests.Infrastructure;
using Xunit;

namespace BookingClone.Tests.Hotels;

[Collection("BookingCloneIntegration")]
public sealed class HotelCreateAuthorizationTests
{
    private readonly TestWebApplicationFactory _factory;

    public HotelCreateAuthorizationTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostHotel_WithoutAuth_ReturnsUnauthorized()
    {
        using var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/Hotel",
            new
            {
                name = "Anonymous Hotel",
                address = "1 Test St"
            });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PostHotel_AsGuest_ReturnsForbidden()
    {
        using var client = _factory.CreateClient();
        var email = $"guest-{Guid.NewGuid():N}@example.com";
        const string password = "Password1";

        var registerResponse = await client.PostAsJsonAsync(
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
        var accessToken = registerBody.GetProperty("accessToken").GetString();
        Assert.False(string.IsNullOrWhiteSpace(accessToken));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var hotelResponse = await client.PostAsJsonAsync(
            "/api/Hotel",
            new
            {
                name = "Guest Hotel",
                address = "2 Test St"
            });

        Assert.Equal(HttpStatusCode.Forbidden, hotelResponse.StatusCode);
    }

    [Fact]
    public async Task PostHotel_AsStaff_ReturnsCreated_AndSetsOwnerFromToken()
    {
        using var client = _factory.CreateClient();
        var email = $"staff-{Guid.NewGuid():N}@example.com";
        const string password = "Password1";

        var registerResponse = await client.PostAsJsonAsync(
            "/api/auth/register",
            new
            {
                fullName = "Staff User",
                email,
                password,
                role = "Staff"
            });
        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var registerBody = await registerResponse.Content.ReadFromJsonAsync<JsonElement>();
        var accessToken = registerBody.GetProperty("accessToken").GetString();
        Assert.False(string.IsNullOrWhiteSpace(accessToken));

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken!);
        var userId = Guid.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var hotelResponse = await client.PostAsJsonAsync(
            "/api/Hotel",
            new
            {
                name = "Staff Hotel",
                address = "3 Test St",
                ownerId = "00000000-0000-0000-0000-000000000001"
            });

        Assert.Equal(HttpStatusCode.Created, hotelResponse.StatusCode);

        var hotelBody = await hotelResponse.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal(userId.ToString(), hotelBody.GetProperty("ownerId").GetString());
        Assert.NotEqual(Guid.Empty, hotelBody.GetProperty("id").GetGuid());
    }
}
