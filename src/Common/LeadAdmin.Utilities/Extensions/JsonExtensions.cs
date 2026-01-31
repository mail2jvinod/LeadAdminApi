using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;

namespace LeadAdmin.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static string GetFieldFromJson(this string jsonFeed, string fieldName, string defaultValue = null)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            var result = parmObject[fieldName] == null ? null : parmObject[fieldName].ToString();

            if (result == null && defaultValue != null)
            {
                result = defaultValue;
            }

            return result;
        }

        public static string UpdateIntFieldInJson(this string jsonFeed, string fieldName, int value)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            parmObject[fieldName] = value;

            return JsonConvert.SerializeObject(parmObject);
        }

        public static string UpdateDecimalFieldInJson(this string jsonFeed, string fieldName, decimal value)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            parmObject[fieldName] = value;

            return JsonConvert.SerializeObject(parmObject);
        }

        public static string RemoveFieldFromJson(this string jsonFeed, string fieldName)
        {
            var parmObject = JObject.Parse(jsonFeed);
            var result = parmObject[fieldName] == null ? null : parmObject[fieldName].ToString();

            if (result != null)
            {
                parmObject.Property(fieldName).Remove();
                return JsonConvert.SerializeObject(parmObject);
            }

            return jsonFeed;
        }

        public static bool IsExists(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);

            return ((parmObject == null || parmObject[fieldName] == null) ? false : true);
        }

        public static string GetDocNo(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? null : ((string)parmObject[fieldName].ToString());
        }

        public static DateTime GetDateTime(this string jsonFeed, string fieldName, short timezoneOffset = 0)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            DateTime? result = parmObject[fieldName] == null ? default(DateTime) : DateTime.Parse(parmObject[fieldName].ToString());

            return (timezoneOffset == default(short) || result == null)
                ? (result ?? DateTime.Now)
                : result.Value.ToServerTime(timezoneOffset);
        }

        public static DateTime GetDate(this string jsonFeed, string fieldName, short timezoneOffset = 0, DateTime? defafaultValue = null)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            DateTime? result = parmObject[fieldName] == null ? default(DateTime) : DateTime.Parse(parmObject[fieldName].ToString());

            if (result == default(DateTime) && defafaultValue.HasValue)
            {
                return defafaultValue.Value;
            }

            return (timezoneOffset == default(short) || result == null)
                ? (result ?? DateTime.Now).Date
                : result.Value.ToServerTime(timezoneOffset).Date;
        }

        public static DateTime? TryDateTime(this string jsonFeed, string fieldName, short timezoneOffset = 0)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            DateTime? result = parmObject[fieldName] == null ? default(DateTime?) : DateTime.Parse(parmObject[fieldName].ToString());

            return (timezoneOffset == default(short) || result == null)
                ? result
                : result.Value.ToServerTime(timezoneOffset);
        }

        public static Guid? TryGuid(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return (parmObject[fieldName] == null || string.IsNullOrWhiteSpace(parmObject[fieldName].ToString())) ? default(Guid?) : Guid.Parse(parmObject[fieldName].ToString());
        }

        public static Guid GetGuidDefault(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return (parmObject[fieldName] == null || string.IsNullOrWhiteSpace(parmObject[fieldName].ToString())) ? Guid.NewGuid() : Guid.Parse(parmObject[fieldName].ToString());
        }

        public static int? TryInt(this string jsonFeed, string fieldName, int? defaultValue = null)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            string inputValue = parmObject[fieldName] == null ? default(string) : parmObject[fieldName].ToString();
            if (!int.TryParse(inputValue, out var result))
            {
                if (defaultValue.HasValue)
                {
                    result = defaultValue.Value;
                }
                else
                {
                    return default(int?);
                }
                Console.WriteLine(inputValue);
            }
            return result;
        }

        public static long? TryLong(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(long?) : long.Parse(parmObject[fieldName].ToString());
        }

        public static bool? TryBool(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(bool?) : bool.Parse(parmObject[fieldName].ToString());
        }

        public static bool GetBool(this string jsonFeed, string fieldName)
        {
            var result = default(bool);
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            if (parmObject[fieldName] == null)
            {
                result = false;
            }
            else
            {
                bool.TryParse(parmObject[fieldName].ToString(), out result);
            }
            return result;
        }

        public static short GetShort(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(short) : short.Parse(parmObject[fieldName].ToString());
        }

        public static int GetInt(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(int) : int.Parse(parmObject[fieldName].ToString());
        }

        public static long GetLong(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(long) : long.Parse(parmObject[fieldName].ToString());
        }

        public static decimal GetDecimal(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(decimal) : decimal.Parse(parmObject[fieldName].ToString(), System.Globalization.NumberStyles.Float);
        }

        public static decimal? TryDecimal(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(decimal?) : decimal.Parse(parmObject[fieldName].ToString(), System.Globalization.NumberStyles.Float);
        }

        public static string ConcatenateRefFields(this string jsonFeed, Guid uniqueId)
        {
            var parmObject = (JObject)JsonConvert.DeserializeObject(jsonFeed);
            parmObject["UniqueId"] = uniqueId;

            return SerializationHelper.Serialize<JObject>(parmObject);
        }
        public static string GetARAPREFNO(this string jsonFeed, string fieldName)
        {
            var parmObject = JsonConvert.DeserializeObject<dynamic>(jsonFeed);
            return parmObject[fieldName] == null ? default(string) : parmObject[fieldName].ToString();
        }
        public static List<string> GetFieldValues(this DataTable dataTable, string columnName = "ColName", List<string> extraFields = null)
        {
            var result = dataTable.AsEnumerable()
                    .Select(r => r.Field<string>("ColName"))
                    .ToList();

            if (extraFields != null && extraFields.Count > 0)
            {
                result.AddRange(extraFields);
            }

            return result;
        }

        public static JObject RemovePhyscialFields(this JObject jsonObject, List<string> fields)
        {
            foreach (var field in fields)
            {
                if (jsonObject[field] != null) jsonObject.Property(field).Remove();
            }
            return jsonObject;
        }

        public static string GetFieldFromJObject(this JObject jsonObject, string fieldName)
        {
            return (jsonObject[fieldName] != null) ? jsonObject[fieldName].ToString() : null;
        }

    }
}
