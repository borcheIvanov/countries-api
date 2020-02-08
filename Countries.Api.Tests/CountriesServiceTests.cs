using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Countries.Api.Models;
using Countries.Api.Repositories;
using Countries.Api.Services;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Countries.Api.Tests
{
    public class CountriesServiceTests
    {
        public CountriesServiceTests()
        {
            _repository = A.Fake<ICountriesRepository>();
            var countries = GetDummyCountries();
            A.CallTo(() => _repository.GetAllAsync()).Returns(countries);
            
            _service = new CountriesService(_repository);
        }

        private readonly ICountriesRepository _repository;
        private readonly ICountriesService _service;

        [Fact]
        public async void GetAllAsync_ShouldBeNotNull()
        {
            A.CallTo(() => _repository.GetAllAsync()).Returns(new List<Country>());

            var result = await _service.GetAllAsync();
            result.Should().NotBeNull();
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnAllCountries()
        {
            var result = await _service.GetAllAsync();
            result.Should().HaveCount(25);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfCountries()
        {
            var result = await _service.GetAllAsync();
            result.Should().BeOfType<List<Country>>();
        }

        [Fact]
        public async void GetPaginated_ShouldReturnExactNumberAsTake()
        {
            var result = await _service.GetPaginated(10, 0);
            result.Should().HaveCount(10);
        }
        
        [Fact]
        public async void GetPaginated_ShouldNotBeNull_IfAllAreSkipped()
        {
            var result = await _service.GetPaginated(5, 30);
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async void GetPaginated_ShouldReturnEmptyCollection_IfAllAreSkipped()
        {
            var result = await _service.GetPaginated(5, 30);
            result.Should().BeEmpty();
        }

        [Fact]
        public async void GetPaginated_ShouldReturnAvailable()
        {
            var result = await _service.GetPaginated(5, 21);
            result.Should().HaveCount(4);
        }
        
        [Fact]
        public void GetSingle_WithNonExistentCode_ShouldThrowException()
        {
            A.CallTo(() => _repository.GetByCodeAsync(A<string>.Ignored))
                .ThrowsAsync(new Exception("Country not found"));

            var result = _service.GetSingle("SMKD").Exception;
            result.Should().NotBeNull();
            result.InnerException.Should().NotBeNull();
            result?.InnerException?.Message.Should().Be("Country not found");
        }
        
        [Fact]
        public async void GetSingle_WithRightCode_ShouldReturnRightCountry()
        {
            var result = await _service.GetSingle("MKD");

            result.Name.Should().Be("Macedonia");
            result.Capital.Should().Be("Skopje");
            result.Alpha3Code.Should().Be("MKD");
        }
        
        [Fact]
        public async void GetSingle_ShouldReturnListOfNeighbouringCountries()
        {   
            var result = await _service.GetSingle("MKD");

            result.BorderingCountries.Should().NotBeNull();
            result.BorderingCountries.Should().HaveCount(2);
            result.BorderingCountries[0].Alpha3Code.Should().Be("SRB");
            result.BorderingCountries[1].Alpha3Code.Should().Be("BGR");
        }

        [Fact]
        public async void GetSingle_CountryWithNoNeighbours_ShouldReturnEmptyListOfNeigbours()
        {

            var result = await _service.GetSingle("NMKD");

            result.BorderingCountries.Should().NotBeNull();
            result.BorderingCountries.Should().BeEmpty();
        }

        private static IList<Country> GetDummyCountries()
        {
            var countries = A.CollectionOfDummy<Country>(25);
            countries[0].Alpha3Code = "NMKD";
            countries[0].Name = "North Macedonia";
            countries[0].Borders = new List<string>();
            countries[0].Capital = "North Skopje";

            countries[1].Alpha3Code = "MKD";
            countries[1].Name = "Macedonia";
            countries[1].Capital = "Skopje";
            countries[1].Borders = new List<string> {"SRB", "BGR"};
            
            countries[5].Alpha3Code = "SRB";
            countries[5].Name = "Serbia";

            countries[6].Alpha3Code = "BGR";
            countries[6].Name = "Bulgaria";
            return countries;
        }
    }
}