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
    [Route("CartItem")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        CartItemRepository cartItemRepository;

        public CartItemController()
        {
            cartItemRepository = new CartItemRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<CartItem>>> GetAllCartItems()
        {
            try
            {
                var result = await cartItemRepository.GetAllCartItems();
                if (result == null)
                {
                    return Ok(new List<CartItem>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CartItem>> GetCartItemById(int id)
        {
            try
            {
                var result = await cartItemRepository.GetCartItemById(id);
                if (result == null)
                {
                    return NotFound("Cart item not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByCartId/{cartId}")]
        public async Task<ActionResult<List<CartItem>>> GetCartItemsByCartId(int cartId)
        {
            try
            {
                var result = await cartItemRepository.GetCartItemsByCartId(cartId);
                if (result == null)
                {
                    return Ok(new List<CartItem>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<CartItem>> AddCartItem([FromBody] CartItem cartItem)
        {
            try
            {
                if (cartItem == null)
                {
                    return BadRequest("Cart item data is required");
                }

                var result = await cartItemRepository.AddCartItem(cartItem);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add cart item");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<CartItem>> UpdateCartItem([FromBody] CartItemUpdateDTO cartItemDTO)
        {
            try
            {
                if (cartItemDTO == null)
                {
                    return BadRequest("Cart item data is required");
                }

                var cartItem = new CartItem
                {
                    Id = cartItemDTO.Id,
                    CartId = cartItemDTO.CartId,
                    VariantId = cartItemDTO.VariantId,
                    Quantity = cartItemDTO.Quantity,
                    UnitPrice = cartItemDTO.UnitPrice,
                    IsSelectedForCheckout = cartItemDTO.IsSelectedForCheckout,
                    Status = cartItemDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = cartItemDTO.Delete,
                    DeleteAt = cartItemDTO.Delete == true ? DateTime.Now : null
                };

                var result = await cartItemRepository.UpdateCartItem(cartItem);
                if (result == null)
                {
                    return NotFound("Cart item not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await cartItemRepository.DeleteCartItem(id, updateBy);
                if (result == null)
                {
                    return NotFound("Cart item not found");
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

