using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadAdmin.API.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    [AllowAnonymous]
    [DigiCollectExceptionFilter]
    public class CoreApiController : DigiCollectController
    {
        private ICoreBusinessAccess coreBusinessAccess = default(ICoreBusinessAccess);
        public CoreApiController(UserContext userContext, ICoreBusinessAccess coreBusinessAccess)
        {
            this.userContext = userContext;
            this.coreBusinessAccess = coreBusinessAccess;
        }

        #region Lead Accessories
        [Route("[action]/{branchId:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Branch>))]
        public IActionResult GetBranches(int branchId)
        {
            var result = this.coreBusinessAccess.GetBranches(branchId);
            return Ok(result);
        }
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Outlet>))]
        public IActionResult GetOutlets(int outletId, int branchId)
        {
            var result = this.coreBusinessAccess.GetOutlets(outletId, branchId);
            return Ok(result);
        }
        [Route("[action]/{categoryId:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Category>))]
        public IActionResult GetCategories(int categoryId)
        {
            var result = this.coreBusinessAccess.GetCategories(categoryId);
            return Ok(result);
        }
        [Route("[action]/{brandId:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Brand>))]
        public IActionResult GetBrands(int brandId)
        {
            var result = this.coreBusinessAccess.GetBrands(brandId);
            return Ok(result);
        }
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ItemModel>))]
        public IActionResult GetModels(int modelId, int brandId)
        {
            var result = this.coreBusinessAccess.GetModels(modelId, brandId);
            return Ok(result);
        }
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ItemType>))]
        public IActionResult GetItemTypes(int itemTypeId)
        {
            var result = this.coreBusinessAccess.GetItemTypes(itemTypeId);
            return Ok(result);
        }
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<vItem>))]
        public IActionResult GetItems(int itemId, int categoryId, int brandId, int modelId, int itemTypeId)
        {
            var result = this.coreBusinessAccess.GetProducts(itemId, categoryId, brandId, modelId, itemTypeId);
            return Ok(result);
        }
        #endregion


    }

}
