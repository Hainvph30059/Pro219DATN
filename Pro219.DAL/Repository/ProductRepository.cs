using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;

namespace Pro219.DAL.Repository
{
    public class ProductRepository
    {
        private readonly ClothesDbContext _context;

        public ProductRepository(ClothesDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<Product> CreateAsync(Product product)
        {
            product.CreatedAt = DateTime.Now;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Product Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        // READ - Get All
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
                .ToListAsync();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
                .ToList();
        }

        // READ - Get By ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.Customer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Product? GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.Customer)
                .FirstOrDefault(p => p.Id == id);
        }

        // READ - Get By Category
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        // READ - Get By Brand
        public async Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();
        }

        public IEnumerable<Product> GetByBrandId(int brandId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.BrandId == brandId)
                .ToList();
        }

        // READ - Get By Status
        public async Task<IEnumerable<Product>> GetByStatusAsync(string status)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.Status == status)
                .ToListAsync();
        }

        public IEnumerable<Product> GetByStatus(string status)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.Status == status)
                .ToList();
        }

        // READ - Search by Name
        public async Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public IEnumerable<Product> SearchByName(string searchTerm)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.Name.Contains(searchTerm))
                .ToList();
        }

        // READ - Get Products on Sale
        public async Task<IEnumerable<Product>> GetProductsOnSaleAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.SaleId != null && p.Sale != null && p.Sale.IsActive && 
                            p.Sale.StartDate <= DateTime.Now && p.Sale.EndDate >= DateTime.Now)
                .ToListAsync();
        }

        public IEnumerable<Product> GetProductsOnSale()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Sale)
                .Include(p => p.ProductImages)
                .Where(p => p.SaleId != null && p.Sale != null && p.Sale.IsActive && 
                            p.Sale.StartDate <= DateTime.Now && p.Sale.EndDate >= DateTime.Now)
                .ToList();
        }

        // UPDATE
        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.BasePrice = product.BasePrice;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.BrandId = product.BrandId;
            existingProduct.SaleId = product.SaleId;
            existingProduct.Status = product.Status;
            existingProduct.UpdateBy = product.UpdateBy;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public Product? Update(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.BasePrice = product.BasePrice;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.BrandId = product.BrandId;
            existingProduct.SaleId = product.SaleId;
            existingProduct.Status = product.Status;
            existingProduct.UpdateBy = product.UpdateBy;

            _context.SaveChanges();
            return existingProduct;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        // DELETE - Soft Delete (Update Status)
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            product.Status = "Deleted";
            await _context.SaveChangesAsync();
            return true;
        }

        public bool SoftDelete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;

            product.Status = "Deleted";
            _context.SaveChanges();
            return true;
        }

        // Check if Product exists
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        // Get Count
        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public int GetCount()
        {
            return _context.Products.Count();
        }

        // Get Count by Status
        public async Task<int> GetCountByStatusAsync(string status)
        {
            return await _context.Products.CountAsync(p => p.Status == status);
        }

        public int GetCountByStatus(string status)
        {
            return _context.Products.Count(p => p.Status == status);
        }
    }
}
