using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGD.Web.Admin.Handler
{
    /// <summary>
    /// Ad 的摘要说明
    /// </summary>
    public class Ad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string retMsg = string.Empty;
            if (context.Request.Form["action"] != null)
            {
                int id = string.IsNullOrEmpty(context.Request.Form["id"]) ? 0 : Convert.ToInt32(context.Request.Form["id"]);
                int sort = string.IsNullOrEmpty(context.Request.Form["sort"]) ? 0 : Convert.ToInt32(context.Request.Form["sort"]);
                int islock = string.IsNullOrEmpty(context.Request.Form["islock"]) ? 0 : Convert.ToInt32(context.Request.Form["islock"]);
                int type = string.IsNullOrEmpty(context.Request.Form["type"]) ? 0 : Convert.ToInt32(context.Request.Form["type"]);
                string title = context.Request.Form["title"];
                string desc = context.Request.Form["desc"];
                string imgurl = context.Request.Form["imgurl"];
                string url = context.Request.Form["url"];
                string size = context.Request.Form["size"];
                BLL.Banner bll = new BLL.Banner();
                Model.Banner model = bll.GetModel(id);
                switch (context.Request.Form["action"])
                {
                    case "GetAd":
                        retMsg = ZGD.Common.JScript.JsonSerializer<Model.Banner>(model);
                        break;
                    case "SaveAd":
                        model = bll.GetModel(id);
                        if (model != null)
                        {
                            model.Title = title;
                            model.Description = desc;
                            model.Url = url;
                            model.ImgUrl = imgurl;
                            model.Sort = sort;
                            model.IsLock = islock;
                            model.aType = type;
                            model.Size = size;
                            if (bll.Update(model))
                                retMsg = "1";
                        }
                        break;
                    case "AddAd":
                        model = new Model.Banner();
                        if (model != null)
                        {
                            model.Title = title;
                            model.Description = desc;
                            model.Url = url;
                            model.ImgUrl = imgurl;
                            model.Sort = sort;
                            model.IsLock = islock;
                            model.aType = type;
                            model.Size = size;
                            if (bll.Add(model) > 0)
                                retMsg = "1";
                        }
                        break;

                }

            }
            context.Response.Write(retMsg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}