using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Countries.Api.Tests
{
    public class HealthCheckTests: BaseTestClass
    {

        [Fact]
        public async Task Health_OnInvoke_ReturnsHealthy()
        {
            var response = await base.HttpClient.GetAsync("/health");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseString.Should().Be("Healthy");
        }
    }
}
