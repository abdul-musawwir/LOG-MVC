using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace LOG_Automation.Controllers
{
    public class Player : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Show(string TeamName, string Sport)
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/getteam");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string m = JsonConvert.SerializeObject(new Dictionary<string, string> { { "TeamName", TeamName }, { "Sport", Sport} });
                StreamWriter.Write(m);
            }
            WebResponse response = request.GetResponse();
            using (var StreamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = StreamReader.ReadToEnd();
                List<StringResponse> m = (List<StringResponse>)JsonConvert.DeserializeObject<IEnumerable<StringResponse>>(result);
                return Json(m);
            }
        }
    }
}
