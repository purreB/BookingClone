using Xunit;

namespace BookingClone.Tests.Infrastructure;

[CollectionDefinition("BookingCloneIntegration")]
public sealed class BookingCloneIntegrationCollection : ICollectionFixture<TestWebApplicationFactory>
{
}
