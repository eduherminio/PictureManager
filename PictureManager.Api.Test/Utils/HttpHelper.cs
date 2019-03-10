using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace PictureManager.Api.Test.Utils
{
    public static class HttpHelper
    {
        public static TBodyResponse Get<TBodyResponse>(HttpClient client, string entityUri, out HttpStatusCode statusCode) where TBodyResponse : class
        {
            HttpResponseMessage response = client.GetAsync(entityUri).Result;

            return GetDataFromHttpResponse<TBodyResponse>(response, out statusCode);
        }

        public static TBodyResponse Post<TBodyRequest, TBodyResponse>(
            HttpClient client, string entityUri, TBodyRequest dtoToSend, out HttpStatusCode statusCode)
            where TBodyRequest : class
            where TBodyResponse : class
        {
            HttpResponseMessage response = PostInternal(client, entityUri, dtoToSend);

            return GetDataFromHttpResponse<TBodyResponse>(response, out statusCode);
        }

        private static HttpResponseMessage PostInternal<TBodyRequest>(HttpClient client, string entityUri, TBodyRequest dtoToSend) where TBodyRequest : class
        {
            ByteArrayContent byteContent = Serialize(dtoToSend);

            return client.PostAsync(entityUri, byteContent).Result;
        }

        public static TBody Post<TBody>(HttpClient client, string entityUri, TBody dtoToSend, out HttpStatusCode statusCode) where TBody : class
        {
            return Post<TBody, TBody>(client, entityUri, dtoToSend, out statusCode);
        }

        public static HttpStatusCode Put<TBodyRequest>(HttpClient client, string entityUri, TBodyRequest dtoToSend) where TBodyRequest : class
        {
            return PutInternal(client, entityUri, dtoToSend).StatusCode;
        }

        private static HttpResponseMessage PutInternal<TBodyRequest>(HttpClient client, string entityUri, TBodyRequest dtoToSend) where TBodyRequest : class
        {
            ByteArrayContent byteContent = Serialize(dtoToSend);

            return client.PutAsync(entityUri, byteContent).Result;
        }

        public static TBody Put<TBody>(HttpClient client, string entityUri, TBody dtoToSend, out HttpStatusCode statusCode) where TBody : class
        {
            return Put<TBody, TBody>(client, entityUri, dtoToSend, out statusCode);
        }

        public static TBodyResponse Put<TBodyRequest, TBodyResponse>(HttpClient client, string entityUri, TBodyRequest dtoToSend, out HttpStatusCode statusCode)
            where TBodyRequest : class
            where TBodyResponse : class
        {
            HttpResponseMessage response = PutInternal(client, entityUri, dtoToSend);

            return GetDataFromHttpResponse<TBodyResponse>(response, out statusCode);
        }

        public static HttpStatusCode Delete<TBodyRequest>(HttpClient client, string entityUri, TBodyRequest entitiesToDelete) where TBodyRequest : class
        {
            ByteArrayContent byteContent = Serialize(entitiesToDelete);

            HttpRequestMessage manualDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, entityUri)
            {
                Content = byteContent
            };

            HttpResponseMessage deleteResponse = client.SendAsync(manualDeleteRequest).Result;

            return deleteResponse.StatusCode;
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

        private static ByteArrayContent Serialize<T>(T entity)
        {
            string content = JsonConvert.SerializeObject(entity);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        private static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
