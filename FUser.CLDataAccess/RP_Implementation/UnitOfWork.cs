using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RPattern_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RP_Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MFUserDbContext _mFUserDbContext;

        public UnitOfWork(MFUserDbContext mFUserDbContext)
        {
            this._mFUserDbContext = mFUserDbContext;
            RP_UserI = new RP_User(_mFUserDbContext);
            RP_ProductI = new RP_Product(_mFUserDbContext);
        }

        public IRP_User RP_UserI { get; private set; }

        public IRP_Product RP_ProductI { get; private set; }

        public void Dispose()
        {
            _mFUserDbContext.Dispose();
        }

        public int Save_UOfWork()
        {
            return _mFUserDbContext.SaveChanges();
        }
    }
}
