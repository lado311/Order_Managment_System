using OrderApi.Domain.Entities;
using OrderManagment.SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Domain.Interfaces
{
    public interface IOrder : IGenericRepository<Order>
    {

    }
}
