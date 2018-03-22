using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using YJ.Common.Alipay;
using System.Collections.Specialized;
using YJ.Common;

namespace YJ.Web.Controllers
{
    public class PayController : BaseController
    {
        public DateTime DefaultTime = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 支付表单页面
        /// </summary>
        /// <param name="m">金额</param>
        /// <param name="t">类型  1活动 </param>
        /// <param name="id">关联id  </param>
        /// <returns></returns>
        public ActionResult Index(decimal m, string t = "", string id = "")
        {
            ViewBag.PayMoney = m;
            ViewBag.AssId = t;
            ViewBag.AssType = id;
            return View();
        }

        /// <summary>
        /// 新增付款记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(Model.PayOrder model)
        {
            Model.AjaxMsgResult result = new Model.AjaxMsgResult();
            result.Msg = "提交失败！";
            result.Success = false;
            if (model == null)
            {
                result.Msg = "未找到付款表单信息！";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                result.Msg = "请输入名称！";
                return Json(result);
            }
            if (!Common.RegexHandler.IsMobile(model.Phone))
            {
                result.Msg = "手机号码格式有误！";
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.vCode))
            {
                result.Msg = "请输入验证码！";
                return Json(result);
            }
            else
            {
                if (model.vCode.ToLower() != Session[DTKeys.SESSION_CODE].ToString().ToLower())
                {
                    result.Msg = "验证码错误！";
                    return Json(result);
                }
            }
            if (model.AssType == 1)
            {
                YJ.BLL.Activity bllAct = new BLL.Activity();
                YJ.Model.Activity actModel = bllAct.GetModel(model.AssID.Value);
                if (actModel == null)
                {
                    result.Msg = "未找到活动信息！";
                    return Json(result);
                }
            }
            model.PayTime = DateTime.Now;
            int msg = new BLL.PayOrder().Add(model);
            if (msg > 0)
            {
                result.Msg = "付款表单提交成功，点击“确定”将跳转至付款页面！";
                result.Source = "/pay/alipayto?no=" + msg;
                result.Success = true;
            }
            return Json(result);
        }

        #region 支付宝    支付跳转空白页面
        public ActionResult AlipayTO(int no)
        {
            //获取数据
            BLL.PayOrder bll = new BLL.PayOrder();
            var orderModel = bll.GetModel(no);
            string sHtmlText = "";
            if (orderModel != null)
            {
                ////////////////////////////////////////////请求参数////////////////////////////////////////////

                //支付类型
                string payment_type = "1";
                //必填，不能修改
                //服务器异步通知页面路径
                string notify_url = AlipayConfig.Notify_Url;
                //需http://格式的完整路径，不能加?id=123这类自定义参数       

                //页面跳转同步通知页面路径
                string return_url = AlipayConfig.Return_Url;
                //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/        
                //卖家支付宝帐户
                string seller_email = AlipayConfig.Seller_Email;
                //必填      

                //商户订单号
                string out_trade_no = orderModel.ID.ToString();
                //商户网站订单系统中唯一订单号，必填       

                //订单名称
                string subject = "港宏宜家在线付款";//ECORderModel.OrderSN;
                //必填      

                //付款金额
                string total_fee = orderModel.PayMoney.ToString();
                //必填      

                //订单描述        
                string body = "港宏宜家在线付款";
                //商品展示地址
                string show_url = orderModel.AssType == 1 ? "http://www" + System.Configuration.ConfigurationManager.AppSettings["Domain"] + "/activity/" + orderModel.AssID : "http://www" + System.Configuration.ConfigurationManager.AppSettings["Domain"] + "/activity";
                //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html       

                //防钓鱼时间戳
                string anti_phishing_key = "";
                //若要使用请调用类文件submit中的query_timestamp函数      

                //客户端的IP地址
                string exter_invoke_ip = "";
                //非局域网的外网IP地址，如：221.0.0.1


                ////////////////////////////////////////////////////////////////////////////////////////////////

                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                sParaTemp.Add("partner", AlipayConfig.Partner);
                sParaTemp.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
                sParaTemp.Add("service", "create_direct_pay_by_user");
                sParaTemp.Add("payment_type", payment_type);
                sParaTemp.Add("notify_url", notify_url);
                sParaTemp.Add("return_url", return_url);
                sParaTemp.Add("seller_email", seller_email);
                sParaTemp.Add("out_trade_no", out_trade_no);
                sParaTemp.Add("subject", subject);
                sParaTemp.Add("total_fee", total_fee);
                sParaTemp.Add("body", body);
                sParaTemp.Add("show_url", show_url);
                sParaTemp.Add("anti_phishing_key", anti_phishing_key);
                sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

                //建立请求
                sHtmlText = YJ.Common.Alipay.Submit.BuildRequest(sParaTemp, "get", "确认");

            }
            else
            {
                sHtmlText = "<script>$(function () {layer.alert('请求失败，请返回重试！');window.location.href ='/activity';});</script>";
                orderModel = new Model.PayOrder();
            }
            orderModel.HtmlText = sHtmlText;
            return View(orderModel);
        }
        #endregion

        #region 支付宝反馈
        /// <summary>
        /// 支付异步 回调
        /// </summary>
        /// <returns></returns>
        public ActionResult PayNotify()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            //日志记录
            YJ.Common.LogHepler.WriteLog("sPara:" + Request.Form["notify_id"] + "**" + Request.Form["sign"] + " \n");

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                BLL.PayOrder bll = new BLL.PayOrder();
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                //日志记录
                YJ.Common.LogHepler.WriteLog("verifyResult:" + verifyResult + " \n");
                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"];
                    int order_no = string.IsNullOrWhiteSpace(out_trade_no) ? 0 : Convert.ToInt32(out_trade_no);

                    //支付宝交易号

                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];

                    //日志记录
                    YJ.Common.LogHepler.WriteLog("out_trade_no:" + out_trade_no + "      trade_no:" + trade_no + "     trade_status:" + trade_status + " \n");

                    if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        var TheECOrder = bll.GetModel(order_no);
                        if (TheECOrder == null)
                        {
                            return Content("fail");
                        }
                        if (TheECOrder.PayTime == null || TheECOrder.PayTime.Value <= DefaultTime)
                        {
                            TheECOrder.PayTime = DateTime.Now;
                        }
                        if (string.IsNullOrWhiteSpace(TheECOrder.PayID))
                        {
                            TheECOrder.PayID = trade_no;
                        }
                        TheECOrder.Status = 1;
                        //日志记录
                        YJ.Common.LogHepler.WriteLog("PayTime:" + TheECOrder.PayTime.ToString() + "     Status:" + TheECOrder.Status + " \n");
                        bool msg = bll.Update(TheECOrder);

                        //发短信
                        if (!string.IsNullOrWhiteSpace(TheECOrder.Phone) && YJ.Common.RegexHandler.IsMobile(TheECOrder.Phone))
                        {
                            string sms = "恭喜您支付成功，您的付款码为" + TheECOrder.ID;
                            SMSHelper.SendSms(TheECOrder.Phone, sms);
                        }
                        //日志记录
                        YJ.Common.LogHepler.WriteLog("result:" + msg.ToString() + " \n");

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。
                    }
                    else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        var TheECOrder = bll.GetModel(order_no);
                        if (TheECOrder == null)
                        {
                            return Content("fail");
                        }
                        if (TheECOrder.PayTime == null || TheECOrder.PayTime.Value <= DefaultTime)
                        {
                            TheECOrder.PayTime = DateTime.Now;
                        }
                        if (string.IsNullOrWhiteSpace(TheECOrder.PayID))
                        {
                            TheECOrder.PayID = trade_no;
                        }
                        TheECOrder.Status = 1;
                        //日志记录
                        YJ.Common.LogHepler.WriteLog("PayTime:" + TheECOrder.PayTime.ToString() + "     Status:" + TheECOrder.Status + " \n");
                        bool msg = bll.Update(TheECOrder);

                        //发短信
                        if (!string.IsNullOrWhiteSpace(TheECOrder.Phone) && YJ.Common.RegexHandler.IsMobile(TheECOrder.Phone))
                        {
                            string sms = "恭喜您支付成功，您的付款码为" + TheECOrder.ID;
                            SMSHelper.SendSms(TheECOrder.Phone, sms);
                        }

                        //日志记录
                        YJ.Common.LogHepler.WriteLog("result:" + msg.ToString() + " \n");
                        //注意：
                        //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。
                    }
                    else
                    {
                        //日志记录
                        YJ.Common.LogHepler.WriteLog("两个if都没进！ \n");
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    return Content("success");

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
            }
        }

        /// <summary>
        /// 支付同步 回调
        /// </summary>
        /// <returns></returns>
        public ActionResult PayReturn()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();
            ViewBag.Message = "支付失败！";
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.QueryString["out_trade_no"];
                    int order_no = string.IsNullOrWhiteSpace(out_trade_no) ? 0 : Convert.ToInt32(out_trade_no);

                    //支付宝交易号
                    string trade_no = Request.QueryString["trade_no"];

                    //交易状态
                    string trade_status = Request.QueryString["trade_status"];
                    BLL.PayOrder bll = new BLL.PayOrder();
                    var TheECOrder = bll.GetModel(order_no);
                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (TheECOrder == null)
                        {
                            ViewBag.Message = "无订单信息！";
                        }
                        else
                        {
                            if (TheECOrder.PayTime != null && TheECOrder.PayTime > DefaultTime && TheECOrder.Status == 1 && !string.IsNullOrWhiteSpace(TheECOrder.PayID))
                            {
                                ViewBag.Message = "恭喜您，支付成功！";
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(TheECOrder.PayID))
                                {
                                    TheECOrder.PayID = trade_no;
                                }
                                if (TheECOrder.PayTime <= DefaultTime || TheECOrder.PayTime == null)
                                {
                                    TheECOrder.PayTime = DateTime.Now;
                                }
                                TheECOrder.Status = 1;
                                bool result = bll.Update(TheECOrder);
                                ViewBag.Message = "恭喜您，支付成功！";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "trade_status=" + Request.QueryString["trade_status"];
                    }

                    //打印页面
                    ViewBag.ActID = TheECOrder.ID;
                    ViewBag.Message = "恭喜您，支付成功！";

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    ViewBag.Message = "验证失败！";
                }
            }
            else
            {
                ViewBag.Message = "无返回参数！";
            }
            return View();
        }


        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        #endregion
    }
}
