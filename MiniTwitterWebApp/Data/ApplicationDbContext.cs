using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Profile>()
                .Property(p => p.Id)
                .IsRequired();

            builder.Entity<Profile>()
                .Property(p => p.DisplayName)
                .IsRequired();

            builder.Entity<FollowersFollowing>()
                .HasKey(ff => new { ff.FollowerId, ff.FollowingId });

            builder.Entity<FollowersFollowing>()
                .HasOne(ff => ff.Follower)
                .WithMany(p => p.ProfilesFollowing)
                .HasForeignKey(ff => ff.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FollowersFollowing>()
                .HasOne(ff => ff.Following)
                .WithMany(p => p.Followers)
                .HasForeignKey(ff => ff.FollowingId);

            builder.Entity<Tweet>()
                .HasOne(t => t.Profile)
                .WithMany(p => p.Tweets)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<FollowersFollowing> FollowersFollowing { get; set; }

    }
}
