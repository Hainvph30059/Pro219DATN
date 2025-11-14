using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class CategoryRepository
    {
        ClothesDbContext _context;
        public CategoryRepository()
        {
            _context = new ClothesDbContext();

        }

        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> listCategory = new List<Category>();
            listCategory = _context.Categories.ToList();
            if (listCategory.Count > 0)
                return listCategory;
            return null;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return null;
            return category;
        }

        public async Task<List<Category>> GetAllParentCategories()
        {
            List<Category> listCategory = new List<Category>();
            listCategory = _context.Categories.Where(x => x.ParentCategoryId == null).ToList();
            if (listCategory.Count > 0)
                return listCategory;
            return null;
        }

        public async Task<Category> AddCategory(Category cate)
        {
            try
            {
                var addedCate = _context.Categories.Add(cate).Entity;
                await _context.SaveChangesAsync();
                return addedCate;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Category>> GetAllSubCategoriesByParentId(int parentId)
        {
            List<Category> listCategory = new List<Category>();
            listCategory = _context.Categories.Where(x => x.ParentCategoryId == parentId).ToList();
            if (listCategory.Count > 0)
                return listCategory;
            return null;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.Id);

                if (existingCategory == null) return null;

                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.Status = category.Status;
                existingCategory.ParentCategoryId = category.ParentCategoryId;
                existingCategory.UpdateBy = category.UpdateBy;

                var updatedCategory = _context.Categories.Update(existingCategory).Entity;
                await _context.SaveChangesAsync();
                return updatedCategory;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
