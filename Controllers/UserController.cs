using Microsoft.AspNetCore.Mvc;
using epos.Repository.IRepository;
using epos.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository IUserRepo)
        {
            _userRepo = IUserRepo;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            return Ok(await _userRepo.CreateUser(user));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            return Ok(_userRepo.GetUsers());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UserID}")]
        public async Task<ActionResult> GetUserById([FromRoute] long userId)
        {
            var result = await _userRepo.GetUserById(userId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public async Task<ActionResult> GetUserByName([FromRoute] string name)
        {
            var result = await _userRepo.GetUserByName(name);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                return Ok(await _userRepo.UpdateUser(user));
            }

            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] long userId)
        {
            try
            {
                return Ok(await _userRepo.DeleteUser(userId));
            }
            
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}