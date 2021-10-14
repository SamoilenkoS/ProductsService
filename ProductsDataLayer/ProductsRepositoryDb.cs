using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDataLayer
{
    public class ProductsRepositoryDb : IProductsRepository
    {
        private readonly EFCoreContext _dbContext;

        public ProductsRepositoryDb(EFCoreContext dbContext)
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
        {
            var items = await _dbContext.Products.Where(x => x.Id == id).ToListAsync();
            if(items.Count != 0)
            {
                return items[0];
            }

            return null;
        }

        public async Task<Product> Update(Product product)
        {
            _dbContext.Attach(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
    }
}
