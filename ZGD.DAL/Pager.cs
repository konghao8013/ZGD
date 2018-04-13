using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ZGD.DBUtility;

namespace ZGD.DAL
{
    public class Pager
    {
        private string _Message = null;
        private string _tableName;
        private string _fieldsName;
        private string _orderField;
        private string _sqlWhere;
        private string _distinct = null;
        /// <summary>
        /// 页面可显示页码
        /// </summary>
        public static int _pageCountShow = 0;

        /// <summary>
        /// 初始化页码
        /// </summary>
        public Pager()
        {
            _pageCountShow = 8;
        }

        /// <summary>
        /// 信息提示(new)
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        /// <summary>
        /// 表名（多表以,号隔开）
        /// </summary>
        public string tableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// 显示数据字段（多字段以,号隔开）
        /// </summary>
        public string fieldsName
        {
            get { return _fieldsName; }
            set { _fieldsName = value; }
        }
        /// <summary>
        /// 排序字段（多字段以,号隔开，后加排序方式  如 "test asc,test1 desc"）
        /// </summary>
        public string orderField
        {
            get { return _orderField; }
            set { _orderField = value; }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string sqlWhere
        {
            get { return _sqlWhere; }
            set { _sqlWhere = value; }
        }
        /// <summary>
        /// 重复字段(可为空)
        /// </summary>
        public string distinct
        {
            get { return _distinct; }
            set { _distinct = value; }
        }

        /// <summary>
        /// 初始化页脚
        /// </summary>
        /// <param name="page">当前页索引</param>
        /// <param name="countShow">允许显示的页码</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="url">处理页面</param>
        /// <param name="urlParam">参数</param>
        /// <param name="isRoute">是否有重写路由</param>
        /// <returns></returns>
        public static string InitPageFooter(int page, int pageCount, int rowCount, string url, string urlParam, bool isRoute = true)
        {
            //当前索引前后 可见页码数
            //如: 1 2 3 4 5 “6” 7 8 9 10
            int idxShowPager = 4;
            //页码拼接起始索引(保持选中项前idxShowPager位)
            int pagerStarIdx = 1;
            //页码结束索引(保持选中项前pagerEndIdx位)
            int pagerEndIdx = pageCount;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"pager\">");
            //sb.Append("共<strong class=\"or\">" + rowCount + "</strong>条数据&nbsp;&nbsp;页次:<strong class=\"red\">" + page + "/" + pageCount + "</strong>页&nbsp;");

            //首页、上一页
            if (page != 1 && page > 0)
            {
                if (isRoute)
                {
                    sb.Append("<a href=\"" + url + (page - 1).ToString() + urlParam + "\">&lt; 上一页</a>");
                }
                else
                {
                    sb.Append("<a href=\"" + url + "?page=" + (page - 1).ToString() + urlParam + "\">&lt; 上一页</a>");
                }
            }
            else
            {
                sb.Append("<a href=\"javascript:;\">&lt; 上一页</a>");
            }
            //页码处理
            if (pageCount > _pageCountShow)
            {
                if (page >= _pageCountShow)
                {
                    if (page > idxShowPager)
                    {
                        //始终显示前面
                        pagerStarIdx = page - idxShowPager;
                    }
                    else
                    {
                        pagerStarIdx = page;
                    }

                    //判断当前页码索引 是否超出总页数
                    if (page + idxShowPager > pageCount)
                    {
                        pagerEndIdx = pageCount;
                    }
                    else
                    {
                        pagerEndIdx = page + idxShowPager;
                    }
                }
                else
                {
                    pagerEndIdx = _pageCountShow;
                }
            }
            else
            {
                pagerEndIdx = pageCount;
            }

            //拼接页码
            for (int i = pagerStarIdx; i <= pagerEndIdx; i++)
            {
                if (page == i)
                {
                    sb.Append("<span>" + i.ToString() + "</span>");
                }
                else
                {
                    if (isRoute)
                    {
                        sb.Append("<a href=\"" + url + i.ToString() + urlParam + "\">" + i.ToString() + "</a>");
                    }
                    else
                    {
                        sb.Append("<a href=\"" + url + "?page=" + i.ToString() + urlParam + "\">" + i.ToString() + "</a>");
                    }
                }
            }
            //尾页、下一页
            if (page != pageCount && page < pageCount)
            {
                if (isRoute)
                {
                    sb.Append("<a href=\"" + url + (page + 1).ToString() + urlParam + "\">下一页 &gt;</a>");
                }
                else
                {
                    sb.Append("<a href=\"" + url + "?page=" + (page + 1).ToString() + urlParam + "\">下一页 &gt;</a>");
                }
            }
            else
            {
                sb.Append("<a href=\"javascript:;\">下一页 &gt;</a>");
            }
            //sb.Append("&nbsp;&nbsp;<input id=\"txtPageIdx\" style=\"width:40px;height: 17px;padding: 1px 0 0 2px; border: solid 1px #ccc;text-align:center\" value=\"" + page + "\" />&nbsp;&nbsp;<input type=\"button\" value=\"GO\" class=\"hand\" onclick=\"GoPage(" + pageCount + ");\" />");
            sb.Append("</div>");
            sb.Append(CreateJs());
            return sb.ToString();
        }

        /// <summary>
        /// 创建页码调转JS
        /// </summary>
        /// <returns></returns>
        public static string CreateJs()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\"> \r\n");
            sb.Append("function GoPage(pCount) { \r\n");
            sb.Append("var txtPageIdx = document.getElementById(\"txtPageIdx\"); \r\n");
            sb.Append("var pIdx_val = txtPageIdx.value; \r\n");
            sb.Append("var reg = /^(([1-9]\\d*)|(0+))$/;");
            sb.Append("if(!reg.test(pIdx_val)){");
            sb.Append("alert(\"页码错误!\");");
            sb.Append("return;");
            sb.Append("}");
            sb.Append("if (pIdx_val != \"\") { \r\n");
            sb.Append("if (parseInt(pIdx_val) > pCount || parseInt(pIdx_val) <= 0) { \r\n");
            sb.Append("txtPageIdx.focus(); \r\n");
            sb.Append("alert('错误页码 !'); \r\n");
            sb.Append("} \r\n");
            sb.Append("else { \r\n");
            sb.Append("var tmp = window.location.href.split(\"page=\"); \r\n");
            sb.Append("if (tmp[1]) { \r\n");
            sb.Append("tmp[1]= tmp[1].replace(tmp[1].substring(0,tmp[1].indexOf('&'))+'&',''); \r\n");
            sb.Append(" window.location.href = tmp[0] + \"page=\" + pIdx_val + \"&\" + tmp[1]; \r\n");
            sb.Append("} \r\n");
            sb.Append("else { \r\n");
            sb.Append("window.location.href = window.location.href + \"?page=\"+ pIdx_val; \r\n");
            sb.Append("} \r\n");
            sb.Append("} \r\n");
            sb.Append("} else { \r\n");
            sb.Append("alert('请输入跳转的页码 !'); \r\n");
            sb.Append("} \r\n");
            sb.Append("} \r\n");
            sb.Append("</script> \r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="pageIdx">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="distinct">去重复</param>
        /// <returns></returns>
        public DataSet GetDataPage(int pageIdx, int pageSize, out int rowCount, out int pageCount, string distinct = "")
        {
            pageCount = 1;
            rowCount = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@TableName", SqlDbType.NVarChar,3000),
                    new SqlParameter("@Fields", SqlDbType.NVarChar,3000),
                    new SqlParameter("@OrderField", SqlDbType.NVarChar,1000),
                    new SqlParameter("@pageSize", SqlDbType.Int,4),
                    new SqlParameter("@pageIndex", SqlDbType.Int,4),
                    new SqlParameter("@sqlWhere", SqlDbType.NVarChar,3000),
                    new SqlParameter("@distinct", SqlDbType.NVarChar,50),
                    new SqlParameter("@rowCount", SqlDbType.Int,4)};
            parameters[0].Value = tableName;
            parameters[1].Value = fieldsName;
            parameters[2].Value = orderField;
            parameters[3].Value = pageSize;
            parameters[4].Value = pageIdx;
            parameters[5].Value = sqlWhere;
            parameters[6].Value = distinct;
            parameters[7].Direction = ParameterDirection.Output;

            DataSet ds = DbHelperSQL.RunProcedure("proc_GetSpiltePageData", parameters);
            if (parameters[7].Value != null && !string.IsNullOrEmpty(parameters[7].Value.ToString()))
            {
                rowCount = Convert.ToInt32(parameters[7].Value);
                if (rowCount < pageSize)
                {
                    pageCount = 1;
                }
                else
                {
                    int tmpRowCount = (rowCount % pageSize);
                    if (tmpRowCount > 0)
                    {
                        pageCount = (rowCount / pageSize) + 1;
                    }
                    else
                    {
                        pageCount = rowCount / pageSize;
                    }
                }
            }
            return ds;
        }
    }
}