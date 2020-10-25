using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Data;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Tweet
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Tweet> Tweet { get;set; }

        public async Task OnGetAsync()
        {
            Tweet = await _context.Tweet
                .Include(t => t.Profile)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
}
