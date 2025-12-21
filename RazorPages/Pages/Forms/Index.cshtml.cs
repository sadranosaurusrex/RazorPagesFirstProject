using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;

namespace RazorPages.Pages.Forms
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.RazorPagesContext _context;

        public IndexModel(RazorPages.Data.RazorPagesContext context)
        {
            _context = context;
        }

        public IList<Form> Form { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Form = await _context.Form.ToListAsync();
        }
    }
}
