using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Employee_Portal_System_1.Startup))]
namespace Employee_Portal_System_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
