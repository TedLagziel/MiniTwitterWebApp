using System.Collections.Generic;

namespace MiniTwitterWebApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public string UserId { get; set; }

        public ICollection<FollowersFollowing> Followers { get; set; }
        public ICollection<FollowersFollowing> ProfilesFollowing { get; set; }

        public IList<Tweet> Tweets { get; set; } = new List<Tweet>();
    }
}
