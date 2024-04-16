using FUser.CLDataAccess.EFContext;
using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDataAccess.ViewModels;
using FUser.CLDomain;
using FUser.WebAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FUser.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class C_FUAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWorkI;
        private readonly IConfiguration _configI;

        // Injection Part
        public C_FUAccountController(IUnitOfWork unitOfWorkI, IConfiguration configI)
        {
            this._unitOfWorkI = unitOfWorkI;
            this._configI = configI;
        }


        // POST : api/c_fuaccount/RegisterUser
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] VM_MURegister vmMURegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MUser UserToCheckName = await _unitOfWorkI.RP_UserI.GetByNameAsync(vmMURegister.UserName);
                    if (UserToCheckName != null)
                        return BadRequest(vmMURegister.UserName + " already exists");
                    MUser mUserToAdd = new MUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = vmMURegister.UserName,
                        Password = vmMURegister.Password,
                        Role = vmMURegister.Role
                    };
                    _unitOfWorkI.RP_UserI.CreateAsync(mUserToAdd);
                    _unitOfWorkI.Save_UOfWork();
                    // Adding the JWT Access Token
                    var SSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configI["Jwt:Key"]));
                    var SignCredentials = new SigningCredentials(SSecurityKey, SecurityAlgorithms.HmacSha256);
                    var JSToken = new JwtSecurityToken(_configI["Jwt:Issuer"],
                                                       _configI["Jwt:Issuer"],
                                                       null,
                                                       expires: DateTime.Now.AddMinutes(120),
                                                       signingCredentials: SignCredentials);
                    string SAccessToken = new JwtSecurityTokenHandler().WriteToken(JSToken);
                    // Adding data to MJwtSTokens table
                    MJwtSTokens mJSTokens = new MJwtSTokens
                    {
                        UserId = mUserToAdd.Id.ToString(),
                        JwtSToken = SAccessToken
                    };
                    _unitOfWorkI.RP_UserI.AddJwtSTokens(mJSTokens);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok(new { Id = mUserToAdd.Id.ToString(), AccessToken = SAccessToken });
                }
                else
                    return BadRequest("Invalid input");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // DELETE : api/c_fuaccount/DeleteUser/id={Id}&accessToken={AccessToken}
        [HttpDelete("DeleteUser/id={Id}&accessToken={AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> DeleteUser(Guid Id, string AccessToken)
        {
            try
            {
                MUser UserToDelete = await _unitOfWorkI.RP_UserI.GetByIDAsync(Id.ToString());
                MJwtSTokens JSTokenToDelete = await _unitOfWorkI.RP_UserI.GetJSTokenAsync(AccessToken);
                if (UserToDelete == null || JSTokenToDelete == null)
                    return NotFound("No user found to delete or no authorization");
                _unitOfWorkI.RP_UserI.DeleteAsync(Id.ToString());
                _unitOfWorkI.RP_UserI.DeleteJwtSTokens(JSTokenToDelete);
                _unitOfWorkI.Save_UOfWork();
                return Ok(UserToDelete.UserName + " is successfully deleted");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // UPDATE : api/c_fuaccount/UpdateUser/AccessToken
        [HttpPut("UpdateUser/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> UpdateUser(string AccessToken, VM_MUserUpdateRole vmUserURole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MUser UserToUpdate = await _unitOfWorkI.RP_UserI.GetByNameAsync(vmUserURole.UserName);
                    UserToUpdate.Role = vmUserURole.Role;
                    _unitOfWorkI.RP_UserI.Update(UserToUpdate);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok(UserToUpdate.UserName + " is successfully updated");
                }
                else
                    return BadRequest("Invalid input");

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // GET : api/c_fuaccount/GetUser/id={Id}&accessToken={AccessToken}
        [HttpGet]
        [Route("GetUser/id={Id}&accessToken={AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> GetUser(Guid Id, string AccessToken)
        {
            try
            {
                MUser UserToGet = await _unitOfWorkI.RP_UserI.GetByIDAsync(Id.ToString());
                return Ok(UserToGet);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }
    }
}
