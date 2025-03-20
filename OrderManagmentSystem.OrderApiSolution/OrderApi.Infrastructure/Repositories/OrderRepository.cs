using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using OrderApi.Domain.Interfaces;
using OrderApi.Infrastructure.Data;
using OrderManagment.SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Repositories
{
    public class OrderRepository(OrderDataContext context) : IOrder
    {
        public async Task<Order> CreateAsync(Order entity)
        {
            var order = await context.Orders.AddAsync(entity);
            await context.SaveChangesAsync();

            return order.Entity.Id > 0 ? order.Entity : null!;
        }

        public async Task<Order> DeleteAsync(Order entity)
        {
            var findedOrder = await GetByIdAsync(entity.Id);
            if (findedOrder is null)
                throw new NotFoundRequestException("order not found by Id");

            context.Orders.Remove(findedOrder);
            await context.SaveChangesAsync();
            return findedOrder;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await context.Orders.ToListAsync();

            return orders is not null ? orders : null!;
        }

        public async Task<Order> GetByAsync(Expression<Func<Order, bool>> predicate)
        {
            var order = await context.Orders.FirstOrDefaultAsync(predicate);
            return order is not null ? order : null!;
        }

        public Task<Order> GetByIdAsync(int id)
        {
            var order = context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order is not null ? order : null!;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            var order = await GetByIdAsync(entity.Id);
            if(order is null)
                throw new NotFoundRequestException("order not found by id");

            context.Orders.Update(entity);
            await context.SaveChangesAsync();
            return order;
        }
    }
}
