using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public Product GetProduct(string productId)
        {
            return GetProducts().First(product => product.Id == productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            using StreamReader jsonFileReader = File.OpenText(JsonSrcFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public void AddRating(string productId, int rating)
        {
            IEnumerable<Product> products = GetProducts();
            Product product = products.First(p => p.Id == productId);

            product.Ratings = product.Ratings ?? new int[] { };

            if (product.Ratings.Length > 0)
            {
                List<int> newRatings = product.Ratings.ToList();
                newRatings.Add(rating);
                product.Ratings = newRatings.ToArray();


            } else
            {
                product.Ratings = new int[] { rating };
            }

            using (FileStream outStream = File.OpenWrite(JsonSrcFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(new Utf8JsonWriter(outStream, new JsonWriterOptions {
                    SkipValidation = true, Indented = true
                }), products);
            }
        }
    }
}
