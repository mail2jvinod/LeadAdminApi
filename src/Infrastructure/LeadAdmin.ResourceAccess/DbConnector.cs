using LeadAdmin.Utilities.Constants;

namespace LeadAdmin.ResourceAccess
{
    public class DbConnector : BaseDbContext
    {
        public DbConnector() : base(ConfigSettings.Instance.Data.ERPConnectionString)
        {

        }

        private static DbConnector _DbConnector = default(DbConnector);
        private static object _syncObject = new object();
        public static DbConnector Instance
        {
            get
            {
                lock (_syncObject)
                {
                    if (_DbConnector == null) _DbConnector = new DbConnector();
                }
                return _DbConnector;
            }
        }

    }
}
