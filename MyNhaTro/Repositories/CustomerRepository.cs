using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyNhaTro.Data;
using MyNhaTro.Models;
using System.Data;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MyNhaTro.Contracts;
using NuGet.Protocol.Plugins;
using Newtonsoft.Json.Linq;
using MyNhaTro.Helper;
using System.Reflection;
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

        public async Task<int> InsertCustomerAsync(CustomerModel customerModel)
        {
            try
            {
                // Sử dụng Dapper để gọi stored procedure
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@customer_code", customerModel.CustomerCode);
                    parameters.Add("@first_name", customerModel.FirstName);
                    parameters.Add("@last_name", customerModel.LastName);
                    parameters.Add("@day_of_birth", customerModel.DayOfBirth.HasValue ? (object)customerModel.DayOfBirth.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@identify_number", customerModel.IdentifyNumber);
                    parameters.Add("@ngaycap", customerModel.Ngaycap.HasValue ? (object)customerModel.Ngaycap.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@noicap", customerModel.Noicap);
                    parameters.Add("@phone", customerModel.Phone);
                    parameters.Add("@mobile_phone", customerModel.MobilePhone);
                    parameters.Add("@permanent_address", customerModel.PermanentAddress);
                    parameters.Add("@job_name", customerModel.JobName);
                    parameters.Add("@work_place", customerModel.WorkPlace);
                    parameters.Add("@date_join", customerModel.DateJoin.HasValue ? (object)customerModel.DateJoin.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@description", customerModel.Description);
                    parameters.Add("@create_date", customerModel.CreateDate);
                    parameters.Add("@crud_type_code", "INSERT");
                    // Thực thi stored procedure
                    var result = await connection.ExecuteAsync(
                        "cus.sp_customer_crud",  // Tên của stored procedure
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result; // Trả về số bản ghi bị ảnh hưởng
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gọi stored procedure: " + ex.Message);
            }
        }
        public async Task<int> UpdateCustomerDetailsAsync(int id, CustomerModel customerModel)
        {
            try
            {
                // Sử dụng Dapper để gọi stored procedure
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("id", customerModel.Id);
                    parameters.Add("@customer_code", customerModel.CustomerCode);
                    parameters.Add("@first_name", customerModel.FirstName);
                    parameters.Add("@last_name", customerModel.LastName);
                    parameters.Add("@day_of_birth", customerModel.DayOfBirth.HasValue ? (object)customerModel.DayOfBirth.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@identify_number", customerModel.IdentifyNumber);
                    parameters.Add("@ngaycap", customerModel.Ngaycap.HasValue ? (object)customerModel.Ngaycap.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@noicap", customerModel.Noicap);
                    parameters.Add("@phone", customerModel.Phone);
                    parameters.Add("@mobile_phone", customerModel.MobilePhone);
                    parameters.Add("@permanent_address", customerModel.PermanentAddress);
                    parameters.Add("@job_name", customerModel.JobName);
                    parameters.Add("@work_place", customerModel.WorkPlace);
                    parameters.Add("@date_join", customerModel.DateJoin.HasValue ? (object)customerModel.DateJoin.Value.ToDateTime(new TimeOnly()) : DBNull.Value);
                    parameters.Add("@description", customerModel.Description);
                    parameters.Add("@create_date", customerModel.CreateDate);
                    parameters.Add("@crud_type_code", "UPDATE");
                    // Thực thi stored procedure
                    var result = await connection.ExecuteAsync(
                        "cus.sp_customer_crud",  // Tên của stored procedure
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return result; // Trả về số bản ghi bị ảnh hưởng
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gọi stored procedure: " + ex.Message);
            }
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

        // Phương thức gọi stored procedure để lấy mã khách hàng
                
       public async Task<string> GetCustomerCodeAsync()
       {
           try
           {
               using (var connection = _context.Database.GetDbConnection())
               {
                   await connection.OpenAsync();

                   using (var command = connection.CreateCommand())
                   {
                       command.CommandText = "GetCustomerCode";  // Tên của stored procedure
                       command.CommandType = System.Data.CommandType.StoredProcedure;

                       var result = await command.ExecuteScalarAsync();
                       return result?.ToString();  // Trả về mã khách hàng hoặc null nếu không có kết quả
                   }
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Lỗi tạo mã khách hàng: " + ex.Message);
           }
       }


        #region // Cách Ánh xạ Model thủ công Có 01 Parameter 
        /*
        public async Task<string> GetCustomerCodeAsync(int customerType)
           {
               try
               {
                   using (var connection = _context.Database.GetDbConnection())
                   {
                       await connection.OpenAsync();

                       using (var command = connection.CreateCommand())
                       {
                           command.CommandText = "GetCustomerCode";  // Tên của stored procedure
                           command.CommandType = System.Data.CommandType.StoredProcedure;

                           // Thêm tham số cho stored procedure
                           var parameter = command.CreateParameter();
                           parameter.ParameterName = "@CustomerType"; // Tên tham số trong stored procedure
                           parameter.Value = customerType;
                           parameter.DbType = System.Data.DbType.Int32; // Kiểu dữ liệu của tham số
                           command.Parameters.Add(parameter);

                           var result = await command.ExecuteScalarAsync();
                           return result?.ToString();  // Trả về mã khách hàng hoặc null nếu không có kết quả
                       }
                   }
               }
               catch (Exception ex)
               {
                   throw new Exception("Lỗi tạo mã khách hàng: " + ex.Message);
               }
           }

        //Nhiều parameter
        public async Task<string> GetCustomerCodeAsync(CustomerCodeParams parameters)  //tạo model CustomerCodeParams
        {
           try
           {
               using (var connection = _context.Database.GetDbConnection())
               {
                   await connection.OpenAsync();

                   using (var command = connection.CreateCommand())
                   {
                       command.CommandText = "GetCustomerCode";  // Tên stored procedure
                       command.CommandType = System.Data.CommandType.StoredProcedure;

                       // Thêm tham số nhanh chóng với phương thức tiện ích HELPER
                       AddParameter(command, "@CustomerType", parameters.CustomerType, DbType.Int32);
                       AddParameter(command, "@Region", parameters.Region, DbType.String);
                       AddParameter(command, "@Status", parameters.Status, DbType.Boolean);

                       var result = await command.ExecuteScalarAsync();
                       return result?.ToString();  // Trả về mã khách hàng hoặc null nếu không có kết quả
                   }
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Lỗi tạo mã khách hàng: " + ex.Message);
           }
        }
        */
        #endregion

        public async Task<IEnumerable<CustomerModel>> GetSPTest()
        {
            var query = "SELECT * FROM cus.customer";
                     
            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                var customers = await connection.QueryAsync<CustomerModel>(query);
                return customers.ToList();
            }


            #region // Cách Ánh xạ Model thủ công
            /*
            using (var connection = _context.Database.GetDbConnection())
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Sử dụng QueryAsync với kết quả trả về là một danh sách các đối tượng (object[])
                var customers = await connection.QueryAsync(query);

                var customerModels = new List<CustomerModel>();

                foreach (var row in customers)
                {
                    var customerModel = new CustomerModel();

                    // Ánh xạ dữ liệu từ các trường
                    customerModel.Id = row.id != null ? (int)row.id : 0;
                    customerModel.FirstName = row.first_name?.ToString();
                    customerModel.LastName = row.last_name?.ToString();

                    // Chuyển ngaycap từ DateTime sang DateOnly
                    if (row.ngaycap != null)
                    {
                        var dateTime = (DateTime)row.ngaycap;
                        customerModel.Ngaycap = DateOnly.FromDateTime(dateTime);
                    }

                    customerModels.Add(customerModel);
                }

                return customerModels; // Trả về danh sách CustomerModel
            }
            */
            #endregion
        
        }
    }
}