using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace PictureManager.Http
{
    public static class HttpHelper
    {
        public static TBodyResponse Get<TBodyResponse>(HttpClient client, Uri entityUri, out HttpStatusCode statusCode) where TBodyResponse : class
        {
            HttpResponseMessage response = client.GetAsync(entityUri).Result;

            return GetDataFromHttpResponse<TBodyResponse>(response, out statusCode);
        }

        public static TBodyResponse Get<TBodyResponse>(HttpClient client, string entityUri, out HttpStatusCode statusCode) where TBodyResponse : class
        {
            HttpResponseMessage response = client.GetAsync(entityUri).Result;

            return GetDataFromHttpResponse<TBodyResponse>(response, out statusCode);
        }

        private static TBodyResponse GetDataFromHttpResponse<TBodyResponse>(HttpResponseMessage response, out HttpStatusCode statusCode)
            where TBodyResponse : class
        {
            TBodyResponse returnObject = null;

            string responseString = response.Content.ReadAsStringAsync().Result;

            statusCode = response.StatusCode;

            try
            {
                returnObject = Deserialize<TBodyResponse>(responseString);
            }
            catch (Exception) { }

            return returnObject;
        }

        private static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
