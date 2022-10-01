using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.UserRequest;
using ShopSecondHand.Data.ResponseModels.UserResponse;
using ShopSecondHand.Repository.UserRepository;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/UserController")]
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
        [HttpGet("getByUserName")]
        public async Task<ActionResult<GetUserResponse>> GetUserByUserName(String name)
        {
            try
            {
                var result = await userRepository.GetUserByUserName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpGet("getByBuildingName")]
        public async Task<ActionResult> GetUserByBuildingName(String name)
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
        [HttpGet("getById")]
        public async Task<ActionResult<GetUserResponse>> GetUserById(Guid id)
        {
            try
            {
                var result = await userRepository.GetUserById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createUser = await userRepository.CreateUser(userRequest);
                if (createUser != null)
                {
                    return Ok(createUser);
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "UserName da ton tai.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(Guid id, [FromBody] UpdateUserRequest userRequest)
        {
            try
            {
                var updateUser = await userRepository.UpdateUser(id, userRequest);

                if (updateUser == null)
                {
                    return NotFound($"User  with UserName = {id} not found!");
                }

                return updateUser;
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
                var updateBuilding = userRepository.GetUserById(id);

                if (updateBuilding == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                userRepository.DeleteUser(id);
                Ok();
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Account!");
            }
        }
    }
}
