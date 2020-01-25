using System.ComponentModel.DataAnnotations;

namespace Countries.Api.DTModels
{
    public class CountriesRequestModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int GetItems { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int SkipItems { get; set; }
    }
}