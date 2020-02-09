using Countries.Api.DTModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace Countries.Api.Tests
{
    public class CountriesControllerTests : BaseTestClass
    {
        [Fact]
        public async void Countries_OnInvoke_Returns200()
        {
            var response = await HttpClient.GetAsync("/countries/all");
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void Countries_OnInvoke_Returns400_IfInvalidGetItemsNumberPassed()
        {
            var response = await HttpClient.GetAsync("/countries/all?getItems=-1&skipItems=5");
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Countries_OnInvoke_Returns400_IfInvalidSkipItemsNumberPassed()
        {
            var response = await HttpClient.GetAsync("/countries/all?getItems=0&skipItems=-5");
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Countries_OnInvoke_ReturnsRightNumberOfItems()
        {
            var response = await HttpClient.GetAsync("/countries/all?getItems=5&skipItems=5");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedCountriesResponseModel>(resultString);
            result.Items.Should().HaveCount(5);
            result.SkippedItems.Should().Be(5);
        }

        [Fact]
        public async void Countries_OnInvoke_ReturnsRightNumberOfItems_IfLessItemsAreAvailable()
        {
            var response = await HttpClient.GetAsync("/countries/all?getItems=5&skipItems=23");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedCountriesResponseModel>(resultString);
            result.Items.Should().HaveCount(2);
            result.SkippedItems.Should().Be(23);
        }

        [Fact]
        public async void Countries_OnInvoke_TotalItemsShouldBe25()
        {
            var response = await HttpClient.GetAsync("/countries/all");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedCountriesResponseModel>(resultString);
            result.TotalItems.Should().Be(25);
        }

        [Fact]
        public async void Countries_OnInvoke_WithoutParams_Returns0Results()
        {
            var response = await HttpClient.GetAsync("/countries/all");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedCountriesResponseModel>(resultString);
            result.Items.Should().BeEmpty();
        }

        [Fact]
        public async void Country_OnInvoke_WithExistingCode_ReturnsCorrectCountry()
        {
            var response = await HttpClient.GetAsync("/countries/country/MKD");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CountryDetailsResponseModel>(resultString);
            result.Name.Should().Be("Macedonia");
            result.Alpha3Code.Should().Be("MKD");
            result.Capital.Should().Be("Skopje");
            result.BorderingCountries.Should().HaveCount(2);
        }

        [Fact]
        public async void Country_OnInvoke_WithExistingCode_ReturnsStatusCode200()
        {
            var response = await HttpClient.GetAsync("/countries/country/MKD");
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void Country_OnInvoke_WithNonExistingCode_Returns500()
        {
            var response = await HttpClient.GetAsync("/countries/country/BMKD");
            response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async void Country_OnInvoke_WithNonExistingCode_ReturnsErrorResponseModelWithCountryNotFound()
        {
            var response = await HttpClient.GetAsync("/countries/country/BMKD");
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ErrorResponseModel>(resultString);

            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.ErrorMessage.Should().Be("Country not found");
        }
    }
}