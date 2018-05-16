//=============================================================================
//
// Project:������ʽ��
// Author: LJ
// Data:2010-4-28 ��дʱ��
// Updated:2010-4-28 �޸�ʱ��
// Remark:����--��������
//
//=============================================================================
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

namespace ZGD.Common
{    
    public class RegexHandler
    {
        public static SortedList<string, string> RegexItem = new SortedList<string, string>();
        public static void InitRegexItem()
        {
            RegexItem.Add("intege", "^-?[1-9]\\d*$");//���� 
            RegexItem.Add("intege1","^[1-9]\\d*$");//������
            RegexItem.Add("intege2","^-[1-9]\\d*$");//������
            RegexItem.Add("num","^([+-]?)\\d*\\.?\\d+$");//����
            RegexItem.Add("num1","^[1-9]\\d*|0$");//������������ + 0��
            RegexItem.Add("num2","^-[1-9]\\d*|0$");//������������ + 0��
            RegexItem.Add("decmal","^([+-]?)\\d*\\.\\d+$");//������
            RegexItem.Add("decmal1","^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$");//��������
            RegexItem.Add("decmal2","^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$");//��������
            RegexItem.Add("decmal3","^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$");//������
            RegexItem.Add("decmal4","^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$");//�Ǹ����������������� + 0��
            RegexItem.Add("decmal5","^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$");//�������������������� + 0��
            RegexItem.Add("email","^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$"); //�ʼ� 
            RegexItem.Add("color","^[a-fA-F0-9]{6}$");//��ɫ
            RegexItem.Add("url","^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$");//url 
            RegexItem.Add("chinese","^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$"); //������ 
            RegexItem.Add("ascii","^[\\x00-\\xFF]+$"); //��ACSII�ַ�
            RegexItem.Add("zipcode","^\\d{6}$"); //�ʱ�
            RegexItem.Add("mobile","^(1)[0-9]{9}$"); //�ֻ�
            RegexItem.Add("ip4","^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$"); //ip��ַ
            RegexItem.Add("picture","(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$"); //ͼƬ
            RegexItem.Add("rar","(.*)\\.(rar|zip|7zip|tgz)$"); //ѹ���ļ� 
            RegexItem.Add("date","^\\d{4}(\\-|\\/|\\.)\\d{1,2}\\1\\d{1,2}$"); //���� 
            RegexItem.Add("qq","^[1-9]*[1-9][0-9]*$"); //QQ���� 
            RegexItem.Add("tel","^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$");//�绰����ĺ���(������֤��������,��������,�ֻ���) 
            RegexItem.Add("username","^\\w+$"); //�����û�ע�ᡣƥ�������֡�26��Ӣ����ĸ�����»�����ɵ��ַ��� 
            RegexItem.Add("letter","^[A-Za-z]+$");//��ĸ 
            RegexItem.Add("letter_u","^[A-Z]+$");//��д��ĸ 
            RegexItem.Add("letter_l","^[a-z]+$");//Сд��ĸ 
            RegexItem.Add("idcard","^[1-9]([0-9]{14}|[0-9]{17})$");//���֤ 
            RegexItem.Add("ps_username","^[\\u4E00-\\u9FA5\\uF900-\\uFA2D_\\w]+$");//���ġ���ĸ������
        }


        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);


        public static string StrReplace(string matchStr, string replaceStr, string regexStr)
        {
           return  Regex.Replace(matchStr, regexStr, replaceStr);
        }
        /// <summary>
        /// ��֤�ַ���
        /// </summary>
        /// <param name="matchStr">����֤�ı�</param>
        /// <param name="regexStr">������ʽ</param>
        /// <returns></returns>
        public static bool RegexStr(string matchStr, string regexStr)
        {
            Match m = Regex.Match(matchStr, regexStr);
            return m.Success;
        }


        /// <summary>
        /// ����Ƿ�����ȷ��Url
        /// </summary>
        /// <param name="strUrl">Ҫ��֤��Url</param>
        /// <returns>�жϽ��</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// ����Ƿ����email��ʽ
        /// </summary>
        /// <param name="strEmail">Ҫ�жϵ�email�ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        /// <summary>
        /// ����Ƿ���SqlΣ���ַ�
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// ����Ƿ���Σ�յĿ����������ӵ��ַ���
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|�ο�|^Guest");
        }

        /// <summary>
        /// ��������ַ����еĻس������з�
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        /// <returns>����󷵻ص��ַ���</returns>
        public static string ClearBR(string str)
        {
            Match m = null;

            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }
        
        /// <summary>
        /// ����ƥ��ָ����ǩ�ڵ�html
        /// </summary>
        /// <param name="matchStr">ƥ���ı�</param>
        /// <param name="htmlTitle">html��ǩ</param>
        /// <param name="replaceStr">�滻�ı�</param>
        /// <returns></returns>
        public string Gettitle(string matchStr, string htmlTitle, string replaceStr)
        {
            Regex rg = new Regex("<" + htmlTitle + ">.*</" + htmlTitle + ">");
            return rg.Replace(matchStr, replaceStr);
        }

        /// <summary>
        /// �Ƿ������ַ���
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = Regex.Match(inputData, @"^?\d+$");
            return m.Success;
        }

        /// <summary>
        /// �Ƴ�Html���
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ��HTML�л�ȡ�ı�,����br,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string html)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regEx.Replace(html, "");
        }

        /// <summary>
        /// ����HTML�еĲ���ȫ��ǩ
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>    
        /// ��Json���л���ʱ����/Date(1294499956278+0800)תΪ�ַ���    
        /// </summary>    
        public static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>    
        /// ��ʱ���ַ���תΪJsonʱ��  
        ///  </summary>   
        public static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        
        /// <summary>
        /// �Ƿ��ֻ�����
        /// </summary>
        /// <param name="input">Ҫ�жϵ��ֻ�����</param>
        /// <returns></returns>
        public static bool IsMobile(string input)
        {
            if (!Regex.IsMatch(input, @"^[1][1-9]\d{9}$", RegexOptions.IgnoreCase))
                return false;
            if (input.Length == 11 && (input.StartsWith("13") || input.StartsWith("14") || input.StartsWith("15") || input.StartsWith("17") || input.StartsWith("18")))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �Ƿ�Ϊ����
        /// </summary>
        /// <param name="input">Ҫ�жϵ��ʼ�</param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            if (!Regex.IsMatch(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase))
                return false;
            return true;
        }

        /// <summary>  
        /// ��ȡ������ͼƬ��ַ
        /// </summary>  
        /// <param name="html">��������</param>  
        /// <returns></returns>  
        public static List<string> GetImgList(string html)
        {
            List<string> imgs = new List<string>();
            Regex r = new Regex(@"<IMG[^>]+src=\s*(?:'(?<src>[^']+)'|""(?<src>[^""]+)""|(?<src>[^>\s]+))\s*[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection mc = r.Matches(html);

            foreach (Match m in mc)
            {
                imgs.Add(m.Groups["src"].Value.ToLower());
            }
            return imgs;
        }
    }
}
