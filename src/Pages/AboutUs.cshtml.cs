using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace YourMobileGuide.Pages
{
    /// <summary>
    /// AboutUs Page
    /// </summary>
    public class AboutUsModel : PageModel
    {
        private readonly ILogger<AboutUsModel> _logger; // Logger for index model to render

        /// <summary>
        /// Constructor taking input Logger
        /// </summary>
        /// <param name="logger"></param>
        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Customize OnGet() method for AboutUs page.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
