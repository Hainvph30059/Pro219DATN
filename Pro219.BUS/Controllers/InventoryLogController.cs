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
    [Route("InventoryLog")]
    [ApiController]
    public class InventoryLogController : ControllerBase
    {
        InventoryLogRepository inventoryLogRepository;

        public InventoryLogController()
        {
            inventoryLogRepository = new InventoryLogRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<InventoryLog>>> GetAllInventoryLogs()
        {
            try
            {
                var result = await inventoryLogRepository.GetAllInventoryLogs();
                if (result == null)
                {
                    return Ok(new List<InventoryLog>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<InventoryLog>> GetInventoryLogById(int id)
        {
            try
            {
                var result = await inventoryLogRepository.GetInventoryLogById(id);
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

        [HttpGet("GetByVariantId/{variantId}")]
        public async Task<ActionResult<List<InventoryLog>>> GetInventoryLogsByVariantId(int variantId)
        {
            try
            {
                var result = await inventoryLogRepository.GetInventoryLogsByVariantId(variantId);
                if (result == null)
                {
                    return Ok(new List<InventoryLog>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<InventoryLog>> AddInventoryLog([FromBody] InventoryLog log)
        {
            try
            {
                if (log == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await inventoryLogRepository.AddInventoryLog(log);
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
        public async Task<ActionResult<InventoryLog>> UpdateInventoryLog([FromBody] InventoryLogUpdateDTO logDTO)
        {
            try
            {
                if (logDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var log = new InventoryLog
                {
                    InventoryLogId = logDTO.InventoryLogId,
                    VariantId = logDTO.VariantId,
                    ChangeQuantity = logDTO.ChangeQuantity,
                    Reason = logDTO.Reason,
                    Status = logDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = logDTO.Delete,
                    DeleteAt = logDTO.Delete == true ? DateTime.Now : null
                };

                var result = await inventoryLogRepository.UpdateInventoryLog(log);
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
        public async Task<ActionResult<InventoryLog>> DeleteInventoryLog(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await inventoryLogRepository.DeleteInventoryLog(id, updateBy);
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



