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
    public class CategoriesController : ControllerBase
    {

        [Route("GetCategories")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get categories", Description = "Get category list.")]
        public async Task<ActionResult<IEnumerable<CategoriesModel>>> GetCategories()
        {
            try
            {
                List<CategoriesModel> Result = new List<CategoriesModel>();
                WebApiDbContext Db = new WebApiDbContext();
                Result = await (from t in Db.Categories
                                where (t.IsActive)
                                select new CategoriesModel
                                {
                                    Id = t.Id,
                                    CategoryName = t.CategoryName,
                                    CategoryDescrip = t.CategoryDescrip,
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

        [Route("GetCategory")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get category", Description = "Get category by identifier.")]
        public async Task<ActionResult<CategoriesModel>> GetCategory(Guid Id)
        {
            try
            {
                CategoriesModel Result = new CategoriesModel();
                WebApiDbContext Db = new WebApiDbContext();
                var Data = await (from t in Db.Categories where (t.IsActive && t.Id == Id) select t).FirstOrDefaultAsync();
                if (Data != null)
                {
                    Result.Id = Data.Id;
                    Result.CategoryName = Data.CategoryName;
                    Result.CategoryDescrip = Data.CategoryDescrip;
                    Result.IsActive = Data.IsActive;
                    Result.CreateDate = Data.CreateDate;
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

        [Route("AddCategory")]
        [HttpPost]
        [SwaggerOperation(Summary = "Add category", Description = "Add new category.")]
        public async Task<ActionResult<NewCategoryModel>> AddCategory([FromBody] NewCategoryModel Model)
        {
            try
            {
                if (Model != null && ModelState.IsValid)
                {
                    using (WebApiDbContext Db = new WebApiDbContext())
                    {
                        Category t = new Category()
                        {
                            Id = Guid.NewGuid(),
                            CategoryName = Model.CategoryName,
                            CategoryDescrip = Model.CategoryDescrip,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                        };
                        Db.Categories.Add(t);
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

        [Route("ChangeCategory")]
        [HttpPost]
        [SwaggerOperation(Summary = "Change category", Description = "Change category by identifier.")]
        public async Task<IActionResult> ChangeCategory([FromBody] ChangeCategoryModel Model)
        {
            try
            {
                WebApiDbContext Db = new WebApiDbContext();
                var GetCategory = await (from t in Db.Categories where (t.IsActive && t.Id == Model.CategoryId) select t).FirstOrDefaultAsync();
                if (GetCategory != null && ModelState.IsValid)
                {
                    GetCategory.CategoryName = Model.CategoryInfo.CategoryName;
                    GetCategory.CategoryDescrip = Model.CategoryInfo.CategoryDescrip;
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

        [Route("DeleteCategory")]
        [HttpPost]
        [SwaggerOperation(Summary = "Delete category", Description = "Delete category by identifier.")]
        public async Task<IActionResult> DeleteCategory([FromBody] Guid Id)
        {
            try
            {
                WebApiDbContext Db = new WebApiDbContext();
                var GetCategory = await (from t in Db.Categories where (t.IsActive && t.Id == Id) select t).FirstOrDefaultAsync();
                if (GetCategory != null)
                {
                    GetCategory.IsActive = false;
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