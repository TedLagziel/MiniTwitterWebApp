using System.Threading.Tasks;
using MiniTwitter.Data.Entities;

namespace MiniTwitter.Services
{
    public interface ITwitterUserService
    {
        Task<TwitterUser> FindByDisplayName(string displayName);
    }
}