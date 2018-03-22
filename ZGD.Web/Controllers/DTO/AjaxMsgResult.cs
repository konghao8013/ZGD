using ZGD.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGD.Web.Controllers
{
    public class AjaxMsgResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public object Source { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
    }
}
