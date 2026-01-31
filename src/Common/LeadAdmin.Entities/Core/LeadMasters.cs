namespace LeadAdmin.Entities.Core
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PIN { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public string GSTNo { get; set; }
    }

    public class Outlet
    {
        public int OutletId { get; set; }
        public Guid UniqueId { get; set; }
        public string Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PIN { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string BranchName { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId {get;set;}
        public string Title { get; set; }
        public string ImgPath { get; set; }
    }

    public class Brand
    {
        public int BrandId { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
    }

    public class ItemModel
    {
        public int ModelId { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
    }

    public class ItemType
    {
        public int ItemsType_Id { get; set; }
        public string Title { get; set; }
        public string ItemsType_Name { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string UomId { get; set; }
        public string Pack { get; set; }
        public int MnfId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public bool IsOnline { get; set; }
        public string HSN { get; set; }
        public decimal GST { get; set; }
        public decimal MRP { get; set; }
        public decimal WSRate { get; set; }
        public decimal SRate { get; set; }
        public string FullDescription { get; set; }
        public bool Status { get; set; }
    }

    public class ItemImages
    {
        public string ImgPath { get; set; }
    }
    public class ItemColors
    {
        public string ColorName { get; set; }
    }

    public class vItem : Item
    {
        public string Uom { get; set; }
        public string Manufacturer { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string ItemTypeTitle { get; set; }
        public List<ItemImages> ItemImages { get; set; }
        public List<ItemColors> ItemColors { get; set; }
    }

}
