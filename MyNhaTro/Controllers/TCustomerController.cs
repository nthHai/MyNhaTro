using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
