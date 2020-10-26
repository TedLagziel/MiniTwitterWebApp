using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DetailsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        [BindProperty]
        public Models.Tweet NewTweet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.Include(p => p.Tweets).SingleOrDefaultAsync(p => p.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostTweetAsync(int profileId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Profile = await _context.Profile.Include(p => p.Tweets ).SingleOrDefaultAsync(p => p.Id == profileId);

            NewTweet.Date = DateTime.Now;
            NewTweet.Profile = Profile;
            NewTweet.ProfileId = Profile.Id;

            if (DetectYoutubeLink(NewTweet.Content))
            {
                var youtubeLink = ExtractYoutubeLink(NewTweet.Content);
                var embeddedLink = TransformYoutubeToEmbed(youtubeLink);

                NewTweet.Content = NewTweet.Content.Replace(youtubeLink, embeddedLink);
            }

            Profile.Tweets.Add(NewTweet);
            _context.Profile.Update(Profile);
            await _context.Tweet.AddAsync(NewTweet);
            await _context.SaveChangesAsync();

            return Page();
        }

        public bool DetectYoutubeLink (string content)
        {
            var containsYoutube = content.Contains("https://www.youtube.com/watch?v=");

            return containsYoutube;
        }

        public string ExtractYoutubeLink(string content)
        {
            var exampleYoutubeLink = "https://www.youtube.com/watch?v=";

            var index =  content.IndexOf("https://www.youtube.com/watch?v=");

            return content.Substring(index, exampleYoutubeLink.Length + 11);
        }


        public string TransformYoutubeToEmbed(string youtubeLink)
        {
            var vidDirectLink = youtubeLink.Substring(Math.Max(0, youtubeLink.Length - 11));

            var embedLink = $"<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/{vidDirectLink}\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>";

            return embedLink;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int profileId, int? tweetId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (tweetId == null)
            {
                return BadRequest();
            }

            _context.Tweet.Remove(new Models.Tweet
            {
                Id = tweetId.Value
            });

            await _context.SaveChangesAsync();

            Profile = await _context.Profile.Include(p => p.Tweets).SingleOrDefaultAsync(p => p.Id == profileId);

            return Page();
        }
    }
}
