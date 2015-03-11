using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExchangeRates.Startup))]
namespace ExchangeRates
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
