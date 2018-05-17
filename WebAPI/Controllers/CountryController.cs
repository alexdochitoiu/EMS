using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.CityModels;
using WebAPI.Models.CountryModels;

namespace WebAPI.Controllers
{
    [Route("api/countries")]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/countries
        [HttpGet(Name = "AllCountries")]
        [ProducesResponseType(typeof(List<DisplayCountryModel>), 200)]
        public async Task<ActionResult> AllCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var displayCountries = _mapper.Map<List<DisplayCountryModel>>(countries);
                return Ok(displayCountries);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/countries/5
        [HttpGet("{id:guid}", Name = "CountryById")]
        [ProducesResponseType(typeof(DisplayCountryModel), 200)]
        public async Task<ActionResult> CountryById(Guid id)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetById(id);
                var displayCountry = _mapper.Map<DisplayCountryModel>(country);
                return Ok(displayCountry);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/countries/Romania
        [HttpGet("{name:alpha}", Name = "CountryByName")]
        [ProducesResponseType(typeof(DisplayCountryModel), 200)]
        public async Task<ActionResult> CountryByName(string name)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetByName(name);
                var displayCountry = _mapper.Map<DisplayCountryModel>(country);
                return Ok(displayCountry);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/countries/5/cities
        [HttpGet("{id:guid}/cities", Name = "GetCities")]
        [ProducesResponseType(typeof(List<DisplayCityModel>), 200)]
        public async Task<ActionResult> GetCities(Guid id)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetById(id);
                var cities = country.Cities.ToList();
                var displayCities = _mapper.Map<List<DisplayCityModel>>(cities);
                return Ok(displayCities);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }
    }
}