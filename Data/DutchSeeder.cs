//using DutchTreat.Data.Entities;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore.Internal;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
////using Newtonsoft.Json;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace DutchTreat.Data
//{
//    public class DutchSeeder
//    {
//        private readonly DutchContext _ctx;
//        private readonly IWebHostEnvironment _env;

//        public DutchSeeder(DutchContext ctx, IWebHostEnvironment env)
//        {
//            _ctx = ctx;
//            _env = env;
//        }

//        public void Seed()
//        {
//            _ctx.Database.EnsureCreated();

//            if (_ctx.Products.Any())
//            {
//                var filePath = Path.Combine(_env.WebRootPath, "Data/art.json");
//                var json = File.ReadAllText(filePath);
//                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

//                _ctx.Products.AddRange(products);

//                var order = new Order()
//                {
//                    OrderDate = DateTime.Today,
//                    OrderNumber = "10000",
//                    Items = new List<OrderItem>()
//                    {
//                        new OrderItem()
//                        {
//                            Product = products.First(),
//                            Quantity = 5,
//                            UnitPrice = products.First().Price
//                        }
//                    }
//                };

//                _ctx.Orders.Add(order);

//                _ctx.SaveChanges();
//            }
//        }
//    }
//}



using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    /// <summary>
    /// Seeders are useful to build lookup information or static data that is
    /// always going to be the same forever. 
    /// </summary>
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly IWebHostEnvironment _env;

        public DutchSeeder(DutchContext ctx, IWebHostEnvironment env)
        {
            _ctx = ctx;
            _env = env;
        }

        public async Task Seed()
        {
            // make sure the datbase actually created!
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                // need to load a lot of data and not want to manually add new product objects
                // one by one. 
                // we need a path to the file first. we could pass a hardcoded string
                // this will work in visual studio but not runtime. so we can inject ihostingenvironment
                var filepath = Path.Combine(_env.WebRootPath, "art.json");
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(File.ReadAllText(filepath));

                _ctx.Products.AddRange(products);

                // add previously created user to the order
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
          {
            new OrderItem()
            {
              Product = products.First(),
              Quantity = 5,
              UnitPrice = products.First().Price
            }
          }
                };
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}
