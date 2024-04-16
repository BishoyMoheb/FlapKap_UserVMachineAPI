using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RP_Implementation;
using FUser.CLDataAccess.RPattern_Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUser.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration config_i)
        {
            ConfigI = config_i;
        }


        public IConfiguration ConfigI { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection SerCollectionI)
        {
            SerCollectionI.AddControllers();

            // Registering MFUserDbContext with the ASP.NET Core dependency injection system
            SerCollectionI.AddDbContext<MFUserDbContext>
                (DbCOBuilder => DbCOBuilder.UseSqlServer(ConfigI.GetConnectionString("DBConn_FUserVM_API")));

            // Registering IUnitOfWork
            SerCollectionI.AddTransient<IUnitOfWork, UnitOfWork>();

            // Add JWT Configuartion variables
            var jwtIssuer = SerCollectionI.Configure<string>(ConfigI.GetSection("Jwt:Issuer"));
            var jwtKey = SerCollectionI.Configure<string>(ConfigI.GetSection("Jwt:Key"));

            // Add JWT Bearer Options
            SerCollectionI.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                          .AddJwtBearer(JWTBOptions =>
                          {
                              JWTBOptions.TokenValidationParameters = new TokenValidationParameters
                              {
                                  ValidateIssuer = true,
                                  ValidateAudience = true,
                                  ValidateLifetime = true,
                                  ValidateIssuerSigningKey = true,
                                  ValidIssuer = jwtIssuer.ToString(),
                                  ValidAudience = jwtIssuer.ToString(),
                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey.ToString()))
                              };
                          });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder AppBuiderI, IWebHostEnvironment WHEnvI)
        {
            if (WHEnvI.IsDevelopment())
            {
                AppBuiderI.UseDeveloperExceptionPage();
            }

            AppBuiderI.UseRouting();

            AppBuiderI.UseAuthorization();

            AppBuiderI.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
