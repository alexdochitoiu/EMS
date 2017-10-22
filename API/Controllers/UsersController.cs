using API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Repositories.CustomRepositories;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        } 

        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (var unitOfWork = new UnitOfWork.UnitOfWork(_context))
            {
                //unitOfWork.Users.Add(new User(1, "gheorge12", "parola_grea"));
                //unitOfWork.Users.Add(new User(2, "ss23", "parola_grea25"));
                //unitOfWork.Complete();
                return unitOfWork.Users.GetAll();
            }
        }
    }
}