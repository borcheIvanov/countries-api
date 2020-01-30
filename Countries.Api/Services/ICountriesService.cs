using System.Collections.Generic;
using System.Threading.Tasks;
using Countries.Api.Models;

namespace Countries.Api.Services
{
    public interface ICountriesService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<IEnumerable<Country>> GetPaginated(int getMany, int skipMany);
        Task<Country> GetSingle(string code);
    }
    
}