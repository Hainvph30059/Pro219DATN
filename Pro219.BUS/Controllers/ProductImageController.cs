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
                return StatusCode(500, Constant.ErrorCode.OtherError);
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
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
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
                return StatusCode(500, Constant.ErrorCode.OtherError);
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
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ProductImage>> AddProductImage([FromBody] ProductImage image)
        {
            try
            {
                if (image == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await productImageRepository.AddProductImage(image);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<ActionResult<ProductImage>> UpdateProductImage([FromBody] ProductImageUpdateDTO imageDTO)
        {
            try
            {
                if (imageDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
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
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await productImageRepository.DeleteProductImage(id, updateBy);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}



