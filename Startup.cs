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
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();

            //var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                //roleManager.Create(role);
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Mahmut215";
                user.UserType = "Admin";
                user.UserGender = "Male";
                user.Email = "mahmoodmasri215@gmail.com";

                var Check = userManager.Create(user, "M112!@gtfk");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");

                }
            }
          
        }
    }
}