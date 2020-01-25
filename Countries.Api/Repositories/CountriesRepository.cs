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
        
        public async Task<Models.Countries> GetAllAsync()
        {
            return await GetFromApi<Models.Countries>(RestCountriesUrl, "all");
        }

        public async Task<IEnumerable<Country>> GetPaginatedAsync(int getMany, int skipMany)
        {
            var allCountries = await GetFromApi<Models.Countries>(RestCountriesUrl, "all");
            return allCountries.Take(getMany + skipMany).Skip(skipMany);
        }
        
        public async Task<Country> GetByCodeAsync(string code)
        {
            return await GetFromApi<Country>(RestCountriesUrl, $"alpha/{code}");
        }
        
        
        private async Task<T> GetFromApi<T>(string url, string action) where T: new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(action, DataFormat.Json);
            return await client.GetAsync<T>(request);
        }
    }
}