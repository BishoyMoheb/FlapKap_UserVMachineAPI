using FUser.CLDataAccess.RPattern_Interfaces;
using FUser.CLDataAccess.ViewModels;
using FUser.CLDomain;
using FUser.WebAPI.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUser.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C_FUser_APIController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWorkI;

        public C_FUser_APIController(IUnitOfWork unitOfWorkI)
        {
            this._unitOfWorkI = unitOfWorkI;
        }


        // GET : api/c_fuser_api/
        [HttpGet]
        public ActionResult GetActionResult()
        {
            return Ok("The FlapKap-Backend-Challenge is Loaded");
        }


        // UPDATE : api/c_fuser_api/deposit/AccessToken
        [HttpPut("deposit/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> Deposit(string AccessToken, [FromBody] VM_MUserDeposit vmMUDeposit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MUser UserToUpdate = await _unitOfWorkI.RP_UserI.GetByNameAsync(vmMUDeposit.UserName);
                    UserToUpdate.Deposit = vmMUDeposit.Deposit;
                    _unitOfWorkI.RP_UserI.Update(UserToUpdate);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok(UserToUpdate.UserName + " has successfully made a deposit");
                }
                else
                    return BadRequest("Invalid input");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // UPDATE : api/c_fuser_api/buy/AccessToken
        [HttpPut("buy/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> Buy(string AccessToken, [FromBody] VM_MProdAmounts vmMPAmounts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MProduct ProductToBuy = await _unitOfWorkI.RP_ProductI.GetByIDAsync(vmMPAmounts.ProductId);
                    MJwtSTokens mJwtSTokens = await _unitOfWorkI.RP_ProductI.GetJSTokenAsync(AccessToken);
                    MUser UserPurchasing = await _unitOfWorkI.RP_UserI.GetByIDAsync(mJwtSTokens.UserId);
                    if (ProductToBuy.Cost > UserPurchasing.Deposit)
                        return BadRequest(UserPurchasing.Deposit + " can not purchase " + ProductToBuy.Cost);
                    int[] Arr = new int[] { 5, 10, 20, 50, 100 };
                    int RemainingCost = UserPurchasing.Deposit - ProductToBuy.Cost;
                    int AmountPurchased = 1;
                    while (RemainingCost > 0)
                    {
                        RemainingCost -= ProductToBuy.Cost;
                        AmountPurchased++;
                        if (Arr.Any(a => a == RemainingCost))
                            break;
                    }
                    UserPurchasing.Deposit -= AmountPurchased * ProductToBuy.Cost;
                    ProductToBuy.AmountAvailable -= AmountPurchased;
                    _unitOfWorkI.RP_UserI.Update(UserPurchasing);
                    _unitOfWorkI.RP_ProductI.Update(ProductToBuy);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok("Total money spent " + (AmountPurchased * ProductToBuy.Cost)
                              + "\nThe product pruchased is  " + ProductToBuy.ProductName
                              + "\nThe change is " + UserPurchasing.Deposit);
                }
                else
                    return BadRequest("Invalid input");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // UPDATE : api/c_fuser_api/reset/AccessToken
        [HttpPut("reset/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> Reset(string AccessToken, [FromBody] VM_MUserDeposit vmMUDeposit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MUser UserToUpdate = await _unitOfWorkI.RP_UserI.GetByNameAsync(vmMUDeposit.UserName);
                    UserToUpdate.Deposit = vmMUDeposit.Deposit;
                    _unitOfWorkI.RP_UserI.Update(UserToUpdate);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok(UserToUpdate.UserName + " has successfully reset a deposit");
                }
                else
                    return BadRequest("Invalid input");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }
    }
}
