using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiNetCore.Web.Models;

namespace WebApiNetCore.Web.Managers
{
    public class ApiManager
    {

        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> GetApiTokenAsync(string Url, string Username, string Password)
        {
            string Result = string.Empty;
            try
            {
                var Params = new
                {
                    Username = Username,
                    Password = Password
                };
                var JsonParams = JsonConvert.SerializeObject(Params);
                var Model = new StringContent(JsonParams, Encoding.UTF8, "application/json");
                HttpResponseMessage Response = await _httpClient.PostAsync(Url, Model);
                if (Response.IsSuccessStatusCode)
                {
                    string ResponseString = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<TokenResponseModel>(ResponseString).Token;
                }
            }
            catch { }
            return Result;
        }

        public async Task<List<ProductsModel>> GetProductsAsync(string Url, string Token)
        {
            List<ProductsModel> Result = new List<ProductsModel>();
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage Response = await _httpClient.GetAsync(Url);
                if (Response.IsSuccessStatusCode)
                {
                    string ResponseString = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<List<ProductsModel>>(ResponseString);
                }
            }
            catch { }
            return Result;
        }

        public async Task<List<CategoriesModel>> GetCategoriesAsync(string Url, string Token)
        {
            List<CategoriesModel> Result = new List<CategoriesModel>();
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage Response = await _httpClient.GetAsync(Url);
                if (Response.IsSuccessStatusCode)
                {
                    string ResponseString = await Response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<List<CategoriesModel>>(ResponseString);
                }
            }
            catch { }
            return Result;
        }

        public async Task<string> AddProductAsync(string Url, string Token, NewProductModel Params)
        {
            string Result = string.Empty;
            try
            {
                var JsonParams = JsonConvert.SerializeObject(Params);
                var Model = new StringContent(JsonParams, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage Response = await _httpClient.PostAsync(Url, Model);
                if (Response.IsSuccessStatusCode)
                {
                    Result = await Response.Content.ReadAsStringAsync();
                }
            }
            catch { }
            return Result;
        }

        public async Task<string> AddCategoryAsync(string Url, string Token, NewCategoryModel Params)
        {
            string Result = string.Empty;
            try
            {
                var JsonParams = JsonConvert.SerializeObject(Params);
                var Model = new StringContent(JsonParams, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage Response = await _httpClient.PostAsync(Url, Model);
                if (Response.IsSuccessStatusCode)
                {
                    Result = await Response.Content.ReadAsStringAsync();
                }
            }
            catch { }
            return Result;
        }

        public async Task<string> GetDataAsync(string Token, string Url)
        {
            using var Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var Response = await Client.GetAsync(Url);
            Response.EnsureSuccessStatusCode();
            return await Response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostDataAsync(string Token, string Url)
        {
            using var Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var Model = new
            {
                Param1 = string.Empty,
                Param2 = string.Empty
            };
            var Params = new StringContent(JsonConvert.SerializeObject(Model), Encoding.UTF8, "application/json");
            var Response = await Client.PostAsync(Url, Params);
            if (Response.IsSuccessStatusCode)
                return await Response.Content.ReadAsStringAsync();
            else
                return Response.StatusCode.ToString();
        }

    }
}