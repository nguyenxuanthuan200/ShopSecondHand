using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSecondHand.Data.RequestModels.OrderDetailRequest;
using ShopSecondHand.Data.ResponseModels.OrderDetailResponse;
using ShopSecondHand.Data.ResponseModels.OrderResponse;
using ShopSecondHand.Repository.OrderDetailRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSecondHand.Controllers
{
    [Route("api/orderdetails")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetOrderDetail()
        {
            try
            {
                return Ok(await orderDetailRepository.GetOrderDetail());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderDetailById(Guid id)
        {
            try
            {
                var result = await orderDetailRepository.GetOrderDetailById(id);
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
        [HttpGet("orders")]
        public async Task<ActionResult> GetOrderDetailByOrderId(Guid id)
        {
            try
            {
                var result = await orderDetailRepository.GetOrderDetailByOrderId(id);
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
        public async Task<ActionResult> CreateOrder(CreateOrderDetailRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }
                var create = await orderDetailRepository.CreateOrderDetail(request);
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
        public async Task<ActionResult> UpdateOrderDetail(Guid id, UpdateOrderDetailRequest request)
        {
            try
            {
                var update = await orderDetailRepository.UpdateOrderDetail(id, request);

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
        public void DeleteOrderDetail(Guid id)
        {
            try
            {
                var delete = orderDetailRepository.GetOrderDetailById(id);

                if (delete == null)
                {
                    StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database.");
                }
                orderDetailRepository.DeleteOrderDetail(id);
                StatusCode(StatusCodes.Status500InternalServerError,
                   "Error Suc!");
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting!");
            }
        }
    }
}
