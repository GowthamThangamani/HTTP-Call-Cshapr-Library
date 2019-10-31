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
