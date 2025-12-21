using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.ProfileForms;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
 
    public IndexModel(ILogger<IndexModel> logger)
    {
        this._logger = logger;
    }

    public async Task OnGetAsync()
    {
    }
}