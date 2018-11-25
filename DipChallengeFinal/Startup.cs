using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DipChallengeFinal.Startup))]
namespace DipChallengeFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
