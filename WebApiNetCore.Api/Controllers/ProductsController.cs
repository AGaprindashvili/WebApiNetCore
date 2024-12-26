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
    public class ProductsController : ControllerBase
    {

        [Route("GetProducts")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get products", Description = "Get products list.")]
        public async Task<ActionResult<IEnumerable<ProductsModel>>> GetProducts()
        {
            try
            {
                List<ProductsModel> Result = new List<ProductsModel>();
                WebApiDbContext Db = new WebApiDbContext();
                Result = await (from t in Db.Products
                                where (t.IsActive)
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

        [Route("GetProduct")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get product", Description = "Get product by identifier.")]
        public async Task<ActionResult<ProductsModel>> GetProduct(Guid Id)
        {
            try
            {
                ProductsModel Result = new ProductsModel();
                WebApiDbContext Db = new WebApiDbContext();
                var Data = await (from t in Db.Products where (t.IsActive && t.Id == Id) select new { t, t.Category }).FirstOrDefaultAsync();
                if (Data != null)
                {
                    Result.Id = Data.t.Id;
                    Result.ProductName = Data.t.ProductName;
                    Result.ProductDescrip = Data.t.ProductDescrip;
                    Result.Specifications = Data.t.Specifications;
                    Result.CategoryId = Data.t.CategoryId;
                    Result.CategoryName = Data.Category.CategoryName;
                    Result.CategoryDescrip = Data.Category.CategoryDescrip;
                    Result.Price = Data.t.Price;
                    Result.Discount = Data.t.Discount;
                    Result.Quantity = Data.t.Quantity;
                    Result.IsActive = Data.t.IsActive;
                    Result.CreateDate = Data.t.CreateDate;
                    return Ok(Result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("AddProduct")]
        [HttpPost]
        [SwaggerOperation(Summary = "Add product", Description = "Add new product.")]
        public async Task<ActionResult<NewProductModel>> AddProduct([FromBody] NewProductModel Model)
        {
            try
            {
                if (Model != null && ModelState.IsValid)
                {
                    using (WebApiDbContext Db = new WebApiDbContext())
                    {
                        Product t = new Product()
                        {
                            Id = Guid.NewGuid(),
                            ProductName = Model.ProductName,
                            ProductDescrip = Model.ProductDescrip,
                            Specifications = Model.Specifications,
                            CategoryId = Model.CategoryId,
                            Price = Model.Price,
                            Discount = Model.Discount,
                            Quantity = Model.Quantity,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                        };
                        Db.Products.Add(t);
                        await Db.SaveChangesAsync();
                    }
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

        [Route("ChangeProduct")]
        [HttpPost]
        [SwaggerOperation(Summary = "Change product", Description = "Change product by identifier.")]
        public async Task<IActionResult> ChangeProduct([FromBody] ChangeProductModel Model)
        {
            try
            {
                WebApiDbContext Db = new WebApiDbContext();
                var GetProduct = await (from t in Db.Products where (t.IsActive && t.Id == Model.ProductId) select t).FirstOrDefaultAsync();
                if (GetProduct != null && ModelState.IsValid)
                {
                    GetProduct.ProductName = Model.ProductInfo.ProductName;
                    GetProduct.ProductDescrip = Model.ProductInfo.ProductDescrip;
                    GetProduct.Specifications = Model.ProductInfo.Specifications;
                    GetProduct.CategoryId = Model.ProductInfo.CategoryId;
                    GetProduct.Price = Model.ProductInfo.Price;
                    GetProduct.Discount = Model.ProductInfo.Discount;
                    GetProduct.Quantity = Model.ProductInfo.Quantity;
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

        [Route("DeleteProduct")]
        [HttpPost]
        [SwaggerOperation(Summary = "Delete product", Description = "Delete product by identifier.")]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid Id)
        {
            try
            {
                WebApiDbContext Db = new WebApiDbContext();
                var GetProduct = await (from t in Db.Products where (t.IsActive && t.Id == Id) select t).FirstOrDefaultAsync();
                if (GetProduct != null)
                {
                    GetProduct.IsActive = false;
                    Db.SaveChanges();
                    return Ok("success");
                }
                else
                {
                    return Ok("error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}