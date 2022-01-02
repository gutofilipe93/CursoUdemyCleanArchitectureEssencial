using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Data.Context;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _db.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
