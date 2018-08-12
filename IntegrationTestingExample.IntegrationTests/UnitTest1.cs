using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebApplication1.Core2._1.Controllers;
using Xunit;

namespace IntegrationTestingExample.IntegrationTests
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.1
    public class UnitTest1 : IClassFixture<WebApplicationFactory<WebApplication1.Core2._1.Program>>
    {
        private readonly WebApplicationFactory<WebApplication1.Core2._1.Program> factory;

        public UnitTest1(WebApplicationFactory<WebApplication1.Core2._1.Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Calls_about_with_default_configuration()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home/About").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Contains("Your application description page.", content);
        }

        [Fact]
        public async Task Calls_about_overriding_detail()
        {
            // Arrange
            var client = this.factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IContentService, StubContentService>();
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/Home/About").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Contains("batatas", content);
        }
    }

    public class StubContentService : IContentService
    {
        public string GetAboutContent()
        {
            return "batatas";
        }
    }
}
