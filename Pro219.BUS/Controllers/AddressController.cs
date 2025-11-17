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
    [Route("Address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        AddressRepository addressRepository;

        public AddressController()
        {
            addressRepository = new AddressRepository();
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Address>> AddAddress([FromBody] AddressDTO addressDTO)
        {
            try
            {
                if (addressDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var address = new Address
                {
                    CustomerId = addressDTO.CustomerId,
                    FullName = addressDTO.FullName,
                    Phone = addressDTO.Phone,
                    Street = addressDTO.Street,
                    City = addressDTO.City,
                    District = addressDTO.District,
                    OtherInfo = addressDTO.OtherInfo,
                    IsDefault = addressDTO.IsDefault,
                    Status = "1"
                };

                var result = await addressRepository.AddAddress(address);
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
        public async Task<ActionResult<Address>> UpdateAddress([FromBody] AddressUpdateDTO addressDTO)
        {
            try
            {
                if (addressDTO == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var address = new Address
                {
                    Id = addressDTO.Id,
                    CustomerId = addressDTO.CustomerId,
                    FullName = addressDTO.FullName,
                    Phone = addressDTO.Phone,
                    Street = addressDTO.Street,
                    City = addressDTO.City,
                    District = addressDTO.District,
                    OtherInfo = addressDTO.OtherInfo,
                    IsDefault = addressDTO.IsDefault,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = addressDTO.Delete,
                    DeleteAt = addressDTO.Delete == true ? DateTime.Now : null
                };

                var result = await addressRepository.UpdateAddress(address);
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

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<ActionResult<List<Address>>> GetByCustomerId(int customerId)
        {
            try
            {
                var result = await addressRepository.GetByCustomerId(customerId);
                if (result == null)
                {
                    return Ok(new List<Address>());
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Address>> GetById(int id)
        {
            try
            {
                var result = await addressRepository.GetById(id);
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            try
            {
                var result = await addressRepository.GetAllAddresses();
                if (result == null)
                {
                    return Ok(new List<Address>());
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
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await addressRepository.DeleteAddress(id, updateBy);
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


