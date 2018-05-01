using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void CreateDefautRolesAndUser()
        { 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admis"))
            {
                role.Name = "Admis";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ahmedoudah4423";
                user.Email = "ahmedoudah4423@gmail.com";
                var Check = userManager.Create(user,"Ah@44233213!");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id,role.Name);
                }
            }
        }
    }
}
