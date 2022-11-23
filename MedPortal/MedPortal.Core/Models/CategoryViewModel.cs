using MedPortal.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Models
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constant.CategoryConstant.MaxCategoryName,MinimumLength = Constant.CategoryConstant.MinCategoryName,ErrorMessage = "Name must be at least {2} characters long")]
        public string Name { get; set; } = null!;
    }
}
