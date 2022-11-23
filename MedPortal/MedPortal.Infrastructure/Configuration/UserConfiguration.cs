using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace MedPortal.Infrastructure.Configuration
{
    /// <summary>
    /// Seeding the User 
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(SeedUser());
        }

        private  List<User> SeedUser()
        {
            var users = new List<User>();    

            var hasher = new PasswordHasher<User>();

            var user = new User()
            {

                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                Address = "Mladost 1 bl 43 vh a et 4 ap 54"
                           

            };

            user.PasswordHash =
          hasher.HashPassword(user, "Rusi123");

            users.Add(user);    

            user = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "GUEST@MAIL.COM",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                Address = "Mladost 3 bl 13 vh c et 7 ap 24"
            };

            user.PasswordHash =
          hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }

    }
}
