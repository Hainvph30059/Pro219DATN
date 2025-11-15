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
    [Route("ProductVariant")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        ProductVariantRepository productVariantRepository;

        public ProductVariantController()
        {
            productVariantRepository = new ProductVariantRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductVariant>>> GetAllProductVariants()
        {
            try
            {
                var result = await productVariantRepository.GetAllProductVariants();
                if (result == null)
                {
                    return Ok(new List<ProductVariant>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductVariant>> GetProductVariantById(int id)
        {
            try
            {
                var result = await productVariantRepository.GetProductVariantById(id);
                if (result == null)
                {
                    return NotFound("Product variant not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByProductId/{productId}")]
        public async Task<ActionResult<List<ProductVariant>>> GetProductVariantsByProductId(int productId)
        {
            try
            {
                var result = await productVariantRepository.GetProductVariantsByProductId(productId);
                if (result == null)
                {
                    return Ok(new List<ProductVariant>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProductVariant>> AddProductVariant([FromBody] ProductVariant variant)
        {
            try
            {
                if (variant == null)
                {
                    return BadRequest("Product variant data is required");
                }

                var result = await productVariantRepository.AddProductVariant(variant);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add product variant");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ProductVariant>> UpdateProductVariant([FromBody] ProductVariantUpdateDTO variantDTO)
        {
            try
            {
                if (variantDTO == null)
                {
                    return BadRequest("Product variant data is required");
                }

                var variant = new ProductVariant
                {
                    Id = variantDTO.Id,
                    ProductId = variantDTO.ProductId,
                    ColorId = variantDTO.ColorId,
                    SizeId = variantDTO.SizeId,
                    SKU = variantDTO.SKU,
                    StockQuantity = variantDTO.StockQuantity,
                    Price = variantDTO.Price,
                    ArrivalTime = variantDTO.ArrivalTime,
                    IsActive = variantDTO.IsActive,
                    Status = variantDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = variantDTO.Delete,
                    DeleteAt = variantDTO.Delete == true ? DateTime.Now : null
                };

                var result = await productVariantRepository.UpdateProductVariant(variant);
                if (result == null)
                {
                    return NotFound("Product variant not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ProductVariant>> DeleteProductVariant(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await productVariantRepository.DeleteProductVariant(id, updateBy);
                if (result == null)
                {
                    return NotFound("Product variant not found");
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

