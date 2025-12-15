using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class ReviewRepository
    {
        ClothesDbContext _context;

        public ReviewRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<Review>> GetAllReviews()
        {
            try
            {
                var reviews = await _context.Reviews
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return reviews;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> GetReviewById(int id)
        {
            try
            {
                var review = await _context.Reviews.FindAsync(id);
                if (review != null && review.Delete == true)
                    return null;
                return review;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Review>> GetReviewsByProductId(int productId)
        {
            try
            {
                var reviews = await _context.Reviews
                    .Where(x => x.ProductId == productId && x.Delete != true)
                    .ToListAsync();
                return reviews;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Review>> GetReviewsByCustomerId(int customerId)
        {
            try
            {
                var reviews = await _context.Reviews
                    .Where(x => x.CustomerId == customerId && x.Delete != true)
                    .ToListAsync();
                return reviews;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> AddReview(Review review)
        {
            try
            {
                review.CreatedAt = DateTime.Now;
                review.Delete = false;
                var addedReview = _context.Reviews.Add(review).Entity;

                var orderItem = await _context.OrderItems.FindAsync(review.OrderItemId);
                if (orderItem != null)
                {
                    orderItem.IsReviewed = true;
                    orderItem.UpdateAt = DateTime.Now;
                    if (!string.IsNullOrEmpty(review.UpdateBy))
                    {
                        orderItem.UpdateBy = review.UpdateBy;
                    }
                }

                await _context.SaveChangesAsync();
                return addedReview;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> UpdateReview(Review review)
        {
            try
            {
                var existingReview = await _context.Reviews.FindAsync(review.Id);

                if (existingReview == null || existingReview.Delete == true) return null;

                existingReview.ProductId = review.ProductId;
                existingReview.CustomerId = review.CustomerId;
                var previousOrderItemId = existingReview.OrderItemId;
                existingReview.OrderItemId = review.OrderItemId;
                existingReview.Title = review.Title;
                existingReview.Content = review.Content;
                existingReview.Overall = review.Overall;
                existingReview.Status = review.Status;
                existingReview.UpdateBy = review.UpdateBy;
                existingReview.UpdateAt = DateTime.Now;
                var newOrderItem = await _context.OrderItems.FindAsync(review.OrderItemId);
                if (newOrderItem != null)
                {
                    newOrderItem.IsReviewed = true;
                    newOrderItem.UpdateAt = DateTime.Now;
                    if (!string.IsNullOrEmpty(review.UpdateBy))
                    {
                        newOrderItem.UpdateBy = review.UpdateBy;
                    }
                }

                if (previousOrderItemId != review.OrderItemId)
                {
                    var previousOrderItem = await _context.OrderItems.FindAsync(previousOrderItemId);
                    if (previousOrderItem != null)
                    {
                        previousOrderItem.IsReviewed = false;
                        previousOrderItem.UpdateAt = DateTime.Now;
                        if (!string.IsNullOrEmpty(review.UpdateBy))
                        {
                            previousOrderItem.UpdateBy = review.UpdateBy;
                        }
                    }
                }

                var updatedReview = _context.Reviews.Update(existingReview).Entity;
                await _context.SaveChangesAsync();
                return updatedReview;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> DeleteReview(int id, string updateBy = null)
        {
            try
            {
                var review = await _context.Reviews.FindAsync(id);

                if (review == null) return null;

                review.Delete = true;
                review.DeleteAt = DateTime.Now;
                review.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(updateBy))
                {
                    review.UpdateBy = updateBy;
                }

                var orderItem = await _context.OrderItems.FindAsync(review.OrderItemId);
                if (orderItem != null)
                {
                    orderItem.IsReviewed = false;
                    orderItem.UpdateAt = DateTime.Now;
                    if (!string.IsNullOrEmpty(updateBy))
                    {
                        orderItem.UpdateBy = updateBy;
                    }
                }

                var updatedReview = _context.Reviews.Update(review).Entity;
                await _context.SaveChangesAsync();
                return updatedReview;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

