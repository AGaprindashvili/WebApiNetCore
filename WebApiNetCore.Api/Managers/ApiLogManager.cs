using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApiNetCore.Api.Data;
using WebApiNetCore.Api.Models;

namespace WebApiNetCore.Api.Managers
{
    public class ApiLogManager : IActionFilter
    {

        private readonly ILogger<ApiLogManager> _logger;

        public ApiLogManager(ILogger<ApiLogManager> logger)
        {
            _logger = logger;
        }

        public async void OnActionExecuting(ActionExecutingContext Context)
        {
            try
            {
                string GetParams = string.Empty;
                if (Context.HttpContext.Request.Method == HttpMethod.Get.Method)
                {
                    var GetQuery = Context.HttpContext.Request.Query.ToDictionary(i => i.Key, i => i.Value.ToString());
                    GetParams = JsonConvert.SerializeObject(GetQuery).ToString();
                }
                if (Context.HttpContext.Request.Method == HttpMethod.Post.Method)
                {
                    var GetArgs = Context.ActionArguments;
                    GetParams = JsonConvert.SerializeObject(GetArgs, Formatting.Indented).ToString();
                }
                using (WebApiDbContext Db = new WebApiDbContext())
                {
                    ApiLog t = new ApiLog()
                    {
                        Id = Guid.NewGuid(),
                        MethodName = Context.HttpContext.Request.Path,
                        MethodParams = GetParams,
                        RequestIp = Context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        ResponseCode = Context.HttpContext.Response.StatusCode.ToString() + " - " + Context.HttpContext.Request.Method,
                        ExecDate = DateTime.Now
                    };
                    Db.ApiLogs.Add(t);
                    await Db.SaveChangesAsync();
                }
            }
            catch { }
        }

        public async void OnActionExecuted(ActionExecutedContext Context)
        {
        }

    }
}