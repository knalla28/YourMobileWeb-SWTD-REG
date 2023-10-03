using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace YourMobileGuide.Pages
{
    /// <summary>
    /// This is the PrivacyModel page, which inherits from the PageModel class.
    /// </summary>
    public class PrivacyModel : PageModel
    {
        // A logger instance used for logging messages.
        private readonly ILogger<PrivacyModel> _logger;

        // Constructor that initializes the logger instance.
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method is executed when the PrivacyModel page is loaded.
        /// It does not perform any action in this example.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
