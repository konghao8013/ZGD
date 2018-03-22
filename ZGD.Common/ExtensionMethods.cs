using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZGD.Common
{

    public static class ExtensionMethods
    {

        /// <summary>
        /// 替换br
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string ToBR(this string val)
        {
            if (!string.IsNullOrWhiteSpace(val)) return val.Replace("\n", "<br />");
            return val;
        }
        /// <summary>
        /// 组装tag标签
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string ToNoImg(this string img)
        {
            if (string.IsNullOrWhiteSpace(img)) return "/Files/Default.jpg";
            return img;
        }

        /// <summary>
        /// 组装手机站 编辑器中的 图片链接
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string ToMobileImg(this string img)
        {
            if (string.IsNullOrWhiteSpace(img)) return "http://" + ZGD.Common.DTKeys.Web + "/Files/Default.jpg";
            img = img.Replace("/UpLoadFiles/", "http://" + ZGD.Common.DTKeys.Web + "/UpLoadFiles/");
            return img;
        }

        /// <summary>
        /// 组装tag标签
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string ToTags(this string tags)
        {
            if (string.IsNullOrWhiteSpace(tags)) return "";
            tags = tags.Trim().Trim(',');
            string[] tag_list = tags.Split(',');
            StringBuilder sb = new StringBuilder();
            int idx = 0;
            foreach (string item in tag_list)
            {
                if (idx > 3) break;
                sb.Append("<a href=\"/news?key=" + item + "\" title=\"" + item + "\">" + item + "</a>");
                idx++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 判断年龄段
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string AgePart(this int age)
        {
            string ages = "";
            if (age > 5)
            {
                int agec = age % 5;
                if (agec == 0)
                {
                    ages = (age - 5 + 1) + "-" + age + "岁";
                }
                else if (agec > 0)
                {
                    ages = (age - agec + 1) + "-" + (age + (5 - agec)) + "岁";
                }

            }
            else { ages = "0-5岁"; }
            return ages;
        }

        /// <summary>
        /// 替换危险字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>result</returns>
        public static string FunStr(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            str = str.Replace("&", "&amp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt");
            str = str.Replace("'", "''");
            str = str.Replace("*", "");
            str = str.Replace("\n", "<br/>");
            str = str.Replace("\r\n", "<br/>");
            str = str.Replace("select", "");
            str = str.Replace("insert", "");
            str = str.Replace("update", "");
            str = str.Replace("delete", "");
            str = str.Replace("create", "");
            str = str.Replace("drop", "");
            str = str.Replace("delcare", "");
            str = str.Replace("   ", "&nbsp;");
            str = str.Trim();
            return str;
        }

        /// <summary>
        /// 创建项目链接  用于多项目（）
        /// </summary>
        /// <param name="value">格式: 割双眼皮|2,开眼角|3</param>
        /// <param name="defaultStr"></param>
        /// <param name="count">查询几个，0不限制</param>
        /// <returns></returns>
        public static string CreateProjectUrl(this string value, string defaultStr = "未知项目", int count = 0)
        {
            string proUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.TrimEnd(',');
                int i = 1;
                foreach (string pro in value.Split(','))
                {
                    if (count > 0 && i > count) break;
                    string[] proInfo = pro.Split('|');
                    if (proInfo.Length > 0)
                        proUrl += "<a href=\"/Project/" + proInfo[1] + "\">" + proInfo[0] + "</a>+";
                    i++;
                }
            }
            return string.IsNullOrWhiteSpace(proUrl) ? defaultStr : proUrl.TrimEnd('+');
        }

        /// <summary>
        /// 选择性返回json
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static string SelectToJson<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var data = source.Select(selector);
            if (data.Count() == 0)
                return "[]";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return json;
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            if (source == null)
                return "{}";
            return Newtonsoft.Json.JsonConvert.SerializeObject(source);
        }

        public static string ToSubString(this string value, int length, string character = "...")
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length < length) return value;
            return value.Substring(0, length) + character;
        }

        /// <summary>
        /// 输出安全电话
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPhone(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value.Length == 11 ? new Regex(@"(\d{3})(\d{4})(\d{4})").Replace(value, "$1****$3") : value;
            return value;
        }

        /// <summary>
        /// 输出安全QQ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToQQ(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value.Length > 4 ? value.Substring(0, 4) + "****" : value;
            return value;
        }

        /// <summary>
        /// 输出安全微信号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToWechat(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value.Length > 4 ? value.Substring(0, 4) + "****" : value;
            return value;
        }

        /// <summary>
        /// 表情转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToExepresstion(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                System.Text.RegularExpressions.Regex rex =
                new System.Text.RegularExpressions.Regex(@"^\d+$");
                string[] ss1 = value.Split(new[] { "/s" }, StringSplitOptions.None);
                string newvalues = "";
                foreach (var item in ss1)
                {
                    if (item.Length > 0)//对象不能为空
                    {
                        if (rex.IsMatch(item))//判断是否是数字
                        {
                            if (item.Length >= 3)//数字长度大于3
                            {
                                string empt = item.Substring(0, 3);
                                if (Convert.ToInt32(empt) >= 1 && Convert.ToInt32(empt) <= 85)
                                {
                                    newvalues += item.Replace(empt, "<img src='/Files/Expression/" + empt + ".png' class='Toexpresstionlist' />");
                                }
                                else
                                {
                                    newvalues += item;//非图标数字
                                }
                            }
                            else
                            {
                                newvalues += item;//非图标数字
                            }
                        }
                        else//非数字即字符和数字
                        {
                            #region //字符串中提取数字
                            string returnStr = string.Empty;
                            for (int i = 0; i < item.Length; i++)
                            {
                                if (Char.IsNumber(item, i) == true)
                                {
                                    returnStr += item.Substring(i, 1);
                                }
                            }
                            #endregion

                            if (returnStr.Length == 0)//没数字全是字符
                            {
                                newvalues += item;
                            }
                            else//数字和字符
                            {
                                int numlength = returnStr.Length;//字符串中数字长度
                                if (numlength >= 3)
                                {
                                    string empt = item.Substring(0, 3);//获取前三位
                                    if (rex.IsMatch(empt))//判断前三位是否数字
                                    {
                                        if (Convert.ToInt32(empt) >= 1 && Convert.ToInt32(empt) <= 85)
                                        {
                                            newvalues += item.Replace(empt, "<img src='/Files/Expression/" + empt + ".png' class='Toexpresstionlist' />");
                                        }
                                        else
                                        {
                                            newvalues += item;
                                        }
                                    }
                                    else//前三位非数字原样输出
                                    {
                                        newvalues += item;
                                    }
                                }
                                else//字符串中数字长度小于3
                                {
                                    newvalues += item;//原样输出
                                }
                            }
                        }
                    }
                }
                return newvalues;
            }
            return "";
        }



        public static string ToEmail(this string value)
        {
            const string pattern = @"^(?<header>\w).*?@";
            Regex regex = new Regex(pattern);
            var macth = regex.Match(value);
            if (macth.Success)
            {
                var replaceValue = macth.Groups["header"].Value + "****@";
                return Regex.Replace(value, pattern, replaceValue);
            }
            return value;
        }

        public static string[] StringSplit(this string source, string split)
        {
            string[] tmp = new string[1];
            int index = source.IndexOf(split, 0);
            if (index < 0)
            {
                tmp[0] = source;
                return tmp;
            }
            tmp[0] = source.Substring(0, index);
            return StringSplit(source.Substring(index + split.Length), split, tmp);
        }
        private static string[] StringSplit(string source, string split, string[] attachArray)
        {
            string[] tmp = new string[attachArray.Length + 1];
            attachArray.CopyTo(tmp, 0);

            int index = source.IndexOf(split, 0);
            if (index < 0)
            {
                tmp[attachArray.Length] = source;
                return tmp;
            }
            tmp[attachArray.Length] = source.Substring(0, index);
            return StringSplit(source.Substring(index + split.Length), split, tmp);
        }

        public static string ToTrimZero(this decimal? value)
        {
            if (!value.HasValue) return string.Empty;
            return ToTrimZero(value.Value.ToString(CultureInfo.InvariantCulture));
        }

        public static string ToTrimZero(this decimal value)
        {
            return ToTrimZero(value.ToString(CultureInfo.InvariantCulture));
        }

        public static string ToTrimZero(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.IndexOf('.') > -1 ? value.TrimEnd('0').TrimEnd('.') : value;
        }

        public static string ToReplaceAllRegex(this string content, string regex, string replacement)
        {
            return Regex.Replace(content, regex, replacement, RegexOptions.IgnoreCase);
        }

        public static string ToReplaceAllString(this string content, string where, string replacement)
        {
            return string.IsNullOrEmpty(content) ? string.Empty : Regex.Replace(content, where, replacement, RegexOptions.IgnoreCase);
        }

        public static string ToReplaceAllRed(this string content, string where)
        {
            var replacement = string.Format("<label style='color:red;'>{0}</label>", where);
            return ToReplaceAllString(content, where, replacement);
        }

        public static string ToName(this string name, string sex)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            name = name.Substring(0, 1) + (sex == "男" ? "先生" : "女士");
            return name;
        }

        public static string ToNickName(this string nickname, string cName, string sex)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                if (string.IsNullOrEmpty(cName)) return string.Empty;
                cName = cName.Substring(0, 1) + (sex == "男" ? "先生" : "女士");
                return cName;
            }
            return nickname;
        }

        public static string ToSqlFilter(this string inText)
        {
            if (string.IsNullOrEmpty(inText)) return string.Empty;
            const string word =
                "ABORT|ABS|ABSOLUTE|ACCESS|ACTION|ADA|ADD|ADMIN|AFTER|AGGREGATE|ALIAS|ALL|ALLOCATE|ALTER|ANALYSE|ANALYZE|AND|ANY|ARE|ARRAY|AS|ASC|ASENSITIVE|ASSERTION|ASSIGNMENT|ASYMMETRIC|AT|ATOMIC|AUTHORIZATION|AVG|BACKWARD|BEFORE|BEGIN|BETWEEN|BIGINT|BINARY|BIT|BITVAR|BIT_LENGTH|BLOB|BOOLEAN|BOTH|BREADTH|BY|C|CACHE|CALL|CALLED|CARDINALITY|CASCADE|CASCADED|CASE|CAST|CATALOG|CATALOG_NAME|CHAIN|CHAR|CHARACTER|CHARACTERISTICS|CHARACTER_LENGTH|CHARACTER_SET_CATALOG|CHARACTER_SET_NAME|CHARACTER_SET_SCHEMA|CHAR_LENGTH|CHECK|CHECKED|CHECKPOINT|CLASS|CLASS_ORIGIN|CLOB|CLOSE|CLUSTER|COALESCE|COBOL|COLLATE|COLLATION|COLLATION_CATALOG|COLLATION_NAME|COLLATION_SCHEMA|COLUMN|COLUMN_NAME|COMMAND_FUNCTION|COMMAND_FUNCTION_CODE|COMMENT|COMMIT|COMMITTED|COMPLETION|CONDITION_NUMBER|CONNECT|CONNECTION|CONNECTION_NAME|CONSTRAINT|CONSTRAINTS|CONSTRAINT_CATALOG|CONSTRAINT_NAME|CONSTRAINT_SCHEMA|CONSTRUCTOR|CONTAINS|CONTINUE|CONVERSION|CONVERT|COPY|CORRESPONDING|COUNT|CREATE|CREATEDB|CREATEUSER|CROSS|CUBE|CURRENT|CURRENT_DATE|CURRENT_PATH|CURRENT_ROLE|CURRENT_TIME|CURRENT_TIMESTAMP|CURRENT_USER|CURSOR|CURSOR_NAME|CYCLE|DATA|DATABASE|DATE|DATETIME_INTERVAL_CODE|DATETIME_INTERVAL_PRECISION|DAY|DEALLOCATE|DEC|DECIMAL|DECLARE|DEFAULT|DEFERRABLE|DEFERRED|DEFINED|DEFINER|DELETE|DELIMITER|DELIMITERS|DEPTH|DEREF|DESC|DESCRIBE|DESCRIPTOR|DESTROY|DESTRUCTOR|DETERMINISTIC|DIAGNOSTICS|DICTIONARY|DISCONNECT|DISPATCH|DISTINCT|DO|DOMAIN|DOUBLE|DROP|DYNAMIC|DYNAMIC_FUNCTION|DYNAMIC_FUNCTION_CODE|EACH|ELSE|ENCODING|ENCRYPTED|END|END-EXEC|EQUALS|ESCAPE|EVERY|EXCEPT|EXCEPTION|EXCLUSIVE|EXEC|EXECUTE|EXISTING|EXISTS|EXPLAIN|EXTERNAL|EXTRACT|FALSE|FETCH|FINAL|FIRST|FLOAT|FOR|FORCE|FOREIGN|FORTRAN|FORWARD|FOUND|FREE|FREEZE|FROM|FULL|FUNCTION|G|GENERAL|GENERATED|GET|GLOBAL|GO|GOTO|GRANT|GRANTED|GROUP|GROUPING|HANDLER|HAVING|HIERARCHY|HOLD|HOST|HOUR|IDENTITY|IGNORE|ILIKE|IMMEDIATE|IMMUTABLE|IMPLEMENTATION|IMPLICIT|IN|INCREMENT|INDEX|INDICATOR|INFIX|INHERITS|INITIALIZE|INITIALLY|INNER|INOUT|INPUT|INSENSITIVE|INSERT|INSTANCE|INSTANTIABLE|INSTEAD |INT|INTEGER|INTERSECT|INTERVAL|INTO|INVOKER|IS|ISNULL|ISOLATION|ITERATE|JOIN|K|KEY|KEY_MEMBER|KEY_TYPE|LANCOMPILER|LANGUAGE|LARGE|LAST|LATERAL|LEADING|LEFT|LENGTH|LESS|LEVEL|LIKE|LIMIT|LISTEN|LOAD|LOCAL|LOCALTIME|LOCALTIMESTAMP|LOCATION|LOCATOR|LOCK|LOWER|M|MAP|MATCH|MAX|MAXVALUE|MESSAGE_LENGTH|MESSAGE_OCTET_LENGTH|MESSAGE_TEXT|METHOD|MIN|MINUTE|MINVALUE|MOD|MODE|MODIFIES|MODIFY|MODULE|MONTH|MORE|MOVE|MUMPS|NAME|NAMES|NATIONAL|NATURAL|NCHAR|NCLOB|NEW|NEXT|NO|NOCREATEDB|NOCREATEUSER|NONE|NOT|NOTHING|NOTIFY|NOTNULL|NULL|NULLABLE|NULLIF|NUMBER|NUMERIC|OBJECT|OCTET_LENGTH|OF|OFF|OFFSET|OIDS|OLD|ON|ONLY|OPEN|OPERATION|OPERATOR|OPTION|OPTIONS|OR|ORDER|ORDINALITY|OUT|OUTER|OUTPUT|OVERLAPS|OVERLAY|OVERRIDING|OWNER|PAD|PARAMETER|PARAMETERS|PARAMETER_MODE|PARAMETER_NAME|PARAMETER_ORDINAL_POSITION|PARAMETER_SPECIFIC_CATALOG|PARAMETER_SPECIFIC_NAME|PARAMETER_SPECIFIC_SCHEMA|PARTIAL|PASCAL|PASSWORD|PATH|PENDANT|PLACING|PLI|POSITION|POSTFIX|PRECISION|PREFIX|PREORDER|PREPARE|PRESERVE|PRIMARY|PRIOR|PRIVILEGES|PROCEDURAL|PROCEDURE|PUBLIC|READ|READS|REAL|RECHECK|RECURSIVE|REF|REFERENCES|REFERENCING|REINDEX|RELATIVE|RENAME|REPEATABLE|REPLACE|RESET|RESTRICT|RESULT|RETURN|RETURNED_LENGTH|RETURNED_OCTET_LENGTH|RETURNED_SQLSTATE|RETURNS|REVOKE|RIGHT|ROLE |ROLLBACK|ROLLUP|ROUTINE|ROUTINE_CATALOG|ROUTINE_NAME|ROUTINE_SCHEMA|ROW|ROWS|ROW_COUNT|RULE|SAVEPOINT|SCALE|SCHEMA|SCHEMA_NAME|SCOPE|SCROLL|SEARCH|SECOND|SECTION|SECURITY|SELECT|SELF|SENSITIVE|SEQUENCE|SERIALIZABLE|SERVER_NAME|SESSION|SESSION_USER|SET|SETOF|SETS|SHARE|SHOW|SIMILAR|SIMPLE|SIZE|SMALLINT|SOME|SOURCE|SPACE|SPECIFIC|SPECIFICTYPE|SPECIFIC_NAME|SQL|SQLCODE|SQLERROR|SQLEXCEPTION|SQLSTATE|SQLWARNING|STABLE|START|STATE|STATEMENT|STATIC|STATISTICS|STDIN|STDOUT|STORAGE|STRICT|STRUCTURE|STYLE|SUBCLASS_ORIGIN|SUBLIST|SUBSTRING|SUM|SYMMETRIC|SYSID|SYSTEM|SYSTEM_USER|TABLE|TABLE_NAME|TEMP|TEMPLATE|TEMPORARY|TERMINATE|THAN|THEN|TIME|TIMESTAMP|TIMEZONE_HOUR|TIMEZONE_MINUTE|TO|TOAST|TRAILING|TRANSACTION|TRANSACTIONS_COMMITTED|TRANSACTIONS_ROLLED_BACK|TRANSACTION_ACTIVE|TRANSFORM|TRANSFORMS|TRANSLATE|TRANSLATION|TREAT|TRIGGER|TRIGGER_CATALOG|TRIGGER_NAME|TRIGGER_SCHEMA|TRIM|TRUE|TRUNCATE|TRUSTED|TYPE|UNCOMMITTED|UNDER|UNENCRYPTED|UNION|UNIQUE|UNKNOWN|UNLISTEN|UNNAMED|UNNEST|UNTIL|UPDATE|UPPER|USAGE|USER|USER_DEFINED_TYPE_CATALOG|USER_DEFINED_TYPE_NAME|USER_DEFINED_TYPE_SCHEMA|USING|VACUUM|VALID|VALIDATOR|VALUE|VALUES|VARCHAR|VARIABLE|VARYING|VERBOSE|VERSION|VIEW|VOLATILE|WHEN|WHENEVER|WHERE|WITH|WITHOUT|WORK|WRITE|YEAR|ZONE";
            const string symbol = "'|\"|-|/|*";
            if (string.IsNullOrEmpty(inText)) return string.Empty;
            var words = word.Split('|');
            inText = words.Aggregate(inText, (current, item) => current.ToUpper().Replace(item, ""));
            var symbols = symbol.Split('|');
            return symbols.Aggregate(inText, (current, item) => current.ToUpper().Replace(item, ""));
        }

        public static string ArrayToString(IEnumerable arr, char join)
        {
            string reStr = string.Empty;
            foreach (string obj in arr)
            {
                reStr += obj + join;
            }
            reStr.TrimEnd(join);
            return reStr;
        }

        /// 经纬度之间的距离
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double rad = EARTH_RADIUS; //Earth radius in Km
            double p1X = lat1 / 180 * Math.PI;
            double p1Y = lng1 / 180 * Math.PI;
            double p2X = lat2 / 180 * Math.PI;
            double p2Y = lng2 / 180 * Math.PI;
            return Math.Acos(Math.Sin(p1Y) * Math.Sin(p2Y) +
                Math.Cos(p1Y) * Math.Cos(p2Y) * Math.Cos(p2X - p1X)) * rad;
        }
        private const double EARTH_RADIUS = 6378.137;//地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        /// <summary>   
        /// 取得HTML中所有图片的 URL。   
        /// </summary>   
        /// <param name="sHtmlText">HTML代码</param>   
        /// <returns>图片的URL列表</returns>   
        public static string[] GetHtmlImageUrlList(this string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }



        public static string GetHttpRequest(string requestUrl, int timeout = 3000)
        {
            string result;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.Default);
                result = streamReader.ReadToEnd();
                //StringBuilder builder = new StringBuilder();
                //while (streamReader.Peek() != -1)
                //{
                //    builder.Append(streamReader.ReadLine());
                //}
                //result = builder.ToString();
            }
            catch (Exception exception)
            {

                result = "错误: " + exception.Message;
            }
            return result;
        }

        public static string PostHttpRequest(string requestUrl, IDictionary<string, string> paraementers,
            int timeout = 3000)
        {
            return "";
        }

        public static byte[] ToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

    }
}
