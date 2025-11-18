using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class InventoryLogRepository
    {
        ClothesDbContext _context;

        public InventoryLogRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<List<InventoryLog>> GetAllInventoryLogs()
        {
            try
            {
                var logs = await _context.InventoryLogs
                    .Where(x => x.Delete != true)
                    .ToListAsync();
                return logs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InventoryLog> GetInventoryLogById(int id)
        {
            try
            {
                var log = await _context.InventoryLogs.FindAsync(id);
                if (log != null && log.Delete == true)
                    return null;
                return log;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<InventoryLog>> GetInventoryLogsByVariantId(int variantId)
        {
            try
            {
                var logs = await _context.InventoryLogs
                    .Where(x => x.VariantId == variantId && x.Delete != true)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                return logs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InventoryLog> AddInventoryLog(InventoryLog log)
        {
            try
            {
                log.CreateAt = DateTime.Now;
                log.Delete = false;
                var addedLog = _context.InventoryLogs.Add(log).Entity;
                await _context.SaveChangesAsync();
                return addedLog;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InventoryLog> UpdateInventoryLog(InventoryLog log)
        {
            try
            {
                var existingLog = await _context.InventoryLogs.FindAsync(log.InventoryLogId);

                if (existingLog == null || existingLog.Delete == true) return null;

                existingLog.VariantId = log.VariantId;
                existingLog.ChangeQuantity = log.ChangeQuantity;
                existingLog.Reason = log.Reason;
                existingLog.Status = log.Status;
                existingLog.UpdateBy = log.UpdateBy;
                existingLog.UpdateAt = DateTime.Now;

                var updatedLog = _context.InventoryLogs.Update(existingLog).Entity;
                await _context.SaveChangesAsync();
                return updatedLog;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InventoryLog> DeleteInventoryLog(int id, string updateBy = null)
        {
            try
            {
                var log = await _context.InventoryLogs.FindAsync(id);

                if (log == null) return null;

                log.Delete = true;
                log.DeleteAt = DateTime.Now;
                log.UpdateAt = DateTime.Now;
                if (!string.IsNullOrEmpty(updateBy))
                {
                    log.UpdateBy = updateBy;
                }

                var updatedLog = _context.InventoryLogs.Update(log).Entity;
                await _context.SaveChangesAsync();
                return updatedLog;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

