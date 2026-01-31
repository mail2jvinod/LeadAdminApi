using System.Data;
using System.Globalization;

namespace LeadAdmin.Utilities.Extensions
{
    public static class DataTableExtensions
    {
        public static int ToInt(this DataRow row, string fieldName)
        {
            var result = default(int);

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            try
            {
                result = Convert.ToInt32(Convert.ToDouble(rawResult));
            }
            catch
            {

            }
            return result;
            //int.TryParse(rawResult, out int result);
        }

        public static int? ToIntNullable(this DataRow row, string fieldName)
        {
            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");


            int? result;
            try
            {
                result = Convert.ToInt32(Convert.ToDouble(rawResult));
            }
            catch
            {
                result = default(int?);
            }
            return result;
        }

        public static long ToLong(this DataRow row, string fieldName)
        {
            var result = default(long);
            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            try
            {
                result = Convert.ToInt64(Convert.ToDouble(rawResult));
            }
            catch
            {

            }

            //long.TryParse(rawResult, out long result);

            return result;
        }

        public static long? ToLongNullable(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            if (long.TryParse(rawResult, out long result))
            {
                return result;
            }
            else
            {
                return default(long?);
            }
        }

        public static short ToShort(this DataRow row, string fieldName)
        {
            var result = default(short);
            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            try
            {
                result = Convert.ToInt16(Convert.ToDouble(rawResult));
            }
            catch
            {

            }

            //short.TryParse(rawResult, out short result);

            return result;
        }

        public static short? ToShortNullable(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            if (short.TryParse(rawResult, out short result))
            {
                return result;
            }
            else
            {
                return default(short?);
            }
        }

        public static DateTime ToDateTime(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();

            DateTime.TryParse(rawResult, out DateTime result);

            return result;
        }
        public static string ToDateTimeFormatString(this DataRow row, string fieldName, string format = null, CultureInfo currentCultureInfo = null)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();

            DateTime.TryParse(rawResult, out DateTime result);

            if (string.IsNullOrWhiteSpace(format)) { format = "dd-MMM-yyyy"; }
            if (currentCultureInfo == null) { currentCultureInfo = new System.Globalization.CultureInfo("en-US"); }

            return result.ToString(format, currentCultureInfo);
        }

        public static bool ToBoolean(this DataRow row, string fieldName)
        {
            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();

            var result = string.Equals(rawResult, "true", StringComparison.InvariantCultureIgnoreCase) || string.Equals(rawResult, "1", StringComparison.InvariantCultureIgnoreCase);

            return result;
        }

        public static string ToStringValue(this DataRow row, string fieldName)
        {
            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();

            return rawResult;
        }

        public static bool IsBetween<T>(this T securedMarks, T fromValue, T toValue)
        {
            return Comparer<T>.Default.Compare(securedMarks, fromValue) >= 0
                && Comparer<T>.Default.Compare(securedMarks, toValue) <= 0;
        }

        public static DateTime? ToDateTimeNullable(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();

            if (DateTime.TryParse(rawResult, out DateTime result))
            {
                return result;
            }
            else
            {
                return default(DateTime?);
            }
        }

        public static decimal ToDecimal(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            decimal.TryParse(rawResult, out decimal result);

            return result;
        }

        public static decimal? ToDecimalNullable(this DataRow row, string fieldName)
        {

            var rawResult = row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString().Replace(",", "");

            if (decimal.TryParse(rawResult, out decimal result))
            {
                return result;
            }
            else
            {
                return default(decimal?);
            }
        }

        public static string ToFormattedString(this DataRow row, string fieldName)
        {
            return row[fieldName] == DBNull.Value ? "" : row[fieldName].ToString();
        }

        public static string ToWhereLongSearch(this DataTable dataTable, string searchFieldName, long value, string resultFieldName)
        {
            return (from row in dataTable.AsEnumerable()
                    where row.Field<long>(searchFieldName) == value
                    select row).First()[resultFieldName].ToString();
        }

        public static string ToWhereIntSearch(this DataTable dataTable, string searchFieldName, int value, string resultFieldName)
        {
            return (from row in dataTable.AsEnumerable()
                    where row.Field<int>(searchFieldName) == value
                    select row).First()[resultFieldName].ToString();
        }

        public static string GetString(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", int rowNo = 0, string defaultValue = "-")
        {

            var result = "";
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];
                result = dataRow[fieldName].ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "" : defaultValue;
            }

            return result;
        }

        public static string GetDateString(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", int rowNo = 0, string format = "")
        {
            if (string.IsNullOrWhiteSpace(format)) format = "dd-MMM-yyyy";

            var result = "";
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];
                result = dataRow[fieldName].ToString();
                result = DateTime.Parse(result).ToString(format);
            }
            catch (Exception)
            {

            }

            return result;
        }

        public static int GetInt(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", int defaultValue = 0, int rowNo = 0)
        {
            var result = default(int);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];

                result = ToInt(dataRow, fieldName);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static decimal GetDecimal(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", decimal defaultValue = 0, int rowNo = 0, int decimalPoints = 1)
        {
            var result = default(decimal);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];

                result = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static string GetIntString(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", string defaultValue = "-", int rowNo = 0)
        {
            var result = default(string);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];

                var resultInt = ToInt(dataRow, fieldName);
                result = resultInt.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public static string GetDecimalString(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", string defaultValue = "-", int rowNo = 0, int decimalPoints = 1)
        {
            var result = default(string);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];

                var resultDecimal = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
                result = resultDecimal.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public static bool IsExists(this Dictionary<string, System.Data.DataTable> model, string key, string fieldName, string expression = "", int rowNo = 0)
        {
            var result = default(bool);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? model[key].Rows[rowNo]
                    : model[key].Select(expression)[rowNo];

                result = (dataRow != null);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static string GetStringIfColumnExists(this DataTable dataTable, DataRow row, string fieldName)
        {
            var result = "";
            try
            {
                if (dataTable.Columns.Contains(fieldName))
                {
                    result = row[fieldName].ToString();
                }
            }
            catch (Exception)
            {
                result = "";
            }

            return result;
        }
        public static Guid GetGuidIfColumnExists(this DataTable dataTable, DataRow row, string fieldName)
        {
            var result = default(Guid);
            try
            {
                if (dataTable.Columns.Contains(fieldName))
                {
                    result = Guid.Parse(row[fieldName].ToString());
                }
            }
            catch (Exception)
            {
                result = default(Guid);
            }

            return result;
        }

        public static DateTime? GetDateTimeIfColumnExists(this DataTable dataTable, DataRow row, string fieldName)
        {
            var result = default(DateTime?);
            try
            {
                if (dataTable.Columns.Contains(fieldName))
                {
                    var rawDateTime = row[fieldName].ToString();

                    if (DateTime.TryParse(rawDateTime, out DateTime rawDate))
                    {
                        result = rawDate;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            if (result < new DateTime(1970, 1, 1))
            {
                result = null;
            }

            return result;
        }

        public static decimal? GetIntIfColumnExists(this DataTable dataTable, DataRow row, string fieldName)
        {
            var result = default(decimal?);
            try
            {
                if (dataTable.Columns.Contains(fieldName))
                {
                    var rawValue = row[fieldName].ToString();

                    if (decimal.TryParse(rawValue, out decimal rawDecimal))
                    {
                        result = rawDecimal;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result == null ? 0 : result;
        }
        public static decimal? GetDecimalIfColumnExists(this DataTable dataTable, DataRow row, string fieldName)
        {
            var result = default(decimal?);
            try
            {
                if (dataTable.Columns.Contains(fieldName))
                {
                    var rawValue = row[fieldName].ToString();

                    if (decimal.TryParse(rawValue, out decimal rawDecimal))
                    {
                        result = rawDecimal;
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result == null ? 0 : result;
        }

        public static DataTable ToDataTable(this DataRow[] rows, DataTable referenceTable)
        {
            var result = referenceTable.Clone();
            try
            {
                foreach (DataRow sourceRow in rows)
                {
                    result.ImportRow(sourceRow);
                }
            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}
