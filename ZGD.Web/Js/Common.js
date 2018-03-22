//=============================================================================
//
// Project:公共JS
// Author:优纳科技  LJ
// Data:2010-5.1 编写时间
// Updated:2010-6.10 修改时间
//
//=============================================================================

//图片按比例缩放 
var flag = false;
function DrawImage(ImgD) {
    var image = new Image();
    var iwidth = 400;  //定义允许图片宽度 
    var iheight = 400;  //定义允许图片高度 
    image.src = ImgD.src;
    if (image.width > 0 && image.height > 0) {
        flag = true;
        if (image.width / image.height >= iwidth / iheight) {
            if (image.width > iwidth) {
                ImgD.width = iwidth;
                ImgD.height = (image.height * iwidth) / image.width;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
            ImgD.alt = image.width + "×" + image.height;
        }
        else {
            if (image.height > iheight) {
                ImgD.height = iheight;
                ImgD.width = (image.width * iheight) / image.height;
            } else {
                ImgD.width = image.width;
                ImgD.height = image.height;
            }
            ImgD.alt = image.width + "×" + image.height;
        }
    }
}  


//获取广告html
function GetAdHtml(type, width, height, filePath, link) {
    var html = "";
    switch (type) {
        case "1":
            html = "<a href=\"" + link + "\" target=\"_blank\"><img src=\"../../" + filePath + "\" width=\"" + width + "\" height=\"" + height + "\" /></a>";
            break;
        case "2":
            html = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" width=\"" + width + "\" height=\"" + height + "\"><param name=\"movie\" value=\"../../" + filePath + "\" /><param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\"><embed src=\"../../" + filePath + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"></embed></object>";
            break;
        case "3":
            html = "<img src=\"" + file + "\" />";
            break;
        case "4":
            html = "<img src=\"" + file + "\" />";
            break;
    }
    return html;
}
function GetElseAdHtml(type, width, height, filePath, remark,id){
    var html = "";
    switch (type) {
        case "1":
            html = "<img id=\"_img"+id+"\" src=\"../../" + filePath + "\" width=\"" + width + "\" height=\"" + height + "\" alt=\"" + remark + "\" />";
            break;
        case "2":
            html = "<object  id=\"_img"+id+"\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0\" width=\"" + width + "\" height=\"" + height + "\"><param name=\"wmode\" value=\"transparent\" /><param name=\"movie\" value=\"../../" + filePath + "\" /><param name=\"quality\" value=\"high\" /><param name=\"wmode\" value=\"transparent\"><embed src=\"../../" + filePath + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"></embed></object>";
            break;
        case "3":
            html = "<img src=\"" + file + "\" />";
            break;
        case "3":
            html = "<img src=\"" + file + "\" />";
            break;
    }
    return html;
}
/*更换验证码*/
function refVcode(idx, imgId) {
    var path = "";
    for (var i = 1; i <= idx; i++) {
        path += "../";
    }
    $("#" + imgId).attr("src", path + "Vcode.ashx?id=" + Math.random());
}

function IsURL(str_url) {
    var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
  + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //ftp的user@
        + "(([0-9]{1,3}.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184
        + "|" // 允许IP和DOMAIN（域名）
        + "([0-9a-z_!~*'()-]+.)*" // 域名- www.
        + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]." // 二级域名
        + "[a-z]{2,6})" // first level domain- .com or .museum
        + "(:[0-9]{1,4})?" // 端口- :80
        + "((/?)|" // a slash isn't required if there is no file name
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
    var re = new RegExp(strRegex);
    //re.test()
    if (re.test(str_url)) {
        return (true);
    } else {
        return (false);
    }
}

//判断输入的是否是整数
function checkNum(id) {
    var regexStr = new RegExp(/[0-9]/);
    var num = document.getElementById(id);
    if (!regexStr.test(num)) {
        num.focus();
        alert("非数字类型！");

    }
}

//禁止回车
function NoSubmit(event) {

    if (event.keyCode == 13) {
        return false;
    }
    return true;
}


// 只允许输入数字(建议 写在事件 onkeypress="return inputNum(this,event);")
function inputNum(obj, e) {
    //    var reg = new RegExp(/[0-9]{0,10}/g);
    //    if (reg.math(stake.value)) { return false }
    var whichCode = (window.Event && e.which) ? e.which : e.keyCode;
    if (whichCode == 13) return false;
    if (whichCode == 9) return true;
    if ((whichCode >= 48 && whichCode <= 57) || whichCode == 8) {
        return true;
    }
    return false;
}

//检查非数字 (建议 写在事件 onkeyup="FormatNum(this.id);")
function FormatNum(id) {
    var input = document.getElementById(id);
    input.value = input.value.replace(/[^\d\.]/g, '');
}

/*判断字符长度*/
function CheckLength(id, lengthstr) {
    var str = document.getElementById(id);
    if (str.value.length > lengthstr) {
        str.value = str.value.substring(0, lengthstr);
        return str.value = str.value;
    }
}

/*截取字符串*/
function subStr(str, lengthStr, defaultStr) {
    if (str.length > lengthStr) {
        str = str.substring(10, lengthstr) + defaultStr;
    }
    return str;
}
//替换特殊字符
function checkVal(val) {
    val = val.replace(/['',\\<,\\>,\\]/g, " ");
    return val;
}

/*数据格式化
*格式(##,###,###.00)
s:数字 n:小数位
*/
function FormatMoney(s, n) {
    if (s * 1 == 0) {
        return "0.00";
    }
    if (s != '' && s != null) {
        var fh = '';
        if (s * 1 == 0) {
            return "0.00";
        }
        else {
            if (s * 1 < 0) {
                s = -1 * s;
                fh = '-';
            }
            n = n >= 0 && n <= 20 ? n : 2;
            s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
            var l = s.split(".")[0].split("").reverse(), r = s.split(".")[1];
            t = "";
            for (i = 0; i < l.length; i++) {
                t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
            }
            if (n == 0) {
                return fh + t.split("").reverse().join("");
            }
            return fh + t.split("").reverse().join("") + "." + r;
        }
    } else {
        return '';
    }
}
//去除金币格式
function ClearFormat(n) {
    n = n.replace(" ", "");
    n = n.replace(/,/g, "");
    return n;
}

//JSON 时间字符串处理
function ChangeDateFormat(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) :
    date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() :
    date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}

//倒计时[天,时,分,秒]
function RemainTime(iTime) {
    var iDay, iHour, iMinute, iSecond;
    var sDay = "", sHour = "", sMinute = "", sSecond = "", sTime = [];
    if (iTime >= 0) {
        //天
        iDay = parseInt(iTime / 24 / 3600);
        if (iDay > 0) sTime.push(iDay); else sTime.push(0);

        //时
        iHour = parseInt((iTime / 3600) % 24);
        if (iHour > 0) sTime.push(iHour); else sTime.push(0);

        //分
        iMinute = parseInt((iTime / 60) % 60);
        if (iMinute > 0) sTime.push(iMinute); else sTime.push(0);

        //秒
        iSecond = parseInt(iTime % 60);
        if (iSecond > 0) sTime.push(iSecond); else sTime.push(0);
    } else {
        sTime = [0, 0, 0, 0];
    }
    return sTime;
}

//上传图片预览
//id：上传控件ID    panelDiv：预览效果容器
function imgShow(id, panelDiv, width, height, submitID) {
    var img = $("#" + panelDiv + " img");
    var fileInput = document.getElementById(id);
    var panel = document.getElementById(panelDiv);
    panel.innerHTML = "";
    if (fileInput.value.split('.')[1] != "swf") {
        if (!fileInput.value.match(/.jpg|.gif|.png|.bmp/i)) {
            panel.innerHTML = "<span class=\"red fz16 fw600\">文件格式不对！</span>";
            $("#" + submitID).attr("disabled", true);
            return false;
        } 
        else {
            $("#" + submitID).removeAttr("disabled");
        }
        panel.style.width = width = width == 0 ? "200px" : width;
        panel.style.height = height = height == 0 ? "120px" : height;
        if (fileInput.files && fileInput.files[0]) {
            img.attr({
                width: "auto",
                height: "auto",
                src: fileInput.files[0].getAsDataURL()
            }).show();
        } else if (panel.filters) {
            fileInput.select();
            var imgSrc = document.selection.createRange().text;
            panel.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = imgSrc;
            //panel.style.display = "block";
        }
    } else {
        panel.innerHTML = "<span class=\"red fz16 fw600\">不支持flash在线即时预览，您可以直接点击保存！</span>";
    }
}
//上传图片预览 清空预览效果并关闭容器
//pID：上传容器
function ClosePanel(pID) {
    var file = $("#" + pID + " input[type='file']");
    var div = $("#" + pID + " .panelDiv");
    div.html("");
    div.after(div.clone());
    div.remove();
    if (document.all) {
        file.after(file.clone());
        file.remove();
    }
    else
        file.val("");
    CloseDiv(pID);
}

//-转换为指定格式的日期字符串
//年月日时分秒 YYYYMMDD hhmmss
//参数说明：date：要转换的字符串 例如【2010-10-10】;pattern转换的日期格式 例如【YYYY年MM月DD日】
//
//
function FormatDate(date, pattern) {
    date = new Date(Date.parse(date.replace("-", "/"))); //转换为日期型
    function formatNumber(data, format) {
        format = format.length;
        data = data || 0;
        //return format == 1 ? data : String(Math.pow(10, format) + data).substr(-format); IE6里要用到
        return format == 1 ? data : (data = String(Math.pow(10, format) + data)).substr(data.length - format);
    }
    return pattern.replace(/([YMDhsm])\1*/g, function(format) {
        switch (format.charAt()) {
            case 'Y':
                return formatNumber(date.getFullYear(), format);
            case 'M':
                return formatNumber(date.getMonth() + 1, format);
            case 'D':
                return formatNumber(date.getDate(), format);
            case 'W':
                return date.getDay() + 1;
            case 'h':
                return formatNumber(date.getHours(), format);
            case 'm':
                return formatNumber(date.getMinutes(), format);
            case 's':
                return formatNumber(date.getSeconds(), format);
        }
    });
}

function GoPage(pCount) {
    var txtPageIdx = document.getElementById("txtPageIdx");
    if (txtPageIdx.value != "") {
        if (parseInt(txtPageIdx.value) > pCount || parseInt(txtPageIdx.value) <= 0) {
            txtPageIdx.focus();
            alert('错误页码 !');
        }
        else {
            var tmp = window.location.href.split("pIdx=");
            if (tmp[1]) {
                var pIdx = tmp[1].split("&");
                window.location.href = tmp[0] + "pIdx=" + txtPageIdx.value + "&" + pIdx[1];
            }
            else {
                window.location.href = window.location.href + "?pIdx=" + txtPageIdx.value;
            }
        }
    } else {
        alert('请输入跳转的页码 !');
    }
}