using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class ProductRepository
    {
        private readonly ClothesDbContext _context;

        public ProductRepository()
        {
            _context = new ClothesDbContext();
        }

        public ProductRepository(ClothesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return products;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null && product.Delete == true)
                    return null;
                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.Now;
                product.Delete = false;
                var addedProduct = _context.Products.Add(product).Entity;
                await _context.SaveChangesAsync();
                return addedProduct;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(product.Id);

                if (existingProduct == null || existingProduct.Delete == true) return null;

                existingProduct.CategoryId = product.CategoryId;
                existingProduct.BrandId = product.BrandId;
                existingProduct.SaleId = product.SaleId;
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.BasePrice = product.BasePrice;
                existingProduct.Status = product.Status;
                existingProduct.UpdateBy = product.UpdateBy;
                existingProduct.UpdateAt = DateTime.Now;

                var updatedProduct = _context.Products.Update(existingProduct).Entity;
                await _context.SaveChangesAsync();
                return updatedProduct;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Product> DeleteProduct(int id, string? updateBy = null)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return null;

                product.Delete = true;
                product.DeleteAt = DateTime.Now;
                product.UpdateAt = DateTime.Now;
                if (updateBy != null)
                {
                    product.UpdateBy = updateBy;
                }
               

                var updatedProduct = _context.Products.Update(product).Entity;
                await _context.SaveChangesAsync();
                return updatedProduct;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Product>> GetTopProductAsync()
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Color)
                .Where(p => p.Delete == false && p.Status == 1)
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopBestSellerAsync()
        {
            var topProductIds = await _context.OrderItems
                .Where(oi => oi.ProductVariant != null)
                .GroupBy(oi => oi.ProductVariant.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(8)
                .Select(x => x.ProductId)
                .ToListAsync();

            if (!topProductIds.Any())
                return new List<Product>();

            var products = await _context.Products
                .Where(p => topProductIds.Contains(p.Id))
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Color)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetProductDetailAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductImages)

                    .Include(p => p.ProductVariants.Where(v => v.Delete != true && v.IsActive == true))
                        .ThenInclude(v => v.Color)

                    .Include(p => p.ProductVariants.Where(v => v.Delete != true && v.IsActive == true))
                        .ThenInclude(v => v.Size)

                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product != null && product.Delete == true)
                    return null;

                return product;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
