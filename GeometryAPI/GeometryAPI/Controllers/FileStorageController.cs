using GeometryAPI.Controllers.Base;
using GeometryAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    public class FileStorageController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public FileStorageController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Route("Download")]
        [HttpGet]
        public IActionResult Download(Guid id, bool thumbnail)
        {
            try
            {
                var file = db.FileStorages
                        .Include(f => f.FileContent)
                        .FirstOrDefault(i => i.Id == id);

                if (file == null)
                {
                    return StatusCode(500);
                }

                var contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
                string fileName = file.FileName;

                if (thumbnail != true)
                {

                    byte[] content = file.FileContent.FullContentData;
                    

                    return File(content, contentType, fileName);
                }

                byte[] cont = file.FileContent.CutContentData;


                return File(cont, contentType, fileName);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }



    }
}
