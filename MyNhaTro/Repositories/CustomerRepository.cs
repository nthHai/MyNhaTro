using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyNhaTro.Data;
using MyNhaTro.Models;

namespace MyNhaTro.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly QuanLyPhongTroContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(QuanLyPhongTroContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;        
        }


        public async Task<int> AddCustomersAsync(CustomerModel model)
        {
            var newCustomer = _mapper.Map<Customer>(model);
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();  //Lưu lại

            return newCustomer.Id;  //Trả về id dòng mới thêm
        }

        
        //Lấy tất cả
        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerModel>>(customers); 
        }

        //Lấy 1 dòng
        public async Task<CustomerModel> GetCustomersAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return _mapper.Map<CustomerModel>(customer);
        }

        //public async Task<int> UpdateCustomersAsync(int id, CustomerModel model) //có trả về giá trị
        public async Task UpdateCustomersAsync(int id, CustomerModel model) //Không trả về gái trị
        {
            if (id == model.Id)
            {
                var updateCustomer = _mapper.Map<Customer>(model);
                _context.Customers.Update(updateCustomer);
                await _context.SaveChangesAsync(); //Lưu lại
            }
        }

        public async Task DeleteCustomersAsync(int id)
        {
            var deleteCustomer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (deleteCustomer != null)
            {
                _context.Customers.Remove(deleteCustomer);
                await _context.SaveChangesAsync(); //Lưu lại
            }
        }

    }
}