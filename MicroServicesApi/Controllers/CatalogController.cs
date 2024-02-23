using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CatalogService.Controllers
{
    [Route("catalog")]
    [ApiController]
    public class CatalogController : Controller
    {
        private static List<Catalog> catalog;
        public CatalogController()
        {
            LoadProductsFromFile();
        }
        private void LoadProductsFromFile()
        {
            try
            {
                string filePath = "C:\\Users\\x6kadavm\\source\\repos\\Microservices\\MicroServicesApi\\MicroServicesApi\\data.json";

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    catalog = JsonSerializer.Deserialize<List<Catalog>>(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products from JSON file: {ex.Message}");
            }
        }
        [HttpGet]
        public ActionResult<List<Catalog>> Get()
        {
            return catalog;
        }
        [HttpGet("{id:int}")]
        public ActionResult<Catalog> Get(int id)
        {
            var product = catalog.FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
