using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniTwitter.Data.Entities;
using MiniTwitter.Services;

namespace MiniTwitter.Views.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<TwitterUser> _userManager;
        private readonly ITwitterUserService _twitterUserService;
        

        public IList<Tweet> Tweets = new List<Tweet>();

        public ProfileModel(UserManager<TwitterUser> userManager, ITwitterUserService twitterUserService)
        {
            _userManager = userManager;
            _twitterUserService = twitterUserService;
        }

        private bool IsCurrentUser { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            
            public string TweetText { get; set; }
        }

        public async Task OnGet()
        {
            var user = await _twitterUserService.FindByDisplayName("1");

            if (HttpContext.User.Identity.Name != null)
            {
                var identityName= HttpContext.User.Identity.Name;

                if (identityName.Equals(user.DisplayName))
                {
                    IsCurrentUser = true;
                }
            }

            Tweets = user.Tweets;
        }
    }
}
