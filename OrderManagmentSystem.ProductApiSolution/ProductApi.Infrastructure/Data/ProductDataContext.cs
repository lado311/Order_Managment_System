using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Data
{
    public class ProductDataContext : DbContext
    {
        public ProductDataContext(DbContextOptions<ProductDataContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
