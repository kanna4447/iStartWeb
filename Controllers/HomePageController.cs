using iStartWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using System.Text;

namespace iStartWeb.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage

        Uri baseAddress = new Uri("<api url>");
        private readonly HttpClient _client;

        public HomePageController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

       
        public List<UserRegisterViewModel> GetSearch(string searchText)
        {
            List<UserRegisterViewModel> list = new List<UserRegisterViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/UserRegistration/GetRegisterUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserRegisterViewModel>>(data);
            }

            List<UserRegisterViewModel> allUsers = list.ToList();

            if (searchText != "" && searchText != null)
            {
                allUsers = allUsers.Where(n => n.Name.Contains(searchText) || n.EmailID.Contains(searchText)).ToList();
            }

            return allUsers;
        }


        public ActionResult Index(string searchText, int page = 1, int pageSize = 3)
        {

            List<UserRegisterViewModel> list = new List<UserRegisterViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/UserRegistration/GetRegisterUsers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserRegisterViewModel>>(data);
            }

           List<UserRegisterViewModel> allUsers = GetSearch(searchText);

            ViewBag.searchText = searchText;

            if (page < 1)

                page = 1;
            int recordsCount = list.Count();

            var pager = new PagerModel(recordsCount, page, pageSize);

            allUsers = allUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.datass = allUsers;           
            this.ViewBag.Pager = pager;
            
            return View(allUsers);
        }

        [HttpGet]
        public ActionResult Edit(int? userId)
        {
            List<UserRegisterViewModel> list = new List<UserRegisterViewModel>();
            
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/UserRegistration/GetRegisterUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserRegisterViewModel>>(data);
                
            }
            var usersList=list.SingleOrDefault(x=>x.RegisterId== userId);   
           
            return View(usersList);
        }
        [HttpPost]
        public ActionResult Edit(int userId, UserRegisterViewModel usereditValues)
        {
            List<UserRegisterViewModel> list = new List<UserRegisterViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/UserRegistration/GetRegisterUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserRegisterViewModel>>(data);
            }

            var retrievedItems = list.Where(x => x.RegisterId == userId).SingleOrDefault();

            if (retrievedItems != null)
            {
                retrievedItems.Name = usereditValues.Name;

            }
            string dataList = JsonConvert.SerializeObject(retrievedItems);
            StringContent content = new StringContent(dataList, Encoding.UTF8, "application/json");
            HttpResponseMessage responses = _client.PostAsync(_client.BaseAddress + "/UserRegistration/EditRegistration?userId="+userId+"&Name="+usereditValues.Name, content).Result;

            
                return RedirectToAction("Index","HomePage");
            
            
            
        }
    }
}






