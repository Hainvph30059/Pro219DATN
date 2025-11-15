using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class CartRepository
    {
        ClothesDbContext _context;

        public CartRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            try
            {
                var carts = await _context.Carts
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return carts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> GetCartById(int id)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart != null && cart.Delete == true)
                    return null;
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> GetCartByCustomerId(int customerId)
        {
            try
            {
                var cart = await _context.Carts
                    .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Delete != true);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            try
            {
                cart.CreateAt = DateTime.Now;
                cart.UpdateAt = DateTime.Now;
                cart.Delete = false;
                var addedCart = _context.Carts.Add(cart).Entity;
                await _context.SaveChangesAsync();
                return addedCart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            try
            {
                var existingCart = await _context.Carts.FindAsync(cart.Id);

                if (existingCart == null || existingCart.Delete == true) return null;

                existingCart.CustomerId = cart.CustomerId;
                existingCart.SessionId = cart.SessionId;
                existingCart.Status = cart.Status;
                existingCart.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(cart.UpdateBy))
                {
                    existingCart.UpdateBy = cart.UpdateBy;
                }

                var updatedCart = _context.Carts.Update(existingCart).Entity;
                await _context.SaveChangesAsync();
                return updatedCart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> DeleteCart(int id, string updateBy = null)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(id);

                if (cart == null) return null;

                cart.Delete = true;
                cart.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(updateBy))
                {
                    cart.UpdateBy = updateBy;
                }

                var updatedCart = _context.Carts.Update(cart).Entity;
                await _context.SaveChangesAsync();
                return updatedCart;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

