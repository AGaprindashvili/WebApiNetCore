using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiNetCore.Api.Models
{
    [SwaggerSchema("Login model")]
    public class LoginModel
    {
        [SwaggerSchema("Username")]
        public string Username { get; set; }

        [SwaggerSchema("Password")]
        public string Password { get; set; }
    }
}