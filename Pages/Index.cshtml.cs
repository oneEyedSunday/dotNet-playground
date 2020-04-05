using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.Models;
using ContosoCrafts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, Products _products)
        {
            _logger = logger;
            Products = _products.GetProducts();
        }

        public void OnGet()
        {
            System.Console.WriteLine("Ok, we are here!!!");
            System.Console.WriteLine("The number of products are {0}", Products.Count());
        }
    }
}
