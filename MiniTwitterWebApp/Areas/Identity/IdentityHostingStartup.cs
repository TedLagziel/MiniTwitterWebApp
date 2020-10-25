using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MiniTwitterWebApp.Areas.Identity.IdentityHostingStartup))]
namespace MiniTwitterWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}