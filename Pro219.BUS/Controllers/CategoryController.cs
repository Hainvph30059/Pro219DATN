using Microsoft.AspNetCore.Mvc;
using Pro219.API.DTOs;
using Pro219.DAL.Models;
using Pro219.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pro219.API.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepository categoryRepository;

        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var result = await categoryRepository.GetAllCategories();
                if (result == null)
                {
                    return Ok(new List<Category>());
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is required");
                }

                var result = await categoryRepository.AddCategory(category);
                if (result == null)
                {
                    return StatusCode(500, "Failed to add category");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("Update")]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category data is required");
                }

                var result = await categoryRepository.UpdateCategory(category);
                if (result == null)
                {
                    return NotFound("Category not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllParentCategories")]
        public async Task<ActionResult<List<Category>>> GetAllParentCategories()
        {
            try
            {
                var result = await categoryRepository.GetAllParentCategories();
                if (result == null)
                {
                    return Ok(new List<Category>());
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetSubCategoriesByParentId/{parentId}")]
        public async Task<ActionResult<List<Category>>> GetSubCategoriesByParentId(int parentId)
        {
            try
            {
                var result = await categoryRepository.GetAllSubCategoriesByParentId(parentId);
                if (result == null)
                {
                    return Ok(new List<Category>());
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var result = await categoryRepository.GetCategoryById(id);
                if (result == null)
                {
                    return NotFound("Category not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllCategoriesWithSubCategories")]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategoriesWithSubCategories()
        {
            try
            {
                var allCategories = await categoryRepository.GetAllCategories();
                if (allCategories == null || allCategories.Count == 0)
                {
                    return Ok(new List<CategoryDTO>());
                }

                var parentCategories = allCategories.Where(c => c.ParentCategoryId == null).ToList();
                var categoryDTOs = parentCategories.Select(c => ConvertToCategoryDTO(c, allCategories)).ToList();

                return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private CategoryDTO ConvertToCategoryDTO(Category category, List<Category> allCategories)
        {
            var subCategories = allCategories.Where(c => c.ParentCategoryId == category.Id).ToList();
            var hasSubCategories = subCategories.Any();

            return new CategoryDTO
            {
                Id = category.Id,
                ParentCategoryId = category.ParentCategoryId,
                UpdateBy = category.UpdateBy,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,
                isParent = hasSubCategories,
                SubCategory = hasSubCategories 
                    ? subCategories.Select(c => ConvertToCategoryDTO(c, allCategories)).ToList() 
                    : null
            };
        }
    }
}
