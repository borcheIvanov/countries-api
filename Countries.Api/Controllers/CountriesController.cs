using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.DTModels;
using Countries.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Countries.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesService countriesService, IMapper mapper)
        {
            _countriesService = countriesService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] CountriesRequestModel requestModel)
        {
            var response = new PaginatedCountriesResponseModel
            {
                Items = _mapper.Map<IEnumerable<CountryResponseModel>>(
                    await _countriesService.GetPaginated(requestModel.GetItems, requestModel.SkipItems)),
                TotalItems = _countriesService.GetAllAsync().GetAwaiter().GetResult().Count,
                SkippedItems = requestModel.SkipItems
            };
            
            return Ok(response);
        }

        [HttpGet("country/{code}")]
        public async Task<IActionResult> GetCountry(string code)
        {
            return Ok(_mapper.Map<CountryDetailsResponseModel>(await _countriesService.GetSingle(code)));
        }
    }
}