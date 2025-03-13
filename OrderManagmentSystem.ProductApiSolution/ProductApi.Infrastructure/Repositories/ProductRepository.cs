using Microsoft.EntityFrameworkCore;
using OrderManagment.SharedLibrary.Exceptions;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProductRepository(ProductDataContext context) : IProduct
    {
        public async Task<Product> CreateAsync(Product entity)
        {
            var product = await GetByAsync(p => p.Name == entity.Name);
            if (product is not null) throw new BadRequestException("this product already exist!");

            var currentEntity = await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
            return currentEntity.Entity;
        }

        public async Task<Product> DeleteAsync(Product entity)
        {
            var findedProduct = await GetByIdAsync(entity.Id);
            if (findedProduct is null) throw new NotFoundRequestException("product not found");

            context.Remove(findedProduct);
            await context.SaveChangesAsync();
            return findedProduct;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await context.Products.ToListAsync();
            return products is not null ? products : null!;
        }

        public async Task<Product> GetByAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = context.Products.Where(predicate).FirstOrDefault()!;
            return product is not null ? product : null;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product is not null ? product : null;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            var product = await GetByIdAsync(entity.Id);
            if (product is null) throw new NotFoundRequestException("product not found");

            context.Products.Update(entity);
            await context.SaveChangesAsync();

            return product;
        }
    }
}
