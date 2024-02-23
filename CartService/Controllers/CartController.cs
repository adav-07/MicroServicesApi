using CartService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CartService.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : Controller
    {
        private static List<Order> orders;
        public CartController()
        {
            LoadProductsFromFile();
        }
        private void LoadProductsFromFile()
        {
            try
            {
                string filePath = "C:\\Users\\x6kadavm\\source\\repos\\Microservices\\MicroServicesApi\\CartService\\data.json";

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    orders = JsonSerializer.Deserialize<List<Order>>(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products from JSON file: {ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return orders;
        }
        [HttpGet("{id:int}")]
        public ActionResult<Order> Get(int id)
        {
            var order = orders.FirstOrDefault(x => x.CustId == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public ActionResult AddItem(Order order)
        {
            orders.Add(order);
            Console.WriteLine("saved " + order.CustId);
            return Ok();
        }
    }
}
