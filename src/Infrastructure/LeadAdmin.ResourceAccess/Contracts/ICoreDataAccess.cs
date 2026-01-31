using LeadAdmin.Entities.Core;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;

namespace LeadAdmin.ResourceAccess.Contracts
{
    public interface ICoreDataAccess
    {
        #region Lead Accessories
        List<Branch> GetBranches(int branchId);
        List<Outlet> GetOutlets(int outletId, int branchId);
        List<Category> GetCategories(int categoryId);
        List<Brand> GetBrands(int brandId);
        List<ItemModel> GetModels(int modelId, int brandId);
        List<ItemType> GetItemTypes(int branchId);
        List<vItem> GetProducts(int productId, int categoryId, int brandId, int modelId, int itemTypeId);

        #endregion
    }

}

