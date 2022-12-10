using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Models
{
    public class UserViewModel
    {
      
        public string Id { get; set; } = null!;

        
        public string? UserName { get; set; } = null!;

       
        [EmailAddress]
        public string? Email { get; set; } = null!;

        public bool? IsActive { get; set; }
    }

  
}
