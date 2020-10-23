using System;
using System.Collections.Generic;
using System.Text;
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

            //builder.Entity<Profile>()
            //    .HasMany(p => p.Tweets)
            //    .WithOne(t => t.Profile);

            builder.Entity<Tweet>()
                .HasOne(t => t.Profile)
                .WithMany(p => p.Tweets)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Tweet> Tweet { get; set; }

    }
}
