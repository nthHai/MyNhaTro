using AutoMapper;
using MyNhaTro.Data;
using MyNhaTro.Models;
using System.Data.Common;
using System.Data;

namespace MyNhaTro.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();  //ReverseMap : map 2 chiều
            
        }

        private void AddParameter(DbCommand command, string name, object value, DbType dbType)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;  // Sử dụng DBNull.Value nếu giá trị null
            parameter.DbType = dbType;
            command.Parameters.Add(parameter);
        }
    }
    
}
