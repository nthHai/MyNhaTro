using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNhaTro.Data;

namespace MyNhaTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly QuanLyPhongTroContext _context;

        public CustomersController(QuanLyPhongTroContext context)
        {
            _context = context;
        }


        // GET: api/Customers
        //Cách tạo bằng code
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        */
        //Cách tự viết
        [HttpGet]
        [Route("/Khachhang/Getlist")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }


        [HttpGet]
        [Route("/Khachhang/Search")]
        public async Task<IActionResult> SearchAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Chuỗi tìm kiếm không được để trống.");
            }
            // Tìm kiếm sản phẩm theo tên một cách không đồng bộ
            var results = await _context.Customers
                                        .Where(
                                                p => p.CustomerCode.Contains(query)||
                                                     p.FirstName!.Contains(query)||
                                                     p.LastName!.Contains(query)||
                                                     p.IdentifyNumber.Contains(query)
                                               )
                                        .ToListAsync();

            if (!results.Any())
            {
                return NotFound("Không tìm thấy sản phẩm nào.");
            }

            return Ok(results);
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
