using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiNetCore.Web.Managers;
using WebApiNetCore.Web.Models;

namespace WebApiNetCore.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly string LoginUrl, GetProductsUrl, AddProductUrl;

        public ProductsController(IConfiguration configuration)
        {
            LoginUrl = configuration.GetValue<string>("ApiSettings:LoginUrl");
            GetProductsUrl = configuration.GetValue<string>("ApiSettings:GetProductsUrl");
            AddProductUrl = configuration.GetValue<string>("ApiSettings:AddProductUrl");
        }

        public async Task<IActionResult> Index()
        {
            ApiManager Api = new ApiManager();
            string Token = await Api.GetApiTokenAsync(LoginUrl, "admin", "123");
            var Products = await Api.GetProductsAsync(GetProductsUrl, Token);
            return View(Products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductModel Model)
        {
            if (ModelState.IsValid)
            {
                ApiManager Api = new ApiManager();
                string Token = await Api.GetApiTokenAsync(LoginUrl, "admin", "123");
                string Result = await Api.AddProductAsync(AddProductUrl, Token, Model);
                return Result == "success" ? RedirectToAction("Index") : View();
            }
            else
            {
                return View(Model);
            }
        }

        public async Task<IActionResult> Js()
        {
            ApiManager Api = new ApiManager();
            ViewData["Token"] = await Api.GetApiTokenAsync(LoginUrl, "admin", "123");
            return View();
        }

    }
}