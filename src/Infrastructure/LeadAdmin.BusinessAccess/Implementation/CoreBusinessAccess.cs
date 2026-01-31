using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Core;
using LeadAdmin.ResourceAccess.Contracts;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;

namespace LeadAdmin.BusinessAccess.Implementation
{
    public class CoreBusinessAccess : ICoreBusinessAccess
    {
        private ICoreDataAccess coreDataAccess;
        public CoreBusinessAccess(UserContext userContext, ICoreDataAccess coreDataAccess)
        {
            this.coreDataAccess = coreDataAccess;
        }

        #region Lead Accessories
        public List<Branch> GetBranches(int branchId)
        {
            return coreDataAccess.GetBranches(branchId);
        }
        public List<Outlet> GetOutlets(int outletId, int branchId)
        {
            return coreDataAccess.GetOutlets(outletId, branchId);
        }
        public List<Category> GetCategories(int categoryId)
        {
            return coreDataAccess.GetCategories(categoryId);
        }
        public List<Brand> GetBrands(int brandId)
        {
            return coreDataAccess.GetBrands(brandId);
        }
        public List<ItemModel> GetModels(int modelId, int brandId)
        {
            return coreDataAccess.GetModels(modelId, brandId);
        }
        public List<ItemType> GetItemTypes(int branchId)
        {
            return coreDataAccess.GetItemTypes(branchId);
        }
        public List<vItem> GetProducts(int productId, int categoryId, int brandId, int modelId, int itemTypeId)
        {
            return coreDataAccess.GetProducts(productId, categoryId, brandId, modelId, itemTypeId);
        }

        #endregion
    }

}
