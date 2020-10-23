using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly MiniTwitterWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(MiniTwitterWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public Tweet NewTweet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.Include(p => p.Tweets).SingleOrDefaultAsync(p => p.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile = await _context.Profile.Include(p => p.Tweets ).SingleOrDefaultAsync(p => p.Id == Profile.Id);

            NewTweet.Date = DateTime.Now;
            NewTweet.Profile = Profile;
            NewTweet.ProfileId = Profile.Id;

            Profile.Tweets.Add(NewTweet);
            _context.Profile.Update(Profile);
            await _context.Tweet.AddAsync(NewTweet);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
