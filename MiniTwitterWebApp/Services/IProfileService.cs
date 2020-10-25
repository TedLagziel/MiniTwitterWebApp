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
        Profile FindProfileById(int id);
        Task<bool> DoesProfileFollowOther(int profileIdA, int profileIdB);
    }
}