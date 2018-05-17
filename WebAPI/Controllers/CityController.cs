using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.CityModels;

namespace WebAPI.Controllers
{
    [Route("api/cities")]
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET api/cities
        [HttpGet(Name = "AllCities")]
        [ProducesResponseType(typeof(List<DisplayCityModel>), 200)]
        public async Task<ActionResult> AllCities()
        {
            try
            {
                var cities = await _unitOfWork.Cities.GetAll();
                var displayCities = _mapper.Map<List<DisplayCityModel>>(cities);
                return Ok(displayCities);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/cities/5
        [HttpGet("{id:guid}", Name = "CityById")]
        [ProducesResponseType(typeof(DisplayCityModel), 200)]
        public async Task<ActionResult> CityById(Guid id)
        {
            try
            {
                var city = await _unitOfWork.Cities.GetById(id);
                var displayCity = _mapper.Map<DisplayCityModel>(city);
                return Ok(displayCity);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/cities/Bacau
        [HttpGet("{name:alpha}", Name = "CityByName")]
        [ProducesResponseType(typeof(DisplayCityModel), 200)]
        public async Task<ActionResult> CityByName(string name)
        {
            try
            {
                var city = await _unitOfWork.Cities.GetByName(name);
                var displayCity = _mapper.Map<DisplayCityModel>(city);
                return Ok(displayCity);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }
    }
}