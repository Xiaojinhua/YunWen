using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YunWen.Models
{
    public class ResultModel
    {
        public bool Success { get; set; } = false;
        public string Msg { get; set; }
        public object Obj { get; set; }
    }
}