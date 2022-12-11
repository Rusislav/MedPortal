using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using MedPortal.Core.Models;
using MedPortal.Infrastructure.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedPortal.Core.Services
{
    public class UserService : IUserService
    {
     
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> _userManager)
        {          
            this.userManager = _userManager;
        }

     /// <summary>
     /// Взимам всички users
     /// </summary>
     /// <returns></returns>

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
         
            var model = await userManager.Users.Select(u => new UserViewModel()

            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive
            }).ToListAsync();


            return model;
        }
        /// <summary>
        /// Забравям конкретен User
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task ForgotUser(string Id)
        {

            var user = await userManager.FindByIdAsync(Id);
            if(user != null)
            {
                user.IsActive = false;
                user.Email = null;
                user.NormalizedEmail = null;
                user.UserName = $"forgottenUser-{DateTime.Now.Ticks}";
                user.NormalizedUserName = null;
                user.PasswordHash = null;

                await userManager.UpdateAsync(user);
            }
          
              
        }
    }
}
