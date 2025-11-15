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
    [Route("ProductImage")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        ProductImageRepository productImageRepository;

        public ProductImageController()
        {
            productImageRepository = new ProductImageRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductImage>>> GetAllProductImages()
        {
            try
            {
                var result = await productImageRepository.GetAllProductImages();
                if (result == null)
                {
                    return Ok(new List<ProductImage>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImageById(int id)
        {
            try
            {
                var result = await productImageRepository.GetProductImageById(id);
                if (result == null)
                {
                    return NotFound("Product image not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByProductId/{productId}")]
        public async Task<ActionResult<List<ProductImage>>> GetProductImagesByProductId(int productId)
        {
            try
            {
                var result = await productImageRepository.GetProductImagesByProductId(productId);
                if (result == null)
                {
                    return Ok(new List<ProductImage>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByVariantId/{variantId}")]
        public async Task<ActionResult<List<ProductImage>>> GetProductImagesByVariantId(int variantId)
        {
            try
            {
                var result = await productImageRepository.GetProductImagesByVariantId(variantId);
                if (result == null)
                {
                    return Ok(new List<ProductImage>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProductImage>> AddProductImage([FromBody] ProductImage image)
        {
            try
            {
                if (image == null)
                {
                    return BadRequest("Product image data is required");
                }

                var result = await productImageRepository.AddProductImage(image);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add product image");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ProductImage>> UpdateProductImage([FromBody] ProductImageUpdateDTO imageDTO)
        {
            try
            {
                if (imageDTO == null)
                {
                    return BadRequest("Product image data is required");
                }

                var image = new ProductImage
                {
                    Id = imageDTO.Id,
                    ProductId = imageDTO.ProductId,
                    ProductVariantId = imageDTO.ProductVariantId,
                    ImageUrl = imageDTO.ImageUrl,
                    IsMain = imageDTO.IsMain,
                    Status = imageDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = imageDTO.Delete,
                    DeleteAt = imageDTO.Delete == true ? DateTime.Now : null
                };

                var result = await productImageRepository.UpdateProductImage(image);
                if (result == null)
                {
                    return NotFound("Product image not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await productImageRepository.DeleteProductImage(id, null, updateBy);
                if (result == null)
                {
                    return NotFound("Product image not found");
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

