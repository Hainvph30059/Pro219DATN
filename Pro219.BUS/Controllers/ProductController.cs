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
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                var result = await productRepository.GetAllProducts();
                if (result == null)
                {
                    return Ok(new List<Product>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var result = await productRepository.GetProductById(id);
                if (result == null)
                {
                    return NotFound("Product not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is required");
                }

                var result = await productRepository.AddProduct(product);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add product");
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
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] ProductUpdateDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return BadRequest("Product data is required");
                }

                var product = new Product
                {
                    Id = productDTO.Id,
                    CategoryId = productDTO.CategoryId,
                    BrandId = productDTO.BrandId,
                    SaleId = productDTO.SaleId,
                    Name = productDTO.Name,
                    Description = productDTO.Description,
                    BasePrice = productDTO.BasePrice,
                    Status = productDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = productDTO.Delete,
                    DeleteAt = productDTO.Delete == true ? DateTime.Now : null
                };

                var result = await productRepository.UpdateProduct(product);
                if (result == null)
                {
                    return NotFound("Product not found or update failed");
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
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await productRepository.DeleteProduct(id, updateBy);
                if (result == null)
                {
                    return NotFound("Product not found");
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

