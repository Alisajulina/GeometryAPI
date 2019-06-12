using GeometryAPI.Controllers.Base;
using GeometryAPI.Entity;
using GeometryAPI.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    public class GenderController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public GenderController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Route("Get")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<GenderEntity> gender = db
                    .Genders
                    .Where(d=>d.IsDeleted != true)
                    .ToList();

                return Json(gender);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }





    }
}
