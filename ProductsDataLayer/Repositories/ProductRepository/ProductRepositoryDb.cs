using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsDataLayer.Repositories.ProductRepository
{
    public class ProductRepositoryDb : IProductRepository
    {
        private readonly EFCoreContext _dbContext;

        public ProductRepositoryDb(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Create(Product product)
        {
            product.Id = Guid.NewGuid();
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task<Product> DeleteById(Guid id)
        {
            var entity = await GetById(id);
            if(entity != null)
            {
                _dbContext.Products.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
            => await _dbContext.Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        public async Task<Product> Update(Product product)
        {
            _dbContext.Attach(product);
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
