using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDomain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RP_Implementation
{
    public class RP_User : GenericRespository<MUser>, IRP_User
    {
        public RP_User(MFUserDbContext mfUserDbContext) : base(mfUserDbContext)
        {
                
        }
    }
}
