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
    public class OrderController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public OrderController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion



    }
}
