using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.ViewModels
{
    public class VM_MProducts
    {
        [Required]
        public string SellerId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int AmountAvailable { get; set; }

        [Required]
        public int Cost { get; set; }
    }
}
