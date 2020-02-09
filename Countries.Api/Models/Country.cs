using System.Collections.Generic;

namespace Countries.Api.Models
{
    public class Country
    {
        public string Alpha3Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NativeName { get; set; } = string.Empty;
        public string Capital { get; set; }  = string.Empty;
        public string Flag { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string SubRegion { get; set; } = string.Empty;
        public long Population { get; set; }
        public List<string> Borders { get; set; } = new List<string>();
        public List<Country> BorderingCountries { get; set; } = new List<Country>();
    }
}