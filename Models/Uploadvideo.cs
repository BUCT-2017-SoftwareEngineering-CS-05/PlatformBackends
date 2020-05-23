using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPBackends.Models
{
    public partial class Uploadvideo
    {
        [Key]
        public int vid { get; set; }
        public int oid { get; set; }
        public string originName { get; set; }
        public string address { get; set; }
        public int status { get; set; }
    }
}
