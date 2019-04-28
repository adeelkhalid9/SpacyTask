using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceService.Services
{
    public class SpacyService
    {
        private HttpClient _client = new HttpClient();
        private readonly string _SpacyUrl = "http://spacyservice:5000/recognizer";

        public async Task<string> GetEntities(string rawData)
        {
            string msg = null;

            var httpContent = new StringContent(rawData, Encoding.UTF8, "application/json");
            using (var content = new MultipartFormDataContent())
            {
                content.Add(httpContent, "rawData");
                var response = await _client.PostAsync(_SpacyUrl, content);
                if (response != null)
                    msg = await response.Content.ReadAsStringAsync();
            }

            return msg;
        }
    }
}
