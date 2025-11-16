using Microsoft.AspNetCore.Authorization;
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
    [Route("Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        CartRepository cartRepository;

        public CartController()
        {
            cartRepository = new CartRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Cart>>> GetAllCarts()
        {
            try
            {
                var result = await cartRepository.GetAllCarts();
                if (result == null)
                {
                    return Ok(new List<Cart>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            try
            {
                var result = await cartRepository.GetCartById(id);
                if (result == null)
                {
                    return NotFound("Cart not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<ActionResult<Cart>> GetCartByCustomerId(int customerId)
        {
            try
            {
                var result = await cartRepository.GetCartByCustomerId(customerId);
                if (result == null)
                {
                    return NotFound("Cart not found for this customer");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Cart>> AddCart([FromBody] Cart cart)
        {
            try
            {
                if (cart == null)
                {
                    return BadRequest("Cart data is required");
                }

                var result = await cartRepository.AddCart(cart);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add cart");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<ActionResult<Cart>> UpdateCart([FromBody] CartUpdateDTO cartDTO)
        {
            try
            {
                if (cartDTO == null)
                {
                    return BadRequest("Cart data is required");
                }

                var cart = new Cart
                {
                    Id = cartDTO.Id,
                    CustomerId = cartDTO.CustomerId,
                    SessionId = cartDTO.SessionId,
                    Status = cartDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = cartDTO.Delete,
                    DeleteAt = cartDTO.Delete == true ? DateTime.Now : null
                };

                var result = await cartRepository.UpdateCart(cart);
                if (result == null)
                {
                    return NotFound("Cart not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<ActionResult<Cart>> DeleteCart(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await cartRepository.DeleteCart(id, updateBy);
                if (result == null)
                {
                    return NotFound("Cart not found");
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

