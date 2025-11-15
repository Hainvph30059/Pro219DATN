using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class ColorRepository
    {
        ClothesDbContext _context;

        public ColorRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<Color>> GetAllColors()
        {
            try
            {
                var colors = await _context.Colors
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return colors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Color> GetColorById(int id)
        {
            try
            {
                var color = await _context.Colors.FindAsync(id);
                if (color != null && color.Delete == true)
                    return null;
                return color;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Color> AddColor(Color color)
        {
            try
            {
                color.CreateAt = DateTime.Now;
                color.Delete = false;
                var addedColor = _context.Colors.Add(color).Entity;
                await _context.SaveChangesAsync();
                return addedColor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Color> UpdateColor(Color color)
        {
            try
            {
                var existingColor = await _context.Colors.FindAsync(color.Id);

                if (existingColor == null || existingColor.Delete == true) return null;

                existingColor.Name = color.Name;
                existingColor.HexCode = color.HexCode;
                existingColor.Status = color.Status;
                existingColor.UpdateBy = color.UpdateBy;
                existingColor.UpdateAt = DateTime.Now;

                var updatedColor = _context.Colors.Update(existingColor).Entity;
                await _context.SaveChangesAsync();
                return updatedColor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Color> DeleteColor(int id, string updateBy = null)
        {
            try
            {
                var color = await _context.Colors.FindAsync(id);

                if (color == null) return null;

                color.Delete = true;
                color.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(updateBy))
                {
                    color.UpdateBy = updateBy;
                }

                var updatedColor = _context.Colors.Update(color).Entity;
                await _context.SaveChangesAsync();
                return updatedColor;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

