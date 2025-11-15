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
        public async Task<ActionResult<List<Sale>>> GetAllSales()
        {
            try
            {
                var result = await saleRepository.GetAllSales();
                if (result == null)
                {
                    return Ok(new List<Sale>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sale>> GetSaleById(int id)
        {
            try
            {
                var result = await saleRepository.GetSaleById(id);
                if (result == null)
                {
                    return NotFound("Sale not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Sale>> AddSale([FromBody] Sale sale)
        {
            try
            {
                if (sale == null)
                {
                    return BadRequest("Sale data is required");
                }

                var result = await saleRepository.AddSale(sale);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add sale");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Sale>> UpdateSale([FromBody] SaleUpdateDTO saleDTO)
        {
            try
            {
                if (saleDTO == null)
                {
                    return BadRequest("Sale data is required");
                }

                var sale = new Sale
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
                    return NotFound("Sale not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Sale>> DeleteSale(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await saleRepository.DeleteSale(id, null, updateBy);
                if (result == null)
                {
                    return NotFound("Sale not found");
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

