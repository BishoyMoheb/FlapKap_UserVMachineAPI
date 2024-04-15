using FUser.CLDataAccess.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.ViewModels
{
    public class VM_MUserDeposit
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [ValidInt]
        public int Deposit { get; set; }
    }
}
