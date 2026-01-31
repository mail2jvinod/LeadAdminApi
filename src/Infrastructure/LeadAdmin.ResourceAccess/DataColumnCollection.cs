using System;
using System.Collections.Generic;
using System.Data;

namespace LeadAdmin.ResourceAccess
{
    public class DataColumnCollection : List<DataColumn>
    {
        public DataColumnCollection AddColumn(string columnName, DbColumnType dataType)
        {
            var parmeter = default(DataColumn);
            switch (dataType)
            {
                case DbColumnType.Int:
                    parmeter = new DataColumn(columnName, typeof(int));
                    break;
                case DbColumnType.Short:
                    parmeter = new DataColumn(columnName, typeof(short));
                    break;
                case DbColumnType.Byte:
                    parmeter = new DataColumn(columnName, typeof(byte));
                    break;
                case DbColumnType.Decimal:
                    parmeter = new DataColumn(columnName, typeof(decimal));
                    break;
                case DbColumnType.String:
                    parmeter = new DataColumn(columnName, typeof(string));
                    break;
                case DbColumnType.Bool:
                    parmeter = new DataColumn(columnName, typeof(bool));
                    break;
                case DbColumnType.DateTime:
                    parmeter = new DataColumn(columnName, typeof(DateTime));
                    break;
                case DbColumnType.Char:
                    parmeter = new DataColumn(columnName, typeof(Char));
                    break;
                case DbColumnType.Long:
                    parmeter = new DataColumn(columnName, typeof(long));
                    break;
                case DbColumnType.UniqueIdentifier:
                    parmeter = new DataColumn(columnName, typeof(Guid));
                    break;
                case DbColumnType.Time:
                    parmeter = new DataColumn(columnName, typeof(TimeSpan));
                    break;
                default:
                    break;
            }
            this.Add(parmeter);
            return this;
        }
    }

    public enum DbColumnType
    {
        Int,
        Short,
        Byte,
        Decimal,
        String,
        Bool,
        DateTime,
        Char,
        Long,
        UniqueIdentifier,
        Time

    }
}
