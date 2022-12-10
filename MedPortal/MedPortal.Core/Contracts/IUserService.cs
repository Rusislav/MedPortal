using MedPortal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Contracts
{
    public interface IUserService
    {

        public Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        public Task ForgotUser(string userId);
    }
}
