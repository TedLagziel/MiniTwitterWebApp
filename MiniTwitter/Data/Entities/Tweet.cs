using System;

namespace MiniTwitter.Data.Entities
{
    public class Tweet
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }  

        public TwitterUser TwitterUser { get; set; }
    }
}
