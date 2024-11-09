using AutoMapper;
using MyNhaTro.Data;
using MyNhaTro.Models;

namespace MyNhaTro.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();  //ReverseMap : map 2 chiều
        }
    }
}
