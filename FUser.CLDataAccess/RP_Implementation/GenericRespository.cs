using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RP_Implementation
{
    public class GenericRespository<T> : IGenericRespository<T> where T : class
    {
        protected readonly MFUserDbContext _mFUserDbContext;

        public GenericRespository(MFUserDbContext mFUserDbContext)
        {
            this._mFUserDbContext = mFUserDbContext;
        }

        public async void CreateAsync(T EModel)
        {
            await _mFUserDbContext.Set<T>().AddAsync(EModel);
        }

        public async void DeleteAsync(string Id)
        {
            var EntityToDelete = await FindAsyncId(Id);
            _mFUserDbContext.Set<T>().Remove(EntityToDelete);
        }

        public async Task<IEnumerable<T>> GetALLAsync()
        {
            var ListToGet = await _mFUserDbContext.Set<T>().ToListAsync();
            return ListToGet;
        }

        public async Task<T> GetByIDAsync(string Id)
        {
            var EntityToGet = await FindAsyncId(Id);
            return EntityToGet;
        }

        private async Task<T> FindAsyncId(string Id)
        {
            T EntityToGet;
            if (typeof(T).Name == "MUser")
                EntityToGet = await _mFUserDbContext.Set<T>().FindAsync(new Guid(Id));
            else
            {
                EntityToGet = await _mFUserDbContext.Set<T>().FindAsync(Id);
            }
            return EntityToGet;
        }

        public async Task<T> GetByNameAsync(string Name)
        {
            T EntityToGet;
            if (typeof(T).Name == "MUser")
            {
                var VResult = await _mFUserDbContext.DbS_Users.FirstOrDefaultAsync(t => t.UserName == Name);
                EntityToGet = VResult as T;
            }
            else
            {
                var VResult = await _mFUserDbContext.DbS_Products.FirstOrDefaultAsync(t => t.ProductName == Name);
                EntityToGet = VResult as T;
            }
            return EntityToGet;
        }

        public T Update(T EntityToUpdate)
        {
            _mFUserDbContext.Set<T>().Update(EntityToUpdate);
            return EntityToUpdate;
        }

        public void AddJwtSTokens(MJwtSTokens mJwtSTokens)
        {
            _mFUserDbContext.DbS_JSTokens.AddAsync(mJwtSTokens);
        }

        public void DeleteJwtSTokens(MJwtSTokens mJwtSTokens)
        {
            _mFUserDbContext.DbS_JSTokens.Remove(mJwtSTokens);
        }

        public async Task<MJwtSTokens> GetJSTokenAsync(string AToken)
        {
            MJwtSTokens JSToken_ToGet = await _mFUserDbContext.DbS_JSTokens.FirstOrDefaultAsync(t => t.JwtSToken == AToken);
            return JSToken_ToGet;
        }

        public async Task<MJwtSTokens> GetJSTokenByIdAsync(string Id)
        {
            MJwtSTokens JSToken_ToGet = await _mFUserDbContext.DbS_JSTokens.FirstOrDefaultAsync(t => t.UserId == Id);
            return JSToken_ToGet;
        }
    }
}
