using Microsoft.AspNetCore.Mvc;
using Stoixima.Data;
using Stoixima.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stoixima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository)
        {
            _repository = repository;
        }

        //private readonly MockUserRepo _repository = new MockUserRepo();

        [HttpGet]
        public ActionResult <IEnumerable<UserModel>> GetAllUsers()
        {
            var userItems = _repository.GetAllUsers();

            return Ok(userItems);
        }

        [HttpGet("{id}")]
        public ActionResult <UserModel> GetUserById(int id)
        {
            var userItem = _repository.GetUserById(id);

            return Ok(userItem);
        }

        [HttpPost]
        public ActionResult <UserModel> CreateUser(UserModel user)
        {
            var userItem = _repository.CreateUser(user);

            return Ok(userItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserModel user)
        {
            _repository.UpdateUser(id, user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _repository.DeleteUser(id);

            return NoContent();
        }
    }
}
