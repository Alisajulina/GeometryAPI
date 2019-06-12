using GeometryAPI.Common.Enums;
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
    public class ProductController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public ProductController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetFilteredProducts(int offset = 1, int count = 10)
        {
            try
            {
                IEnumerable<ProductEntity> products = db.Products
                              .Include(p => p.ProductFileLink)
                                .ThenInclude(f => f.File)
                              .Where(_ => _.IsDeleted != true)
                              .OrderBy(o => o.Name)
                              .Skip((offset - 1) * count).Take(count);

                return Json(GetShortResponse(products));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Route("GetCatSortList")]
        [HttpGet]
        public IActionResult GetCatSortList(Guid catid, int offset = 1, int count = 10)
        {
            try
            {
                IEnumerable<ProductEntity> products = db.Products
                    .Include(p => p.ProductFileLink)
                                .ThenInclude(f => f.File)
                    .Where(_ => _.IsDeleted != true)
                              .OrderBy(o => o.CategoryId == catid)
                              .Skip(offset * count).Take(count);

                return Json(GetShortResponse(products));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Route("GetSubCatSortList")]
        [HttpGet]
        public IActionResult GetSubCatSortList(Guid subcatid, int offset = 1, int count = 10)
        {
            try
            {
                IEnumerable<ProductEntity> products = db.Products
                    .Include(p => p.ProductFileLink)
                                .ThenInclude(f => f.File)
                    .Where(_ => _.IsDeleted != true)
                              .OrderBy(o => o.SubCategoryId == subcatid)
                              .Skip(offset * count).Take(count);

                return Json(GetShortResponse(products));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Route("GetProduct")]
        [HttpGet]
        public IActionResult GetProduct(Guid id)
        {
            try
            {
                ProductEntity product = db.Products
                    .Include(p => p.ProductFileLink)
                                .ThenInclude(f => f.File)
                    .FirstOrDefault(p => p.Id == id && p.IsDeleted != true);

                if (product != null)
                {
                    var files = (product.ProductFileLink.Count() != 0) ? product.ProductFileLink.Where(_ => _.IsDeleted != true).Select(f => f.FileId).ToArray() : null;

                    ProductResponse response = new ProductResponse()
                    {
                        Id = product.Id,
                        AvailableSizeId = product.AvailableSizeId,
                        VendorCode = product.VendorCode,
                        Descriprion = product.Descriprion,
                        Name = product.Name,
                        Price = product.Price,
                        Thumbnail = files
                    };
                    return Json(response);
                }
                return StatusCode(500);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        #region Private Region
        private IEnumerable<ProductShortResponse> GetShortResponse(IEnumerable<ProductEntity> products)
        {
            return products
                .Select(p =>
                {
                    var files = (p.ProductFileLink.Count() != 0) ? p.ProductFileLink.Where(_ => _.IsDeleted != true).Select(f => f.FileId).ToArray() : null;
                    //var files = p.ProductFileLink.FirstOrDefault(_ => _.File.FileContent.CutContentData != null);

                    var response = new ProductShortResponse()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        AvailableSizeId = p.AvailableSizeId,
                        Thumbnail = files
                    };
                    return response;
                }).ToList();
        }
        #endregion
    }
}
