using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public Models.Tweet NewTweet { get; set; }

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

        public async Task<IActionResult> OnPostTweetAsync(int profileId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile = await _context.Profile.Include(p => p.Tweets ).SingleOrDefaultAsync(p => p.Id == profileId);

            NewTweet.Date = DateTime.Now;
            NewTweet.Profile = Profile;
            NewTweet.ProfileId = Profile.Id;

            Profile.Tweets.Add(NewTweet);
            _context.Profile.Update(Profile);
            await _context.Tweet.AddAsync(NewTweet);
            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int profileId, int? tweetId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (tweetId == null)
            {
                return BadRequest();
            }

            _context.Tweet.Remove(new Models.Tweet
            {
                Id = tweetId.Value
            });

            await _context.SaveChangesAsync();

            Profile = await _context.Profile.Include(p => p.Tweets).SingleOrDefaultAsync(p => p.Id == profileId);

            return Page();
        }
    }
}
