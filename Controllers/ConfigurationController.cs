using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analyzer.Controllers
{
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        const string configRoutes = "AnalyzerConfigs/";
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("apiconfig")]
        public ActionResult GetAPIConfig()
        {
            FileStream stream = new FileStream(configRoutes + "apiconfig.json", FileMode.Open);
            return File(stream, "application/json");
        }
        [HttpPut("apiconfig")]
        public ActionResult PutAPIConfig(string config)
        {
            System.IO.File.Copy(configRoutes + "apiconfig.json", configRoutes + "apiconfig.bak.json", true);
            var fs = new FileStream(configRoutes + "apiconfig.json", FileMode.Truncate);

            byte[] bs = Encoding.ASCII.GetBytes(config);
            fs.Write(bs.AsSpan());
            fs.Flush();fs.Close();
            return GetAPIConfig();
        }

        [HttpGet("autorun")]
        public ActionResult GetAutorunConfig()
        {
            FileStream stream = new FileStream(configRoutes + "autorun.json", FileMode.Open);
            return File(stream, "application/json");
        }
        [HttpPut("autorun")]
        public ActionResult PutAutorunConfig(string config)
        {
            System.IO.File.Copy(configRoutes + "autorun.json", configRoutes + "autorun.bak.json",true);
            var fs = new FileStream(configRoutes + "autorun.json", FileMode.Truncate);

            byte[] bs = Encoding.ASCII.GetBytes(config);
            fs.Write(bs.AsSpan());
            fs.Flush(); fs.Close();
            return GetAutorunConfig();
        }

        [HttpGet("crawlerconfig")]
        public ActionResult GetCrawlerConfig()
        {
            FileStream stream = new FileStream(configRoutes + "crawlerconfig.json", FileMode.Open);
            return File(stream, "application/json");
        }
        [HttpPut("crawlerconfig")]
        public ActionResult PutCrawlerConfig(string config)
        {
            System.IO.File.Copy(configRoutes + "crawlerconfig.json", configRoutes + "crawlerconfig.bak.json", true);
            var fs = new FileStream(configRoutes + "crawlerconfig.json", FileMode.Truncate);

            byte[] bs = Encoding.ASCII.GetBytes(config);
            fs.Write(bs.AsSpan());
            fs.Flush(); fs.Close();
            return GetCrawlerConfig();
        }

        [HttpGet("keywords")]
        public ActionResult GetKeywordsConfig()
        {
            FileStream stream = new FileStream(configRoutes + "keywords.json", FileMode.Open);
            return File(stream, "application/json");
        }
        [HttpPut("keywords")]
        public ActionResult PutKeywordsConfig(string config)
        {
            System.IO.File.Copy(configRoutes + "keywords.json", configRoutes + "keywords.bak.json", true);
            var fs = new FileStream(configRoutes + "keywords.json", FileMode.Truncate);

            byte[] bs = Encoding.ASCII.GetBytes(config);
            fs.Write(bs.AsSpan());
            fs.Flush(); fs.Close();
            return GetKeywordsConfig();
        }
    }
}
