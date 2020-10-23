using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

    }
}
