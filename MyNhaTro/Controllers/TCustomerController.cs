﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNhaTro.Contracts;
using MyNhaTro.Models;
using MyNhaTro.Repositories;

namespace MyNhaTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Tự viết API theo Repository
    public class TCustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        
        public TCustomerController(ICustomerRepository repo)
        {
            _customerRepository = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                return Ok(await _customerRepository.GetAllCustomersAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomersAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddNewCustomer(CustomerModel model)
        {
            try
            { 
                var newCustomerID = await _customerRepository.AddCustomersAsync(model);
                var customer = await _customerRepository.GetCustomersAsync(newCustomerID);
                return customer == null ? NotFound() : Ok(customer);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerModel model)
        {
            if (id == model.Id)
            {
                await _customerRepository.UpdateCustomersAsync(id, model);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _customerRepository.DeleteCustomersAsync(id);
            return Ok();
        }

        // API để lấy mã khách hàng tự động từ stored procedure
        
        [HttpGet("GetCustomerCode")]
        public async Task<IActionResult> GetCustomerCode()
        {
            try
            {
                var customerCode = await _customerRepository.GetCustomerCodeAsync();
                return Ok(customerCode);  // Trả về mã khách hàng
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi tạo mã khách hàng: {ex.Message}");
            }
        }

        //Gọi store procedure
        [HttpPost("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(CustomerModel model)
        {
            var result = await _customerRepository.InsertCustomerAsync(model);

            if (result <= 0)
            {
                return Ok("Thêm mới thành công.");
            }
            return BadRequest("Không thể thêm khách hàng");
        }

        //Gọi store procedure
        [HttpPost("UpdateCustomerDetails")]
        public async Task<IActionResult> UpdateCustomerDetails(int id, CustomerModel model)
        {
            var result = await _customerRepository.UpdateCustomerDetailsAsync(id,model);

            if (result <= 0)
            {
                return Ok("Cập nhật thành công.");
            }
            return BadRequest("Không thể cập nhật thông tin khách hàng");
        }

        [HttpGet("GetSPTest")]
        public async Task<IActionResult> GetSPTest()
        {
            try
            {
                var customers = await _customerRepository.GetSPTest();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
