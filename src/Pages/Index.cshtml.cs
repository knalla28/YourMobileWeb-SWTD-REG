using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using YourMobileGuide.Models;
using YourMobileGuide.Services;

namespace YourMobileGuide.Pages
{
    /// <summary>
    /// Represents the model for the Index page.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="productService">The product service instance.</param>
        public IndexModel(ILogger<IndexModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        /// <summary>
        /// Gets the product service.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Gets or sets the list of products.
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Handles the GET request for the page.
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }
    }
}
