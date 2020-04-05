using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.Models;
using ContosoCrafts.Services;
using ContosoCrafts.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoCrafts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(Products _products)
        {
            ProductsService = _products;
        }

        public Products ProductsService { get; }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductsService.GetProducts();
        }

        [HttpGet]
        [Route("{productId}")]
        public Product GetProduct(string productId)
        {
            return ProductsService.GetProduct(productId);
        }

        [HttpPatch]
        [Route("rate/{productId}")]
        public ActionResult GetSomething(string productId, [FromBody]RateRequest request)
        {
            ProductsService.AddRating(productId, request.rating);
            return Ok($"Successfully rated #{productId} {request.rating} stars.\n");
        }
    }

}
