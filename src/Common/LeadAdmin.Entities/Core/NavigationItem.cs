namespace LeadAdmin.Entities.Core
{
    public class NavigationItem
    {
        public NavigationItem()
        {
            this.ChildItems = new List<NavigationItem>();
        }

        public Guid NavigationUid { get; set; }

        public string Title { get; set; }
        public string NavigationPath { get; set; }
        public string DependentOn { get; set; }
        public string RedirectPath { get; set; }

        public int LevelId { get; set; }
        public Guid ParentUid { get; set; }
        public string ImagePath { get; set; }
        public int SortId { get; set; }

        public string LocalStorageKey { get; set; }
        public string PageDataGridKey { get; set; }
        public string ModulesList { get; set; }
        public bool IsFavorite { get; set; }

        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsCopy { get; set; }
        public bool IsScan { get; set; }

        public int MaxRows { get; set; }

        public bool hasChildren { get; set; }

        public List<NavigationItem> ChildItems { get; set; }
    }
}
