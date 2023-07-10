using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OdataWebClient.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var apiUrl = $"https://localhost:44300/api/pet-shop/Login?email={Uri.EscapeDataString(Email)}&password={Uri.EscapeDataString(Password)}";
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("/Home");
                    }
                    else
                    {
                        ErrorMessage = "Email or password is incorrect.";
                    }
                }
                catch (Exception)
                {
                    ErrorMessage = "An error occurred while processing your request.";
                }
            }

            return Page();
        }
    }
}