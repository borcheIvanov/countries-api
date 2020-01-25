using System.Collections.Generic;

namespace Countries.Api.DTModels
{
    public class PaginatedCountriesResponseModel
    {
        public IEnumerable<CountryResponseModel> Items { get; set; }
        public int TotalItems { get; set; }
        public int SkippedItems { get; set; }
    }
}