﻿using System.Data;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var customer = _customerService.GetAllCustomers();
            return Ok(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid Customer data");
            }

            _customerService.AddCustomer(customer);

            return Ok("Customer added successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{CustomerId}")]
        public IActionResult UpdateProduct([FromBody] Customer updatedCustomer, int CustomerId)
        {
            Console.WriteLine("con" + CustomerId);
            if (updatedCustomer == null)
            {
                return BadRequest("Invalid Customer data");
            }


            _customerService.UpdateCustomer(updatedCustomer, CustomerId);

            return Ok("Customer updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{CustomerId}")]
        public IActionResult DeleteProduct(int CustomerId)
        {
            _customerService.DeleteCustomer(CustomerId);

            return Ok("Product deleted successfully");
        }
    }
    
}
