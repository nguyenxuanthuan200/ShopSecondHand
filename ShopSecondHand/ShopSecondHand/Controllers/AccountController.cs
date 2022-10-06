using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.Common;
using ShopSecondHand.Data.RequestModels.AccountRequest;
using ShopSecondHand.Data.ResponseModels.AccountResponse;
using ShopSecondHand.Repository.AccountRepository;
using ShopSecondHand.Service.IService;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAccountService _userService;
        public AccountController(IMapper mapper,
            IAccountService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.GetById(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<GetAccountResponse>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<GetAccountResponse>(result.Data));

            // Add error response data information
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id,UpdateAccountRequest payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUser(id,payload);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<UpdateAccountResponse>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.DeleteUser(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<GetAccountResponse>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        [HttpGet("buildings")]
        public async Task<ActionResult> GetUserByBuildingName(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.GetUserByBuildingId(id);

            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<GetAccountResponse>(result.Data));

            // Add error response data information
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
            //try
            //{
            //    var result = await userRepository.GetUserByBuildingName(name);
            //    if (result == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(result);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Error retrieving data from the database.");

            //}
        }
        //[HttpGet]
        //public async Task<ActionResult> GetUser()
        //{
        //    try
        //    {
        //        return Ok(await userRepository.GetUser());
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database.");
        //    }
        //}
        //[HttpGet("search")]
        //public async Task<ActionResult> GetUserByUserName(string name)
        //{
        //    try
        //    {
        //        var result = await userRepository.GetUserByUserName(name);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database.");

        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetUserById(Guid id)
        //{
        //    try
        //    {
        //        var result = await userRepository.GetUserById(id);
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database.");

        //    }
        //}
        //[HttpPost]
        //public async Task<ActionResult> CreateUser(CreateUserRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var create = await userRepository.CreateUser(request);
        //        if (create == null)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError,
        //          "UserName da ton tai.");
        //        }
        //            return Ok(create);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //           e.Message);
        //    }
        //}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        //{
        //    try
        //    {
        //        var update = await userRepository.UpdateUser(id, request);

        //        if (update == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(update);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating User!");
        //    }

        //}
        //[HttpDelete("{id}")]
        //public void DeleteUser(Guid id)
        //{
        //    try
        //    {
        //        var delete = userRepository.GetUserById(id);

        //        if (delete == null)
        //        {
        //            StatusCode(StatusCodes.Status500InternalServerError,
        //           "Error retrieving data from the database.");
        //        }
        //        userRepository.DeleteUser(id);
        //        StatusCode(StatusCodes.Status200OK,
        //           "Delete Success.");
        //    }
        //    catch (Exception)
        //    {
        //        StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error deleting Account!");
        //    }
        //}
    }
}
