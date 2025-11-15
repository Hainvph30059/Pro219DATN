using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class SaleRepository
    {
        ClothesDbContext _context;

        public SaleRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<Sale>> GetAllSales()
        {
            try
            {
                var sales = await _context.Sales
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return sales;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Sale> GetSaleById(int id)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale != null && sale.Delete == true)
                    return null;
                return sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Sale> AddSale(Sale sale)
        {
            try
            {
                sale.CreateAt = DateTime.Now;
                sale.Delete = false;
                var addedSale = _context.Sales.Add(sale).Entity;
                await _context.SaveChangesAsync();
                return addedSale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            try
            {
                var existingSale = await _context.Sales.FindAsync(sale.Id);

                if (existingSale == null || existingSale.Delete == true) return null;

                existingSale.Name = sale.Name;
                existingSale.Description = sale.Description;
                existingSale.Type = sale.Type;
                existingSale.SaleValue = sale.SaleValue;
                existingSale.StartDate = sale.StartDate;
                existingSale.EndDate = sale.EndDate;
                existingSale.IsActive = sale.IsActive;
                existingSale.Status = sale.Status;
                existingSale.UpdateBy = sale.UpdateBy;
                existingSale.UpdateByString = sale.UpdateByString;
                existingSale.UpdateAt = DateTime.Now;

                var updatedSale = _context.Sales.Update(existingSale).Entity;
                await _context.SaveChangesAsync();
                return updatedSale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Sale> DeleteSale(int id, int? updateBy = null, string updateByString = null)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);

                if (sale == null) return null;

                sale.Delete = true;
                sale.UpdateAt = DateTime.Now;
                if (updateBy.HasValue)
                {
                    sale.UpdateBy = updateBy;
                }
                if (!string.IsNullOrEmpty(updateByString))
                {
                    sale.UpdateByString = updateByString;
                }

                var updatedSale = _context.Sales.Update(sale).Entity;
                await _context.SaveChangesAsync();
                return updatedSale;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

