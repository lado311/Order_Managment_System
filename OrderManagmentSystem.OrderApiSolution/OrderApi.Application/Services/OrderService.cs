using OrderApi.Application.DTOs;
using OrderApi.Domain.Interfaces;
using OrderManagment.SharedLibrary.Exceptions;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services
{
    public class OrderService(IOrder orderRepository, HttpClient client, ResiliencePipelineProvider<string> resiliencePipeline) : IOrderService
    {

        //get products
        public async Task<ProductDto> GetProduct(int productId)
        {
            var httpResponse = await client.GetAsync($"api/Product/get-product/{productId}");
            if (!httpResponse.IsSuccessStatusCode)
                return null;

            return await httpResponse.Content.ReadFromJsonAsync<ProductDto>();
        }

        //Get User
        public async Task<UserDto> GetUser(int userId)
        {
            var httpResponse = await client.GetAsync($"api/users/{userId}");
            if (!httpResponse.IsSuccessStatusCode)
                return null;

            return await httpResponse.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<OrderDetailDto> GetOrderDetails(int clientId)
        {
            var order = await orderRepository.GetByIdAsync(clientId);
            if (order is null)
                return null;

            var retryPipeLine = resiliencePipeline.GetPipeline("my-retry-pipeline");

            //prepare product
            ProductDto productDto = await retryPipeLine.ExecuteAsync(async token => await GetProduct(order.ProductId));
            
            //prepare user
            //var userDto = await retryPipeLine.ExecuteAsync(async token => await GetUser(order.ClientId));

            return new OrderDetailDto(
                order.Id,
                order.ProductId,
                order.ClientId,
                "userDto.Email",
                "userDto.PhoneNumber",
                productDto.Name,
                order.PurchaseQuantity,
                productDto.Price,
                order.PurchaseQuantity * productDto.Price,
                order.OrderedDate);
        }

        public Task<IEnumerable<OrderDto>> GetOrdersByClientId(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
