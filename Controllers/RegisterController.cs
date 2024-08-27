using iStartWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace iStartWeb.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        Uri baseAddress = new Uri("<api url>");
        private readonly HttpClient _client;

        public RegisterController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserRegisterViewModel userRegister)
        {

           string data = JsonConvert.SerializeObject(userRegister);

            if (string.IsNullOrWhiteSpace(userRegister.Password))
            {
                TempData["passwordError"] = "Password should not be empty";
            }

            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            
            if (string.IsNullOrWhiteSpace(userRegister.Password) || !hasUpperChar.IsMatch(userRegister.Password))
            {
                TempData["notcontainUpperChar"] = "Password should contain at least one upper case letter";
            }

           else  if (string.IsNullOrWhiteSpace(userRegister.Password) || !hasNumber.IsMatch(userRegister.Password))
            {
                TempData["notcontainNumber"] = "Password should contain at least one numeric value";
            }

            else if(string.IsNullOrWhiteSpace(userRegister.Password) || !hasSymbols.IsMatch(userRegister.Password))
            {
                TempData["notcontainSymbol"] = "Password should contain at least one special case character";
            }

            else if (string.IsNullOrWhiteSpace(userRegister.ReEnterPassword))
            {
                TempData["confirmpasswordError"] = "Confirm Password";

            }
            else if(userRegister.Password != userRegister.ReEnterPassword)
            {
                TempData["passwordmismatchError"] = "Password Mismatch";
            }

            else
            {
                
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/UserRegistration/RegisterAdd", content).Result;

               
               
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMsg"] = "Suceessfully Registered !";
                    return RedirectToAction("Index");


                }
                else 
                {
                    TempData["emailExists"] = "Email already exists !";
                    return RedirectToAction("Index");
                }
            }
            
            return View();
        }

    }
}
