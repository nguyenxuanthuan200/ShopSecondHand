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
    public class UserController : Controller
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
        [HttpGet("{name}")]
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
        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            try
            {
                if (userRequest == null)
                {
                    return BadRequest();
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
        [HttpPut("{userName}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateBuilding(String userName, [FromBody] UpdateUserRequest userRequest)
        {
            try
            {
                var updateUser = await userRepository.UpdateUser(userName, userRequest);

                if (updateUser == null)
                {
                    return NotFound($"User  with UserName = {userName} not found!");
                }

                return updateUser;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Store!");
            }

        }
        [HttpDelete("{userName}")]
        public void DeleteUser(String userName)
        {
            try
            {
                var updateBuilding = userRepository.GetUserByUserName(userName);

                if (updateBuilding == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                userRepository.DeleteUser(userName);
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
