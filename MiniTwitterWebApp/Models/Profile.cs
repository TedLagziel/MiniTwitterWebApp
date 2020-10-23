using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MiniTwitterWebApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public string UserId { get; set; }

        public IList<Profile> ProfilesFollowing { get; set; }
        public IList<Tweet> Tweets { get; set; } = new List<Tweet>();
    }
}
