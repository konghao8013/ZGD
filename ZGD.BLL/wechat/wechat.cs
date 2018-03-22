using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using LitJson;

namespace ZGD.BLL
{
    public class wechat
    {
        private readonly Model.SiteConfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.wechat dal;

        public wechat()
        {
            dal = new DAL.wechat(siteConfig.sysdatabaseprefix);
        }


        #region 绑定

        public int bind_Edit(Model.wechat_bind model)
        {
            return dal.bind_Edit(model);
        }
        public Model.wechat_bind bind_Model()
        {
            return dal.bind_Model();
        }
        #endregion

        #region 素材
        public bool fodder_Exists(int id)
        {
            return dal.fodder_Exists(id);
        }
        /// <summary>
        /// 增加一条数据及其子表
        /// </summary>
        public int fodder_Add(Model.wechat_fodder model)
        {
            return dal.fodder_Add(model);
        }
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public int fodder_Update(int id, string xml)
        {
            return dal.fodder_Update(id, xml);
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        public DataSet fodder_Query(string type)
        {
            return dal.fodder_Query(type);
        }
        public DataSet fodder_Query()
        {
            return dal.fodder_Query();
        }
        public DataSet fodder_Query(int id)
        {
            return dal.fodder_Query(id);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public int fodder_Delete(int id)
        {
            return dal.fodder_Delete(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wechat_fodder fodder_model(int id)
        {
            return dal.fodder_model(id);
        }
        #endregion

        #region 回复
        public bool reply_Exists(int id)
        {
            return dal.reply_Exists(id);
        }
        public bool reply_Exists(string type, string key)
        {
            return dal.reply_Exists(type, key);
        }
        public bool reply_Exists(int id, string type, string key)
        {
            return dal.reply_Exists(id, type, key);
        }
        public bool reply_Exists(string type)
        {
            return dal.reply_Exists(type);
        }
        public int reply_Add(Model.wechat_reply model)
        {
            return dal.reply_Add(model);
        }
        public int reply_Update(int id, int fodder_id)
        {
            return dal.reply_Update(id, fodder_id);
        }
        public int reply_Update(int id, int fodder_id, string key)
        {
            return dal.reply_Update(id, fodder_id, key);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet reply_Query(int page, int pageSize, string strWhere, string sortName, out int totalCount)
        {
            int pageCount = 0, rowCount = 0;
            DataSet ds = dal.reply_Query(page, pageSize, strWhere, sortName, out rowCount, out pageCount);
            totalCount = rowCount;
            return ds;
        }
        public DataSet reply_Query(string type)
        {
            return dal.reply_Query(type);
        }
        public int reply_Delete(int id)
        {
            return dal.reply_Delete(id);
        }
        public Model.wechat_reply reply_model(int id)
        {
            return dal.reply_model(id);
        }
        public Model.wechat_reply reply_model(string type)
        {
            return dal.reply_model(type);
        }

        #endregion

        #region 菜单
        public DataTable menu_list(int parentid)
        {
            return dal.menu_list(parentid);
        }
        public DataTable menu_list()
        {
            return dal.menu_list();
        }
        public int menu_add(Model.wechat_menu model)
        {
            return dal.menu_add(model);
        }
        public int menu_update(Model.wechat_menu model)
        {
            return dal.menu_update(model);
        }
        public bool menu_update_fild(int id, string strValue)
        {
            return dal.menu_update_fild(id, strValue);
        }
        public int menu_Delete(int id)
        {
            return dal.menu_Delete(id);
        }
        public int menu_count(int id)
        {
            return dal.menu_count(id);
        }
        public Model.wechat_menu menu_model(int id)
        {
            return dal.menu_model(id);
        }
        #endregion


        #region dataTable转换成Json格式
        /// <summary>      
        /// dataTable转换成Json格式      
        /// </summary>      
        /// <param name="dt"></param>      
        /// <returns></returns>      
        public string ToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName.ToString());
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    string v = dt.Rows[i][j].ToString();
                    v = v.Replace("\"", "\\\"");
                    jsonBuilder.Append(v);
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        #endregion dataTable转换成Json格式



        public string xml_txt()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[{0}]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[{1}]]></FromUserName>");
            sb.Append("<CreateTime>{2}</CreateTime>");
            sb.Append("<MsgType><![CDATA[text]]></MsgType>");
            sb.Append("<Content><![CDATA[{3}]]></Content>");
            sb.Append("<FuncFlag>0</FuncFlag>");
            sb.Append("</xml>");
            return sb.ToString();
        }

        #region 生成菜单
        public string GetAccessToken()
        {
            try
            {
                Model.wechat_bind bind = dal.bind_Model();
                if (bind == null) { return "nobind"; }
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
                string token = "";

                WebClient webClient = new WebClient();
                Byte[] bytes = webClient.DownloadData(string.Format("{0}&appid={1}&secret={2}", url, bind.appid, bind.appsecret));
                string result = Encoding.GetEncoding("utf-8").GetString(bytes);

                JsonData jd = JsonMapper.ToObject(result);
                token = (String)jd["access_token"];
                return token;
            }
            catch
            {
                return "";
            }
        }
        public string get_menu_str()
        {
            StringBuilder sb = new StringBuilder();
            List<Model.wechat_menu> list = dal.menu_list(" and menu_parent_id=0");
            if (list.Count > 0)
            {
                sb.Append("{\"button\":[");

                foreach (Model.wechat_menu m in list)
                {
                    List<Model.wechat_menu> clist = dal.menu_list(" and menu_parent_id=" + m.id);


                    string s = "";
                    if (m.menu_type == "click")
                    { s = string.Format("\"key\":\"{0}\"", m.menu_key); }
                    else
                    { s = string.Format("\"url\":\"{0}\"", m.menu_url); }

                    if (clist.Count == 0)
                    {
                        sb.Append("{");
                        sb.AppendFormat("\"type\":\"{0}\",\"name\":\"{1}\",{2}", m.menu_type, m.menu_name, s);
                        sb.Append("},");
                    }
                    else
                    {
                        sb.Append("{");
                        sb.AppendFormat("\"name\":\"{0}\",", m.menu_name);
                        sb.Append("\"sub_button\":[");
                        foreach (Model.wechat_menu cm in clist)
                        {
                            string cs = "";
                            if (cm.menu_type == "click")
                            { cs = string.Format("\"key\":\"{0}\"", cm.menu_key); }
                            else
                            { cs = string.Format("\"url\":\"{0}\"", cm.menu_url); }
                            sb.Append("{");
                            sb.AppendFormat("\"type\":\"{0}\",\"name\":\"{1}\",{2}", cm.menu_type, cm.menu_name, cs);
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("]},");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]}");

            }
            return sb.ToString();
        }
        public bool create_menu()
        {
            string menu_str = get_menu_str();
            string token = GetAccessToken();
            if (token == "") { return false; }
            try
            {
                string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + token;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(menu_str);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = requestBytes.Length;

                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                string backstr = sr.ReadToEnd();
                sr.Close();
                res.Close();

                JsonData js = JsonMapper.ToObject(backstr);
                if ((String)js["errmsg"] == "ok")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        public string[] view_wechat_reply_fodder(string type, string key)
        {
            return dal.view_wechat_reply_fodder(type, key);
        }
        public string[] view_wechat_menu_fodder(string key)
        {
            return dal.view_wechat_menu_fodder(key);
        }
        public void write_log(string str)
        {
            dal.write_log(str);
        }
        public DataSet log_list()
        {
            return dal.log_list();
        }
        public void clearLog()
        {
            dal.clearLog();
        }
    }
}
