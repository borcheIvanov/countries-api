using System.Collections.Generic;
using System.Net.Http;
using Countries.Api.Models;
using Countries.Api.Repositories;
using Countries.Api.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Countries.Api.Tests
{
    public class BaseTestClass
    {
        protected readonly HttpClient HttpClient;
        protected readonly ICountriesRepository Repository;
        protected readonly ICountriesService Service;

        public BaseTestClass()
        {
            Repository = A.Fake<ICountriesRepository>();
            var countries = GetDummyCountries();
            A.CallTo(() => Repository.GetAllAsync()).Returns(countries);
            
            Service = new CountriesService(Repository);
            
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            HttpClient = testServer.CreateClient();
        }
        
        protected static IList<Country> GetDummyCountries()
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