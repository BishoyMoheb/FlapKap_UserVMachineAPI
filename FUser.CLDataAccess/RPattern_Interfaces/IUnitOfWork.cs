using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RPattern_Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRP_User RP_UserI { get; }
        IRP_Product RP_ProductI { get; }
        int Save_UOfWork();
    }
}
