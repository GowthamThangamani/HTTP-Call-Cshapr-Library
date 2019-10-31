using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Configuration;   
using Newtonsoft.Json;        
using Newtonsoft.Json.Linq;

namespace TestStandard{
    class Http {
        static async Task<string> FindDataArea(){
            var client = new HttpClient();
            var request = new HttpRequestMessage {
                RequestUri = new Uri("HTTP URL"),
                Method = HttpMethod.Get,
                Headers = {
                    { "X-Version", "1" },
                    { HttpRequestHeader.Authorization.ToString(), "Bearer "+ GetAccessToken() },
            }
            };
           var response = client.SendAsync(request).Result;
           return await response.Content.ReadAsStringAsync();
        }
        
        
        
        static string HttpCallFormBody()
        {
            var content = new Dictionary<string, string>();
            content.Add("grant_type", "refresh_token");
            content.Add("scope", "Files.Read Files.ReadWrite Files.Read.All Files.ReadWrite.All Sites.Read.All Sites.ReadWrite.All offline_access"); // Scope get from config.csx (each login one Scope)
            content.Add("redirect_uri", "https://example.com");
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://east02.pcawebapi.jp/v1/Acc20/Auth/Token"),
                Content = new FormUrlEncodedContent(content)
            };
            var response = client.SendAsync(httpRequestMessage).Result;
            var responsedata = response.Content.ReadAsStringAsync().Result;
            return responsedata;
        }
    }
}
