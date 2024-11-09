
using Microsoft.AspNetCore.Mvc;
using MyNhaTro.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MyNhaTro.Data;

namespace MyNhaTro_FE.Controllers 
{
    public class CustomerController : Controller
    {
       

        private string baseURL = "https://localhost:7155/";

        // Lấy danh sách khách hàng
        public async Task<IActionResult> Index()
        {
            
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

            return View(lstCustomers);

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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateCustomer(CustomerModel CustomerModel)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(baseURL + "api/TCustomer/"); //"Khachhang/Getlist/"
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpClient.PutAsJsonAsync("", CustomerModel);

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

        public IActionResult ErrorPage()
        {
            return View(); 
        }
    }
}
