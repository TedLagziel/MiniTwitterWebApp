using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly MiniTwitterWebApp.Data.ApplicationDbContext _context;

        public IndexModel(MiniTwitterWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Profile> Profile { get;set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profile.ToListAsync();
        }
    }
}
