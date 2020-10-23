using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Data;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Areas.Identity.Pages
{
    public class NewsfeedModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewsfeedModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public IOrderedEnumerable<Tweet> TweetsInFeed { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile
                .Include(p => p.ProfilesFollowing)
                .ThenInclude(p => p.Tweets)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }

            var tweets = new List<Tweet>();

            foreach (var profileFollowing in Profile.ProfilesFollowing)
            {
                foreach (var tweet in profileFollowing.Tweets)
                {
                    tweets.Add(tweet);
                }
            }

            TweetsInFeed = tweets.OrderByDescending(t => t.Date);

            return Page();
        }
    }
}
