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
    public class CountriesServiceTests : BaseTestClass
    {
        [Fact]
        public async void GetAllAsync_ShouldBeNotNull()
        {
            A.CallTo(() => Repository.GetAllAsync()).Returns(new List<Country>());

            var result = await Service.GetAllAsync();
            result.Should().NotBeNull();
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnAllCountries()
        {
            var result = await Service.GetAllAsync();
            result.Should().HaveCount(25);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfCountries()
        {
            var result = await Service.GetAllAsync();
            result.Should().BeOfType<List<Country>>();
        }

        [Fact]
        public async void GetPaginated_ShouldReturnExactNumberAsTake()
        {
            var result = await Service.GetPaginated(10, 0);
            result.Should().HaveCount(10);
        }
        
        [Fact]
        public async void GetPaginated_ShouldNotBeNull_IfAllAreSkipped()
        {
            var result = await Service.GetPaginated(5, 30);
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async void GetPaginated_ShouldReturnEmptyCollection_IfAllAreSkipped()
        {
            var result = await Service.GetPaginated(5, 30);
            result.Should().BeEmpty();
        }

        [Fact]
        public async void GetPaginated_ShouldReturnAvailable()
        {
            var result = await Service.GetPaginated(5, 21);
            result.Should().HaveCount(4);
        }
        
        [Fact]
        public void GetSingle_WithNonExistentCode_ShouldThrowException()
        {
            A.CallTo(() => Repository.GetByCodeAsync(A<string>.Ignored))
                .ThrowsAsync(new Exception("Country not found"));

            var result = Service.GetSingle("SMKD").Exception;
            result.Should().NotBeNull();
            result?.InnerException.Should().NotBeNull();
            result?.InnerException?.Message.Should().Be("Country not found");
        }
        
        [Fact]
        public async void GetSingle_WithRightCode_ShouldReturnRightCountry()
        {
            var result = await Service.GetSingle("MKD");

            result.Name.Should().Be("Macedonia");
            result.Capital.Should().Be("Skopje");
            result.Alpha3Code.Should().Be("MKD");
        }
        
        [Fact]
        public async void GetSingle_ShouldReturnListOfNeighbouringCountries()
        {   
            var result = await Service.GetSingle("MKD");

            result.BorderingCountries.Should().NotBeNull();
            result.BorderingCountries.Should().HaveCount(2);
            result.BorderingCountries[0].Alpha3Code.Should().Be("SRB");
            result.BorderingCountries[1].Alpha3Code.Should().Be("BGR");
        }

        [Fact]
        public async void GetSingle_CountryWithNoNeighbours_ShouldReturnEmptyListOfNeigbours()
        {

            var result = await Service.GetSingle("NMKD");

            result.BorderingCountries.Should().NotBeNull();
            result.BorderingCountries.Should().BeEmpty();
        }
        
    }
}