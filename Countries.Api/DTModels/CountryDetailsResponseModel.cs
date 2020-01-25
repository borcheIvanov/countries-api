using System.Collections.Generic;

namespace Countries.Api.DTModels
{
    public class CountryDetailsResponseModel: CountryResponseModel
    {
        public string NativeName { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Capital { get; set; }
        public long Population { get; set; }
        public IEnumerable<CountryResponseModel>  BorderingCountries { get; set; } = new List<CountryResponseModel>();
    }
}