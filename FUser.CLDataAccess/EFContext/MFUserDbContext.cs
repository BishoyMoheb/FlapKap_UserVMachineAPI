using FUser.CLDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.CLDataAccess.EFContext
{
    public class MFUserDbContext : DbContext
    {
        public MFUserDbContext(DbContextOptions<MFUserDbContext> DBConOptions)
            : base(DBConOptions)
        {
        }

        public DbSet<MUser> DbS_Users { get; set; }
        public DbSet<MProduct> DbS_Products { get; set; }
        public DbSet<MJwtSTokens> DbS_JSTokens { get; set; }
    }
}
