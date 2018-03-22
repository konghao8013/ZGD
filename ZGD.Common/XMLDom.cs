//=============================================================================
//
// Project:XML操作类
// Author: LJ
// Data:2010-4-28 编写时间
// Updated:2010-4-28 修改时间
// Remark:XML操作--公共方法
//
//=============================================================================
//使用的时候用SetConfigName方法设置文件名
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;

namespace ZGD.Common
{
    public class XMLDom
    {
        /// <summary>
        /// 获取文件服务器路径（如："~/File/Img/xxx.txt"）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 获取xml文件dom对象
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDom(string xmlPath)
        {
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.Load(GetMapPath(xmlPath));
            return xmlDom;
        }

        /// <summary>
        /// 获取一个节点的属性
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public static string ReadConfig(string xmlPath, string configKey)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodes = xmlDom.GetElementsByTagName("add");
            foreach (XmlNode node in nodes)
            {
                XmlAttribute att = node.Attributes["key"];
                if (att.Value == configKey)
                {
                    att = node.Attributes["value"];
                    return att.Value;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取指定节点 指定属性的值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="configKey"></param>
        /// <param name="keyAttributes">属性key</param>
        /// <returns></returns>
        public static string ReadConfig(string xmlPath, string configKey, string keyAttributes)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodes = xmlDom.GetElementsByTagName("add");
            foreach (XmlNode node in nodes)
            {
                XmlAttribute att = node.Attributes["key"];
                if (att.Value == configKey)
                {
                    att = node.Attributes[keyAttributes];
                    return att.Value;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取指定节点 指定属性的值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="keyAttributes">属性key</param>
        /// <param name="nodeInfo">节点名</param>
        /// <returns></returns>
        public static List<Hashtable> ReadConfig(string xmlPath, List<string> keyAttributes, string nodeInfo)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodes = xmlDom.GetElementsByTagName(nodeInfo);
            List<Hashtable> list = new List<Hashtable>();

            foreach (XmlNode node in nodes)
            {
                Hashtable gTable = new Hashtable();
                foreach (string key in keyAttributes)
                {
                    XmlAttribute att = node.Attributes[key];
                    if (att != null)
                        gTable.Add(key, att.Value);
                }
                if (gTable != null)
                {
                    gTable.Add("Text", node.InnerText);
                    list.Add(gTable);
                }
            }
            return list;
        }

        /// <summary>
        /// 读取一个节点的内容
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <returns></returns>
        public static string ReadXml(string xmlPath, string node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            StringBuilder sb = new StringBuilder();
            XmlNode objNode = xmlDom.SelectSingleNode(node);
            if (objNode == null)
            {
                return "-1";
            }
            else
            {
                foreach (XmlNode node1 in objNode.ChildNodes)
                {
                    sb.Append(node1.InnerText);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取指定节点所有属性值
        /// key,value
        /// </summary>
        /// <param name="xmlPath">文件地址</param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static Hashtable GetXMLAttributes(string xmlPath, string nodeName)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            Hashtable table = new Hashtable();
            XmlNodeList nodes = xmlDom.GetElementsByTagName(nodeName);
            foreach (XmlNode node in nodes)
            {
                table.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
            }
            return table;
        }

        /// <summary>
        /// 获取指定节点所有指定属性值
        /// </summary>
        /// <param name="xmlPath">文件地址</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static List<string> GetXMLAttributes(string xmlPath, string nodeName, string attrName)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            List<string> list = new List<string>();
            XmlNodeList nodes = xmlDom.GetElementsByTagName(nodeName);
            foreach (XmlNode node in nodes)
            {
                list.Add(node.Attributes[attrName].Value);
            }
            return list;
        }

        /// <summary>
        /// 读取一个节点下所有内容(不含标记)
        /// 生成数据(值)
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <returns></returns>
        public static List<string> ReadXmlToList(string xmlPath, string node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            List<string> list = null;
            XmlNodeList nodeList = xmlDom.SelectNodes(node);
            if (nodeList.Count > 0)
            {
                list = new List<string>();
                foreach (XmlNode childNode in nodeList)
                {
                    foreach (XmlNode childNode2 in childNode.ChildNodes)
                    {
                        list.Add(childNode2.InnerText);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 读取一个节点下所有内容
        /// 生成数据字典集合(key：节点名 value：节点值)
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlToDictionary(string xmlPath, string node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            Dictionary<string, string> dic = null;
            XmlNodeList nodeList = xmlDom.SelectNodes(node);
            if (nodeList.Count > 0)
            {
                dic = new Dictionary<string, string>();
                foreach (XmlNode childNode in nodeList)
                {
                    foreach (XmlNode childNode2 in childNode.ChildNodes)
                    {
                        dic.Add(childNode2.Name, childNode2.InnerText);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 读取一个节点下所有内容
        /// 生成数据字典集合(key：节点属性 value：节点值)
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <param name="attributes">属性名</param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlToDictionary(string xmlPath, string node, string attributes)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            Dictionary<string, string> dic = null;
            XmlNodeList nodeList = xmlDom.SelectNodes(node);
            if (nodeList.Count > 0)
            {
                dic = new Dictionary<string, string>();
                foreach (XmlNode childNode in nodeList)
                {
                    dic.Add(childNode.Attributes[attributes].Value, childNode.InnerText);
                }
            }
            return dic;
        }

        /// <summary>
        /// 生成数据字典集合(key：节点属性 value：节点值)
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <param name="attributes">属性名 TKEY</param>
        /// <param name="attributes">属性名 TVALUE</param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlToDictionary(string xmlPath, string node, string Kattributes, string Vattributes)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            Dictionary<string, string> dic = null;
            XmlNodeList nodeList = xmlDom.SelectNodes(node);
            if (nodeList.Count > 0)
            {
                dic = new Dictionary<string, string>();
                foreach (XmlNode childNode in nodeList)
                {
                    dic.Add(childNode.Attributes[Kattributes].Value, childNode.Attributes[Vattributes].Value);
                }
            }
            return dic;
        }

        /// <summary>
        /// 读取一个节点下所有内容
        /// 生成json数据集合
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <param name="node">节点路劲  例activity/ad_1 ||  activity</param>
        /// <returns></returns>
        public static string ReadXmlToJson(string xmlPath, string node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodeList = xmlDom.SelectNodes(node);
            StringBuilder json = new StringBuilder();
            if (nodeList != null && nodeList.Count > 0)
            {
                json.Append("[");
                int idx = 0;
                foreach (XmlNode childNode in nodeList)
                {
                    int idx2 = 0;
                    json.Append("{");
                    foreach (XmlNode childNode2 in childNode.ChildNodes)
                    {
                        json.Append("\"" + childNode2.Name + "\":\"" + childNode2.InnerText + "\"");
                        if (idx2 != childNode.ChildNodes.Count - 1)
                            json.Append(",");
                        idx2++;
                    }
                    if (idx == nodeList.Count - 1)
                        json.Append("}");
                    else
                        json.Append("},");
                    idx++;
                }
                json.Append("]");
                return json.ToString().TrimEnd(']').TrimEnd(',') + "]";
            }
            else
            {
                return "0";
            }
        }

        //修改配置文件
        /// <summary>
        /// 修改指定节点，指定键的值
        /// </summary>
        /// <param name="xmlPath">文件地址</param>
        /// <param name="tagName">指定节点</param>
        /// <param name="configKey">指定键</param>
        /// <param name="configValue">设置的值</param>
        public static void SaveConfig(string xmlPath, string tagName, string configKey, string configValue)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodes = xmlDom.GetElementsByTagName(tagName);
            foreach (XmlNode node in nodes)
            {
                XmlAttribute att = node.Attributes["key"];
                if (att.Value == configKey)
                {
                    att = node.Attributes["value"];
                    att.Value = configValue;
                    break;
                }
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 修改指定节点 指定属性的值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="tagName"></param>
        /// <param name="configKey"></param>
        /// <param name="configValue"></param>
        public static void SaveConfig(string xmlPath, string tagName, string configKey, string configValue, string keyAttributes)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodes = xmlDom.GetElementsByTagName(tagName);
            foreach (XmlNode node in nodes)
            {
                XmlAttribute att = node.Attributes["key"];
                if (att.Value == configKey)
                {
                    att = node.Attributes[keyAttributes];
                    att.Value = configValue;
                    break;
                }
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /**************************************************
         * xml操作
         ************************************************/
        /// <summary>
        /// 插入一节点（无属性）
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">当前节点路径</param>
        /// <param name="Element">新节点</param>
        /// <param name="Content">节点值</param>
        public static void AddXmlElement(string xmlPath, string MainNode, string Element, string Content)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNode objNode = xmlDom.SelectSingleNode(MainNode);
            XmlElement objElement = xmlDom.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 添加一个节点（带属性）
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="Element">节点名称</param>
        /// <param name="hs">节点的属性 键值对集合</param>
        /// <param name="Content">节点值</param>
        public static void AddXmlElement(string xmlPath, string MainNode, string Element, Hashtable hs, string Content)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNode objNode = xmlDom.SelectSingleNode(MainNode);
            XmlElement objElement = null;
            objElement = xmlDom.CreateElement(Element);
            if (hs != null)
            {
                foreach (object obj in hs.Keys)
                {
                    objElement.SetAttribute(obj.ToString(), hs[obj].ToString());
                }
            }
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 添加多个节点
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="dic">节点的键值对集合</param>
        /// <param name="Content">节点值</param>
        /// <returns></returns>
        public static void AddXmlElement(string xmlPath, string MainNode, Dictionary<string, string> dic)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            //添加集合上一层 节点
            XmlElement objElement = xmlDom.CreateElement(MainNode);
            //添加子节点
            foreach (string keys in dic.Keys)
            {
                XmlElement objChildElement = xmlDom.CreateElement(keys);
                objChildElement.InnerText = dic[keys].ToString();
                objElement.AppendChild(objChildElement);
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 修改、添加多个节点(当指定节点不存在就创建、有就修改)
        /// 深度=2
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="dic">节点值</param>
        /// <returns></returns>
        public static void XmlElementHandler(string xmlPath, string MainNode, Dictionary<string, string> dic)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodeList = xmlDom.SelectNodes(MainNode);
            if (nodeList.Count > 0)
            {
                foreach (XmlNode childNode in nodeList)
                {
                    foreach (XmlNode childNode2 in childNode.ChildNodes)
                    {
                        if (dic.ContainsKey(childNode2.Name))
                        {
                            childNode2.InnerText = dic[childNode2.Name].ToString();
                        }
                    }
                }
            }
            else
            {
                XmlElement objParentElement = xmlDom.SelectSingleNode(MainNode.Split('/')[0]) as XmlElement;
                XmlElement objChildElement = xmlDom.CreateElement(MainNode.Split('/')[1]);
                objParentElement.AppendChild(objChildElement);
                //添加子节点
                foreach (string keys in dic.Keys)
                {
                    XmlElement objChildElement2 = xmlDom.CreateElement(keys);
                    objChildElement2.InnerText = dic[keys].ToString();
                    objChildElement.AppendChild(objChildElement2);
                }
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 修改单个节点内容
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="con">节点值</param>
        /// <returns></returns>
        public static void EditXmlElement(string xmlPath, string MainNode, string content)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNode node = xmlDom.SelectSingleNode(MainNode);
            if (node != null)
            {
                node.InnerText = content;
                xmlDom.Save(GetMapPath(xmlPath));
            }
        }

        /// <summary>
        /// 修改多个节点
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="hs">节点的键值对集合</param>
        /// <param name="Content">节点值</param>
        /// <returns></returns>
        public static void EditXmlElement(string xmlPath, string MainNode, Dictionary<string, string> dic)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNodeList nodeList = xmlDom.SelectNodes(MainNode);
            if (nodeList.Count > 0)
            {
                foreach (XmlNode childNode in nodeList)
                {
                    foreach (XmlNode childNode2 in childNode.ChildNodes)
                    {
                        if (dic.ContainsKey(childNode2.Name))
                        {
                            childNode2.InnerText = dic[childNode2.Name].ToString();
                        }
                    }
                }
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 添加一个节点,已经存在就修改，如果没有就创建
        /// </summary>
        /// <param name="xmlPath">Xml文档路径</param>
        /// <param name="MainNode">节点的路径</param>
        /// <param name="Element">节点名称</param>
        /// <param name="hs">节点的键值对集合</param>
        /// <param name="Content">节点值</param>
        public static void EidtXmlElement(string xmlPath, string MainNode, string Element, Hashtable hs, string Content)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            XmlNode objNode = xmlDom.SelectSingleNode(MainNode);
            XmlElement objElement = null;
            if (xmlDom.SelectSingleNode(MainNode + "/" + Element) == null)
            {
                objElement = xmlDom.CreateElement(Element);
                if (hs != null)
                {
                    foreach (object obj in hs.Keys)
                    {
                        objElement.SetAttribute(obj.ToString(), hs[obj].ToString());
                    }
                }
                objElement.InnerText = Content;
                objNode.AppendChild(objElement);
            }
            else
            {
                objElement = objNode.SelectSingleNode(Element) as XmlElement;
                if (hs != null)
                {
                    foreach (object obj in hs.Keys)
                    {
                        objElement.SetAttribute(obj.ToString(), hs[obj].ToString());
                    }
                }
                objElement.InnerText = Content;
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// 删除XML节点和此节点下的子节点
        /// </summary>
        /// <param name="xmlPath">xml文档路径</param>
        /// <param name="Node">节点路径</param>
        public static void DeleteXmlNode(string xmlPath, string Node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            xmlDom.SelectSingleNode(mainNode).RemoveChild(xmlDom.SelectSingleNode(Node));
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// 删除匹配xpath 表达式的第一个节点(节点中的子元素同时也被删除)
        /// </summary>
        /// <param name="xmlPath">XML文档完全文件名 (包含物理路径)</param>
        /// <param name="xpath">要匹配的xpath 表达式 (例如："//节点名//子节点名")</param>
        /// <returns>成功返回true ,失败返回 false</returns>
        public static bool DeleteXMLNodeByXPath(string xmlPath, string xpath)
        {
            bool isSuccess = false;
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            try
            {
                XmlNode xmlNode = xmlDom.SelectSingleNode(xpath);
                if (xmlNode != null)
                {
                    xmlNode.ParentNode.RemoveChild(xmlNode);
                }
                xmlDom.Save(GetMapPath(xmlPath));
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }

        /// <summary>
        /// 获得要查找的项的值
        /// </summary>
        /// <param name="phyPath">物理路径</param>
        /// <param name="id">编号</param>
        /// <returns>选择节点的值的集合</returns>
        public static Hashtable GetValueByContent(string phyPath, int id)
        {
            Hashtable ht_data = new Hashtable();
            XmlDocument xmlDom = GetXmlDom(phyPath);
            XmlNode node = xmlDom.SelectSingleNode(string.Format("Pages/Page[@id={0}]", id));
            XmlNodeList nodeList = node.ChildNodes;
            foreach (XmlNode tempNode in nodeList)
            {
                ht_data.Add(tempNode.Name, tempNode.InnerText);
            }
            return ht_data;
        }
    }
}
