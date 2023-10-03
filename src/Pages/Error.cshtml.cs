using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace YourMobileGuide.Pages
{
    /// <summary>
    /// This page model represents the error page.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // The ID of the current request that resulted in an error.
        public string RequestId { get; set; }

        // Determines whether the RequestId should be displayed to the user.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // A logger instance used for logging errors and other messages.
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Initializes a new instance of the ErrorModel class.
        /// </summary>
        /// <param name="logger">The logger instance to be used.</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This method is executed when the ErrorModel page is loaded.
        /// It sets the RequestId property to the current activity ID or trace ID.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
