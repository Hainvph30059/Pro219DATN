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
    [Route("DiscountCode")]
    [ApiController]
    public class DiscountCodeController : ControllerBase
    {
        DiscountCodeRepository discountCodeRepository;

        public DiscountCodeController()
        {
            discountCodeRepository = new DiscountCodeRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<DiscountCode>>> GetAllDiscountCodes()
        {
            try
            {
                var result = await discountCodeRepository.GetAllDiscountCodes();
                if (result == null)
                {
                    return Ok(new List<DiscountCode>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<DiscountCode>> GetDiscountCodeById(int id)
        {
            try
            {
                var result = await discountCodeRepository.GetDiscountCodeById(id);
                if (result == null)
                {
                    return NotFound("Discount code not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<DiscountCode>> GetDiscountCodeByCode(string code)
        {
            try
            {
                var result = await discountCodeRepository.GetDiscountCodeByCode(code);
                if (result == null)
                {
                    return NotFound("Discount code not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<DiscountCode>> AddDiscountCode([FromBody] DiscountCode discountCode)
        {
            try
            {
                if (discountCode == null)
                {
                    return BadRequest("Discount code data is required");
                }

                var result = await discountCodeRepository.AddDiscountCode(discountCode);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add discount code");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<DiscountCode>> UpdateDiscountCode([FromBody] DiscountCodeUpdateDTO discountCodeDTO)
        {
            try
            {
                if (discountCodeDTO == null)
                {
                    return BadRequest("Discount code data is required");
                }

                var discountCode = new DiscountCode
                {
                    DiscountId = discountCodeDTO.DiscountId,
                    Code = discountCodeDTO.Code,
                    DiscountType = discountCodeDTO.DiscountType,
                    Value = discountCodeDTO.Value,
                    MinOrderValue = discountCodeDTO.MinOrderValue,
                    StartDate = discountCodeDTO.StartDate,
                    EndDate = discountCodeDTO.EndDate,
                    IsActive = discountCodeDTO.IsActive,
                    Status = discountCodeDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = discountCodeDTO.Delete,
                    DeleteAt = discountCodeDTO.Delete == true ? DateTime.Now : null
                };

                var result = await discountCodeRepository.UpdateDiscountCode(discountCode);
                if (result == null)
                {
                    return NotFound("Discount code not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DiscountCode>> DeleteDiscountCode(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await discountCodeRepository.DeleteDiscountCode(id, null, updateBy);
                if (result == null)
                {
                    return NotFound("Discount code not found");
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

