using YJ.Common;
using YJ.Wechat;
using YJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.IO;

namespace YJ.Web.Controllers
{
    public class WeChatController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 微信公共平台
        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        /// <param name="signature">微信加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <returns></returns>
        public ActionResult API(string signature, string timestamp, string nonce, string echostr)
        {
            Model.wechat_bind bind = n_wechat.bind_Model();
            if (bind != null)
            {
                if (bind.wechat_check == 1)
                {
                    if (Request.HttpMethod.ToUpper() == "GET")
                    {
                        bool flag = CheckSignature(bind.token, signature, timestamp, nonce);
                        n_wechat.write_log(Request.RawUrl);
                        if (flag)
                        {
                            return Content(echostr);
                        }
                    }
                }
                else
                {
                    if (Request.HttpMethod.ToUpper() == "POST")
                    {
                        //string kx = "<xml><ToUserName><![CDATA[gh_a9d5b701c450]]></ToUserName>";
                        //kx += "<FromUserName><![CDATA[on0I2t76FpoEWbgnYYVKv9d3UfXA]]></FromUserName>";
                        //kx += "<CreateTime>1403254613</CreateTime>";
                        //kx += "<MsgType><![CDATA[text]]></MsgType>";
                        //kx += "<Content><![CDATA[1]]></Content>";
                        //kx += "<MsgId>6026932670996800050</MsgId>";
                        //kx += "</xml>";

                        StreamReader stream = new StreamReader(Request.InputStream);
                        string xmlstr = stream.ReadToEnd();
                        n_wechat.write_log(xmlstr.ToString());
                        //记录来源请求信息
                        xml_model xml = initXml(xmlstr);
                        string result = processRequest(xml);
                        //记录反馈请求信息
                        n_wechat.write_log(result.ToString());
                        return Content(result);
                    }
                }
            }
            return Content("");
        }
        
        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult BindMember(string code)
        {
            Model.wechat_bind bind = n_wechat.bind_Model();
            AccessToken accessToken = new AccessToken();
            ViewBag.OpenID = "";
            ViewBag.Msg = "";
            ViewBag.IsBind = false;
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string Code = Request.QueryString["code"].ToString();
                //获得Token
                accessToken = WeiXinTool.GetUserAccessToken(bind.appid, bind.appsecret, code);
                if (accessToken != null)
                {
                    //获取用户信息
                    //OAuthUser oAuthUser = WeiXinTool.Instance.GetUserInfo(accessToken.access_token, accessToken.openid);
                    //if (oAuthUser != null)
                    //{
                    //    ViewBag.Msg = "code传值：" + Code + "<br />获取值access_token：" + accessToken.access_token + "<br />用户OPENID:" + oAuthUser.openid + "<br>用户昵称:" + oAuthUser.nickname + "<br>性别:" + oAuthUser.sex + "<br>所在省:" + oAuthUser.province + "<br>所在市:" + oAuthUser.city + "<br>所在国家:" + oAuthUser.country + "<br>头像地址:" + oAuthUser.headimgurl + "<br>用户特权信息:" + oAuthUser.privilege;
                    //}
                    //else
                    //{
                    ViewBag.Msg = "code：" + Code + "<br />access_token：" + accessToken.access_token + "<br />refresh_token：" + accessToken.refresh_token + "<br />openid：" + accessToken.openid + "<br />scope：" + accessToken.scope + "<br />";
                    ViewBag.OpenID = accessToken.openid;
                    DataSet ds = new BLL.VipCard().GetList(" OpenId='" + accessToken.openid + "'");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.IsBind = true;
                        return Redirect("/WeiXin/VipQy");
                    }
                    //}
                }
                else
                {
                    ViewBag.Msg = "code：" + Code;
                }
            }
            else
            {
                ViewBag.Msg = "无传值";
            }
            return View(accessToken);
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="vipNo"></param>
        /// <returns></returns>
        public JsonResult CheckBind(string openId, string vipNo)
        {
            BLL.VipCard bllVip = new BLL.VipCard();
            if (string.IsNullOrWhiteSpace(openId))
            {
                return Json(new AjaxMsgResult
                {
                    Success = false,
                    Msg = "微信验证失败！"
                });
            }
            if (string.IsNullOrWhiteSpace(vipNo))
            {
                return Json(new AjaxMsgResult
                {
                    Success = false,
                    Msg = "请入住VIP卡号！"
                });
            }
            VipCard model = bllVip.GetModel(vipNo);
            if (model == null)
            {
                return Json(new AjaxMsgResult
                {
                    Success = false,
                    Msg = "此卡号不存在，欲获取尊卡身份请与小浔联系"
                });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(model.OpenId))
                {
                    return Json(new AjaxMsgResult
                    {
                        Success = false,
                        Msg = "请入的VIP卡已绑定，请勿重复操作"
                    });
                }
            }
            if (bllVip.BindVip(vipNo, openId) > 0)
            {
                return Json(new AjaxMsgResult
                {
                    Success = true,
                    Msg = "注册成功，感谢您选择港宏宜家，系统已确定您的VIP身份，待我们的合约进去集团总部备案后，客户经理会通知您"
                });
            }
            return Json(new AjaxMsgResult
            {
                Success = false,
                Msg = "绑定失败"
            });
        }

        /// <summary>
        /// 绑定用户成功
        /// </summary>
        /// <returns></returns>
        public ActionResult BindSuccess()
        {
            return View();
        }
        /// <summary>
        /// VIP权益
        /// </summary>
        /// <returns></returns>
        public ActionResult VipQy()
        {
            return View();
        }
        /// <summary>
        /// VIP新家
        /// </summary>
        /// <returns></returns>
        public ActionResult VipXj()
        {
            return View();
        }

        #endregion

        BLL.wechat n_wechat = new BLL.wechat();

        #region 验证签名
        /// <summary>  
        /// 验证微信签名  
        /// * 将token、timestamp、nonce三个参数进行字典序排序  
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密  
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
        /// </summary>  
        /// <returns></returns>  
        private bool CheckSignature(string token, String signature, String timestamp, String nonce)
        {

            //加密/校验流程：  
            //1. 将token、timestamp、nonce三个参数进行字典序排序  
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);//字典排序  
            //2.将三个参数字符串拼接成一个字符串进行sha1加密  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();


            new BLL.wechat().write_log("signature:" + signature + ";tmpStr:" + tmpStr + ";token:" + token);

            //3.开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 回复
        /// <summary>
        /// 封装提交数据
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public xml_model initXml(string xmlstr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlstr);
            XmlElement rootElement = doc.DocumentElement;
            xml_model xml = new xml_model();
            xml.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            xml.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            xml.MsgType = rootElement.SelectSingleNode("MsgType").InnerText;
            xml.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            if (xml.MsgType == "event")
            {
                xml.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                xml.Event = rootElement.SelectSingleNode("Event").InnerText;
            }
            if (xml.MsgType == "text")
            {
                xml.Content = rootElement.SelectSingleNode("Content").InnerText;
                xml.MsgId = rootElement.SelectSingleNode("MsgId").InnerText;
            }
            if (xml.MsgType == "voice")
            {
                xml.Content = rootElement.SelectSingleNode("Recognition").InnerText;
            }

            return xml;
        }
        /// <summary>
        /// 处理微信发来的请求 
        /// </summary>
        /// <param name="xml"></param>
        public string processRequest(xml_model xml)
        {
            try
            {
                if (xml.MsgType == "text")//文字消息
                {
                    return replyKey(xml);
                }
                else if (xml.MsgType == "event")// 事件类型 
                {
                    if (xml.Event == "subscribe")// 订阅  
                    {
                        string[] res = n_wechat.view_wechat_reply_fodder("attention", "");
                        return getXMLtemp(xml, res[0], res[1]);
                    }
                    else if (xml.Event == "unsubscribe")// 取消订阅 
                    {
                        // TODO 取消订阅后用户再收不到公众号发送的消息，因此不需要回复消息  
                    }
                    else if (xml.Event == "CLICK")// 自定义菜单点击事件 
                    {
                        return replyEventKey(xml);// TODO 自定义菜单权没有开放，暂不处理该类消息  
                    }
                }
                else if (xml.MsgType == "location")
                {
                }
                else
                    if (xml.MsgType == "voice")
                    {
                        return replyVoice(xml);
                    }

            }
            catch (Exception e)
            {

            }
            return "";
        }

        #region 关键字回复
        /// <summary>
        /// 关键字回复
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string replyKey(xml_model xml)
        {

            string key = xml.Content;
            string[] res = n_wechat.view_wechat_reply_fodder("key", key);
            if (res[0] == "")
            {
                res = n_wechat.view_wechat_reply_fodder("default", "");
            }
            return getXMLtemp(xml, res[0], res[1]);
        }
        #endregion

        #region 语音回复
        /// <summary>
        /// 语音回复
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string replyVoice(xml_model xml)
        {

            string key = xml.Content;
            string[] res = n_wechat.view_wechat_reply_fodder("key", key);
            if (res[0] == "")
            {
                //res = n_wechat.view_wechat_reply_fodder("default", "");
                res[0] = "txt";
                res[1] = "对不起，您说的语言资料[" + key + "]未找到！";
            }
            return getXMLtemp(xml, res[0], res[1]);
        }
        #endregion

        #region 事件回复
        /// <summary>
        /// 事件回复
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string replyEventKey(xml_model xml)
        {
            string key = xml.EventKey;
            string[] res = n_wechat.view_wechat_menu_fodder(key);
            if (res[0] == "")
            {
                res = n_wechat.view_wechat_reply_fodder("default", "");
            }
            return getXMLtemp(xml, res[0], res[1]);
        }
        #endregion

        #region 回复模板
        string getXMLtemp(xml_model xmlModel, string replyType, object obj)
        {
            string base_path = Request.Url.Scheme + "://" + Request.Url.Authority;
            n_wechat.write_log(base_path);
            string xml = "";
            switch (replyType)
            {
                case "txt":
                    xml = "<xml><ToUserName><![CDATA[" + xmlModel.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + xmlModel.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + obj.ToString() + "]]></Content><FuncFlag>1</FuncFlag></xml>";
                    break;
                case "new":
                    string jsonstr = formatJsonStr(obj.ToString());
                    LitJson.JsonData json = LitJson.JsonMapper.ToObject(jsonstr);
                    xml = "<xml>";
                    xml += "<ToUserName><![CDATA[" + xmlModel.FromUserName + "]]></ToUserName>";
                    xml += "<FromUserName><![CDATA[" + xmlModel.ToUserName + "]]></FromUserName>";
                    xml += "<CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
                    xml += "<MsgType><![CDATA[news]]></MsgType>";
                    xml += "<ArticleCount>1</ArticleCount>";
                    xml += "<Articles>";
                    xml += "<item>";
                    xml += "<Title><![CDATA[" + json["title"].ToString() + "]]></Title> ";
                    xml += "<Description><![CDATA[" + json["des"].ToString() + "]]></Description>";

                    xml += "<PicUrl><![CDATA[" + base_path + json["cover"].ToString() + "]]></PicUrl>";
                    xml += "<Url><![CDATA[" + json["link"].ToString() + "]]></Url>";
                    xml += "</item>";
                    xml += "</Articles>";
                    xml += "</xml> ";

                    break;
                case "news":
                    string jsonstr3 = formatJsonStr(obj.ToString());
                    LitJson.JsonData jsons = LitJson.JsonMapper.ToObject(jsonstr3);
                    xml = "<xml>";
                    xml += "<ToUserName><![CDATA[" + xmlModel.FromUserName + "]]></ToUserName>";
                    xml += "<FromUserName><![CDATA[" + xmlModel.ToUserName + "]]></FromUserName>";
                    xml += "<CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
                    xml += "<MsgType><![CDATA[news]]></MsgType>";
                    xml += "<ArticleCount>" + jsons.Count + "</ArticleCount>";
                    xml += "<Articles>";
                    for (int i = 0; i < jsons.Count; i++)
                    {
                        LitJson.JsonData cjson = jsons[i];
                        xml += "<item>";
                        xml += "<Title><![CDATA[" + cjson["title"].ToString() + "]]></Title> ";
                        xml += "<Description><![CDATA[]]></Description>";
                        xml += "<PicUrl><![CDATA[" + base_path + cjson["cover"].ToString() + "]]></PicUrl>";
                        xml += "<Url><![CDATA[" + cjson["link"].ToString() + "]]></Url>";
                        xml += "</item>";
                    }
                    xml += "</Articles>";
                    xml += "</xml> ";

                    break;
            }
            return xml;
        }

        #endregion
        #endregion

        #region 其它

        /// <summary>  
        /// datetime转换为unixtime  
        /// </summary>  
        /// <param name="time"></param>  
        /// <returns></returns>  
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public string formatJsonStr(string jsonstr)
        {
            jsonstr = Regex.Replace(jsonstr, @"title\s{0,}:\s{0,}""", "\"title\":\"");
            jsonstr = Regex.Replace(jsonstr, @"cover\s{0,}:\s{0,}""", "\"cover\":\"");
            jsonstr = Regex.Replace(jsonstr, @"des\s{0,}:\s{0,}""", "\"des\":\"");
            jsonstr = Regex.Replace(jsonstr, @"link\s{0,}:\s{0,}""", "\"link\":\"");
            return jsonstr;
        }
        #endregion
    }
    public class xml_model
    {
        public string ToUserName;
        public string FromUserName;
        public string CreateTime;
        public string MsgType;
        public string Event;
        public string EventKey;
        public string Content;
        public string MsgId;
    }
}