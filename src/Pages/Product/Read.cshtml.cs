using YourMobileGuide.Models;
using YourMobileGuide.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YourMobileGuide.Pages.Product
{
    /// <summary>
    /// Represents the page model for displaying a product.
    /// </summary>
    public class ReadModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadModel"/> class.
        /// </summary>
        /// <param name="productService">The JSON file product service.</param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The product to be displayed on the page
        public ProductModel Product;

        /// <summary>
        /// Gets the product with the given ID from the JSON file product service and sets the Product property.
        /// </summary>
        /// <param name="id">The ID of the product to be displayed.</param>
        public void OnGet(string id)
        {
            Product = ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}
