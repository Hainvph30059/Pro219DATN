using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro219.DAL.Repository
{
    public class AddressRepository
    {
        ClothesDbContext _context;

        public AddressRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<Address> AddAddress(Address address)
        {
            try
            {
                var addedAddress = _context.Addresses.Add(address).Entity;
                await _context.SaveChangesAsync();
                return addedAddress;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            try
            {
                var existingAddress = await _context.Addresses.FindAsync(address.Id);

                if (existingAddress == null) return null;

                existingAddress.CustomerId = address.CustomerId;
                existingAddress.FullName = address.FullName;
                existingAddress.Phone = address.Phone;
                existingAddress.Street = address.Street;
                existingAddress.City = address.City;
                existingAddress.District = address.District;
                existingAddress.OtherInfo = address.OtherInfo;
                existingAddress.IsDefault = address.IsDefault;
                existingAddress.Status = address.Status;

                var updatedAddress = _context.Addresses.Update(existingAddress).Entity;
                await _context.SaveChangesAsync();
                return updatedAddress;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Address>> GetByCustomerId(int customerId)
        {
            try
            {
                var addresses = await _context.Addresses
                    .Where(x => x.CustomerId == customerId)
                    .ToListAsync();
                return addresses;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Address> GetById(int id)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(id);
                return address;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            try
            {
                var addresses = await _context.Addresses.ToListAsync();
                return addresses;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
