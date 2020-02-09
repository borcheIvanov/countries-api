using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Countries.Api.Models;
using LazyCache;

namespace Countries.Api.Repositories
{
    public class CountriesRepositoryCachingDecorator : ICountriesRepository
    {
        private const string CacheKey = "AllCountries";

        private readonly IAppCache _cache = new CachingService();
        private readonly ICountriesRepository _decoratedRepository;

        public CountriesRepositoryCachingDecorator(
            ICountriesRepository decoratedRepository)
        {
            _decoratedRepository = decoratedRepository;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await GetAllCountries();
        }

        public async Task<IEnumerable<Country>> GetPaginatedAsync(int getMany, int skipMany)
        {
            var allCountries = await GetAllAsync();
            return allCountries.Take(getMany + skipMany).Skip(skipMany);
        }

        public async Task<Country> GetByCodeAsync(string code)
        {
            var country = (await GetAllCountries()).ToList().FirstOrDefault(x =>
                string.Equals(x.Alpha3Code, code, StringComparison.CurrentCultureIgnoreCase));
            
            if (country is null)
                throw new Exception("Country not found");

            country.BorderingCountries = (await GetAllCountries()).ToList()
                .Where(x => country.Borders.Contains(x.Alpha3Code)).ToList();
            
            return country;
        }

        private async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _cache.GetOrAdd(CacheKey, async () => await _decoratedRepository.GetAllAsync());
        }
    }
}