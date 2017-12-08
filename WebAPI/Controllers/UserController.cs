using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Models.UserModels;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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
                _logger.LogError(exp.Message);
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
                _logger.LogError(exp.Message);
                return BadRequest();
            }
        }

        // POST api/users
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(User), 201)]
        public async Task<ActionResult> CreateUser([FromBody]CreatingUserModel model)
        {
            var user = Data.Core.Domain.User.Create(
                model.FirstName,
                model.LastName,
                model.Gender,
                model.DateOfBirth,
                model.Email,
                "Secret",
                model.Phone,
                model.Address);
            await _userRepository.Add(user);
            return CreatedAtRoute("GetUserRoute", new {id = user.Id}, user);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody]User model)
        {
            var user = _userRepository.GetByIdAsync(id);
            user.Result.Update(
                model.FirstName,
                model.LastName,
                model.Gender,
                model.DateOfBirth,
                model.Email,
                "Secret",
                model.Phone,
                model.Address);
            await _userRepository.Edit(user.Result);
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
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
