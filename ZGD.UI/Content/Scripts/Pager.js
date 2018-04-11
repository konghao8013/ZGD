
//-----------------------
//通用分页：页脚初始化
//-----------------------
//显示页码
var pageCountShow = 5;
//页码索引
var _pIdx = 1;

//页脚初始化
var InitPager = function(pIdx, pCount, rCount, PagerBackEvent) {
    _pIdx = pIdx;
    var tmp_pager = "共有<font class=\"fw600 or\" id=\"pagerCount\">{TMP_ROWCOUNT}</font>条数据&nbsp;页次<font class=\"fw600 red\">{TMP_CURIDX}/{TMP_PAGECOUNT}</font>&nbsp;{TMP_PAGELIST}";
    //var tmp_pager = "<div class=\"pager\" id=\"pager\">{TMP_PAGELIST}</div>";
    tmp_pager = tmp_pager.replace(/{TMP_ROWCOUNT}/g, rCount);
    tmp_pager = tmp_pager.replace(/{TMP_PAGECOUNT}/g, pCount);
    tmp_pager = tmp_pager.replace(/{TMP_CURIDX}/g, pIdx);

    //当前索引前后 可见页码数
    //如: 1 2 3 4 5 “6” 7 8 9 10
    var idxShowPager = 3,
        pagerStarIdx = 1, //页码拼接起始索引(保持选中项前idxShowPager位)
        pagerEndIdx = pCount; //页码结束索引(保持选中项前pagerEndIdx位)
    var pagerStr = new Array();

    //首页、上一页
    if (pIdx != 1 && pIdx > 0) {
        var perv = parseInt(pIdx) - 1;
        pagerStr.push("<a href=\"javascript:;\" onclick=\"" + PagerBackEvent + "(1);\">首页</a><a href=\"javascript:;\" onclick=\"" + PagerBackEvent + "(" + perv + ");\">上一页</a>");
    }

    //页码处理
    if (pCount > pageCountShow) {
        if (pIdx >= pageCountShow) {
            if (pIdx > idxShowPager) {
                //始终显示前面
                pagerStarIdx = pIdx - idxShowPager;
            }
            else {
                pagerStarIdx = pIdx;
            }

            //判断当前页码索引 是否超出总页数
            if (pIdx + idxShowPager > pCount) {
                pagerEndIdx = pCount;
            }
            else {
                pagerEndIdx = pIdx + idxShowPager;
            }
        }
        else {
            pagerEndIdx = pageCountShow;
        }
    }
    else {
        pagerEndIdx = pCount;
    }

    //拼接页码
    for (var i = pagerStarIdx; i <= pagerEndIdx; i++) {
        if (pIdx == i) {
            pagerStr.push("<span>" + i + "</span>");
        }
        else {
            pagerStr.push("<a href=\"javascript:;\" onclick=\"" + PagerBackEvent + "(" + i + ");\">" + i + "</a>");
        }
    }

    //尾页、下一页
    if (pIdx != pCount && pIdx < pCount) {
        var next = parseInt(pIdx) + 1;
        pagerStr.push("<a href=\"javascript:;\" onclick=\"" + PagerBackEvent + "(" + next + " );\" >下一页</a><a href=\"javascript:;\" onclick=\"" + PagerBackEvent + "(" + pCount + ");\">尾页</a>");
    }
    tmp_pager = tmp_pager.replace("{TMP_PAGELIST}", pagerStr.join(""));
    return tmp_pager;
}
