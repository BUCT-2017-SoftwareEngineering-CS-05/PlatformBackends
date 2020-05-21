using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Baidu.Aip.Nlp;
using Microsoft.EntityFrameworkCore;
using MPBackends.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MPBackends.Controllers
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

                string src = news.Content;
                if (news.Content.Length > 1020)
                {
                    if (news.Content.Length > 2980)
                    {
                        _ = src.Substring(0, 2980);
                    }
                    var content = src;

                    var maxSummaryLen = 300;
                    // 如果有可选参数
                    var options = new Dictionary<string, object>{
                        {"title", news.Title}
                    };
                    // 带参数调用新闻摘要接口
                    var ress = client.NewsSummary(content, maxSummaryLen, options);
                    Console.WriteLine(ress);
                    src = (string)ress["summary"];
                }
                var result = client.SentimentClassify(src);
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