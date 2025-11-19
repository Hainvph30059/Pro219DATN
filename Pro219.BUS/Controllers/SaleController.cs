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
    [Route("Sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        SaleRepository saleRepository;

        public SaleController()
        {
            saleRepository = new SaleRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<SaleOff>>> GetAllSales()
        {
            try
            {
                var result = await saleRepository.GetAllSales();
                if (result == null)
                {
                    return Ok(new List<SaleOff>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<SaleOff>> GetSaleById(int id)
        {
            try
            {
                var result = await saleRepository.GetSaleById(id);
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
        public async Task<ActionResult<SaleOff>> AddSale([FromBody] SaleOff sale)
        {
            try
            {
                if (sale == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await saleRepository.AddSale(sale);
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
        public async Task<ActionResult<SaleOff>> UpdateSale([FromBody] SaleUpdateDTO saleDTO)
        {
            try
            {
                if (saleDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var sale = new SaleOff
                {
                    Id = saleDTO.Id,
                    Name = saleDTO.Name,
                    Description = saleDTO.Description,
                    Type = saleDTO.Type,
                    SaleValue = saleDTO.SaleValue,
                    StartDate = saleDTO.StartDate,
                    EndDate = saleDTO.EndDate,
                    IsActive = saleDTO.IsActive,
                    Status = saleDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = saleDTO.Delete,
                    DeleteAt = saleDTO.Delete == true ? DateTime.Now : null
                };

                var result = await saleRepository.UpdateSale(sale);
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
        public async Task<ActionResult<SaleOff>> DeleteSale(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await saleRepository.DeleteSale(id, updateBy);
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



