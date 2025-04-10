using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services;
using OrderApi.Domain.Entities;
using OrderApi.Domain.Interfaces;
using OrderManagment.SharedLibrary.Exceptions;

namespace OrderApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrder orderRepository, IMapper mapper, IOrderService orderService) : ControllerBase
    {


        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderRepository.GetAllAsync();
            if(!orders.Any()) throw new BadRequestException("something wrong, orders list is empty!");

            return Ok(orders);
        }

        [HttpGet("get-order/{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order is null) throw new NotFoundRequestException($"order not found by id: {id}");

            return Ok(mapper.Map<OrderDto>(order));
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> AddOrder(OrderDto orderDto)
        {
            var result = await orderRepository.CreateAsync(mapper.Map<Order>(orderDto));
            if (result is null) throw new BadRequestException("something wrong!, order not created");

            return Ok(result);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrder(OrderDto orderDto)
        {
            var result = await orderRepository.UpdateAsync(mapper.Map<Order>(orderDto));
            if (result is null) throw new BadRequestException("something wrong, order not updated");

            return Ok(result);
        }

        [HttpDelete("delete-order/{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order is null) throw new NotFoundRequestException($"order not found by id: {id}");

            var result = await orderRepository.DeleteAsync(order);
            if (result is null) throw new BadRequestException("something wrong, order not deleted");

            return Ok(result);
        }



        //get client

        [HttpGet("get-client-orders/{clientId:int}")]
        public async Task<IActionResult> GetClientOrder(int clientId)
        {
            var order = await orderService.GetOrdersByClientId(clientId);
            if (!order.Any()) throw new NotFoundRequestException($"order not found, by client id: {clientId}");

            return Ok(order);
        }


        [HttpGet("get-order-detail/{orderId:int}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var orderDetail = await orderService.GetOrderDetails(orderId);
            if (orderDetail is null) throw new NotFoundRequestException($"order detail not found by id: {orderId}");

            return Ok(orderDetail);
        }
    }
}
