using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using MPBackends.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http.Headers;

namespace MPBackends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadVideoController : Controller
    {
        private readonly UploadvideoContext _museumContext;
        public UploadVideoController(UploadvideoContext museumContext)
        {
            _museumContext = museumContext;
        }

        [HttpPost]
        [Route("Uploading")]
        public JsonResult Uploading([FromQuery] int oid, [FromForm] IFormFile file)
        {
            if (file.Length > 0)
            {
                //声明新实体
                Uploadvideo newVideo = new Uploadvideo();
                newVideo.originName = file.FileName;//加入原文件名

                //分离出文件后缀
                string[] getarr = newVideo.originName.Split('.');

                string newFileName = Path.GetRandomFileName();//随机生成新文件名
                string[] getAry = newFileName.Split('.');//分离出错误后缀

                //加上正确后缀
                newFileName = getAry[0] + "." + getarr[1];

                newVideo.address = Path.Combine(@".\UploadVideo",
                       newFileName);//生成新存储路径
                newVideo.status = -1;
                newVideo.oid = oid;

                _museumContext.Uploadvideo.Add(newVideo);
                _museumContext.SaveChanges();//加入数据库
                using (var stream = System.IO.File.Create(newVideo.address))
                {
                    file.CopyTo(stream);
                    stream.Flush();//保存到本地

                }
                return Json(new
                {
                    status = 1,
                    filename = newVideo.originName,
                    pathname = newVideo.address
                });
            }
            else
            {
                return Json(new { status = 0 });
            }
        }
    }

}
