using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniTwitter.Data.Entities;

namespace MiniTwitter.Data
{
    public class ApplicationDbContext : IdentityDbContext<TwitterUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Tweet>(tweet =>
            {
                tweet.HasKey(x => x.Id);
                tweet.HasOne(x => x.TwitterUser);
            });
        }
    }
}
