using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LeadAdmin.Utilities
{
    public static class SerializationHelper
    {
        public static string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string SerializeFirstRow(DataTable value)
        {
            if (value == null || value.Rows.Count > 0)
                return JArray.FromObject(value,
                    JsonSerializer.CreateDefault(
                        new JsonSerializerSettings
                        { NullValueHandling = NullValueHandling.Ignore })).First().ToString();
            else
                return string.Empty;
        }

        public static List<string> SerializeAllRows(DataTable value)
        {
            var result = new List<string>();

            if (value != null && value.Rows.Count > 0)
            {
                for (int i = 0; i < value.Rows.Count; i++)
                {
                    string json = new JObject(
                    value.Columns.Cast<DataColumn>()
                      .Select(c => new JProperty(c.ColumnName, JToken.FromObject(value.Rows[i][c])))
                        ).ToString(Formatting.None);

                    result.Add(json);
                }
            }

            return result;
        }

        public static T Deserialize<T>(string value)
        {
            var result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
            }
            catch (System.Exception)
            {
            }
            return result;
        }
    }
}
