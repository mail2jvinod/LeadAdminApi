using LeadAdmin.Utilities.Helpers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace LeadAdmin.Utilities.Extensions
{
    public static class ExcelHelper
    {
        private static string GetFormattedValue(ICell cell, DataFormatter dataFormatter, IFormulaEvaluator formulaEvaluator)
        {
            string returnValue = string.Empty;
            if (cell != null)
            {
                try
                {
                    // Get evaluated and formatted cell value
                    returnValue = dataFormatter.FormatCellValue(cell, formulaEvaluator);
                }
                catch
                {
                    // When failed in evaluating the formula, use stored values instead...
                    // and set cell value for reference from formulae in other cells...
                    if (cell.CellType == CellType.Formula)
                    {
                        switch (cell.CachedFormulaResultType)
                        {
                            case CellType.String:
                                returnValue = cell.StringCellValue;
                                cell.SetCellValue(cell.StringCellValue);
                                break;
                            case CellType.Numeric:
                                returnValue = dataFormatter.FormatRawCellContents(cell.NumericCellValue, 0, cell.CellStyle.GetDataFormatString());
                                cell.SetCellValue(cell.NumericCellValue);
                                break;
                            case CellType.Boolean:
                                returnValue = cell.BooleanCellValue.ToString();
                                cell.SetCellValue(cell.BooleanCellValue);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        returnValue = cell.ToString();
                    }
                }
            }

            return (returnValue ?? string.Empty).Trim();
        }

        public static void CreateExcel(this DataTable dataTable, int organizationId, string fileName, bool hideFirstRow = false)
        {
            var wb = new XSSFWorkbook();
            var sheet = wb.CreateSheet();

            var headerRow = sheet.CreateRow(0); //To add a row in the table
            foreach (DataColumn column in dataTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);

            int rowIndex = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                var dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dataTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            using (var memoryStream = new MemoryStream())
            {
                wb.Write(memoryStream);
                memoryStream.ToArray().SaveFile(fileName, (short)organizationId, 0);
            }

        }

        #region Read Excel
        public static DataSet ReadExcel(MemoryStream memoryStream, int rowPosition = 0, bool singleSheet = true, Dictionary<string, int> skipRows = null)
        {
            DataFormatter dataFormatter;
            IFormulaEvaluator formulaEvaluator;
            IWorkbook workbook;
            var initialRowPosition = rowPosition;

            workbook = new XSSFWorkbook(memoryStream);
            dataFormatter = new DataFormatter(CultureInfo.InvariantCulture);
            formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);

            var dataSet = new DataSet();
            for (int r = 0; r < (singleSheet ? 1 : workbook.NumberOfSheets); r++)
            {
                ISheet sheet = workbook.GetSheetAt(r); // zero-based index of your target sheet
                DataTable dt = new DataTable(sheet.SheetName);

                if (skipRows != null && skipRows.ContainsKey(sheet.SheetName.ToLower().Trim()))
                {
                    rowPosition = skipRows[sheet.SheetName.ToLower().Trim()];
                }
                else
                {
                    rowPosition = initialRowPosition;
                }

                // write header row
                IRow headerRow = sheet.GetRow(rowPosition);
                var columnNames = new List<string>();
                var columnIndex = 0;
                foreach (ICell headerCell in headerRow)
                {
                    var columnName = headerCell.ToString().Trim();
                    if (columnNames.Exists(c => string.Equals(c, columnName, StringComparison.OrdinalIgnoreCase)))
                    {
                        columnName = "c" + columnIndex;
                    };
                    dt.Columns.Add(columnName);
                    columnNames.Add(columnName);
                    columnIndex++;
                }

                // write the rest
                for (int row = rowPosition + 1; row <= sheet.LastRowNum; row++)
                {
                    DataRow dataRow = dt.NewRow();

                    if (sheet.GetRow(row) != null)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dataRow[i] = GetFormattedValue(sheet.GetRow(row).GetCell(i, MissingCellPolicy.RETURN_NULL_AND_BLANK), dataFormatter, formulaEvaluator);
                        }
                    }

                    dt.Rows.Add(dataRow);
                    dt.AcceptChanges();
                }
                dataSet.Tables.Add(dt);
            }
            return dataSet;
        }
        public static DataSet ReadExcel(string filePath, int rowPosition = 0, bool singleSheet = true, Dictionary<string, int> skipRows = null)
        {
            DataFormatter dataFormatter;
            IFormulaEvaluator formulaEvaluator;
            IWorkbook workbook;
            var initialRowPosition = rowPosition;

            var uriRequest = System.Net.WebRequest.Create(filePath);
            using (var stream = uriRequest.GetResponse().GetResponseStream())
            {
                workbook = new XSSFWorkbook(stream);
                dataFormatter = new DataFormatter(CultureInfo.InvariantCulture);
                formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);
            }

            var dataSet = new DataSet();
            for (int r = 0; r < (singleSheet ? 1 : workbook.NumberOfSheets); r++)
            {
                ISheet sheet = workbook.GetSheetAt(r); // zero-based index of your target sheet
                DataTable dt = new DataTable(sheet.SheetName);

                if (skipRows != null && skipRows.ContainsKey(sheet.SheetName.ToLower().Trim()))
                {
                    rowPosition = skipRows[sheet.SheetName.ToLower().Trim()];
                }
                else
                {
                    rowPosition = initialRowPosition;
                }

                // write header row
                IRow headerRow = sheet.GetRow(rowPosition);
                var columnNames = new List<string>();
                var columnIndex = 0;
                foreach (ICell headerCell in headerRow)
                {
                    var columnName = headerCell.ToString().Trim();
                    if (columnNames.Exists(c => string.Equals(c, columnName, StringComparison.OrdinalIgnoreCase)))
                    {
                        columnName = "c" + columnIndex;
                    };
                    dt.Columns.Add(columnName);
                    columnNames.Add(columnName);
                    columnIndex++;
                }

                // write the rest
                for (int row = rowPosition + 1; row <= sheet.LastRowNum; row++)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (sheet.GetRow(row) == null) continue;
                        dataRow[i] = GetFormattedValue(sheet.GetRow(row).GetCell(i, MissingCellPolicy.RETURN_NULL_AND_BLANK), dataFormatter, formulaEvaluator);
                    }
                    dt.Rows.Add(dataRow);
                    dt.AcceptChanges();
                }
                dataSet.Tables.Add(dt);
            }
            return dataSet;
        }
        public static List<string> ReadExcelDataByRow(MemoryStream memoryStream, int rowNumber = 0)
        {
            DataFormatter dataFormatter;
            IFormulaEvaluator formulaEvaluator;
            IWorkbook workbook;

            workbook = new XSSFWorkbook(memoryStream);
            dataFormatter = new DataFormatter(CultureInfo.InvariantCulture);
            formulaEvaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);

            ISheet sheet = workbook.GetSheetAt(0); // zero-based index of your target sheet
            var result = new List<string>();

            // write header row
            IRow headerRow = sheet.GetRow(rowNumber);
            var columnIndex = 0;

            foreach (ICell headerCell in headerRow)
            {
                result.Add(GetFormattedValue(sheet.GetRow(rowNumber).GetCell(columnIndex, MissingCellPolicy.RETURN_NULL_AND_BLANK), dataFormatter, formulaEvaluator));
                columnIndex++;
            }

            return result;
        }
        #endregion

        #region Data Tables
        public static DataTable ToDataTable<T>(this List<T> items, bool skipLogColumns = false)
            where T : class, new()
        {
            var logColumns = new List<string> { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                if (skipLogColumns && logColumns.Contains(prop.Name, StringComparer.OrdinalIgnoreCase)) continue;

                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[dataTable.Columns.Count];
                for (int i = 0; i < Props.Length; i++)
                {
                    if (skipLogColumns && logColumns.Contains(Props[i].Name, StringComparer.OrdinalIgnoreCase)) continue;

                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public static DataTable ToDataTable<T>(this List<T> items, DataTable dataTable) where T : new()
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (T item in items)
            {
                var dataRow = dataTable.NewRow();
                for (int i = 0; i < Props.Length; i++)
                {
                    if (dataTable.Columns.Contains(Props[i].Name))
                    {
                        dataRow[Props[i].Name] = item;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }
            dataTable.AcceptChanges();
            return dataTable;
        }
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IDictionary<Type, ICollection<PropertyInfo>> _Properties = new Dictionary<Type, ICollection<PropertyInfo>>();

            try
            {
                var tableColumns = new List<string>();
                foreach (DataColumn item in table.Columns)
                {
                    tableColumns.Add(item.ColumnName);
                }

                var objType = typeof(T);
                ICollection<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                var list = new List<T>(table.Rows.Count);

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    foreach (var prop in properties)
                    {
                        if (!tableColumns.Contains(prop.Name)) continue;

                        try
                        {
                            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = row[prop.Name] == DBNull.Value ? null : Convert.ChangeType(row[prop.Name], propType);

                            prop.SetValue(obj, safeValue, null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return new List<T>();
            }
        }
        public static T ToFirstOrDefault<T>(this DataTable table) where T : new()
        {
            IDictionary<Type, ICollection<PropertyInfo>> _Properties = new Dictionary<Type, ICollection<PropertyInfo>>();

            try
            {
                var tableColumns = new List<string>();
                foreach (DataColumn item in table.Columns)
                {
                    tableColumns.Add(item.ColumnName);
                }

                var objType = typeof(T);
                ICollection<PropertyInfo> properties;

                lock (_Properties)
                {
                    if (!_Properties.TryGetValue(objType, out properties))
                    {
                        properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                        _Properties.Add(objType, properties);
                    }
                }

                if (table.Rows.Count == 0)
                {
                    return default(T);
                }

                var result = new T();

                var row = table.Rows[0];

                foreach (var prop in properties)
                {
                    if (!tableColumns.Contains(prop.Name)) continue;

                    try
                    {
                        var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        var safeValue = row[prop.Name] == DBNull.Value ? null : Convert.ChangeType(row[prop.Name], propType);

                        prop.SetValue(result, safeValue, null);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }

                return result;
            }
            catch
            {
                return default(T);
            }
        }
        #endregion

    }
}
