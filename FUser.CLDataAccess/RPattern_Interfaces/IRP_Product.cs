using FUser.CLDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RPattern_Interfaces
{
    public interface IRP_Product : IGenericRespository<MProduct>
    {
        int GetPIDCount();
    }
}
