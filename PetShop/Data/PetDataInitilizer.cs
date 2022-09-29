using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PetShop.Data
{
    public class PetDataInitilizer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if(!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser admin = new ApplicationUser();
            admin.Email = "mepujan10@gmail.com";
            admin.DateofBirth = DateTime.Parse("2000-01-01");
            admin.UserName = "mepujan10@gmail.com";
            if (userManager.FindByEmail(admin.Email) == null)
            {
                var result = userManager.Create(admin, "Password123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Admin");
                }
            }
            context.Pets.Add(
                new Pet { Name = "Leo", IsMale = true, Breed = "Husky"}
                );
            context.Pets.Add(
                new Pet { Name = "Fluffy", IsMale = true, Breed = "Husky"}
                );
            context.Pets.Add(
                new Pet { Name = "Bluffy", IsMale = true, Breed = "Husky"}
                );
            context.Pets.Add(
                new Pet { Name = "Kitty", IsMale = true, Breed = "Husky"}
                );
            base.Seed(context);
        }
    }
}