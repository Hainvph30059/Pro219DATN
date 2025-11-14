using Microsoft.AspNetCore.Mvc;
using Pro219.API.DTOs;
using Pro219.DAL.Models;
using Pro219.DAL.Repository;
using System;
using System.Collections.Generic;
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
                    return BadRequest("Address data is required");
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
                    Status = "1" // Default status
                };

                var result = await addressRepository.AddAddress(address);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add address");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Address>> UpdateAddress([FromBody] Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest("Address data is required");
                }

                var result = await addressRepository.UpdateAddress(address);
                if (result == null)
                {
                    return NotFound("Address not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                    return NotFound("Address not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
