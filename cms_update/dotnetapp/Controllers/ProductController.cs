﻿using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetProductById(int id)
        //{
        //    var product = _productService.GetProductById(id);

        //    if (product == null)
        //    {
        //        return NotFound($"Product with ID {id} not found.");
        //    }

        //    return Ok(product);
        //}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            _productService.AddProduct(product);

            return Ok("Product added successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ProductId}")]
        public IActionResult UpdateProduct([FromBody] Product updatedProduct, int ProductId)
        {
            Console.WriteLine("con"+ProductId);
            if (updatedProduct == null)
            {
                return BadRequest("Invalid product data");
            }


            _productService.UpdateProduct(updatedProduct, ProductId);

            return Ok("Product updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{ProductId}")]
        public IActionResult DeleteProduct(int ProductId)
        {
            _productService.DeleteProduct(ProductId);

            return Ok("Product deleted successfully");
        }
    }
}
