using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagment.SharedLibrary.Exceptions;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class ProductController(IProduct productRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await productRepository.GetAllAsync();
            if (!result.Any()) throw new BadRequestException("product list is empty");

            return Ok(result);
        }

        [HttpGet("get-product/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null) throw new NotFoundRequestException("product not found");

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            
            var entity = await productRepository.CreateAsync(product);
            return Ok(entity);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            var result = await productRepository.UpdateAsync(product);

            return Ok($"Updated Product: {result}");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            var result = await productRepository.DeleteAsync(product);

            return Ok($"Deleted Product: {result}");
        }
    }
}
