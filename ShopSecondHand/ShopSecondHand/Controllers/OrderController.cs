using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.OrderRequest;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Models;
using ShopSecondHand.Repository.OrderRepository;
using System;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetOrder()
        {
            try
            {
                return Ok(await orderRepository.GetOrder());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderById(id);
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
        [HttpGet("posts")]
        public async Task<ActionResult> GetOrderByPostId(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderByPostId(id);
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
        [HttpGet("users")]
        public async Task<ActionResult> GetOrderByUserId(Guid id)
        {
            try
            {
                var result = await orderRepository.GetOrderByUserId(id);
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
        public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                var create = await orderRepository.CreateOrder(request);
                if (create == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                  "Order da ton tai.");
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
        public async Task<ActionResult> UpdateOrder(Guid id, UpdateOrderRequest request)
        {
            try
            {
                var update = await orderRepository.UpdateOrder(id, request);

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
        public void DeleteBuilding(Guid id)
        {
            try
            {
                var delete = orderRepository.GetOrderById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                orderRepository.DeleteBuilding(id);
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
