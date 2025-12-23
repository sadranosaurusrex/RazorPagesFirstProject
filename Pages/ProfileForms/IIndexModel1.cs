using Microsoft.AspNetCore.Mvc;
using RazorPages.Models;

namespace RazorPages.Pages.ProfileForms
{
    public interface IIndexModel1
    {
        IList<Form> Form { get; set; }

        Task OnGetAsync();
        Task<IActionResult> OnPostPopulateAsync();
    }
}