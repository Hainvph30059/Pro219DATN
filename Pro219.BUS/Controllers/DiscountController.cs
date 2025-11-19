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
    [Route("DiscountCode")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        DiscountRepository discountCodeRepository;

        public DiscountController()
        {
            discountCodeRepository = new DiscountRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Discount>>> GetAllDiscountCodes()
        {
            try
            {
                var result = await discountCodeRepository.GetAllDiscountCodes();
                if (result == null)
                {
                    return Ok(new List<Discount>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Discount>> GetDiscountCodeById(int id)
        {
            try
            {
                var result = await discountCodeRepository.GetDiscountCodeById(id);
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

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<Discount>> GetDiscountCodeByCode(string code)
        {
            try
            {
                var result = await discountCodeRepository.GetDiscountCodeByCode(code);
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

        [HttpPost("Add")]
        public async Task<ActionResult<Discount>> AddDiscountCode([FromBody] Discount discountCode)
        {
            try
            {
                if (discountCode == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await discountCodeRepository.AddDiscountCode(discountCode);
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
        public async Task<ActionResult<Discount>> UpdateDiscountCode([FromBody] DiscountCodeUpdateDTO discountCodeDTO)
        {
            try
            {
                if (discountCodeDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var discountCode = new Discount
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
        public async Task<ActionResult<Discount>> DeleteDiscountCode(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await discountCodeRepository.DeleteDiscountCode(id, updateBy);
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



