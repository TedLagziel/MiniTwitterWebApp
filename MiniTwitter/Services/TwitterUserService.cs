using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitter.Data;
using MiniTwitter.Data.Entities;

namespace MiniTwitter.Services
{
    public class TwitterUserService : ITwitterUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public TwitterUserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TwitterUser> FindByDisplayName(string displayName)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(x => x.DisplayName == displayName);
        }
    }
}
