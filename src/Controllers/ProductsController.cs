using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YourMobileGuide.Models;
using YourMobileGuide.Services;

namespace YourMobileGuide.Controllers
{
    /// <summary>
    /// This class is a controller that should be treated as an API controller.
    /// API controllers are typically used to handle HTTP requests and responses.
    /// </summary>
    [ApiController]

    /// <summary>
    /// This attribute specifies the base URL path for all actions in this controller.
    /// In this case, all actions in this controller will be prefixed with "/products".
    /// </summary>
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        // Constructor that initializes the ProductService instance.
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // This property holds the ProductService instance that is used to retrieve and manipulate product data.
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// This method handles HTTP GET requests to the base URL path of this controller.
        /// It retrieves all product data and returns it as a list of ProductModel objects.
        /// </summary>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetAllData();
        }

        /// <summary>
        /// This method handles HTTP PATCH requests to the base URL path of this controller.
        /// It accepts a RatingRequest object in the request body and uses it to update the rating of a specific product.
        /// </summary>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);

            // Return an HTTP OK response with no data.
            return Ok();
        }

        /// <summary>
        /// This class defines a data model for the RatingRequest object that is used to update product ratings.
        /// </summary>
        public class RatingRequest
        {
            public string ProductId { get; set; }
            public int Rating { get; set; }
        }
    }
}
