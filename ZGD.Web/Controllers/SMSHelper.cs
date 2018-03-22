using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGD.Web.Controllers
{
    public class SMSHelper
    {
        static string word = "【致公党装饰】";
        static string smsAccount = ConfigurationManager.AppSettings["SmsAccount"].ToString();
        static string smsPassword = ConfigurationManager.AppSettings["SmsPassword"].ToString();

        /// <summary>
        /// 短信发送
        /// 16位短信编号	短信提交成功	请注意查收短信
        ///-01	账号余额不足 登录web.900112.com充值
        ///-02	未开通接口授权 联系相关的客户经理，开通产品授权
        ///-03	账号密码错误 联系相关的客户经理，核对注册资料，重置密码
        ///-04	参数个数不对或者参数类型错误 检查参数是否乱码或者参数传了空值
        ///-110	IP被限制 联系技术支持
        ///-12	其他错误 联系技术支持
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static AjaxMsgResult SendSms(string phone, string content)
        {
            AjaxMsgResult result = new AjaxMsgResult();
            result.Success = false;
            result.Msg = "发送失败！";
            if (string.IsNullOrWhiteSpace(phone))
            {
                result.Msg = "手机号码不能为空！";
                return result;
            }
            if (!Common.RegexHandler.IsMobile(phone))
            {
                result.Msg = "手机号码格式错误！";
                return result;
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                result.Msg = "短信内容不能为空！";
                return result;
            }
            content = content + word;
            SmsLinkWs.Service1 lk = new SmsLinkWs.Service1();
            string msg = lk.SendMessages(smsAccount, smsPassword, phone, content, "");
            if (!string.IsNullOrWhiteSpace(msg) && msg.Length == 16)
            {
                result.Msg = "发送成功，请注意查收短信！";
                result.Success = true;
                return result;
            }
            return result;
        }
    }
}