using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ContosoCrafts.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.Services
{
    public class Products
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Products(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonSrcFileName
        {
            get
            {
                return Path.Combine(WebHostEnvironment.WebRootPath, "database", "products.json");
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using StreamReader jsonFileReader = File.OpenText(JsonSrcFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
