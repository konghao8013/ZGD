﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGD.Common
{
    public class MoneyHandler
    {

        /// <summary>
        /// 金钱转换转换(格式：00,000,000.00)
        /// </summary>
        /// <param name="obj">时间</param>
        /// <param name="fType">类型</param>
        /// <returns></returns>
        public static string FormatMoney(object money)
        {
            if (!string.IsNullOrEmpty(money.ToString()))
            {
                try
                {
                    return string.Format("{0:N2}", money);
                }
                catch { return "0.00"; }
            }
            else
                return "0.00";
        }
        /// <summary>
        /// 金钱转换转换(格式：00,000,000.0000)
        /// </summary>
        /// <param name="obj">时间</param>
        /// <param name="fType">类型</param>
        /// <returns></returns>
        public static string Formatmoney(object money)
        {
            try
            {
                return string.Format("{0:00.0000}", money);
            }
            catch { return "0.0000"; }
        }

        /// <summary>
        /// 转换汉字金额
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string MoneyToSimple(decimal money)
        {
            if (money >= 100000000)
            {
                return Convert.ToInt32(money / 100000000).ToString() + "亿";
            }
            else if (money >= 10000000)
            {
                //return Convert.ToInt32(money / 10000000).ToString() + "千万";
                return "千万";
            }
            else if (money >= 1000000)
            {
                //return Convert.ToInt32(money / 1000000).ToString() + "百万";
                return "百万";
            }
            else if (money >= 100000)
            {
                //return Convert.ToInt32(money / 100000).ToString() + "十万";
                return "十万";
            }
            else if (money >= 10000)
            {
                return Convert.ToInt32(money / 10000).ToString() + "万";
            }
            else if (money >= 1000)
            {
                return Convert.ToInt32(money / 1000).ToString() + "千";
            }
            else if (money >= 100)
            {
                return Convert.ToInt32(money / 100).ToString() + "百";
            }

            return Convert.ToInt32(money).ToString();
        }

        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /**/
        /// <summary> 
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
    }
}
