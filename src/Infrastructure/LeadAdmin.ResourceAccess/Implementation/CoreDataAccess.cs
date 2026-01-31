using LeadAdmin.Entities.Core;
using LeadAdmin.ResourceAccess.Contracts;

namespace LeadAdmin.ResourceAccess.Implementation
{
    public class CoreDataAccess : DataAccess, ICoreDataAccess
    {
        public CoreDataAccess(UserContext userContext) : base(userContext)
        {
            
        }

        #region Lead Accessories
        public List<Branch> GetBranches(int branchId)
        {
            var result = this.collectDbContext.Branch($"(0 = {branchId} Or BranchId = {branchId})");
            return result;
        }
        public List<Outlet> GetOutlets(int outletId, int branchId)
        {
            var result = this.collectDbContext.ExecuteQuery_ToList<Outlet>($"select BranchName,O.* from dbo.Outlet O Inner Join Branch B on B.BranchId=O.BranchId Where (0 = {outletId} Or O.OutletId = {outletId}) and (0 = {branchId} Or O.BranchId={branchId})");
            return result;
        }
        public List<Category> GetCategories(int categoryId)
        {
            var result = this.collectDbContext.Category($"(0 = {categoryId} Or CategoryId = {categoryId})");
            return result;
        }
        public List<Brand> GetBrands(int brandId)
        {
            var result = this.collectDbContext.Brand($"(0 = {brandId} Or BrandId = {brandId})");
            return result;
        }
        public List<ItemModel> GetModels(int modelId, int brandId)
        {
            var result = this.collectDbContext.ExecuteQuery_ToList<ItemModel>($"select B.Title Brand,M.* from dbo.Model M Inner Join Brand B on B.BrandId=M.BrandId Where (0 = {modelId} Or M.ModelId = {modelId}) and (0 = {brandId} Or B.BrandId={brandId})");
            return result;
        }
        public List<ItemType> GetItemTypes(int itemTypeId)
        {
            var result = this.collectDbContext.ItemType($"(0 = {itemTypeId} Or ItemsType_Id = {itemTypeId})");
            return result;
        }
        public List<vItem> GetProducts(int productId, int categoryId, int brandId, int modelId, int itemTypeId)
        {
            var qryCond = categoryId == 0 ? "" : $" and CatgId = {categoryId}";
            qryCond += brandId == 0 ? "" : $" and BrandId = {brandId}";
            qryCond += modelId == 0 ? "" : $" and ModelId = {modelId}";
            qryCond += itemTypeId == 0 ? "" : $" and ItemTypeId = {itemTypeId}";

            var result = this.collectDbContext.vItem($"(0 = {productId} Or ItemId = {productId})" + qryCond);
            return result;
        }
        public List<ItemImages> GetProductImages(int productId)
        {
            var result = this.collectDbContext.ItemImages($"(0 = {productId} Or ItemId = {productId})");
            return result;
        }
        public List<ItemColors> GetProductColors(int productId)
        {
            var result = this.collectDbContext.ItemColors($"(0 = {productId} Or ItemId = {productId})");
            return result;
        }
        #endregion

    }
}
