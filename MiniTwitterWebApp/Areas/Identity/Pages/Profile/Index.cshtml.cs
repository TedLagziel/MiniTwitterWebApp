using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Models;
using MiniTwitterWebApp.Services;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IProfileService _profileService;

        public IndexModel(Data.ApplicationDbContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        public IList<Models.Profile> Profile { get;set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int ProfileAId { get; set; }
            public int ProfileBId { get; set; }
        }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profile.ToListAsync();
        }

        public async Task<IActionResult> OnPostFollowAsync(int? profileAId, int? profileBId)
        {
            if (profileAId.HasValue && profileBId.HasValue)
            {
                var profileA = await _profileService.FindProfileByIdAsync(profileAId.Value);
                var profileB = await _profileService.FindProfileByIdAsync(profileBId.Value);

                await _context.FollowersFollowing.AddAsync(new FollowersFollowing
                {
                    Follower = profileA,
                    FollowerId = profileA.Id,
                    Following = profileB,
                    FollowingId = profileB.Id
                });

                await _context.SaveChangesAsync();

                return RedirectToPage($"./Details", new {id = profileBId});
            }

            return Page();
        }
        public async Task<IActionResult> OnPostUnfollowAsync(int? profileAId, int? profileBId)
        {
            if (profileAId.HasValue && profileBId.HasValue)
            {
                var profileA = await _profileService.FindProfileByIdAsync(profileAId.Value);
                var profileB = await _profileService.FindProfileByIdAsync(profileBId.Value);

                _context.FollowersFollowing.Remove(new FollowersFollowing
                {
                    Follower = profileA,
                    FollowerId = profileA.Id,
                    Following = profileB,
                    FollowingId = profileB.Id
                });

                await _context.SaveChangesAsync();

                Profile = await _context.Profile.ToListAsync();

                return Page();
            }

            return Page();
        }
    }
}
