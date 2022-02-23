using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LOG_Automation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCaptain(string TeamName, string UserName, string UserPassword)
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/registercaptain");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string m = JsonConvert.SerializeObject(new Dictionary<string, string> { { "TeamName", TeamName }, { "UserName", UserName }, { "UserPassword", UserPassword } });
                Debug.WriteLine(m);
                StreamWriter.Write(m);
            }
            WebResponse response = request.GetResponse();
            using (var StreamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = StreamReader.ReadToEnd();
                string m = JsonConvert.DeserializeObject<string>(result);
                Debug.WriteLine(m);
                return Json(m);
            }
        }

        [HttpPost]
        public IActionResult ShowCaptain()
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/getcaptains");
            request.Method = "GET";
            request.ContentType = "application/json";
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
