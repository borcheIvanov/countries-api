using System.Collections.Generic;

namespace Countries.Api.DTModels
{
    public class CountryDetailsResponseModel: CountryResponseModel
    {
        public string NativeName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string SubRegion { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public long Population { get; set; }
        public IEnumerable<CountryResponseModel>  BorderingCountries { get; set; } = new List<CountryResponseModel>();
    }
}