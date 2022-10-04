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
    [Route("api/categorys")]
    [ApiController]
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
        [HttpGet("search")]
        public async Task<ActionResult> GetCategoryByName(string name)
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
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(Guid id)
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
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");

            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                var create = await categoryRepository.CreateCategory(request);
                if (create == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                  "Category da ton tai.");
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
        public async Task<ActionResult> UpdateCategory(Guid id,UpdateCategoryRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var update = await categoryRepository.UpdateCategory(id, request);

                if (update == null)
                {
                    return NotFound();
                }

                return Ok(update);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Store!");
            }

        }
        [HttpDelete("{id}")]
        public void DeleteCategory(Guid id)
        {
            try
            {

                var delete = categoryRepository.GetCategoryById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                else
                {
                    categoryRepository.DeleteCategory(id);
                    StatusCode(StatusCodes.Status200OK,
                   "Delete Success.");
                }
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting!");
            }
        }
    }

}
