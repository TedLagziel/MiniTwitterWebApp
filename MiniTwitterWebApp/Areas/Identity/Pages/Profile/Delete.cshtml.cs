using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class DeleteModel : PageModel
    {
        private readonly MiniTwitterWebApp.Data.ApplicationDbContext _context;

        public DeleteModel(MiniTwitterWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.FindAsync(id);

            if (Profile != null)
            {
                _context.Profile.Remove(Profile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
