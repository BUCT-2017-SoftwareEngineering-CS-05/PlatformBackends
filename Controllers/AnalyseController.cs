using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Baidu.Aip.Nlp;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Analyzer.Models;
using Newtonsoft.Json;

namespace Analyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyseController : Controller
    {
        private readonly NewsContext _context;

        public AnalyseController(NewsContext context)
        {
            _context = context;
        }
        // 设置APPID/AK/SK

        [HttpGet("Analyse")]
        public async Task<string> AnalyseAsync(long id)
        {
            var news = await _context.News.FindAsync(id);
            if (news.Analyseresult == null)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                StreamReader sr = new StreamReader("AnalyzerConfigs/apiconfig.json");
                string json = sr.ReadToEnd();
                //json文件转为 对象  T 创建的类 字段名 应该和json文件中的保持一致     
                var data = JsonConvert.DeserializeObject<T>(json);

                var APP_ID = data.appid;
                var API_KEY = data.apikey;
                var SECRET_KEY = data.secretkey;

                var client = new Nlp(API_KEY, SECRET_KEY);
                client.Timeout = 60000;  // 修改超时时间

                var result = client.SentimentClassify(news.Content);
                String[] res = { "negative", "neutral", "positive" };
                if (result.ContainsKey("error_code"))
                {
                    return result.ToString();
                }
                news.Analyseresult = res[(int)result["items"][0]["sentiment"]];
                _context.Entry(news).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return res[(int)result["items"][0]["sentiment"]];
            }
            return news.Analyseresult;
        }

        
    }

    internal class T
    {
        public string url;
        public string appid;
        public string apikey;
        public string secretkey;
    }
}