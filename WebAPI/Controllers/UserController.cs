﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Infrastructure;
using WebAPI.Models.AnnouncementModels;
using WebAPI.Models.IncidentModels;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<IncidentController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/users
        [HttpGet(Name="AllUsers")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        [AuthorizeToken]
        public async Task<ActionResult> AllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync<object>(
                      t => t.Include(user => user.Address)
                                .ThenInclude(address => address.Country)
                            .Include(user => user.Address)
                                .ThenInclude(address => address.City));
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(users);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/5
        [HttpGet("{id:guid}", Name="UserById")]
        [ProducesResponseType(typeof(DisplayUserModel), 200)]
        public async Task<ActionResult> UserById(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/21
        [HttpGet("{age:int}", Name="UsersByAge")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> UsersByAge(int age)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByAgeAsync(age);
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(user);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/by-username/dokee131
        [HttpGet("by-username/{username}", Name = "UsersByUsername")]
        [ProducesResponseType(typeof(DisplayUserModel), 200)]
        public async Task<ActionResult> UsersByUsername(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByUsernameAsync(username);
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/alexdochitoiu@gmail.com
        [HttpGet("{email}", Name = "UsersByEmail")]
        [ProducesResponseType(typeof(DisplayUserModel), 200)]
        public async Task<ActionResult> UsersByEmail(string email)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByEmailAsync(email);
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/5/announcements
        [HttpGet("{id:guid}/announcements", Name = "UserAnnouncements")]
        [ProducesResponseType(typeof(List<DisplayAnnouncementModel>), 200)]
        public async Task<ActionResult> UserAnnouncements(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                if (user == null) return NotFound();

                var displayAnnouncements = _mapper.Map<List<DisplayAnnouncementModel>>(user.Announcements);
                return Ok(displayAnnouncements);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest(exp.Message);
            }
        }

        // GET api/users/radius
        [HttpGet("radius", Name = "AllUsersWithinARadius")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> AllUsersWithinARadius([FromQuery] IncidentQueryParamsModel queryModel)
        {
            try
            {
                var centerLat = Convert.ToDouble(queryModel.CenterLatitude);
                var centerLng = Convert.ToDouble(queryModel.CenterLongitude);
                var km = Convert.ToDouble(queryModel.Kilometers);
                var nearbyUsers = await _unitOfWork
                    .Users
                    .GetUsersWithinARadiusAsync(centerLat, centerLng, km);
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(nearbyUsers);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                _logger.LogCritical(exp.Message);
                return BadRequest(exp.Message);
            }
        }

        // POST api/users
        [HttpPost(Name="CreateUser")]
        [ProducesResponseType(typeof(DisplayUserModel), 201)]
        public async Task<ActionResult> CreateUser([FromBody]CreatingUserModel model)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetByNameAsync(model.Country);
                var city = await _unitOfWork.Cities.GetByNameAsync(model.City);
                var address = Address.Create(country, city, model.Street, model.Number, model.ZipCode);
                var user = ApplicationUser.Create(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    model.Username,
                    model.Phone,
                    address,
                    Convert.ToDouble(model.CurrentLongitude),
                    Convert.ToDouble(model.CurrentLatitude));
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // PUT api/users/5
        [HttpPut("{id:guid}", Name="UpdateUser")]
        [ProducesResponseType(typeof(DisplayUserModel), 200)]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody]CreatingUserModel model)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(id);
                user.Update(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    model.Username,
                    model.Phone,
                    null,
                    Convert.ToDouble(model.CurrentLongitude),
                    Convert.ToDouble(model.CurrentLatitude));
                await _unitOfWork.Users.EditAsync(user, id);
                await _unitOfWork.CompleteAsync();
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // DELETE api/users/5
        [HttpDelete("{id:guid}", Name="DeleteUser")]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _unitOfWork.Users.GetByIdAsync(id).Result;
                _unitOfWork.Users.Delete(user);
                if (_unitOfWork.CompleteAsync().Result != 1)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }
    }
}
