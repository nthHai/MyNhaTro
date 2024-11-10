
using Microsoft.AspNetCore.Mvc;
using MyNhaTro.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MyNhaTro.Data;
using X.PagedList;
using X.PagedList.Extensions;
using Microsoft.EntityFrameworkCore;


namespace MyNhaTro_FE.Controllers 
{
    public class CustomerController : Controller
    {
       

        private string baseURL = "https://localhost:7155/";

       
        // Lấy danh sách khách hàng
        public async Task<IActionResult> Index(int? page=1)
        {
            int pageSize = 20; // Số lượng khách hàng mỗi trang
            int pageNumber = (page ?? 1); // Nếu không có số trang thì mặc định là trang 1

            List<CustomerModel> lstCustomers = new List<CustomerModel>();

            using (var _httpClient = new HttpClient() )
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/TCustomer/"); //"Khachhang/Getlist/"
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    //string result = getData.Content.ReadAsStringAsync().Result;
                    string result = await getData.Content.ReadAsStringAsync();
                    lstCustomers = JsonConvert.DeserializeObject<List<CustomerModel>>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            // Phân trang danh sách khách hàng
            var pagedCustomers = lstCustomers.ToPagedList(pageNumber, pageSize);

            return View(pagedCustomers);

         }

        /*
       // Tạo mới khách hàng
       [HttpPost]
       public async Task<IActionResult> Create(CustomerModel customer)
       {
           var json = JsonConvert.SerializeObject(customer);
           var content = new StringContent(json, Encoding.UTF8, "application/json");
           var response = await _httpClient.PostAsync("/api/customers", content);

           if (response.IsSuccessStatusCode)
               return RedirectToAction("Index");

           return View("Error");
       }

       // Sửa khách hàng
       [HttpPost]
       public async Task<IActionResult> Edit(int id, CustomerModel customer)
       {
           var json = JsonConvert.SerializeObject(customer);
           var content = new StringContent(json, Encoding.UTF8, "application/json");
           var response = await _httpClient.PutAsync($"/api/customers/{id}", content);

           if (response.IsSuccessStatusCode)
               return RedirectToAction("Index");

           return View("Error");
       }

       // Xóa khách hàng
       public async Task<IActionResult> Delete(int id)
       {
           var response = await _httpClient.DeleteAsync($"/api/customers/{id}");

           if (response.IsSuccessStatusCode)
               return RedirectToAction("Index");

           return View("Error");
       }
       */

        /*
        public async Task<IActionResult> Create()
        {
            return View();
        }
        */

        public async Task<IActionResult> Create()
        {
            // Gọi API để lấy mã khách hàng
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/TCustomer/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _httpClient.GetAsync("GetCustomerCode");

                if (response.IsSuccessStatusCode)
                {
                    var customerCode = await response.Content.ReadAsStringAsync();
                    ViewBag.CustomerCode = customerCode?.Replace("\"", ""); // Loại bỏ dấu nháy kép nếu có;  // Lưu mã khách hàng vào ViewBag để hiển thị trong View
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View();
        }

        public async Task<IActionResult> CreateCustomer(CustomerModel CustomerModel)
        {
           
            CustomerModel.CreateDate = DateTime.Now;

            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/TCustomer/"); //"Khachhang/Getlist/"
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("", CustomerModel);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }
            }

        }
        

        public async Task<IActionResult> Details(int Id)
        {
            CustomerModel customerDetails = new CustomerModel();
            
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/TCustomer/"); 
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.GetAsync($"{Id}");
                
                if (getData.IsSuccessStatusCode)
                {
                    //string result = getData.Content.ReadAsStringAsync().Result;
                    string result = await getData.Content.ReadAsStringAsync();
                    customerDetails = JsonConvert.DeserializeObject<CustomerModel>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            return View(customerDetails);
        }


        public IActionResult ErrorPage()
        {
            return View(); 
        }
    }
}
