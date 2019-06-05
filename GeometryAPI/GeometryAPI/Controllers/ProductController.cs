using GeometryAPI.Controllers.Base;
using GeometryAPI.Entity;
using Microsoft.AspNetCore.Mvc;
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

        [Route("GetFilteredProducts")]
        [HttpGet]
        public async Task<IActionResult> GetFilteredProducts(int offset = 0, int count = 20, )
        {
            try
            {
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}
