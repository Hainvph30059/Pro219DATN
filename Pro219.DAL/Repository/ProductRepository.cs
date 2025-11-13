using Microsoft.EntityFrameworkCore;
using Pro219.DAL.Context;
using Pro219.DAL.Models;

namespace Pro219.DAL.Repository
{
    public class ProductRepository
    {
        private readonly ClothesDbContext _context;

        public ProductRepository(ClothesDbContext context)
        {
            _context = context;
        }

       
    }
}
