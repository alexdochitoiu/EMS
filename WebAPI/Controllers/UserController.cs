using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> AllUsers()
        {
            try
            {
                var users = await _userRepository.GetAll(
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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DisplayUserModel), 200)]
        public async Task<ActionResult> UserById(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                var displayUser = _mapper.Map<DisplayUserModel>(user);
                return Ok(displayUser);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/alex
        [HttpGet("{firstName}")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> UsersByFirstName(string firstName)
        {
            try
            {
                var user = await _userRepository.GetByFirstName(firstName);
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(user);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/dochitoiu
        [HttpGet("{lastName}")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> UsersByLastName(string lastName)
        {
            try
            {
                var user = await _userRepository.GetByLastName(lastName);
                var displayUsers = _mapper.Map<List<DisplayUserModel>>(user);
                return Ok(displayUsers);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/21
        [HttpGet("{age}")]
        [ProducesResponseType(typeof(List<DisplayUserModel>), 200)]
        public async Task<ActionResult> UsersByLastName(int age)
        {
            try
            {
                var user = await _userRepository.GetByAge(age);
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
        [HttpPost]
        [ProducesResponseType(typeof(DisplayUserModel), 201)]
        public async Task<ActionResult> CreateUser([FromBody]CreatingUserModel model)
        {
            try
            {
                var country = Country.Create("Romania", "RO");
                var city = City.Create("Bacau", "BC", 2.45, 22.3);
                var address = Address.Create(country, city, "Calea Moldovei", "192", "600352");
                var user = Data.Core.Domain.User.Create(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    "Secret",
                    model.Phone,
                    address);
                await _userRepository.Add(user);
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
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody]CreatingUserModel model)
        {
            try
            {
                var user = _userRepository.GetById(id).Result;
                user.Update(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    "Secret",
                    model.Phone,
                    null);
                await _userRepository.Edit(user, id);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = _userRepository.GetById(id).Result;
                var status = await _userRepository.Delete(user);
                if (status > 0)
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
