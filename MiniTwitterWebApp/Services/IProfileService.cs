using System.Threading.Tasks;
using MiniTwitterWebApp.Models;

namespace MiniTwitterWebApp.Services
{
    public interface IProfileService
    {
        Task<Profile> FindProfileByIdAsync(int id);
        Task CreateNewProfileAsync(string displayName,string userId);
        Task<bool> IsCurrentUserProfileOwner(string userName, int profileId);
        Task<Profile> FindProfileWithUserName(string userName);
    }
}