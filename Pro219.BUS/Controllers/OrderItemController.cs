using Microsoft.AspNetCore.Mvc;
using Pro219.API.DTOs;
using Pro219.DAL.Models;
using Pro219.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pro219.API.Controllers
{
    [Route("OrderItem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        OrderItemRepository orderItemRepository;

        public OrderItemController()
        {
            orderItemRepository = new OrderItemRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OrderItem>>> GetAllOrderItems()
        {
            try
            {
                var result = await orderItemRepository.GetAllOrderItems();
                if (result == null)
                {
                    return Ok(new List<OrderItem>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            try
            {
                var result = await orderItemRepository.GetOrderItemById(id);
                if (result == null)
                {
                    return NotFound("Order item not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByOrderId/{orderId}")]
        public async Task<ActionResult<List<OrderItem>>> GetOrderItemsByOrderId(int orderId)
        {
            try
            {
                var result = await orderItemRepository.GetOrderItemsByOrderId(orderId);
                if (result == null)
                {
                    return Ok(new List<OrderItem>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByProductVariantId/{productVariantId}")]
        public async Task<ActionResult<List<OrderItem>>> GetOrderItemsByProductVariantId(int productVariantId)
        {
            try
            {
                var result = await orderItemRepository.GetOrderItemsByProductVariantId(productVariantId);
                if (result == null)
                {
                    return Ok(new List<OrderItem>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<OrderItem>> AddOrderItem([FromBody] OrderItem orderItem)
        {
            try
            {
                if (orderItem == null)
                {
                    return BadRequest("Order item data is required");
                }

                var result = await orderItemRepository.AddOrderItem(orderItem);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add order item");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<OrderItem>> UpdateOrderItem([FromBody] OrderItem orderItem)
        {
            try
            {
                if (orderItem == null)
                {
                    return BadRequest("Order item data is required");
                }

                orderItem.UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = await orderItemRepository.UpdateOrderItem(orderItem);
                if (result == null)
                {
                    return NotFound("Order item not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await orderItemRepository.DeleteOrderItem(id, updateBy);
                if (result == null)
                {
                    return NotFound("Order item not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

