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

        public async Task<ProductImage> DeleteProductImage(int id, string? updateBy = null)
        {
            try
            {
                var image = await _context.ProductImages.FindAsync(id);

                if (image == null) return null;

                image.Delete = true;
                image.DeleteAt = DateTime.Now;
                image.UpdateAt = DateTime.Now;
                if (updateBy != null)
                {
                    image.UpdateBy = updateBy;
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

        public async Task<List<ProductImage>> AddRangeProductImages(List<ProductImage> images)
        {
            try
            {
                foreach (var image in images)
                {
                    image.CreateAt = DateTime.Now;
                    image.Delete = false;
                }

                _context.ProductImages.AddRange(images);

                await _context.SaveChangesAsync();

                return images;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<ProductImage>> DeleteRangeProductImages(List<int> ids, string? updateBy = null)
        {
            try
            {
                var imagesToDelete = await _context.ProductImages
                    .Where(x => ids.Contains(x.Id) && x.Delete != true)
                    .ToListAsync();

                if (!imagesToDelete.Any()) return new List<ProductImage>();

                DateTime now = DateTime.Now;

                foreach (var image in imagesToDelete)
                {
                    image.Delete = true;
                    image.DeleteAt = now;
                    image.UpdateAt = now;
                    if (updateBy != null)
                    {
                        image.UpdateBy = updateBy;
                    }
                }

                _context.ProductImages.UpdateRange(imagesToDelete);
                await _context.SaveChangesAsync();

                return imagesToDelete;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

