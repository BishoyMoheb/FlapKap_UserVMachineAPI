using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.Validation
{
    public class ValidIntAttribute : ValidationAttribute
    {
        public override bool IsValid(object OValue)
        {
            int[] Arr = new int[] { 5, 10, 20, 50, 100 };
            foreach (int AItem in Arr)
            {
                if (AItem == (int)OValue)
                    return true;
            }
            ErrorMessage = "Unacceptable amount for depoist";
            return false;
        }
    }
}
