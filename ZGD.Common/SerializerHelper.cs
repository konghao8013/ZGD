using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace ZGD.Common
{
    public class SerializerHelper
    {
        public SerializerHelper() { }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                // open the stream...
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            FileStream fs = null;
            // serialize it...
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

        }

        /// <summary>
        /// unicode解码
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string DecodeUnicode(Match match)
        {
            if (!match.Success)
            {
                return null;
            }

            char outStr = (char)int.Parse(match.Value.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            return new string(outStr, 1);
        }


        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    t = (T)xmlSerializer.Deserialize(xmlReader);
                }
            }
            return t;
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static string XmlSerialize(object obj)
        {
            if (obj == null) return string.Empty;
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj, xsn);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }

        /// <summary>
        /// Json反序列化对象 基于Newtonsoft.Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) { return default(T); }
            var jSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(json, jSetting);
        }

        /// <summary>
        /// Json反序列化对象 基于Newtonsoft.Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject ParseJObject(string json)
        {
            if (string.IsNullOrEmpty(json)) { return null; }
            return JObject.Parse(json);
        }

        /// <summary>
        /// Json序列化对象 基于Newtonsoft.Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonSerialize(object obj)
        {
            if (obj == null) { return null; }
            var jSetting = new JsonSerializerSettings
            {
                //日期类型默认格式化处理
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, jSetting);
        }

    }
}
