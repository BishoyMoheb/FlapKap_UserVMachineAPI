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
    public class C_FUProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWorkI;

        public C_FUProductsController(IUnitOfWork unitOfWorkI)
        {
            this._unitOfWorkI = unitOfWorkI;
        }


        // POST : api/c_fuproducts/addproduct/AccessToken
        [HttpPost("addproduct/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> AddProduct(string AccessToken, [FromBody] VM_MProducts vmMProducts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MProduct ProductToCheckName = await _unitOfWorkI.RP_ProductI.GetByNameAsync(vmMProducts.ProductName);
                    if (ProductToCheckName != null)
                        return BadRequest("This Product " + vmMProducts.ProductName + " already exists");
                    else
                    {
                        MProduct mProduct = new MProduct()
                        {
                            ProductId = "P_ID_000_" + (_unitOfWorkI.RP_ProductI.GetPIDCount() + 1).ToString(),
                            ProductName = vmMProducts.ProductName,
                            AmountAvailable = vmMProducts.AmountAvailable,
                            Cost = vmMProducts.Cost,
                            SellerId = vmMProducts.SellerId,
                            UserName = vmMProducts.UserName
                        };
                        _unitOfWorkI.RP_ProductI.CreateAsync(mProduct);
                        _unitOfWorkI.Save_UOfWork();
                        return Ok();
                    }
                }
                else
                    return BadRequest("Invalid input");
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // DELETE : api/c_fuproducts/deleteproduct/productName={ProductName}&accessToken={AccessToken}
        [HttpDelete("deleteproduct/productId={ProductId}&accessToken={AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> DeleteProduct(string ProductId, string AccessToken)
        {
            try
            {
                MProduct ProductToDelete = await _unitOfWorkI.RP_ProductI.GetByIDAsync(ProductId);
                if (ProductToDelete == null)
                    return NotFound("Can not delete " + ProductToDelete.ProductName + " as it doesn't exists");
                else
                {
                    _unitOfWorkI.RP_ProductI.DeleteAsync(ProductToDelete.ProductId);
                    _unitOfWorkI.Save_UOfWork();
                    return Ok(ProductToDelete.ProductName + " is deleted successfully.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // UPDATE : api/c_fuproducts/updateproduct/AccessToken
        [HttpPut("updateproduct/{AccessToken}")]
        [Custom_Authorize]
        public async Task<IActionResult> UpdateProduct(string AccessToken, [FromBody] VM_MProducts vmMProducts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MProduct ProductToUpdate = await _unitOfWorkI.RP_ProductI.GetByNameAsync(vmMProducts.ProductName);
                    if (ProductToUpdate == null)
                        return NotFound("Can not update " + vmMProducts.ProductName + " as it doesn't exists");
                    else
                    {
                        ProductToUpdate.ProductName = vmMProducts.ProductName;
                        ProductToUpdate.AmountAvailable = vmMProducts.AmountAvailable;
                        ProductToUpdate.Cost = vmMProducts.Cost;
                        ProductToUpdate.SellerId = vmMProducts.SellerId;
                        _unitOfWorkI.RP_ProductI.Update(ProductToUpdate);
                        _unitOfWorkI.Save_UOfWork();
                        return Ok(ProductToUpdate.ProductName + " is updated");
                    }
                }
                else
                    return BadRequest("Invalid input");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }


        // GET : api/c_fuproducts/getproduct/productName={ProductName}&accessToken={AccessToken}
        [HttpGet("getproduct/productId={ProductId}&accessToken={AccessToken}")]
        public async Task<IActionResult> GetProduct(string ProductId, string AccessToken)
        {
            try
            {
                MJwtSTokens JSTokenToGet = await _unitOfWorkI.RP_ProductI.GetJSTokenAsync(AccessToken);
                if (JSTokenToGet == null)
                    return new JsonResult("UnAuthorized for accessing data") { StatusCode = 401 };
                MProduct ProductToGet = await _unitOfWorkI.RP_ProductI.GetByIDAsync(ProductId);
                if (ProductToGet == null)
                    return NotFound("Can not get this product as it doesn't exists");
                else
                    return Ok(ProductToGet);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }
    }
}
