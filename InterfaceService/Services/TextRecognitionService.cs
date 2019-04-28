using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceService.Services
{
    public class TextRecognitionService
    {
        private HttpClient _client = new HttpClient();
        private readonly string _OCRUrl = "http://testrecognitionservice:5000/convertfiles";

        public async Task<string> ConvertPdfToText(IFormFile file)
        {
            string msg = null;

            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            ByteArrayContent bytes = new ByteArrayContent(data);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();
            multiContent.Add(bytes, "file", file.FileName);
            var response = await _client.PostAsync(_OCRUrl, multiContent);

            if (response != null)
            {
                msg = await response.Content.ReadAsStringAsync();
            }

            return msg;
        }
    }
}
