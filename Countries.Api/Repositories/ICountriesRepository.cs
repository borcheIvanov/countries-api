using System.Collections.Generic;
using System.Threading.Tasks;
using Countries.Api.Models;

namespace Countries.Api.Repositories
{
    public interface ICountriesRepository
    {
        Task<Models.Countries> GetAllAsync();
        Task<IEnumerable<Country>> GetPaginatedAsync(int getMany, int skipMany);
        Task<Country> GetByCodeAsync(string code);
    }
}