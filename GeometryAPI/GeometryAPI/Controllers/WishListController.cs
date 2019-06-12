using GeometryAPI.Controllers.Base;
using GeometryAPI.Data.Response;
using GeometryAPI.Entity;
using GeometryAPI.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    public class WishListController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public WishListController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public IActionResult GetWishList(Guid userid)
        {
            try
            {
                IEnumerable<WishListEntity> list = db.WishLists
                    .Include(w=>w.Product)
                    .Where(_ => _.IsDeleted != true && _.UserId == userid)
                              .OrderBy(o => o.Product.Name);

                return Json(GetShortResponse(list));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("AddWish")]
        [HttpGet]
        public IActionResult AddWish(Guid prodid, Guid userid)
        {
            try
            {

                var prod = db.Products.FirstOrDefault(p => p.Id == prodid && p.IsDeleted !=true);
                var user = db.Users.FirstOrDefault(u => u.Id == userid && u.IsDeleted != true);

                if (prod != null && user != null)
                {
                    WishListEntity wishList = new WishListEntity()
                    {
                        UserId = userid,
                        ProductId = prodid
                    };

                    db.WishLists.Add(wishList);
                    db.SaveChanges();


                    return StatusCode(200);
                }
                return StatusCode(500);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("DeleteWish")]
        [HttpDelete]
        public IActionResult DeleteWish(Guid wishid)
        {
            try
            {
                var wish = db.WishLists.FirstOrDefault(w => w.Id == wishid && w.IsDeleted != true);

                if (wish != null)
                {
                    wish.IsDeleted = true;

                    db.WishLists.Update(wish);
                    db.SaveChanges();

                    return StatusCode(200);
                }
                return StatusCode(500);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        #region Private Region
        private IEnumerable<ProductShortResponse> GetShortResponse(IEnumerable<WishListEntity> wishLists)
        {
            return wishLists
                .Select(p =>
                {
                    var files = (p.Product.ProductFileLink.Count() != 0) ? p.Product.ProductFileLink.Where(_ => _.IsDeleted != true).Select(f => f.FileId).ToArray() : null;
                    //var files = p.ProductFileLink.FirstOrDefault(_ => _.File.FileContent.CutContentData != null);

                    var response = new ProductShortResponse()
                    {
                        Id = p.Product.Id,
                        Name = p.Product.Name,
                        Price = p.Product.Price,
                        AvailableSizeId = p.Product.AvailableSizeId,
                        Thumbnail = files
                    };
                    return response;
                }).ToList();
        }
        #endregion

    }
}
