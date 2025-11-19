using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class CartItemRepository
    {
        ClothesDbContext _context;

        public CartItemRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            try
            {
                var cartItems = await _context.CartItems
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return cartItems;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);
                if (cartItem != null && cartItem.Delete == true)
                    return null;
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CartItem>> GetCartItemsByCartId(int cartId)
        {
            try
            {
                var cartItems = await _context.CartItems
                    .Where(x => x.CartId == cartId && x.Delete != true)
                    .ToListAsync();
                return cartItems;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> AddCartItem(CartItem cartItem)
        {
            try
            {
                cartItem.CreateAt = DateTime.Now;
                cartItem.Delete = false;
                var addedCartItem = _context.CartItems.Add(cartItem).Entity;
                await _context.SaveChangesAsync();
                return addedCartItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            try
            {
                var existingCartItem = await _context.CartItems.FindAsync(cartItem.Id);

                if (existingCartItem == null || existingCartItem.Delete == true) return null;

                existingCartItem.CartId = cartItem.CartId;
                existingCartItem.VariantId = cartItem.VariantId;
                existingCartItem.Quantity = cartItem.Quantity;
                existingCartItem.UnitPrice = cartItem.UnitPrice;
                existingCartItem.Status = cartItem.Status;
                existingCartItem.UpdateBy = cartItem.UpdateBy;
                existingCartItem.UpdateAt = DateTime.Now;

                var updatedCartItem = _context.CartItems.Update(existingCartItem).Entity;
                await _context.SaveChangesAsync();
                return updatedCartItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> DeleteCartItem(int id, string updateBy = null)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);

                if (cartItem == null) return null;

                cartItem.Delete = true;
                cartItem.DeleteAt = DateTime.Now;
                cartItem.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(updateBy))
                {
                    cartItem.UpdateBy = updateBy;
                }

                var updatedCartItem = _context.CartItems.Update(cartItem).Entity;
                await _context.SaveChangesAsync();
                return updatedCartItem;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

