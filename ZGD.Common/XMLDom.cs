//=============================================================================
//
// Project:XML������
// Author: LJ
// Data:2010-4-28 ��дʱ��
// Updated:2010-4-28 �޸�ʱ��
// Remark:XML����--��������
//
//=============================================================================
//ʹ�õ�ʱ����SetConfigName���������ļ���
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
        /// ��ȡ�ļ�������·�����磺"~/File/Img/xxx.txt"��
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// ��ȡxml�ļ�dom����
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
        /// ��ȡһ���ڵ������
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
        /// ��ȡָ���ڵ� ָ�����Ե�ֵ
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="configKey"></param>
        /// <param name="keyAttributes">����key</param>
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
        /// ��ȡָ���ڵ� ָ�����Ե�ֵ
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="keyAttributes">����key</param>
        /// <param name="nodeInfo">�ڵ���</param>
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
        /// ��ȡһ���ڵ������
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
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
        /// ��ȡָ���ڵ���������ֵ
        /// key,value
        /// </summary>
        /// <param name="xmlPath">�ļ���ַ</param>
        /// <param name="nodeName">�ڵ���</param>
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
        /// ��ȡָ���ڵ�����ָ������ֵ
        /// </summary>
        /// <param name="xmlPath">�ļ���ַ</param>
        /// <param name="nodeName">�ڵ���</param>
        /// <param name="attrName">������</param>
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
        /// ��ȡһ���ڵ�����������(�������)
        /// ��������(ֵ)
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
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
        /// ��ȡһ���ڵ�����������
        /// ���������ֵ伯��(key���ڵ��� value���ڵ�ֵ)
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
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
        /// ��ȡһ���ڵ�����������
        /// ���������ֵ伯��(key���ڵ����� value���ڵ�ֵ)
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
        /// <param name="attributes">������</param>
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
        /// ���������ֵ伯��(key���ڵ����� value���ڵ�ֵ)
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
        /// <param name="attributes">������ TKEY</param>
        /// <param name="attributes">������ TVALUE</param>
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
        /// ��ȡһ���ڵ�����������
        /// ����json���ݼ���
        /// </summary>
        /// <param name="xmlPath">�ļ�·��</param>
        /// <param name="node">�ڵ�·��  ��activity/ad_1 ||  activity</param>
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

        //�޸������ļ�
        /// <summary>
        /// �޸�ָ���ڵ㣬ָ������ֵ
        /// </summary>
        /// <param name="xmlPath">�ļ���ַ</param>
        /// <param name="tagName">ָ���ڵ�</param>
        /// <param name="configKey">ָ����</param>
        /// <param name="configValue">���õ�ֵ</param>
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
        /// �޸�ָ���ڵ� ָ�����Ե�ֵ
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
         * xml����
         ************************************************/
        /// <summary>
        /// ����һ�ڵ㣨�����ԣ�
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">��ǰ�ڵ�·��</param>
        /// <param name="Element">�½ڵ�</param>
        /// <param name="Content">�ڵ�ֵ</param>
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
        /// ���һ���ڵ㣨�����ԣ�
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="Element">�ڵ�����</param>
        /// <param name="hs">�ڵ������ ��ֵ�Լ���</param>
        /// <param name="Content">�ڵ�ֵ</param>
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
        /// ��Ӷ���ڵ�
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="dic">�ڵ�ļ�ֵ�Լ���</param>
        /// <param name="Content">�ڵ�ֵ</param>
        /// <returns></returns>
        public static void AddXmlElement(string xmlPath, string MainNode, Dictionary<string, string> dic)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            //��Ӽ�����һ�� �ڵ�
            XmlElement objElement = xmlDom.CreateElement(MainNode);
            //����ӽڵ�
            foreach (string keys in dic.Keys)
            {
                XmlElement objChildElement = xmlDom.CreateElement(keys);
                objChildElement.InnerText = dic[keys].ToString();
                objElement.AppendChild(objChildElement);
            }
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// �޸ġ���Ӷ���ڵ�(��ָ���ڵ㲻���ھʹ������о��޸�)
        /// ���=2
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="dic">�ڵ�ֵ</param>
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
                //����ӽڵ�
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
        /// �޸ĵ����ڵ�����
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="con">�ڵ�ֵ</param>
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
        /// �޸Ķ���ڵ�
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="hs">�ڵ�ļ�ֵ�Լ���</param>
        /// <param name="Content">�ڵ�ֵ</param>
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
        /// ���һ���ڵ�,�Ѿ����ھ��޸ģ����û�оʹ���
        /// </summary>
        /// <param name="xmlPath">Xml�ĵ�·��</param>
        /// <param name="MainNode">�ڵ��·��</param>
        /// <param name="Element">�ڵ�����</param>
        /// <param name="hs">�ڵ�ļ�ֵ�Լ���</param>
        /// <param name="Content">�ڵ�ֵ</param>
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

        /// ɾ��XML�ڵ�ʹ˽ڵ��µ��ӽڵ�
        /// </summary>
        /// <param name="xmlPath">xml�ĵ�·��</param>
        /// <param name="Node">�ڵ�·��</param>
        public static void DeleteXmlNode(string xmlPath, string Node)
        {
            XmlDocument xmlDom = GetXmlDom(xmlPath);
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            xmlDom.SelectSingleNode(mainNode).RemoveChild(xmlDom.SelectSingleNode(Node));
            xmlDom.Save(GetMapPath(xmlPath));
        }

        /// <summary>
        /// ɾ��ƥ��xpath ���ʽ�ĵ�һ���ڵ�(�ڵ��е���Ԫ��ͬʱҲ��ɾ��)
        /// </summary>
        /// <param name="xmlPath">XML�ĵ���ȫ�ļ��� (��������·��)</param>
        /// <param name="xpath">Ҫƥ���xpath ���ʽ (���磺"//�ڵ���//�ӽڵ���")</param>
        /// <returns>�ɹ�����true ,ʧ�ܷ��� false</returns>
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
        /// ���Ҫ���ҵ����ֵ
        /// </summary>
        /// <param name="phyPath">����·��</param>
        /// <param name="id">���</param>
        /// <returns>ѡ��ڵ��ֵ�ļ���</returns>
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
