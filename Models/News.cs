using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPBackends.Models
{
    public class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Museum { get; set; }
        public string Publishtime { get; set; }
        public string Source { get; set; }
        public string Analyseresult { get; set; }
    }
}
