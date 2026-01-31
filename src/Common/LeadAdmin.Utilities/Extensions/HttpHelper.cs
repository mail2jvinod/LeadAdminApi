using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LeadAdmin.Utilities.Extensions
{
    public static class HttpHelper
    {
        public static async Task<string> Post(string requestUri, string jsonContent, Dictionary<string, string> headers, bool includeApplicationJsonHeader = true)
        {
            var response = default(string);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                HttpContent content = new StringContent(jsonContent);
                if (includeApplicationJsonHeader)
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                HttpResponseMessage result = await client.PostAsync(requestUri, content);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                {
                    var rawResponse = new
                    {
                        responseMessage = "Credits not available",
                        responseCode = "406"
                    };
                    response = SerializationHelper.Serialize(rawResponse);
                }
            }
            return response;
        }
        public static async Task<string> PostFiles(string requestUri, string dataKeyName, string jsonContent, Dictionary<string, string> headers, Dictionary<string, byte[]> files, bool includeApplicationJsonHeader = true)
        {
            var response = default(string);

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(jsonContent), name: dataKeyName);

            foreach (var item in files)
            {
                var fileStreamContent = new ByteArrayContent(item.Value);
                fileStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
                formContent.Add(fileStreamContent, name: "file", fileName: item.Key);
            }

            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                HttpResponseMessage result = await client.PostAsync(requestUri, formContent);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                {
                    var rawResponse = new
                    {
                        responseMessage = "Credits not available",
                        responseCode = "406"
                    };
                    response = SerializationHelper.Serialize(rawResponse);
                }
            }

            return response;
        }

        public static async Task<string> GetString(string requestUri, Dictionary<string, string> headers, bool includeApplicationJsonHeader = true)
        {
            var response = default(string);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();
                if (includeApplicationJsonHeader)
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                HttpResponseMessage result = await client.GetAsync(requestUri);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                {
                    var rawResponse = new
                    {
                        responseMessage = "Credits not available",
                        responseCode = "406"
                    };
                    response = SerializationHelper.Serialize(rawResponse);
                }
            }
            return response;
        }

        public static byte[] GetBytes(string requestUri, Dictionary<string, string> headers)
        {
            var response = default(byte[]);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Clear();

                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                response = client.GetByteArrayAsync(requestUri).Result;
            }
            return response;
        }
    }
}
