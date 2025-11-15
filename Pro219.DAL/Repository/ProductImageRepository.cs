using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class ProductImageRepository
    {
        ClothesDbContext _context;

        public ProductImageRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<ProductImage>> GetAllProductImages()
        {
            try
            {
                var images = await _context.ProductImages
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return images;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProductImage> GetProductImageById(int id)
        {
            try
            {
                var image = await _context.ProductImages.FindAsync(id);
                if (image != null && image.Delete == true)
                    return null;
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ProductImage>> GetProductImagesByProductId(int productId)
        {
            try
            {
                var images = await _context.ProductImages
                    .Where(x => x.ProductId == productId && x.Delete != true)
                    .ToListAsync();
                return images;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ProductImage>> GetProductImagesByVariantId(int variantId)
        {
            try
            {
                var images = await _context.ProductImages
                    .Where(x => x.ProductVariantId == variantId && x.Delete != true)
                    .ToListAsync();
                return images;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProductImage> AddProductImage(ProductImage image)
        {
            try
            {
                image.CreateAt = DateTime.Now;
                image.Delete = false;
                var addedImage = _context.ProductImages.Add(image).Entity;
                await _context.SaveChangesAsync();
                return addedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProductImage> UpdateProductImage(ProductImage image)
        {
            try
            {
                var existingImage = await _context.ProductImages.FindAsync(image.Id);

                if (existingImage == null || existingImage.Delete == true) return null;

                existingImage.ProductId = image.ProductId;
                existingImage.ProductVariantId = image.ProductVariantId;
                existingImage.ImageUrl = image.ImageUrl;
                existingImage.IsMain = image.IsMain;
                existingImage.Status = image.Status;
                existingImage.UpdateBy = image.UpdateBy;
                existingImage.UpdateByString = image.UpdateByString;
                existingImage.UpdateAt = DateTime.Now;

                var updatedImage = _context.ProductImages.Update(existingImage).Entity;
                await _context.SaveChangesAsync();
                return updatedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProductImage> DeleteProductImage(int id, int? updateBy = null, string updateByString = null)
        {
            try
            {
                var image = await _context.ProductImages.FindAsync(id);

                if (image == null) return null;

                image.Delete = true;
                image.UpdateAt = DateTime.Now;
                if (updateBy.HasValue)
                {
                    image.UpdateBy = updateBy;
                }
                if (!string.IsNullOrEmpty(updateByString))
                {
                    image.UpdateByString = updateByString;
                }

                var updatedImage = _context.ProductImages.Update(image).Entity;
                await _context.SaveChangesAsync();
                return updatedImage;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

