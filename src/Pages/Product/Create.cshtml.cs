using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourMobileGuide.Models;
using YourMobileGuide.Services;

namespace YourMobileGuide.Pages.Product
{
    /// <summary>
    /// Represents the Create page for a product.
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Instance of the data middle tier service.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="productService">The JSON file product service.</param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// The product model used for binding data in the post request.
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Handles the post request.
        /// Posts the model back to the page, calls the data layer to update the data,
        /// and redirects to the index page.
        /// </summary>
        /// <returns>The action result of the post request.</returns>
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            ProductService.CreateData(Product);

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Handles the get request.
        /// Creates a new ProductModel instance with default values to be edited before adding to the data store.
        /// </summary>
        public void OnGet()
        {
            Product = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "",
                Description = "",
                Url = "",
                Image = ""
            };
        }
    }
}
