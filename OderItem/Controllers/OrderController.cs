using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderItem.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        
        [HttpGet("{id}")]
        public IActionResult GetCartBy(int id)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44324/api/MenuItem");
                var responseTask = client.GetAsync("MenuItem");
                responseTask.Wait();
                var result = responseTask.Result;


                List<Cart> ltems = new List<Cart>();


                if (result.IsSuccessStatusCode)
                {

                    string jsonData = result.Content.ReadAsStringAsync().Result;


                    ltems = JsonConvert.DeserializeObject<List<Cart>>(jsonData);
                    Cart obj1 = ltems.SingleOrDefault(item => item.Id == id);

                    //Cart obj = new Cart();
                    obj1.MenuItemId = 1;
                    obj1.UserId = 1;


                    return Ok(obj1);

                }
                else
                {

                    return BadRequest();

                }

            };
        }
        [HttpGet]
        public Cart Post()
        {
            Cart c = new Cart();
            c.Id = 1;
            c.UserId = 1;
            c.MenuItemId = 1;
            c.MenuItemName = "Computer";
            return c;
        }
    }
}
