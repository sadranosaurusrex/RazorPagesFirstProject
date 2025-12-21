using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Forms
{
    public class EditModel : PageModel
    {
        private readonly RazorPages.Data.RazorPagesContext _context;

        public EditModel(RazorPages.Data.RazorPagesContext context)
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

            var form =  await _context.Form.FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            Form = form;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Form).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(Form.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FormExists(int id)
        {
            return _context.Form.Any(e => e.Id == id);
        }
    }
}
