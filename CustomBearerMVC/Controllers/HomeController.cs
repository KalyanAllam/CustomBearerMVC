using CustomBearerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CustomBearerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            IEnumerable<Employee> employees = GetEmployee();
            return View(employees);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        IEnumerable<Employee> GetEmployee()
        {
            IEnumerable<Employee> employees = null;
        
        using (HttpClient client = new HttpClient()) 
            {

                string url = "https://localhost:44361/api/Employee";
                Uri uri = new Uri(url);

               // System.Threading.Tasks.Task<HttpResponseMessage> result = client.GetAsync(uri);

                string input = "test:pass";

                byte[] array=System.Text.Encoding.UTF8.GetBytes(input);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Convert.ToBase64String(array));
                System.Threading.Tasks.Task<HttpResponseMessage> result = client.GetAsync(uri);

                if (result.Result.IsSuccessStatusCode)
                {
                    System.Threading.Tasks.Task<string> response = result.Result.Content.ReadAsStringAsync();
                    employees = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Result);


                }
            }



            return employees;
        }

    }
}