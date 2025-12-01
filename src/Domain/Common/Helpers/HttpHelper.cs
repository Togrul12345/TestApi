using System.Text;
using System.Text.Json;

namespace Domain.Common.Helpers
{
    public static class HttpHelper
    {
        #region GetMethods
        public static async Task<string> GetMethodAsync(string url, Dictionary<string, string> headerCollections = null!)
        {
            string result = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                //added Header
                if (headerCollections != null)
                {
                    foreach (var header in headerCollections)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                //Exception handling 
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    result = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    result = $"HTTP_ERROR :: HttpRequestException raised! :: {ex.Message}";
                }
                catch (ArgumentException ex)
                {
                    result = $"HTTP_ERROR :: ArgumentException raised! :: {ex.Message}";
                }
                catch (Exception ex)
                {
                    result = $"HTTP_ERROR :: Exception raised! :: {ex.Message}";
                }
            }
            return result;
        }
        #endregion

        #region PostMethods
        public static async Task<T> PostMethodAsync<T>(dynamic postedData, string postUrl, string contentType, Dictionary<string, string> headerCollections = null!)
        {
            using (var httpClient = new HttpClient())
            {
                // Serialize the posted data to JSON
                var json = JsonSerializer.Serialize(postedData);
                var content = new StringContent(json, Encoding.UTF8, contentType);

                // Add headers if any
                if (headerCollections != null)
                {
                    foreach (var header in headerCollections)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                // Send the POST request
                var response = await httpClient.PostAsync(postUrl, content);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response
                if (contentType == "application/json")
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseJson)!;
                }

                return default!;
            }
        }
        #endregion
    }
}
