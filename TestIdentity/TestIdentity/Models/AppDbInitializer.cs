using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace TestIdentity.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleAdmin = new IdentityRole { Name = "admin" };
            var roleUser = new IdentityRole { Name = "user" };
            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            var admin = new ApplicationUser
                        {
                          Email = ConfigurationManager.AppSettings.Get("AdminEmail"),
                          UserName = "somemail@gmail.com" ,
                          EmailConfirmed = true
                        };
            string password = ConfigurationManager.AppSettings.Get("AdminPass");

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                // add role to admin user and registered user
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleUser.Name);
            }
            base.Seed(context);
         }
    }
}