using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InterfaceService.Models;
using Microsoft.AspNetCore.Http;
using InterfaceService.Services;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace InterfaceService.Controllers
{
    public class InterfaceController : Controller
    {
        private TextRecognitionService _textRecognitionService;
        private SpacyService _spacyService;

        public InterfaceController(TextRecognitionService textRecognitionService, SpacyService spacyService)
        {
            _textRecognitionService = textRecognitionService;
            _spacyService = spacyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Process(IFormFile file)
        {
            //var fileExtention = Path.GetExtension(file.FileName);
            //if (!string.Equals(fileExtention, ".jpg", StringComparison.OrdinalIgnoreCase)
            //    && !string.Equals(fileExtention, ".png", StringComparison.OrdinalIgnoreCase)
            //    && !string.Equals(fileExtention, ".gif", StringComparison.OrdinalIgnoreCase)
            //    && !string.Equals(fileExtention, ".jpeg", StringComparison.OrdinalIgnoreCase))
            //{

            //}


            var text = await _textRecognitionService.ConvertPdfToText(file);

            var entities_str = await _spacyService.GetEntities(text);

            JObject entities_json = JObject.Parse(entities_str);

            string rawText = "";
            JToken ents_json = null;

            List<List<string>> entities = new List<List<string>>();

            foreach (var ent in entities_json)
            {
                if (ent.Key.Equals("text"))
                    rawText = ent.Value.ToString();
                if (ent.Key.Equals("ents"))
                    ents_json = ent.Value;
            }

            //rawText = Regex.Replace(rawText, @"\t|\n|\r", "");

            TextToken[] ents_json_list = ents_json.ToObject<TextToken[]>();
            foreach (var ent in ents_json_list)
            {
                List<string> entity = new List<string>();
                string textData = rawText.Substring(ent.Start, ent.End-ent.Start);
                string labelData = ent.Label;
                entity.Add(textData);
                entity.Add(labelData);
                entities.Add(entity);
            }

            ViewData["entities"] = entities;
            return View();
        }
    }
}
