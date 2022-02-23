using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LOG_Automation.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostLogin(string Role,string UserName,string UserPassword)
        {
            WebRequest request = WebRequest.Create("http://localhost:5003/api/signin");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var StreamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string m = JsonConvert.SerializeObject(new Dictionary<string, string> { { "Role", Role }, { "UserName", UserName }, { "UserPassword", UserPassword } });
                StreamWriter.Write(m);
            }
            WebResponse response = request.GetResponse();
            using(var StreamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = StreamReader.ReadToEnd();
                List<StringResponse> m = (List<StringResponse>)JsonConvert.DeserializeObject<IEnumerable<StringResponse>>(result);
                return Json(m[0]);
            }
        }
    }

    class StringResponse
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Token { get; set; }
        public string Sport { get; set; }
        public string TeamName { get; set; }
        public string CaptainName { get; set; }

    }
}
