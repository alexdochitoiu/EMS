using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public async Task<ActionResult> Users()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return Ok(users);
            }
            catch (Exception exp)
            {
                Console.Write(exp);
                return BadRequest();
            }
        }

        // GET api/users/5
        [HttpGet("{id}", Name = "GetUserRoute")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> Users(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(typeof(User), 201)]
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
                return Ok(user);
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
                var user = _userRepository.GetByIdAsync(id).Result;
                //Debug
                Console.Write(user.FirstName + "\n\n");

                Console.Write(model.FirstName + "\n");
                //End Debug
                user.Update(
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Email,
                    "Secret",
                    model.Phone,
                    null);
                await _userRepository.Edit(user);
                return Ok(user);
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
            var status = await _userRepository.Delete(id);
            if (!status)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
