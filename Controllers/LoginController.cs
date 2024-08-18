using iStartWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Text;
using System.Web.Mvc;


namespace iStartWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        Uri baseAddress = new Uri("https://localhost:44309/api");
        private readonly HttpClient _client;
       
        public LoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login"); 
        }
        
        
        public ActionResult Index(UserRegisterViewModel userlogin)

        {
            string data = JsonConvert.SerializeObject(userlogin);
            if (string.IsNullOrWhiteSpace(userlogin.EmailID))
            {

                TempData["emailerrorEmpty"] = "Email address should not be empty";

            }
            if (string.IsNullOrWhiteSpace(userlogin.Password))
            {

                TempData["passworderrorEmpty"] = "Password should not be empty";

            }

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync
                (_client.BaseAddress + "/UserLogin/LoginCheck?EmailID=" + userlogin.EmailID + "&Password=" + userlogin.Password,content).Result;

            if (response.IsSuccessStatusCode)
            {
                //string listitems = response.Content.ReadAsStringAsync().Result;
                
                        Session["email"] = userlogin.EmailID;
               
                    return Redirect("/HomePage/Index");

            }
            return RedirectToAction("Index");
        }
    }
}
