using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Infrastructure.SMS;
using WebAPI.Models.AlertModels;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/alert")]
    public class AlertController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly ISmsSender _smsService;
        private readonly IMapper _mapper;

        public AlertController(
            IUnitOfWork unitOfWork,
            ILogger<AlertController> logger,
            ISmsSender smsService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _smsService = smsService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("nearby", Name = "AlertNearbyUsersViaSms")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<IActionResult> AlertNearbyUsersViaSms([FromBody] AlertNearbyModel alertNearbyModel)
        {
            try
            {
                var centerLat = Convert.ToDouble(alertNearbyModel.Latitude);
                var centerLng = Convert.ToDouble(alertNearbyModel.Longitude);
                var km = Convert.ToDouble(alertNearbyModel.Radius);

                var nearbyUsers = await _unitOfWork
                    .Users
                    .GetUsersWithinARadiusAsync(centerLat, centerLng, km);
                
                nearbyUsers.ForEach(async delegate(ApplicationUser user)
                {
                    if (user.PhoneNumber == null) return;

                    await _smsService.SendSms("+4"+user.PhoneNumber, alertNearbyModel.Message);
                });

                var displayUsers = _mapper.Map<List<DisplayUserModel>>(nearbyUsers);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }
    }
}