using System.Collections.Generic;
using System.Threading.Tasks;
using Countries.Api.Models;
using Countries.Api.Repositories;

namespace Countries.Api.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
             _countriesRepository = new CountriesRepositoryCachingDecorator(countriesRepository);
        }
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _countriesRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Country>> GetPaginated(int getMany, int skipMany)
        {
            return await _countriesRepository.GetPaginatedAsync(getMany, skipMany);
        }


        public async Task<Country> GetSingle(string code)
        {
            return await _countriesRepository.GetByCodeAsync(code);
        }
    }
}