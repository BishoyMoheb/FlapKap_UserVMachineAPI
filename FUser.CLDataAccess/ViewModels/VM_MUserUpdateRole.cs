using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.ViewModels
{
    public class VM_MUserUpdateRole
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
