using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Data;
using MiniTwitterWebApp.Services;

namespace MiniTwitterWebApp.Areas.Identity.Pages
{
    public class NewsfeedModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfileService _profileService;

        public NewsfeedModel(ApplicationDbContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public IOrderedEnumerable<Models.Tweet> TweetsInFeed { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var id = (await _profileService.FindProfileWithUserName(HttpContext.User.Identity.Name)).Id;

            Profile = await _context.Profile.SingleOrDefaultAsync(p => p.Id == id);

            var followingProfiles = _context.FollowersFollowing
                .Include(p => p.Following)
                    .ThenInclude(p => p.Tweets)
                .Where(ff => ff.FollowerId.Equals(id))
                .ToList();

            if (Profile == null)
            {
                return NotFound();
            }

            var tweets = followingProfiles
                .SelectMany(profileFollowing => profileFollowing.Following.Tweets)
                .ToList();

            TweetsInFeed = tweets.OrderByDescending(t => t.Date);

            return Page();
        }
    }
}
