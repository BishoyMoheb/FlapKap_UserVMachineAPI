using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.ViewModels
{
    public class VM_MUChangePassword
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password doesn't match with Paswword")]
        public string ConfirmPassword { get; set; }
    }
}
