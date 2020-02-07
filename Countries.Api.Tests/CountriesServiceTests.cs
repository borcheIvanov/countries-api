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
        private readonly ICountriesRepository _repository;
        private readonly ICountriesService _service;
        public CountriesServiceTests()
        {
            _repository = A.Fake<ICountriesRepository>();
            _service = new CountriesService(_repository);
        }
        
        [Fact]
        public async void GetAllAsyncShouldBeNotNull()
        {
            A.CallTo(() => _repository.GetAllAsync()).Returns(new List<Country>());

            var result = await _service.GetAllAsync();
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async void GetAllAsyncShouldReturnListOfCountries()
        {
            var countries = A.CollectionOfFake<Country>(25);
         
            A.CallTo(() => _repository.GetAllAsync()).Returns(countries);

            var result = await _service.GetAllAsync();
            result.Should().BeOfType<List<Country>>();
        }
        
        [Fact]
        public async void GetAllAsyncShouldReturnAllCountries()
        {
            var countries = A.CollectionOfDummy<Country>(25);
            A.CallTo(() => _repository.GetAllAsync()).Returns(countries);

            var result = await _service.GetAllAsync();
            result.Should().HaveCount(25);
        }
        
        [Fact]
        public async void FakERepository()
        {
            var countries = A.CollectionOfDummy<Country>(25);
            var country = new Country {Alpha3Code = "MKD", Name = "Macedonia", Capital = "Skopje"};
            countries.Add(country);
            
            A.CallTo(() => _repository.GetByCodeAsync("MKD")).Returns(country);

            var res = await _service.GetSingle("bla");

            A.CallTo(() => _repository.GetByCodeAsync("bla")).MustHaveHappenedOnceExactly();
        }
        
        
        
    }
}