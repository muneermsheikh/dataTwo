using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager, DataContext context)
        {
            if (await userManager.Users.AnyAsync()) return;

            var indData = await System.IO.File.ReadAllTextAsync("Data/IndustrySeedData.json");
            var inds = JsonSerializer.Deserialize<List<Industry>>(indData);
            if (inds != null) 
            {
                foreach(var ind in inds)
                { context.Industries.Add(ind); }
            }

            var profData = await System.IO.File.ReadAllTextAsync("Data/CategorySeedData.json");
            var profs = JsonSerializer.Deserialize<List<Profession>>(profData);
            if (profs != null) 
            {
                foreach(var prof in profs)
                {context.Professions.Add(prof);}
            }
            
            var qData = await System.IO.File.ReadAllTextAsync("Data/QualificationSeedData.json");
            var qs = JsonSerializer.Deserialize<List<Qualification>>(qData);
            if (qs != null) 
            {
                foreach(var q in qs)
                { context.Qualifications.Add(q); }
            }

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users != null) 
            {
                var roles = new List<AppRole>
                {
                    new AppRole {Name = "Admin"},
                    new AppRole {Name = "Member"},
                    new AppRole {Name = "Candidate"},
                    new AppRole {Name = "HR Manager"},
                    new AppRole {Name = "HR Supervisor"},
                    new AppRole {Name = "HR Executive"},
                    new AppRole {Name = "Document Controller-Admin"},
                    new AppRole {Name = "Document Controller-Processing"},
                    new AppRole {Name = "Process Manager"},
                    new AppRole {Name = "Process Supervisor"},
                    new AppRole {Name = "Process Executive-Visa-KSA"},
                    new AppRole {Name = "Process Executive-Visa-Non KSA"},
                    new AppRole {Name = "Process Executive-Ticketing"},
                    new AppRole {Name = "Receptionist"},
                    new AppRole {Name = "Finance Manager"},
                    new AppRole {Name = "Finance Supervisor"},
                    new AppRole {Name = "Finance Executive"}
                };

                foreach(var role in roles)
                { await roleManager.CreateAsync(role); }

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, "Member");
                }

                var admin = new AppUser
                {
                    UserName = "admin"
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] {"Admin", "HR Supervisor"});
            }


            context.SaveChanges();
            
        }
    }
}