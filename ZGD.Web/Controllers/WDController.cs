using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ZGD.Common;
using ZGD.Web.Tools;

namespace ZGD.Web.Controllers
{
    public class WDController : UserBaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [UserAuthLogin(false)]
        public ActionResult Index()
        {
            //热门标签
            DataTable dt_tag = new ZGD.BLL.Links().GetList(0, " IsLock=0 and ClassId=70 ", " SortId asc");
            ViewBag.Tags = dt_tag;

            ZGD.BLL.Question bll = new BLL.Question();
            ZGD.BLL.Answer a_bll = new BLL.Answer();
            ViewBag.JH = bll.GetList(5, " q.IsLock=0 and q.IsGood=1 ", " q.ID asc");
            ViewBag.JJ = bll.GetList(6, " q.IsLock=0 and q.Status=2 ", " q.ID asc");
            ViewBag.QD = bll.GetList(16, " q.IsLock=0 and (select count(1) from Answer where QuestionId=q.ID)<=0", " q.ID asc");
            ViewBag.QACount = bll.GetCount(" IsLock=0 ");

            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [UserAuthLogin(false)]
        public ActionResult Page(int page = 1, string key = "")
        {
            string pager = string.Empty, where = " 1=1 ", url = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                url += "&key=" + key;
                key = HttpUtility.UrlDecode(key);
                where += " and (q.Title like '%" + key + "%' or q.Contents like '%" + key + "%')";
            }

            var result = new ZGD.BLL.Question().GetList(page, 10, where, " q.IsTop desc, q.ID desc", url, "/wd/page", out pager);
            ViewBag.Pager = pager;
            ViewBag.Page = page;
            ViewBag.SearchKey = key;
            return View(result);
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserAuthLogin(false)]
        public ActionResult Details(int id)
        {
            ZGD.BLL.Question bll = new BLL.Question();
            ZGD.BLL.Answer a_bll = new BLL.Answer();
            ZGD.Model.Question qa = bll.GetModel(id);
            if (qa != null)
            {
                //点击率
                bll.UpdateField(id, " Click=Click+1");
            }
            DataSet ds = a_bll.GetListByUser(0, " a.QuestionId=" + qa.ID, " a.IsAccept desc,a.ID asc");
            ViewBag.AnList = ds != null ? ds.Tables[0] : new DataTable();
            //相关
            string aboutKey = string.IsNullOrEmpty(qa.Title) ? "" : qa.Title;
            DataTable dt_about = string.IsNullOrWhiteSpace(aboutKey) ? null : bll.GetList(24, " q.IsLock=0 and q.ID<>" + id + " and (q.Title like '%" + aboutKey + "%' or q.Keyword like '%" + aboutKey + "%') ", " q.PubTime desc");
            ViewBag.AboutQA = dt_about;
            return View(qa);
        }

        /// <summary>
        /// 提交问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Add(Model.Question model)
        {
            if (model == null)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "参数错误！" });
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "问题标题不能为空！" });
            }
            if (!string.IsNullOrWhiteSpace(model.Contents))
            {
                model.Contents = StringHandler.InputText(model.Contents, model.Contents.Length);
            }
            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                model.ImgUrl = StringHandler.InputText(model.ImgUrl, model.ImgUrl.Length);
            }
            model.Title = StringHandler.InputText(model.Title, model.Title.Length);
            model.PubTime = DateTime.Now;
            model.UserId = DTO.OperSession.LoginUser.ID;

            ZGD.BLL.Question bll = new BLL.Question();
            int add_id = bll.Add(model);
            if (add_id > 0)
            {
                return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！", Source = "/wd/" + add_id });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }

        /// <summary>
        /// 提交问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Answer(Model.Answer model)
        {
            if (model == null)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "参数错误！" });
            }
            if (model.QuestionId <= 0)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "错误的问题！" });
            }
            if (string.IsNullOrWhiteSpace(model.Contents))
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "请输入回答内容！" });
            }
            if (!string.IsNullOrWhiteSpace(model.ImgUrl))
            {
                model.ImgUrl = StringHandler.InputText(model.ImgUrl, model.ImgUrl.Length);
            }

            model.Contents = StringHandler.InputText(model.Contents, model.Contents.Length);
            ZGD.BLL.Question bll = new BLL.Question();
            var q = bll.GetModel(model.QuestionId);
            if (q != null)
            {
                if (q.UserId == DTO.OperSession.LoginUser.ID)
                {
                    return Json(new AjaxMsgResult() { Success = false, Msg = "不能回答自己的问题！" });
                }
            }

            model.PubTime = DateTime.Now;
            model.UserId = DTO.OperSession.LoginUser.ID;

            int add_id = new BLL.Answer().Add(model);
            if (add_id > 0)
            {
                return Json(new AjaxMsgResult() { Success = true, Msg = "回答成功！" });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }

        /// <summary>
        /// 采纳回答
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult Accept(int id)
        {
            ZGD.BLL.Question bll = new BLL.Question();
            ZGD.BLL.Answer a_bll = new BLL.Answer();
            ZGD.Model.Answer an = a_bll.GetModel(id);
            if (an == null)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "参数错误！" });
            }
            if (an.QuestionId <= 0)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "错误的问题！" });
            }
            var q = bll.GetModel(an.QuestionId);
            if (q == null)
            {
                return Json(new AjaxMsgResult() { Success = false, Msg = "错误的问题！" });
            }

            if (a_bll.UpdateField(id, "IsAccept=1") > 0)
            {
                bll.UpdateField(an.QuestionId, "Status=2");
                return Json(new AjaxMsgResult() { Success = true, Msg = "提交成功！" });
            }
            return Json(new AjaxMsgResult() { Success = false, Msg = "提交失败！" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    HttpPostedFileBase imgFile = Request.Files[0];
                    if (imgFile == null)
                    {
                        return Json(new AjaxMsgResult { Success = false, Msg = "请上传图片！" });
                    }
                    UpLoad upFiles = new UpLoad();
                    var result = upFiles.fileSaveAsBase(imgFile, false, false);
                    if (result.Success)
                    {
                        return Json(new AjaxMsgResult { Success = true, Msg = "上传成功！", Source = result.Source });
                    }
                }
                else
                {
                    return Json(new AjaxMsgResult { Success = false, Msg = "请上传图片！" });
                }
            }
            catch (Exception e)
            {
                return Json(new AjaxMsgResult { Success = true, Msg = "上传异常：" + e.Message });
            }
            return Json(new AjaxMsgResult { Success = false, Msg = "上传失败！" });
        }
    }
}
