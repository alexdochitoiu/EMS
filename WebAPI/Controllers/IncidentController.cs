using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Models.IncidentModels;

namespace WebAPI.Controllers
{
    [Route("api/incidents")]
    public class IncidentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public IncidentController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ILogger<IncidentController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/incidents
        [HttpGet(Name = "AllIncidents")]
        [ProducesResponseType(typeof(List<DisplayIncidentModel>), 200)]
        public async Task<ActionResult> AllIncidents()
        {
            try
            {
                var incidents = await _unitOfWork.Incidents.GetAllAsync(
                        load: t => t.Include(i => i.Reporter),
                        orderBy: t => t.Id);
                var displayIncidents = _mapper.Map<List<DisplayIncidentModel>>(incidents);
                return Ok(displayIncidents);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        // GET api/incidents/radius
        [HttpGet("radius", Name = "AllIncidentsWithinARadius")]
        [ProducesResponseType(typeof(List<DisplayIncidentModel>), 200)]
        public async Task<ActionResult> AllIncidentsWithinARadius([FromQuery] IncidentQueryParamsModel queryModel)
        {
            try
            {
                var centerLat = Convert.ToDouble(queryModel.CenterLatitude);
                var centerLng = Convert.ToDouble(queryModel.CenterLongitude);
                var km = Convert.ToDouble(queryModel.Kilometers);
                _logger.LogCritical($"{centerLat} {centerLng} {km}");
                var incidents = await _unitOfWork
                    .Incidents
                    .GetIncidentsWithinARadiusAsync(centerLat, centerLng, km);
                var displayIncidents = _mapper.Map<List<DisplayIncidentModel>>(incidents);
                return Ok(displayIncidents);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        // GET api/incidents/5
        [HttpGet("{id:guid}", Name = "IncidentById")]
        [ProducesResponseType(typeof(DisplayIncidentModel), 200)]
        public async Task<ActionResult> IncidentById(Guid id)
        {
            try
            {
                var incident = await _unitOfWork.Incidents.GetByIdAsync(id);
                var displayIncident = _mapper.Map<DisplayIncidentModel>(incident);
                return Ok(displayIncident);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        // POST api/incidents
        [HttpPost(Name = "CreateIncident")]
        [ProducesResponseType(typeof(DisplayIncidentModel), 201)]
        public async Task<ActionResult> CreateIncident([FromBody]CreatingIncidentModel model)
        {
            try
            {
                var reporter = await _unitOfWork.Users.GetByIdAsync(Guid.Parse(model.ReporterId));
                if (reporter == null)
                {
                    _logger.LogCritical("Reporter not found in database");
                    return BadRequest("Reporter not found in database");
                }

                var incident = Incident.Create(
                    model.Summary,
                    model.Description,
                    model.Longitude,
                    model.Latitude,
                    model.Severity,
                    reporter);

                await _unitOfWork.Incidents.AddAsync(incident);
                await _unitOfWork.CompleteAsync();

                var displayIncident = _mapper.Map<DisplayIncidentModel>(incident);
                return Ok(displayIncident);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }
    }
}