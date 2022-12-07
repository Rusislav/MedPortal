using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Models
{
    public class CheckoutViewModel
    {
        [Required]
        [Precision(18, 2)]
        [Range(typeof(decimal), "0.0", "9000.0", ConvertValueInInvariantCulture = true, ErrorMessage = "Price must be positive number")]
        public decimal TotalPrice { get; set; }
    }
}
