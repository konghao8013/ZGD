﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZGD.BLL
{
    /// <summary>  
    /// 微信公众平台操作类  
    /// </summary>  
    public class wechat_api
    {
        //private string Token = "my_weixin_token"; //换成自己的token  
        //public void Auth()
        //{
        //    string echoStr = System.Web.HttpContext.Current.Request.QueryString["echoStr"];
        //    if (CheckSignature()) //校验签名是否正确  
        //    {
        //        if (!string.IsNullOrEmpty(echoStr))
        //        {
        //            System.Web.HttpContext.Current.Response.Write(echoStr); //返回原值表示校验成功  
        //            System.Web.HttpContext.Current.Response.End();
        //        }
        //    }
        //}
        //public void Handle(string postStr)
        //{
        //    //封装请求类  
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(postStr);
        //    XmlElement rootElement = doc.DocumentElement;
        //    //MsgType  
        //    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
        //    //接收的值--->接收消息类(也称为消息推送)  
        //    RequestXML requestXML = new RequestXML();
        //    requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
        //    requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
        //    requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
        //    requestXML.MsgType = MsgType.InnerText;

        //    //根据不同的类型进行不同的处理  
        //    switch (requestXML.MsgType)
        //    {
        //        case "text": //文本消息  
        //            requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
        //            break;
        //        case "image": //图片  
        //            requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
        //            break;
        //        case "location": //位置  
        //            requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
        //            requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
        //            requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
        //            requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
        //            break;
        //        case "link": //链接  
        //            break;
        //        case "event": //事件推送 支持V4.5+  
        //            break;
        //    }

        //    //消息回复  
        //    ResponseMsg(requestXML);
        //}
        ///// <summary>  
        ///// 验证微信签名  
        ///// * 将token、timestamp、nonce三个参数进行字典序排序  
        ///// * 将三个参数字符串拼接成一个字符串进行sha1加密  
        ///// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
        ///// </summary>  
        ///// <returns></returns>  
        //private bool CheckSignature()
        //{
        //    string signature = System.Web.HttpContext.Current.Request.QueryString["signature"];
        //    string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"];
        //    string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"];
        //    //加密/校验流程：  
        //    //1. 将token、timestamp、nonce三个参数进行字典序排序  
        //    string[] ArrTmp = { Token, timestamp, nonce };
        //    Array.Sort(ArrTmp);//字典排序  
        //    //2.将三个参数字符串拼接成一个字符串进行sha1加密  
        //    string tmpStr = string.Join("", ArrTmp);
        //    tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        //    tmpStr = tmpStr.ToLower();
        //    //3.开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
        //    if (tmpStr == signature)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>  
        ///// 消息回复(微信信息返回)  
        ///// </summary>  
        ///// <param name="requestXML">The request XML.</param>  
        //private void ResponseMsg(RequestXML requestXML)
        //{
        //    try
        //    {
        //        string resxml = "";
        //        //主要是调用数据库进行关键词匹配自动回复内容，可以根据自己的业务情况编写。  
        //        //1.通常有，没有匹配任何指令时，返回帮助信息  
        //        AutoResponse mi = new AutoResponse(requestXML.Content, requestXML.FromUserName);

        //        switch (requestXML.MsgType)
        //        {
        //            case "text":
        //                //在这里执行一系列操作，从而实现自动回复内容.   
        //                string _reMsg = mi.GetReMsg();
        //                if (mi.msgType == 1)
        //                {
        //                    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>2</ArticleCount><Articles>";
        //                    resxml += mi.GetRePic(requestXML.FromUserName);
        //                    resxml += "</Articles><FuncFlag>1</FuncFlag></xml>";
        //                }
        //                else
        //                {
        //                    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + _reMsg + "]]></Content><FuncFlag>1</FuncFlag></xml>";
        //                }
        //                break;
        //            case "location":
        //                string city = GetMapInfo(requestXML.Location_X, requestXML.Location_Y);
        //                if (city == "0")
        //                {
        //                    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[好啦，我们知道您的位置啦。您可以:" + mi.GetDefault() + "]]></Content><FuncFlag>1</FuncFlag></xml>";
        //                }
        //                else
        //                {
        //                    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[好啦，我们知道您的位置啦。您可以:" + mi.GetDefault() + "]]></Content><FuncFlag>1</FuncFlag></xml>";
        //                }
        //                break;
        //            case "image":
        //                //图文混合的消息 具体格式请见官方API“回复图文消息”   
        //                break;
        //        }

        //        System.Web.HttpContext.Current.Response.Write(resxml);
        //        WriteToDB(requestXML, resxml, mi.pid);
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
        //        //wx_logs.MyInsert("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
        //    }
        //}
        ///// <summary>  
        ///// unix时间转换为datetime  
        ///// </summary>  
        ///// <param name="timeStamp"></param>  
        ///// <returns></returns>  
        //private DateTime UnixTimeToTime(string timeStamp)
        //{
        //    DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    long lTime = long.Parse(timeStamp + "0000000");
        //    TimeSpan toNow = new TimeSpan(lTime);
        //    return dtStart.Add(toNow);
        //}
        ///// <summary>  
        ///// datetime转换为unixtime  
        ///// </summary>  
        ///// <param name="time"></param>  
        ///// <returns></returns>  
        //private int ConvertDateTimeInt(System.DateTime time)
        //{
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        //    return (int)(time - startTime).TotalSeconds;
        //}
        ///// <summary>  
        ///// 调用百度地图，返回坐标信息  
        ///// </summary>  
        ///// <param name="y">经度</param>  
        ///// <param name="x">纬度</param>  
        ///// <returns></returns>  
        //public string GetMapInfo(string x, string y)
        //{
        //    try
        //    {
        //        string res = string.Empty;
        //        string parame = string.Empty;
        //        string url = "http://maps.googleapis.com/maps/api/geocode/xml";

        //        parame = "latlng=" + x + "," + y + "&language=zh-CN&sensor=false";//此key为个人申请  
        //        res = webRequestPost(url, parame);

        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(res);

        //        XmlElement rootElement = doc.DocumentElement;
        //        string Status = rootElement.SelectSingleNode("status").InnerText;

        //        if (Status == "OK")
        //        {
        //            //仅获取城市  
        //            XmlNodeList xmlResults = rootElement.SelectSingleNode("/GeocodeResponse").ChildNodes;
        //            for (int i = 0; i < xmlResults.Count; i++)
        //            {
        //                XmlNode childNode = xmlResults[i];
        //                if (childNode.Name == "status")
        //                {
        //                    continue;
        //                }
        //                string city = "0";
        //                for (int w = 0; w < childNode.ChildNodes.Count; w++)
        //                {
        //                    for (int q = 0; q < childNode.ChildNodes[w].ChildNodes.Count; q++)
        //                    {
        //                        XmlNode childeTwo = childNode.ChildNodes[w].ChildNodes[q];
        //                        if (childeTwo.Name == "long_name")
        //                        {
        //                            city = childeTwo.InnerText;
        //                        }
        //                        else if (childeTwo.InnerText == "locality")
        //                        {
        //                            return city;
        //                        }
        //                    }
        //                }
        //                return city;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteTxt("map异常:" + ex.Message.ToString() + "Struck:" + ex.StackTrace.ToString());  
        //        return "0";
        //    }
        //    return "0";
        //}
        ///// <summary>  
        ///// Post 提交调用抓取  
        ///// </summary>  
        ///// <param name="url">提交地址</param>  
        ///// <param name="param">参数</param>  
        ///// <returns>string</returns>  
        //public string webRequestPost(string url, string param)
        //{
        //    byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);
        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
        //    req.Method = "Post";
        //    req.Timeout = 120 * 1000;
        //    req.ContentType = "application/x-www-form-urlencoded;";
        //    req.ContentLength = bs.Length;

        //    using (Stream reqStream = req.GetRequestStream())
        //    {
        //        reqStream.Write(bs, 0, bs.Length);
        //        reqStream.Flush();
        //    }

        //    using (WebResponse wr = req.GetResponse())
        //    {
        //        //在这里对接收到的页面内容进行处理  
        //        Stream strm = wr.GetResponseStream();
        //        StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

        //        string line;
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            sb.Append(line + System.Environment.NewLine);
        //        }
        //        sr.Close();
        //        strm.Close();
        //        return sb.ToString();
        //    }
        //}
        ///// <summary>  
        ///// 将本次交互信息保存至数据库中  
        ///// </summary>  
        ///// <param name="requestXML"></param>  
        ///// <param name="_xml"></param>  
        ///// <param name="_pid"></param>  
        //private void WriteToDB(RequestXML requestXML, string _xml, int _pid)
        //{
        //    WeiXinMsg wx = new WeiXinMsg();
        //    wx.FromUserName = requestXML.FromUserName;
        //    wx.ToUserName = requestXML.ToUserName;
        //    wx.MsgType = requestXML.MsgType;
        //    wx.Msg = requestXML.Content;
        //    wx.Creatime = requestXML.CreateTime;
        //    wx.Location_X = requestXML.Location_X;
        //    wx.Location_Y = requestXML.Location_Y;
        //    wx.Label = requestXML.Label;
        //    wx.Scale = requestXML.Scale;
        //    wx.PicUrl = requestXML.PicUrl;
        //    wx.reply = _xml;
        //    wx.pid = _pid;
        //    try
        //    {
        //        wx.Add();
        //    }
        //    catch (Exception ex)
        //    {
        //        //wx_logs.MyInsert(ex.Message);  
        //        //ex.message;  
        //    }
        //}
    }
}
