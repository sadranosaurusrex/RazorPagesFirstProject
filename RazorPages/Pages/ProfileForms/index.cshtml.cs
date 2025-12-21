using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
 
    public IndexModel(ILogger<IndexModel> logger)
    {
        this._logger = logger;
    }

    public IList<Form> Form { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Form = new List<Form>();
    }
}