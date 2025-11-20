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

        public async Task<List<Product>> GetLatestProducts()
        {
            try
            {
                //SELECT TOP 8 p.Id, p.Name, p.BasePrice, p.CreatedAt FROM Product p 
                //join ProductVariant pv ON pv.ProductId = p.Id 
                //where p.[Delete] = 0 and p.Status = 1 
                //order by p.CreatedAt desc SELECT TOP 8 p.Id, p.Name, p.BasePrice, p.CreatedAt FROM Product p 
                //join ProductVariant pv ON pv.ProductId = p.Id 
                //where p.[Delete] = 0 and p.Status = 1 
                //order by p.CreatedAt desc 
                return await _context.Products
                    .Join(_context.ProductVariants,
                        p => p.Id,
                        pv => pv.ProductId,
                        (p, pv) => p)
                    .Where(p => p.Delete == false && p.Status == 1)
                    .OrderByDescending(p => p.CreatedAt)
                    .Distinct()
                    .Take(8)
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        BasePrice = p.BasePrice,
                        CreatedAt = p.CreatedAt
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<List<Product>> GetBestSellProduct()
        {
            try
            {
                //Select top 8 p.id, p.name, sum(oi.quantity) from Product p 
                //join ProductVariant pv on pv.ProductID = p.Id 
                //join OrderItem oi on oi.ProductVariantId = pv.Id 
                //join [order] o on o.OrderId = oi.OrderId 
                //where p.[Delete] = 0 and o.[Delete] = 0 and o.Status = 3 //hoàn thành
                //group by p.id,p.Name 
                //order by sum(oi.Quantity);Select top 8 p.id, p.name, sum(oi.quantity) from Product p 
                //join ProductVariant pv on pv.Id = p.Id 
                //join OrderItem oi on oi.ProductVariantId = pv.Id 
                //join [order] o on o.OrderId = oi.OrderId 
                //where p.[Delete] = 0 and o.[Delete] = 0 and o.Status = 3 //hoàn thành
                //group by p.id,p.Name 
                //order by sum(oi.Quantity);
                var query = from p in _context.Products
                            join pv in _context.ProductVariants on p.Id equals pv.ProductId
                            join oi in _context.OrderItems on pv.Id equals oi.ProductVariantId
                            join o in _context.Orders on oi.OrderId equals o.OrderId
                            where p.Delete == false
                                  && o.Delete == false
                                  && o.Status == 3
                            group oi by new { p.Id, p.Name, p.BasePrice, p.CreatedAt } into g
                            orderby g.Sum(x => x.Quantity) descending
                            select new Product 
                            {
                                Id = g.Key.Id,
                                Name = g.Key.Name,
                                BasePrice = g.Key.BasePrice,
                                CreatedAt = g.Key.CreatedAt
                            };

                return await query.Take(8).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }

        }

    } 
}
