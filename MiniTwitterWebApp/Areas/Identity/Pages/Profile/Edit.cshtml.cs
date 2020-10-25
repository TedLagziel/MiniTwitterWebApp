using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniTwitterWebApp.Services;

namespace MiniTwitterWebApp.Areas.Identity.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IProfileService _profileService;

        public EditModel(Data.ApplicationDbContext context, IProfileService profileService)
        {
            _context = context;
            _profileService = profileService;
        }

        [BindProperty]
        public Models.Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
                return Unauthorized();
            }

            var isUserSameAsProfile = await _profileService.IsCurrentUserProfileOwner(userName, id.GetValueOrDefault());

            if (!isUserSameAsProfile)
            {
                return Forbid();
            }

            Profile = await _context.Profile.FirstOrDefaultAsync(m => m.Id == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Profile);
            _context.Entry(Profile).Property(p => p.DisplayName).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(Profile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.Id == id);
        }
    }
}
