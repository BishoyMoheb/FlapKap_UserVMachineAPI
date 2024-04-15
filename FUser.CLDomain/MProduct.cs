using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDomain
{
    public class MProduct
    {
        [Key]
        public string ProductId { get; set; }
        public string SellerId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public int AmountAvailable { get; set; }
        public int Cost { get; set; }
    }
}
