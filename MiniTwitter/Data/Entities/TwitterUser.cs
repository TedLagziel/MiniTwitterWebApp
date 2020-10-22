using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MiniTwitter.Data.Entities
{
    public class TwitterUser : IdentityUser
    {
        [PersonalData]
        public string DisplayName { get; set; }
        public IList<Tweet> Tweets { get; set; }
        public IList<TwitterUser> UsersFollowing { get;  } = new List<TwitterUser>();
    }
}
