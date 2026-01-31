using System.Data;
using Microsoft.Data.SqlClient;

namespace LeadAdmin.ResourceAccess
{
    public class ParmsCollection : List<SqlParameter>
    {
        public ParmsCollection AddParm(string parameterName, SqlDbType dbType, object value, string typeName = null, ParameterDirection direction = ParameterDirection.Input)
        {
            var parmeter = new SqlParameter { ParameterName = parameterName, SqlDbType = dbType };

            parmeter.Value = (value != null) ? value : DBNull.Value;
            parmeter.Direction = direction;

            if (!string.IsNullOrWhiteSpace(typeName)) parmeter.TypeName = typeName;

            this.Add(parmeter);

            return this;
        }
    }
}
