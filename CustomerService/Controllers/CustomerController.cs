using CustomerServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace CustomerServices.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers;
        public CustomerController()
        {
            LoadProductsFromFile();
        }

        private void LoadProductsFromFile()
        {
            try
            {
                string filePath = "C:\\Users\\x6kadavm\\source\\repos\\Microservices\\MicroServicesApi\\CustomerService\\data.json";

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    customers = JsonSerializer.Deserialize<List<Customer>>(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products from JSON file: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<Customer>> Get()
        {
            return customers;
        }
        [HttpGet("{id:int}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        [HttpPost]
        public async Task<ActionResult> Add(int custid, int prodid, int qty)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7196");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/cart");
            Order data = new Order
            (
                custid = custid,
                prodid = prodid,
                qty = qty
            );
            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                return Ok("saved");
            }
            else
            {
                // Handle the error
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            return NotFound();
        }

    }
}
