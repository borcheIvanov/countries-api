
using Countries.Api.Services;
using FakeItEasy;
using Xunit;
using FluentAssertions;


namespace Countries.Api.Tests
{
    public class CountriesServiceTests
    {
        [Fact]
        public async void TestOne()
        {
            var countriesCollection = A.CollectionOfFake<Models.Country>(25);
            var service = A.Fake<ICountriesService>();
            var countries = new Models.Countries();
            countries.AddRange(countriesCollection);
            A.CallTo( () => service.GetAllAsync()).Returns(countries);

            var result = await service.GetAllAsync();
            result.Should().BeOfType<Models.Countries>();
        }
    }
}