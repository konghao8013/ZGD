using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGD.BLL
{
    public class AD
    {
        private static string baseConfigPath = "~/Files/Ad.xml";
        /// <summary>
        /// 获取焦点图广告
        /// </summary>
        /// <returns></returns>
        public static List<Ad> GetAd(string type, int top)
        {
            List<Ad> focusAd = new List<Ad>();
            for (int i = 0; i < top; i++)
            {
                string tmpXmlNode = string.Format("Ad/{1}/ad_{0}", i, type);
                Dictionary<string, string> tmp_dic = ZGD.Common.XMLDom.ReadXmlToDictionary(baseConfigPath, tmpXmlNode);
                if (tmp_dic["state"].ToString() == "0")
                {
                    Ad model = new Ad();
                    model.ID = tmp_dic["id"].ToString();
                    model.Type = tmp_dic["type"].ToString();
                    model.Size = tmp_dic["size"].ToString();
                    model.Files = tmp_dic["files"].ToString();
                    model.Links = tmp_dic["links"].ToString();
                    model.Remark = tmp_dic["remark"].ToString();
                    model.State = tmp_dic["state"].ToString();
                    focusAd.Add(model);
                }
            }
            return focusAd;
        }
    }

    /// <summary>
    /// 焦点图广告
    /// </summary>
    public class Ad
    {
        private string id;
        private string type;
        private string size;
        private string files;
        private string links;
        private string remark;
        private string state;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Files
        {
            get { return files; }
            set { files = value; }
        }

        public string Links
        {
            get { return links; }
            set { links = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
