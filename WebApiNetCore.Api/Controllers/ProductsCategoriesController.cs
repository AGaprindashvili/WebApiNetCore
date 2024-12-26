using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using WebApiNetCore.Api.Data;
using WebApiNetCore.Api.Models;

namespace WebApiNetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsCategoriesController : ControllerBase
    {

        [Route("GetProductsByCategory")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get products by category", Description = "Get products list by category identifier.")]
        public async Task<ActionResult<IEnumerable<ProductsModel>>> GetProductsByCategory(Guid CategoryId)
        {
            try
            {
                List<ProductsModel> Result = new List<ProductsModel>();
                WebApiDbContext Db = new WebApiDbContext();
                Result = await (from t in Db.Products
                                where (t.IsActive && t.Category.IsActive && t.CategoryId == CategoryId)
                                select new ProductsModel
                                {
                                    Id = t.Id,
                                    ProductName = t.ProductName,
                                    ProductDescrip = t.ProductDescrip,
                                    Specifications = t.Specifications,
                                    CategoryId = t.CategoryId,
                                    CategoryName = t.Category.CategoryName,
                                    CategoryDescrip = t.Category.CategoryDescrip,
                                    Price = t.Price,
                                    Discount = t.Discount,
                                    Quantity = t.Quantity,
                                    IsActive = t.IsActive,
                                    CreateDate = t.CreateDate
                                }).OrderByDescending(o => o.CreateDate).ToListAsync();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ChangeProductCategory")]
        [HttpPost]
        [SwaggerOperation(Summary = "Change product category", Description = "Change product category by product and category identifier.")]
        public async Task<IActionResult> ChangeProductCategory([FromBody] ChangeProductCategoryModel Model)
        {
            try
            {
                WebApiDbContext Db = new WebApiDbContext();
                var GetProduct = await (from t in Db.Products where (t.IsActive && t.Id == Model.ProductId) select t).FirstOrDefaultAsync();
                if (GetProduct != null && ModelState.IsValid)
                {
                    GetProduct.CategoryId = Model.NewCategoryId;
                    Db.SaveChanges();
                    return Ok("success");
                }
                else
                {
                    var Err = ModelState.Where(m => m.Value.Errors.Count > 0).SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors = Err });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}