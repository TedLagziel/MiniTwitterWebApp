using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Data;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileService(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> IsCurrentUserProfileOwner(string? userName, int profileId)
        {
            if (userName == null)
            {
                return false;
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return false;
            }

            var profile = await _context.Profile.SingleOrDefaultAsync(p => p.Id == profileId);

            return profile.UserId.Equals(user.Id);
        }

        public async Task<Profile> FindProfileWithUserName(string userName)
        {
            if (userName == null)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            var profile = await _context.Profile.SingleOrDefaultAsync(p => p.UserId == user.Id);

            return profile;
        }

        public async Task<Profile> FindProfileByIdAsync(int id)
        {
            return await _context.Profile.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Profile FindProfileById(int id)
        {
            return _context.Profile.SingleOrDefault(x => x.Id == id);
        }

        public async Task CreateNewProfileAsync(string displayName, string userId)
        {
            var profile = new Profile
            {
                DisplayName = displayName,
                UserId = userId
            };

            await _context.Profile.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesProfileFollowOther(int profileIdA, int profileIdB)
        {
            var profileAFollowing = _context.Profile.Include(p => p.ProfilesFollowing);

            return (await profileAFollowing
                .FirstAsync(p => p.Id == profileIdA))
                .ProfilesFollowing
                .Any(following => following.FollowingId == profileIdB);
        }
    }
}
