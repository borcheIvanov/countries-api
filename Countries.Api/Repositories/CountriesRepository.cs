using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Countries.Api.Models;
using RestSharp;

namespace Countries.Api.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private const string RestCountriesUrl = "https://restcountries.eu/rest/v2"; 
        
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await GetFromApi<List<Country>>(RestCountriesUrl, "all");
        }

        public async Task<IEnumerable<Country>> GetPaginatedAsync(int getMany, int skipMany)
        {
            var allCountries = await GetFromApi<List<Country>>(RestCountriesUrl, "all");
            return allCountries.Take(getMany + skipMany).Skip(skipMany);
        }
        
        public async Task<Country> GetByCodeAsync(string code)
        {
            var country = await GetFromApi<Country>(RestCountriesUrl, $"alpha/{code}");
            if (country is null)
                throw new Exception("Country not found");
            return country;
        }
        
        
        private async Task<T> GetFromApi<T>(string url, string action) where T: new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(action, DataFormat.Json);
            return await client.GetAsync<T>(request);
        }
    }
}