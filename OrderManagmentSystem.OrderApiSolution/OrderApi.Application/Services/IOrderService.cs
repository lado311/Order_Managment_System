﻿using OrderApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByClientId(int clientId);
        Task<OrderDetailDto> GetOrderDetails(int clientId);

    }
}
