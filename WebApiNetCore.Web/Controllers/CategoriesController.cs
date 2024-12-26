using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiNetCore.Web.Managers;
using WebApiNetCore.Web.Models;

namespace WebApiNetCore.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly string LoginUrl, GetCategoriesUrl, AddCategoryUrl;

        public CategoriesController(IConfiguration configuration)
        {
            LoginUrl = configuration.GetValue<string>("ApiSettings:LoginUrl");
            GetCategoriesUrl = configuration.GetValue<string>("ApiSettings:GetCategoriesUrl");
            AddCategoryUrl = configuration.GetValue<string>("ApiSettings:AddCategoryUrl");
        }

        public async Task<IActionResult> Index()
        {
            ApiManager Api = new ApiManager();
            string Token = await Api.GetApiTokenAsync(LoginUrl, "admin", "123");
            var Products = await Api.GetCategoriesAsync(GetCategoriesUrl, Token);
            return View(Products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCategoryModel Model)
        {
            if (ModelState.IsValid)
            {
                ApiManager Api = new ApiManager();
                string Token = await Api.GetApiTokenAsync(LoginUrl, "admin", "123");
                string Result = await Api.AddCategoryAsync(AddCategoryUrl, Token, Model);
                return Result == "success" ? RedirectToAction("Index") : View();
            }
            else
            {
                return View(Model);
            }
        }

    }
}