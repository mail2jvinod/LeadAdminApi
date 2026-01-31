using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LeadAdmin.Utilities.Extensions
{
    public class PrintModel : Dictionary<string, System.Data.DataTable>
    {
        public static PrintModel ToPrintModel(Dictionary<string, System.Data.DataTable> source)
        {
            PrintModel result = new PrintModel();

            if (source != null)
            {
                foreach (var kvp in source)
                {
                    result.Add(kvp.Key, kvp.Value);
                }
            }

            return result;
        }
        public string GetString(string key, string fieldName, string expression = "", int rowNo = 0, string defaultValue = "-")
        {

            var result = "";
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];
                result = dataRow[fieldName].ToString();
            }
            catch (Exception ex)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "" : defaultValue;
            }

            return result;
        }

        public string GetDateString(string key, string fieldName, string expression = "", int rowNo = 0)
        {
            var result = "";
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];
                result = dataRow[fieldName].ToString();
                result = DateTime.Parse(result).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {

            }

            return result;
        }

        public string GetDateStringFormat(string key, string fieldName, string expression = "", int rowNo = 0, string format = "dd-MMM-yyyy HH:mm:ss")
        {
            if (string.IsNullOrWhiteSpace(format)) format = "dd-MMM-yyyy HH:mm:ss";

            var result = "";
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];
                result = dataRow[fieldName].ToString();
                result = DateTime.Parse(result).ToString(format);
            }
            catch (Exception)
            {

            }

            return result;
        }

        public int GetInt(string key, string fieldName, string expression = "", int defaultValue = 0, int rowNo = 0)
        {
            var result = default(int);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];

                result = ToInt(dataRow, fieldName);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public decimal GetDecimal(string key, string fieldName, string expression = "", decimal defaultValue = 0, int rowNo = 0, int decimalPoints = 1)
        {
            var result = default(decimal);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];

                result = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public string GetIntString(string key, string fieldName, string expression = "", string defaultValue = "-", int rowNo = 0)
        {
            var result = default(string);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];

                var resultInt = ToInt(dataRow, fieldName);
                result = resultInt.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public string GetDecimalString(string key, string fieldName, string expression = "", string defaultValue = "-", int rowNo = 0, int decimalPoints = 1)
        {
            var result = default(string);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];

                var resultDecimal = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
                result = resultDecimal.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public bool IsExists(string key, string fieldName, string expression = "", int rowNo = 0)
        {
            var result = default(bool);
            try
            {
                var dataRow = string.IsNullOrWhiteSpace(expression)
                    ? this[key].Rows[rowNo]
                    : this[key].Select(expression)[rowNo];

                result = (dataRow != null);
            }
            catch (Exception)
            {
            }

            return result;
        }

        public int ToInt(DataRow row, string fieldName)
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

        public DataRow[] ToDataRows(string key)
        {
            try
            {
                return this[key].Rows.Cast<DataRow>().ToArray();
            }
            catch (Exception)
            {
                return new List<DataRow>().ToArray();
            }
        }

        public DataRow[] ToDataRowsFromIndex(string key, int fromRowIndex, int takeCount)
        {
            try
            {
                var totalRows = this[key].Rows.Cast<DataRow>().ToArray();
                return totalRows.Skip(fromRowIndex + 1).Take(takeCount).ToArray();
            }
            catch (Exception)
            {
                return new List<DataRow>().ToArray();
            }
        }

        public decimal ToDataRowsFromIndex_Total(string key, int fromRowIndex, int takeCount, string fieldName)
        {
            var result = default(decimal);
            try
            {
                var rows = ToDataRowsFromIndex(key, fromRowIndex, takeCount);
                foreach (var dataRow in rows)
                {
                    result += DataRow_GetDecimal(dataRow, fieldName, 0, 2);
                }
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        public string DataRow_GetString(DataRow dataRow, string fieldName, string defaultValue = "-")
        {

            var result = "";
            try
            {
                result = dataRow[fieldName].ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "" : defaultValue;
            }

            return result;
        }

        public string DataRow_GetDateString(DataRow dataRow, string fieldName)
        {
            var result = "";
            try
            {
                result = dataRow[fieldName].ToString();
                result = DateTime.Parse(result).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {

            }

            return result;
        }

        public int DataRow_GetInt(DataRow dataRow, string fieldName, int defaultValue = 0)
        {
            var result = default(int);
            try
            {
                result = ToInt(dataRow, fieldName);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public decimal DataRow_GetDecimal(DataRow dataRow, string fieldName, decimal defaultValue = 0, int decimalPoints = 1)
        {
            var result = default(decimal);
            try
            {
                result = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public string DataRow_GetIntString(DataRow dataRow, string fieldName, string defaultValue = "-")
        {
            var result = default(string);
            try
            {
                var resultInt = ToInt(dataRow, fieldName);
                result = resultInt.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public string DataRow_GetDecimalString(DataRow dataRow, string fieldName, string defaultValue = "-", int decimalPoints = 1)
        {
            var result = default(string);
            try
            {
                var resultDecimal = decimal.Round(decimal.Parse(dataRow[fieldName].ToString()), decimalPoints, MidpointRounding.AwayFromZero);
                result = resultDecimal.ToString();
            }
            catch (Exception)
            {
                result = (string.IsNullOrWhiteSpace(defaultValue)) ? "-" : defaultValue;
            }

            return result;
        }

        public bool DataRow_IsExists(DataRow dataRow)
        {
            var result = default(bool);
            try
            {
                result = (dataRow != null);
            }
            catch (Exception)
            {
            }

            return result;
        }

    }
}
