using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegistroBioseguridad.Startup))]
namespace RegistroBioseguridad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
