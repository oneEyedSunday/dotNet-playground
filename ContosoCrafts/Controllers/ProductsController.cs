using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.Models;
using ContosoCrafts.Services;
using ContosoCrafts.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoCrafts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(Products _products, ILogger<ProductsController> logger)
        {
            ProductsService = _products;
            _logger = logger;
        }

        public Products ProductsService { get; }
        private readonly ILogger<ProductsController> _logger;

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductsService.GetProducts();
        }

        [HttpGet]
        [Route("{productId}")]
        public ActionResult<Product> GetProduct(string productId)
        {
            try
            {
               return ProductsService.GetProduct(productId);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, new { productId = productId });
                return NotFound($"Product #{productId} not found.\n");
            }
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
