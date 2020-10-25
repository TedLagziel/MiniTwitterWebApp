namespace MiniTwitterWebApp.Models
{
    public class FollowersFollowing
    {
        public int? FollowerId { get; set; }
        public virtual Profile Follower { get; set; }
        public int? FollowingId { get; set; }
        public virtual Profile Following { get; set; }

    }
}
