using GeometryAPI.Controllers.Base;
using GeometryAPI.Data.Response;
using GeometryAPI.Entity;
using GeometryAPI.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public CategoriesController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Route("GetCategories")]
        [HttpGet]
        public IActionResult GetCatWithSubCatList()
        {
            try
            {
                IEnumerable<CategoryEntity> categories = db.Categories
                    .Include(s => s.SubCategoryLink)
                    .Where(_ => _.IsDeleted != true)
                    .ToList();

                var test = GetCategoriesResponse(categories);

                return Json(test);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        //[Route("GetSubCategoriesByCatId")]
        //[HttpGet]
        //public IActionResult GetCatWithSubCatList(Guid id)
        //{
        //    try
        //    {
        //        IEnumerable<SubCategoryEntity> subcat = db.SubCategories
        //            .Where(_ => _.IsDeleted != true && _.CategoryId == id)
        //            .ToList();

        //        return Json(GetCategoriesResponse(categories));
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500);
        //    }
        //}


        #region Private Region
        private IEnumerable<CategoriesResponse> GetCategoriesResponse(IEnumerable<CategoryEntity> categories)
        {
            return categories
                .Select(c =>
                {
                    var response = new CategoriesResponse()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SubCategories = c.SubCategoryLink.ToList()
                    };
                    return response;
                }).ToList();
        }
        #endregion

    }
}

