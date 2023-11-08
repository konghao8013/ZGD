using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public class HtmlEncodeBinder
{
    public static string HtmlEncodeEval(object dataItem, string expression)
    {
        object value = DataBinder.Eval(dataItem, expression);
        if (value != null)
        {
            return HttpUtility.HtmlEncode(value.ToString());
        }
        return string.Empty;
    }
}