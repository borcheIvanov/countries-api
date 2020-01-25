using System.Collections.Generic;

namespace Countries.Api.Models
{
    public class Country
    {
        public string Alpha3Code { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Capital { get; set; }
        public string Flag { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public long Population { get; set; }
        public List<string> Borders { get; set; }
        public List<Country> BorderingCountries { get; set; } = new List<Country>();
    }
}