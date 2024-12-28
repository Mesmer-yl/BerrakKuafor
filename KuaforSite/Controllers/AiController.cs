using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Text.RegularExpressions;

namespace KuaforSite.Controllers
{
    public class AiController : Controller
    {
        [HttpGet]
        public IActionResult Index(string? gender)
        {
            ViewBag.Gender= gender;
            TempData["Gender"] = gender;
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromBody] Dictionary<string, string> answers)
        {
            var gender = TempData["Gender"];
            string facetype = "";
            string hairtype = "";
            string hairlength = "";
            string jaghead = "";
            foreach (var answer in answers)
            {
                if (answer.Key == "answer1")
                    facetype = answer.Value;
                if (answer.Key == "answer2")
                    jaghead = answer.Value;
                if (answer.Key == "answer3")
                    hairtype = answer.Value;
                if (answer.Key == "answer4")
                    hairlength = answer.Value;
            }
            var aiQuery = $"{facetype} yuz tipine sahip, {hairtype} sac tipine sahip, {hairlength} sac uzunluguna sahip, {jaghead} alin yapisina sahip, {gender} cinsiyetindeki bir birey icin 3 tane sac modeli oner. ";
            string apiKey = "";
            string model = "gpt-4o";
            ChatClient client = new ChatClient(model, apiKey);
            ChatCompletion completion = client.CompleteChat("");
            var response = completion.Content[0].Text;
            response = Regex.Replace(response, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
            response = response.Replace("\\n", "<br>");
            response = response.Replace("\n\n", "</p><p>").Replace("\n", "<br />");
            response = $"<div class='response-ai'><p>{response}</p></div>";
            return Json(response);
        }
    }
}
