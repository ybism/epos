using Microsoft.AspNetCore.Mvc;
using epos.Repository.IRepository;
using epos.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateOrder([FromRoute] int userId, [FromBody] List<FoodItem> order)
        {
            try
            {
                return Ok(await _orderRepo.CreateOrder(userId, order));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok(_orderRepo.GetOrders());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{OrderID}")]
        public async Task<ActionResult> GetOrderById([FromRoute] int orderId)
        {
            var order = await _orderRepo.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound("Order does not exist");
            }

            return Ok(order);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] Order order)
        {
            try
            {
                return Ok(await _orderRepo.UpdateOrder(order));
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder([FromBody] long id)
        {
            try
            {
                await _orderRepo.DeleteOrder(id);
                return Ok();
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}