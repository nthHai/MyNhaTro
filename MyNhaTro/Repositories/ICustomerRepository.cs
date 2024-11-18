using Microsoft.EntityFrameworkCore;
using MyNhaTro.Data;
using MyNhaTro.Models;

namespace MyNhaTro.Repositories
{
    public interface ICustomerRepository
    {
        //lấy tất cả
        public Task<List<CustomerModel>> GetAllCustomersAsync();
        //lấy 01
        public Task<CustomerModel> GetCustomersAsync(int id);
        //Thêm
        public Task<int> AddCustomersAsync(CustomerModel model);
        //Sửa
        public Task UpdateCustomersAsync(int id, CustomerModel model);
        //Xóa
        public Task DeleteCustomersAsync(int id);

        Task<string> GetCustomerCodeAsync();  // Phương thức để lấy mã khách hàng

        Task<int> InsertCustomerAsync(CustomerModel CustomerModel);
    }
}
