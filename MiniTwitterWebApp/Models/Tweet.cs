using System;

namespace MiniTwitterWebApp.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
