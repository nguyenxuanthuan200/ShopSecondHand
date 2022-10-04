using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.ProductRequest;
using ShopSecondHand.Data.ResponseModels.ProductResponse;
using ShopSecondHand.Repository.ProductRepository;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProduct()
        {
            try
            {
                return Ok(await productRepository.GetProduct());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            try
            {
                var result = await productRepository.GetProductById(id);
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
        [HttpGet("search")]
        public async Task<ActionResult> GetProductByName(string name)
        {
            try
            {
                var result = await productRepository.GetProductByName(name);
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
        public async Task<ActionResult> CreateProduct(CreateProductRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                var create = await productRepository.CreateProduct(request);
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
        public async Task<ActionResult> UpdateProduct(Guid id, UpdateProductRequest request)
        {
            try
            {
                var update = await productRepository.UpdateProduct(id, request);

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
        public void DeleteProduct(Guid id)
        {
            try
            {
                var delete = productRepository.GetProductById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                productRepository.DeleteProduct(id);
                //Ok();
                StatusCode(StatusCodes.Status200OK,
                   "Delete Success.");
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting!");
            }
        }
    }
}
