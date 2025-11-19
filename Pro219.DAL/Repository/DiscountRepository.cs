using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class DiscountRepository
    {
        ClothesDbContext _context;

        public DiscountRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<Discount>> GetAllDiscountCodes()
        {
            try
            {
                var discountCodes = await _context.DiscountCodes
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return discountCodes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> GetDiscountCodeById(int id)
        {
            try
            {
                var discountCode = await _context.DiscountCodes.FindAsync(id);
                if (discountCode != null && discountCode.Delete == true)
                    return null;
                return discountCode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> GetDiscountCodeByCode(string code)
        {
            try
            {
                var discountCode = await _context.DiscountCodes
                    .FirstOrDefaultAsync(x => x.Code == code && x.Delete != true);
                return discountCode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> AddDiscountCode(Discount discountCode)
        {
            try
            {
                discountCode.CreateAt = DateTime.Now;
                discountCode.Delete = false;
                var addedDiscountCode = _context.DiscountCodes.Add(discountCode).Entity;
                await _context.SaveChangesAsync();
                return addedDiscountCode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> UpdateDiscountCode(Discount discountCode)
        {
            try
            {
                var existingDiscountCode = await _context.DiscountCodes.FindAsync(discountCode.DiscountId);

                if (existingDiscountCode == null || existingDiscountCode.Delete == true) return null;

                existingDiscountCode.Code = discountCode.Code;
                existingDiscountCode.DiscountType = discountCode.DiscountType;
                existingDiscountCode.Value = discountCode.Value;
                existingDiscountCode.MinOrderValue = discountCode.MinOrderValue;
                existingDiscountCode.StartDate = discountCode.StartDate;
                existingDiscountCode.EndDate = discountCode.EndDate;
                existingDiscountCode.IsActive = discountCode.IsActive;
                existingDiscountCode.Status = discountCode.Status;
                existingDiscountCode.UpdateBy = discountCode.UpdateBy;
                existingDiscountCode.UpdateBy = discountCode.UpdateBy;
                existingDiscountCode.UpdateAt = DateTime.Now;

                var updatedDiscountCode = _context.DiscountCodes.Update(existingDiscountCode).Entity;
                await _context.SaveChangesAsync();
                return updatedDiscountCode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> DeleteDiscountCode(int id, string? updateBy = null)
        {
            try
            {
                var discountCode = await _context.DiscountCodes.FindAsync(id);

                if (discountCode == null) return null;

                discountCode.Delete = true;
                discountCode.DeleteAt = DateTime.Now;
                discountCode.UpdateAt = DateTime.Now;
                if (updateBy!=null)
                {
                    discountCode.UpdateBy = updateBy;
                }
              

                var updatedDiscountCode = _context.DiscountCodes.Update(discountCode).Entity;
                await _context.SaveChangesAsync();
                return updatedDiscountCode;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

