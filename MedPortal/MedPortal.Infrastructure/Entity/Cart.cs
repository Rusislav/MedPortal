using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Infrastructure.Entity
{
    public class Cart
    {
        public Cart()
        {
            CardProducts = new List<CardProduct>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public List<CardProduct> CardProducts { get; set; } 

    }
}


