using MedPortal.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Infrastructure.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constant.CategoryConstant.MaxCategoryName)]
        public string Name { get; set; } = null!;
    }
}
