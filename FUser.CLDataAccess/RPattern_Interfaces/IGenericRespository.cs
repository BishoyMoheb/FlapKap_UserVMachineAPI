using FUser.CLDomain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.RPattern_Interfaces
{
    public interface IGenericRespository<T> where T : class
    {
        Task<T> GetByIDAsync(string id);
        Task<T> GetByNameAsync(string Name);
        Task<IEnumerable<T>> GetALLAsync();
        void CreateAsync(T EModel);
        T Update(T EntityToUpdate);
        void DeleteAsync(string Id);
        void AddJwtSTokens(MJwtSTokens mJwtSTokens);
        void DeleteJwtSTokens(MJwtSTokens mJwtSTokens);
        Task<MJwtSTokens> GetJSTokenAsync(string AToken);
        Task<MJwtSTokens> GetJSTokenByIdAsync(string Id);
    }
}
