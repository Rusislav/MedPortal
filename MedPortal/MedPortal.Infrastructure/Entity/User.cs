

using MedPortal.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedPortal.Infrastructure.Entity
{
    public class User : IdentityUser
    {
     

        [StringLength(Constant.UserConstant.MaxAddress)]
        public string? Address { get; set; }

     
    }
}
