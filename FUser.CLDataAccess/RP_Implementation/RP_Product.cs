using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RP_Implementation
{
    public class RP_Product : GenericRespository<MProduct>, IRP_Product
    {
        public RP_Product(MFUserDbContext mfUserDbContext) : base(mfUserDbContext)
        {

        }

        public int GetPIDCount()
        {
            int CountResult = _mFUserDbContext.DbS_Products.Count();
            return CountResult;
        }
    }
}
