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
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("ApplyDiscountCodeValue")]
        public async Task<ActionResult<decimal>> ApplyDiscountCodeValue(string code, decimal totalAmount)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

                if (userIdClaim == null)
                {
                    return BadRequest("Chỉ áp dụng cho khách hàng đã đăng nhập");
                }

                var discountCode = await discountCodeRepository.GetDiscountCodeByCode(code);
                if (discountCode == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }

                int userTimeUsed = await discountCodeRepository.GetUserTimeUsed(code, int.Parse(userIdClaim));

                if (discountCode.IsReusable == false && userTimeUsed >= 1)
                {
                    return BadRequest("Mã giảm giá không thể sử dụng lại");
                }
                else if (userTimeUsed >= discountCode.MaxUsage)
                {
                    return BadRequest("Mã giảm giá đã hết lượt sử dụng");
                }

                if (discountCode.StartDate > DateTime.Now)
                {
                    return BadRequest("Mã giảm giá đã hết hạn");
                }
                if (discountCode.EndDate < DateTime.Now)
                {
                    return BadRequest("Mã giảm giá đã hết hạn");
                }
                if (discountCode.IsActive == false)
                {
                    return BadRequest("Mã giảm giá đã hết hạn");
                }
                if (discountCode.MaxUsage != null && discountCode.UsageCount >= discountCode.MaxUsage)
                {
                    return BadRequest("Mã giảm giá đã hết lượt sử dụng");

                }
                if (discountCode.MinOrderValue != null && totalAmount < discountCode.MinOrderValue)
                {
                    return BadRequest("Đơn hàng không đủ giá trị để sử dụng mã giảm giá");
                }
                if (discountCode.MaxDiscountAmount != null && discountCode.Value > discountCode.MaxDiscountAmount)
                {
                    return BadRequest("Mã giảm giá vượt quá giá trị giảm giá tối đa");
                }
                if (discountCode.Status == 1) // Percent
                {
                    return Ok(totalAmount * (discountCode.Value / 100));
                }
                else if (discountCode.Status == 2) // Fixed Amount
                {
                    return Ok(discountCode.Value);
                }
                else
                {
                    return BadRequest("Mã giảm giá không hợp lệ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
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
        public async Task<ActionResult<DiscountCode>> GetDiscountCodeByCode(string code)
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
        public async Task<ActionResult<DiscountCode>> AddDiscountCode([FromBody] DiscountCode discountCode)
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
        public async Task<ActionResult<DiscountCode>> UpdateDiscountCode([FromBody] DiscountCodeUpdateDTO discountCodeDTO)
        {
            try
            {
                if (discountCodeDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
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
        public async Task<ActionResult<DiscountCode>> DeleteDiscountCode(int id)
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



