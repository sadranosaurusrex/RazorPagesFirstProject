using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Forms
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPages.Data.RazorPagesContext _context;

        public DeleteModel(RazorPages.Data.RazorPagesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Form Form { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form.FirstOrDefaultAsync(m => m.Id == id);

            if (form == null)
            {
                return NotFound();
            }
            else
            {
                Form = form;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form.FindAsync(id);
            if (form != null)
            {
                Form = form;
                _context.Form.Remove(Form);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
