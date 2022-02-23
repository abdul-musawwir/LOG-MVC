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
    public class AddPlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(string teamname, string username, string userpassword, string captainname, string sport)
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/addplayer");
            request.Method = "POST";
            request.ContentType = "application/json";
            Debug.WriteLine("This is working");
            using (var StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string m = JsonConvert.SerializeObject(new Dictionary<string, string> { { "TeamName", teamname }, { "UserName", username }, { "UserPassword", userpassword }, { "CaptainName", captainname}, { "Sport", sport} });
                Debug.WriteLine(m);
                StreamWriter.Write(m);
            }
            WebResponse response = request.GetResponse();
            using (var StreamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = StreamReader.ReadToEnd();
                string m = JsonConvert.DeserializeObject<string>(result);
                return Json(m);
            }
        }

        [HttpPost]
        public IActionResult Show(string teamname, string sport)
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/getplayers");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string m = JsonConvert.SerializeObject(new Dictionary<string, string> { { "TeamName", teamname },{ "Sport", sport } });
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
