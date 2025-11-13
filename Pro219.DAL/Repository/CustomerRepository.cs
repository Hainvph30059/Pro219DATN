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
    public class CustomerRepository
    {
        ClothesDbContext _context;
        public CustomerRepository()
        {
            _context = new ClothesDbContext();
        }

        public async Task<Customer> GetByKeyAndPassword(string keyword, string hashPassword)
        {
            Customer avaiableUser = await _context.Customers.FirstOrDefaultAsync(x => x.Email == keyword || x.PhoneNumber == keyword);
            if (avaiableUser == null)
            {
                return null;
            }
            else
            {
                if (avaiableUser.PasswordHash.ToUpper() == hashPassword.ToUpper())
                {
                    return avaiableUser;
                }

                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetByIdCustomer(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByIdCustomerSendMail(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                var addedCustomer = _context.Customers.Add(customer).Entity;
                await _context.SaveChangesAsync();
                return addedCustomer;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.Id);

                if (existingCustomer == null) return null;

                existingCustomer.FullName = customer.FullName;
                existingCustomer.DateOfBirth = customer.DateOfBirth;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Email = customer.Email;
                existingCustomer.PasswordHash = customer.PasswordHash;
                existingCustomer.Status = customer.Status;
                existingCustomer.LastLogin = customer.LastLogin;

                var updatedCustomer = _context.Customers.Update(existingCustomer).Entity;
                await _context.SaveChangesAsync();
                return updatedCustomer;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Customer> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null) return null;

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Customer> FindCustomerExistByKeyWord(string key)
        {
            try
            {
                var c = _context.Customers.FirstOrDefault(x => x.Email == key || x.PhoneNumber == key);

                if (c == null)
                    return null;
                return c;


            }
            catch
            {
                return null;
            }
        }


    }
}
