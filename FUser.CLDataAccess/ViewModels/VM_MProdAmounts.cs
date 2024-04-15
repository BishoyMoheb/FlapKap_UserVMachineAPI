using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.ViewModels
{
    public class VM_MProdAmounts
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public int AmountAvailable { get; set; }
    }
}
