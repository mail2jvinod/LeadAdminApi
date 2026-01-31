using LeadAdmin.Entities.Core;
using LeadAdmin.Utilities.Constants;

namespace LeadAdmin.ResourceAccess
{
    public class CollectDbContext : BaseDbContext
    {
        public CollectDbContext() : base(ConfigSettings.Instance.Data.ERPConnectionString)
        {

        }
        public List<Branch> Branch(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<Branch>(DbTableNames.Branch, whereCondition, orderBy);
        }
        public List<Outlet> Outlet(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<Outlet>(DbTableNames.Outlet, whereCondition, orderBy);
        }
        public List<Category> Category(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<Category>(DbTableNames.Category, whereCondition, orderBy);
        }
        public List<Brand> Brand(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<Brand>(DbTableNames.Brand, whereCondition, orderBy);
        }
        public List<ItemModel> ItemModl(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<ItemModel>(DbTableNames.ItemModel, whereCondition, orderBy);
        }
        public List<ItemType> ItemType(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<ItemType>(DbTableNames.ItemType, whereCondition, orderBy);
        }
        public List<vItem> vItem(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<vItem>(DbTableNames.vItem, whereCondition, orderBy);
        }
        public List<ItemImages> ItemImages(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<ItemImages>(DbTableNames.ItemImages, whereCondition, orderBy);
        }
        public List<ItemColors> ItemColors(string whereCondition = "", string orderBy = "")
        {
            return this.GetTable<ItemColors>(DbTableNames.ItemColors, whereCondition, orderBy);
        }



    }
}


public class DbTableNames
{

    public const string Branch = "dbo.Branch";
    public const string Outlet = "dbo.Outlet";
    public const string Category = "dbo.Category";
    public const string Brand = "dbo.Brand";
    public const string ItemModel = "dbo.Model";
    public const string ItemType = "dbo.ItemsType";
    public const string vItem = "dbo.vItem";
    public const string ItemImages = "dbo.ItemVsImgPath";
    public const string ItemColors = "dbo.ItemVsColor";
}