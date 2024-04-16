using FUser.CLDataAccess.RP_Implementation;
using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUser.WebAPI.Security
{
    public class Custom_Authorize : ActionFilterAttribute
    {
        private IUnitOfWork _unitOfWorkI { get; set; }

        public override void OnActionExecuting(ActionExecutingContext AExContext)
        {
            string RequestPath = AExContext.HttpContext.Request.Path;
            string AccessToken;
            string Id = string.Empty;
            string[] RPathArr;
            _unitOfWorkI = (IUnitOfWork)AExContext.HttpContext
                                                  .RequestServices
                                                  .GetService(typeof(IUnitOfWork));
            if (RequestPath.Contains('&'))
            {
                RPathArr = RequestPath.Split('&');
                Id = RPathArr[0].Split('=')[1];
                AccessToken = RPathArr[1].Split('=')[1];
            }
            else
            {
                RPathArr = RequestPath.Split('/');
                AccessToken = RPathArr[RPathArr.Length - 1];
            }
            MJwtSTokens JSTokenFoGet = _unitOfWorkI.RP_UserI
                                                   .GetJSTokenAsync(AccessToken)
                                                   .GetAwaiter()
                                                   .GetResult();
            if (JSTokenFoGet == null)
            {
                AExContext.Result = new JsonResult("UnAuthorized for accessing data") 
                                    { StatusCode = 401 };
                return;
            }
            else
            {
                MUser mUser = _unitOfWorkI.RP_UserI
                                          .GetByIDAsync(JSTokenFoGet.UserId)
                                          .GetAwaiter()
                                          .GetResult();
                if (mUser.Id.ToString() != Id && !string.IsNullOrEmpty(Id) && !Id.StartsWith('P'))
                {
                    AExContext.Result = new JsonResult("No data was FOUND")
                    { StatusCode = 404 };
                    return;
                }
                else if (mUser.Role != "seller" && AExContext.RouteData.Values.Values.Contains("C_FUProducts"))
                {
                    AExContext.Result = new JsonResult("UnAuthorized ROLE ")
                    { StatusCode = 401 };
                    return;
                }
                else if (mUser.Role != "buyer" && AExContext.RouteData.Values.Values.Contains("C_FUser_API"))
                {
                    AExContext.Result = new JsonResult("UnAuthorized ROLE ")
                    { StatusCode = 401 };
                    return;
                }
            }
        }
    }
}
