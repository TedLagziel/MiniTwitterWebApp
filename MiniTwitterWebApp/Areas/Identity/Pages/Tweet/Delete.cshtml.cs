using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Tweet
{
    public class DeleteModel : PageModel
    {
        private readonly MiniTwitterWebApp.Data.ApplicationDbContext _context;

        public DeleteModel(MiniTwitterWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Tweet Tweet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tweet = await _context.Tweet
                .Include(t => t.Profile).FirstOrDefaultAsync(m => m.Id == id);

            if (Tweet == null)
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

            Tweet = await _context.Tweet.FindAsync(id);

            if (Tweet != null)
            {
                _context.Tweet.Remove(Tweet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
