using MedPortal.Core.Contracts;
using MedPortal.Core.Services;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure.Entity;
using MedPortal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MedPortal.Core.Models;

namespace MedPortal.Core.UnitTests.ServicesTests
{
    public class UserUnitTests
    {
        private List<User> user;
        private ApplicationDbContext dbContext;
       

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>()).Result);
            return mgr;
        }



        [OneTimeSetUp]
        public void Setup()
        {
            user = new List<User>()
            {
                new User() { Id = "71335055-7e12-4284-9102-16038be032ad" , Email = "user@mail.bg" , NormalizedEmail = "USER@MAIL.BG", IsActive = true, UserName = "user@mail.bg", NormalizedUserName = "USER@MAIL.BG"},
                new User() { Id = "61335055-7e12-4284-9102-16038be0321e", Email = "user@mail.com",NormalizedEmail = "USER@MAIL.COM",IsActive = true, UserName = "user@mail.com", NormalizedUserName = "USER@MAIL.COM"},

            };


        

            var DbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Give a Unique name to the DB
                    .Options;
            dbContext = new ApplicationDbContext(DbOptions);
            dbContext.AddRange(user);
            dbContext.SaveChanges();

        }
        [Test]
        public void TestGetCart()
        {
            var userManager = MockUserManager<User>(user.ToList()).Object;
            UserService service;
            service = new UserService(userManager);
            var count = dbContext.Users.Count();


            var result = service.GetAllUsersAsync();


            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IEnumerable<UserViewModel>>>(result);
            Assert.That(dbContext.Users.Count(), Is.EqualTo(count));

        }

        [Test]
        public void TestForgotUser()
        {

            var userManager = MockUserManager<User>(user.ToList()).Object;
            UserService service;
            service = new UserService(userManager);
            var count = dbContext.Users.Count();

                  

            var result = service.ForgotUser("71335055-7e12-4284-9102-16038be032ad");


            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task>(result);
            Assert.IsNull(userManager.Users.FirstOrDefault(u => u.Id == "71335055-7e12-4284-9102-16038be032ad"));
           
        }
    }
}
