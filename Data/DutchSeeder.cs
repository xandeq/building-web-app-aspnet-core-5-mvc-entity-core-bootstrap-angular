using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx
            , IHostingEnvironment hosting
            , UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public async Task Seed()
        {
            // make sure the datbase actually created!
            _ctx.Database.EnsureCreated();

            StoreUser applicationUser = new StoreUser();
            Guid guid = Guid.NewGuid();
            applicationUser.Id = guid.ToString();
            applicationUser.UserName = "xxx";
            applicationUser.Email = "xxx@xxx.com";
            applicationUser.NormalizedUserName = "wx@hotmail.com";
            applicationUser.FirstName = "Alexandre";
            applicationUser.LastName = "Queiroz";


            _ctx.Users.Add(applicationUser);


            var hashedPassword = HashPassword("xxx10#");
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();
            applicationUser.PasswordHash = hashedPassword;

            _ctx.SaveChanges();

            if (!_ctx.Products.Any())
            {
                // need to load a lot of data and not want to manually add new product objects
                // one by one. 
                // we need a path to the file first. we could pass a hardcoded string
                // this will work in visual studio but not runtime. so we can inject ihostingenvironment
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                _ctx.Products.AddRange(products);

                // add previously created user to the order
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = applicationUser,
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