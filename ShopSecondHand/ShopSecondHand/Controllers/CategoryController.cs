using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.CategoryRequest;
using ShopSecondHand.Data.ResponseModels.CategoryResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.CategoryRepository;
using System;
using System.Threading.Tasks;
namespace ShopSecondHand.Controllers
{
    [Route("api/CategoryController")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategory()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await categoryRepository.GetCategory());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("getCategoryByName")]
        public async Task<ActionResult<GetCategoryResponse>> GetCategoryByName(String name)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await categoryRepository.GetCategoryByName(name);
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
        [HttpGet("getCategoryById")]
        public async Task<ActionResult<GetCategoryResponse>> GetCategoryById(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await categoryRepository.GetCategoryById(id);
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
        public async Task<ActionResult<CreateCategoryResponse>> CreateCategory([FromBody] CreateCategoryRequest categoryRequest)
        {
            try
            { 
                var createBuilding = await categoryRepository.CreateCategory(categoryRequest);
                if (createBuilding != null)
                {
                    return Ok(createBuilding);

                    //return CreatedAtAction(nameof(GetAccountById),
                    // new { id = createdAccount.AccountId }, createdAccount);
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Name da ton tai.");



            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   e.Message);
            }
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<UpdateCategoryResponse>> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest buildingRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updateBuilding = await categoryRepository.UpdateCategory(id, buildingRequest);

                if (updateBuilding == null)
                {
                    return NotFound($"Category  with Id = {id} not found!");
                }

                return Ok($"update thanh cong");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Store!");
            }

        }
        [HttpDelete("{id:Guid}")]
        public void DeleteCategory(Guid id)
        {
            try
            {

                var updateBuilding = categoryRepository.GetCategoryById(id);

                if (updateBuilding == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                else {
                    categoryRepository.DeleteCategory(id);
                    Ok();
                        }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Account!");
            }
        }
    }

}
