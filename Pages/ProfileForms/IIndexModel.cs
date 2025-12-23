using Microsoft.AspNetCore.Mvc;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms
{
    public interface IIndexModel
    {
        IList<Form> Form { get; set; }

        Task OnGetAsync();
        Task<IActionResult> OnPostPopulateAsync();
    }
}