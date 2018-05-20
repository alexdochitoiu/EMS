using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET api/users
        [HttpGet(Name="AllUsers")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> AllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll(
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
                var user = await _unitOfWork.Users.GetById(id);
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
                var user = await _unitOfWork.Users.GetByAge(age);
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(user);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // POST api/users
        [HttpPost(Name="CreateUser")]
        [ProducesResponseType(typeof(DisplayUserModel), 201)]
        public async Task<ActionResult> CreateUser([FromBody]CreatingUserModel model)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetByName(model.Country);
                var city = await _unitOfWork.Cities.GetByName(model.City);
                var address = Address.Create(country, city, model.Street, model.Number, model.ZipCode);
                var user = Data.Core.Domain.Entities.Identity.User.Create(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    "Secret",
                    model.Phone,
                    address);
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.Complete();
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
                var user = await _unitOfWork.Users.GetById(id);
                user.Update(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    "Secret",
                    model.Phone,
                    null);
                await _unitOfWork.Users.Edit(user, id);
                await _unitOfWork.Complete();
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
                var user = _unitOfWork.Users.GetById(id).Result;
                _unitOfWork.Users.Delete(user);
                if (_unitOfWork.Complete().Result != 1)
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
