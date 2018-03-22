using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace ZGD.Common
{
    public class JsonHelper
    {
        public static T GetJson<T>(string ObjectText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            T result = js.Deserialize<T>(ObjectText);
            return result;
        }
    }
}
