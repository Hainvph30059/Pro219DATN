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
    [Route("PaymentMethod")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        PaymentMethodRepository paymentMethodRepository;

        public PaymentMethodController()
        {
            paymentMethodRepository = new PaymentMethodRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PaymentMethod>>> GetAllPaymentMethods()
        {
            try
            {
                var result = await paymentMethodRepository.GetAllPaymentMethods();
                if (result == null)
                {
                    return Ok(new List<PaymentMethod>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethodById(int id)
        {
            try
            {
                var result = await paymentMethodRepository.GetPaymentMethodById(id);
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
        public async Task<ActionResult<PaymentMethod>> AddPaymentMethod([FromBody] PaymentMethod paymentMethod)
        {
            try
            {
                if (paymentMethod == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await paymentMethodRepository.AddPaymentMethod(paymentMethod);
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
        public async Task<ActionResult<PaymentMethod>> UpdatePaymentMethod([FromBody] PaymentMethodUpdateDTO paymentMethodDTO)
        {
            try
            {
                if (paymentMethodDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var paymentMethod = new PaymentMethod
                {
                    Id = paymentMethodDTO.Id,
                    Name = paymentMethodDTO.Name,
                    Description = paymentMethodDTO.Description,
                    IsActive = paymentMethodDTO.IsActive,
                    Status = paymentMethodDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = paymentMethodDTO.Delete,
                    DeleteAt = paymentMethodDTO.Delete == true ? DateTime.Now : null
                };

                var result = await paymentMethodRepository.UpdatePaymentMethod(paymentMethod);
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
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await paymentMethodRepository.DeletePaymentMethod(id, updateBy);
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



