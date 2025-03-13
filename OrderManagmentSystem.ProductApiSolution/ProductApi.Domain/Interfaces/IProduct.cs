using OrderManagment.SharedLibrary.Interfaces;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Interfaces
{
    public interface IProduct : IGenericRepository<Product> {}
}
