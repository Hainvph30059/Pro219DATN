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
    [Route("Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        ReviewRepository reviewRepository;

        public ReviewController()
        {
            reviewRepository = new ReviewRepository();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Review>>> GetAllReviews()
        {
            try
            {
                var result = await reviewRepository.GetAllReviews();
                if (result == null)
                {
                    return Ok(new List<Review>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            try
            {
                var result = await reviewRepository.GetReviewById(id);
                if (result == null)
                {
                    return NotFound("Review not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByProductId/{productId}")]
        public async Task<ActionResult<List<Review>>> GetReviewsByProductId(int productId)
        {
            try
            {
                var result = await reviewRepository.GetReviewsByProductId(productId);
                if (result == null)
                {
                    return Ok(new List<Review>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<ActionResult<List<Review>>> GetReviewsByCustomerId(int customerId)
        {
            try
            {
                var result = await reviewRepository.GetReviewsByCustomerId(customerId);
                if (result == null)
                {
                    return Ok(new List<Review>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review review)
        {
            try
            {
                if (review == null)
                {
                    return BadRequest("Review data is required");
                }

                var result = await reviewRepository.AddReview(review);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add review");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Review>> UpdateReview([FromBody] ReviewUpdateDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null)
                {
                    return BadRequest("Review data is required");
                }

                var review = new Review
                {
                    UniqueID = reviewDTO.UniqueID,
                    ProductId = reviewDTO.ProductId,
                    CustomerId = reviewDTO.CustomerId,
                    Title = reviewDTO.Title,
                    Content = reviewDTO.Content,
                    Overall = reviewDTO.Overall,
                    Status = reviewDTO.Status,
                    UpdateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UpdateAt = DateTime.Now,
                    Delete = reviewDTO.Delete,
                    DeleteAt = reviewDTO.Delete == true ? DateTime.Now : null
                };

                var result = await reviewRepository.UpdateReview(review);
                if (result == null)
                {
                    return NotFound("Review not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            try
            {
                var updateBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await reviewRepository.DeleteReview(id, updateBy);
                if (result == null)
                {
                    return NotFound("Review not found");
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

