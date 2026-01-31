using LeadAdmin.Entities.Core;

namespace LeadAdmin.ResourceAccess
{
    public abstract class DataAccess
    {
        protected UserContext userContext;
        protected CollectDbContext collectDbContext;

        private static object _syncObject = new object();

        public DataAccess(UserContext userContext = null)
        {
            this.collectDbContext = new CollectDbContext();
            if (userContext != null)
            {
                this.userContext = userContext;
            }
            else
            {
                throw new ApplicationException("Invalid user context");
            }
        }

        protected void Log(string message = null, Exception ex = null)
        {
            Console.WriteLine(string.IsNullOrWhiteSpace(message) ? "Doesn't exists" : message);
            Console.WriteLine(ex == null ? "Doesn't exists" : ex.ToString());
        }
    }
}
