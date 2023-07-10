using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdataWebClient.Model;

namespace OdataWebClient.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<Pet>? Pets { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:44300/api/pet/GetAll");

            if (response.IsSuccessStatusCode)
            {
                Pets = await response.Content.ReadFromJsonAsync<List<Pet>>();
            }
            else
            {
                Pets = new List<Pet>();
            }

            return Page();
        }
    }
}
