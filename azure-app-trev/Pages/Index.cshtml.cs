using Microsoft.AspNetCore.Mvc.RazorPages;

namespace azure_app_trev.Pages;

public class IndexModel(IConfiguration configuration) : PageModel
{
    private readonly IConfiguration _configuration = configuration;



    public void OnGet()
    {
        ViewData["Greeting"] = _configuration["Greeting"];

    }
}
