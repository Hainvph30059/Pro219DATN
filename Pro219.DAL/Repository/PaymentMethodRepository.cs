using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class PaymentMethodRepository
    {
        ClothesDbContext _context;

        public PaymentMethodRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods()
        {
            try
            {
                var paymentMethods = await _context.PaymentMethods
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return paymentMethods;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaymentMethod> GetPaymentMethodById(int id)
        {
            try
            {
                var paymentMethod = await _context.PaymentMethods.FindAsync(id);
                if (paymentMethod != null && paymentMethod.Delete == true)
                    return null;
                return paymentMethod;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaymentMethod> AddPaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                paymentMethod.CreateAt = DateTime.Now;
                paymentMethod.Delete = false;
                var addedPaymentMethod = _context.PaymentMethods.Add(paymentMethod).Entity;
                await _context.SaveChangesAsync();
                return addedPaymentMethod;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaymentMethod> UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                var existingPaymentMethod = await _context.PaymentMethods.FindAsync(paymentMethod.Id);

                if (existingPaymentMethod == null || existingPaymentMethod.Delete == true) return null;

                existingPaymentMethod.Name = paymentMethod.Name;
                existingPaymentMethod.Description = paymentMethod.Description;
                existingPaymentMethod.IsActive = paymentMethod.IsActive;
                existingPaymentMethod.Status = paymentMethod.Status;
                existingPaymentMethod.UpdateBy = paymentMethod.UpdateBy;
                existingPaymentMethod.UpdateByString = paymentMethod.UpdateByString;
                existingPaymentMethod.UpdateAt = DateTime.Now;

                var updatedPaymentMethod = _context.PaymentMethods.Update(existingPaymentMethod).Entity;
                await _context.SaveChangesAsync();
                return updatedPaymentMethod;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaymentMethod> DeletePaymentMethod(int id, int? updateBy = null, string updateByString = null)
        {
            try
            {
                var paymentMethod = await _context.PaymentMethods.FindAsync(id);

                if (paymentMethod == null) return null;

                paymentMethod.Delete = true;
                paymentMethod.UpdateAt = DateTime.Now;
                if (updateBy.HasValue)
                {
                    paymentMethod.UpdateBy = updateBy;
                }
                if (!string.IsNullOrEmpty(updateByString))
                {
                    paymentMethod.UpdateByString = updateByString;
                }

                var updatedPaymentMethod = _context.PaymentMethods.Update(paymentMethod).Entity;
                await _context.SaveChangesAsync();
                return updatedPaymentMethod;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

