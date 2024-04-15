using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDomain
{
    public class MJwtSTokens
    {
        [Key]
        public string UserId { get; set; }
        public string JwtSToken { get; set; }
    }
}
