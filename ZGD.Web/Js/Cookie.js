//获得Cookie解码后的值
function GetCookieVal(offset)
{
    var endstr = document.cookie.indexOf (";", offset);
    if (endstr == -1)
    endstr = document.cookie.length;
    return unescape(document.cookie.substring(offset, endstr));
}

function SetCookie(name, value)//设定Cookie值
{
    var expdate = new Date();
    var argv = SetCookie.arguments;
    var argc = SetCookie.arguments.length;
    var expires = (argc > 2) ? argv[2] : null;
    var path = (argc > 3) ? argv[3] : null;
    var domain = (argc > 4) ? argv[4] : null;
    var secure = (argc > 5) ? argv[5] : false;
    if(expires!=null) 
        expdate.setTime(expdate.getTime() + ( expires * 1000 ));
    document.cookie = name + "=" + escape (value) +((expires == null) ? "" : ("; expires="+ expdate.toGMTString()))
    +((path == null) ? "" : ("; path=" + path)) +((domain == null) ? "" : ("; domain=" + domain))
    +((secure == true) ? "; secure" : "");
}
//删除Cookie
function DelCookie(name)
{
    var exp = new Date();
    exp.setTime (exp.getTime() - 1);
    var cval = GetCookie (name);
    document.cookie = name + "=" + cval + "; expires="+ exp.toGMTString();
}
//获得Cookie的原始值
function GetCookie(name)
{
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen)
    {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return GetCookieVal (j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0) 
            break;
    }
    return null;
}

//编码程序： 
function CodeCookie(str) {
    var strRtn = "";
    for (var i = str.length - 1; i >= 0; i--) {
        strRtn += str.charCodeAt(i);
        if (i) strRtn += "a"; //用a作分隔符 
    }
    return strRtn;
}
//解码程序： 
function DecodeCookie(str) {
    var strArr;
    var strRtn = "";
    strArr = str.split("a");
    for (var i = strArr.length - 1; i >= 0; i--)
        strRtn += String.fromCharCode(eval(strArr[i]));
    return strRtn;
} 