using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.UserRequest;
using ShopSecondHand.Data.ResponseModels.UserResponse;
using ShopSecondHand.Repository.UserRepository;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                return Ok(await userRepository.GetUser());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult> GetUserByUserName(string name)
        {
            try
            {
                var result = await userRepository.GetUserByUserName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpGet("buildings")]
        public async Task<ActionResult> GetUserByBuildingName(string name)
        {
            try
            {
                var result = await userRepository.GetUserByBuildingName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            try
            {
                var result = await userRepository.GetUserById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var create = await userRepository.CreateUser(request);
                if (create == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                  "UserName da ton tai.");
                }
                    return Ok(create);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        {
            try
            {
                var update = await userRepository.UpdateUser(id, request);

                if (update == null)
                {
                    return NotFound();
                }

                return Ok(update);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating User!");
            }

        }
        [HttpDelete("{id}")]
        public void DeleteUser(Guid id)
        {
            try
            {
                var delete = userRepository.GetUserById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                userRepository.DeleteUser(id);
                StatusCode(StatusCodes.Status200OK,
                   "Delete Success.");
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Account!");
            }
        }
    }
}
