using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
using System.Linq;

namespace ZGD.BLL
{
    /// <summary>
    /// NewsInfo
    /// </summary>
    public partial class NewsInfo
    {
        private readonly ZGD.DAL.NewsInfo dal = new ZGD.DAL.NewsInfo();
        public NewsInfo()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId(string where = "")
        {
            return dal.GetMaxId(where);
        }

        /// <summary>
        /// 得到最小ID
        /// </summary>
        public int GetMinId(string where = "")
        {
            return dal.GetMinId(where);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetZtCount(string strWhere)
        {
            return dal.GetZtCount(strWhere);
        }

        /// <summary>
        /// 返回总浏览量
        /// </summary>
        public int GetSumClick(string strWhere)
        {
            return dal.GetSumClick(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.NewsInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            dal.UpdateField(Id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.NewsInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            return dal.Delete(Id);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.NewsInfo GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获取相关标签
        /// </summary>
        public List<string> GetTagsByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return null;

            //根据关键词获取标签
            DataSet ds = dal.GetList(" Keyword like '%" + keyword + "%'");
            List<string> tags = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string tag = string.Empty;
                string[] tag_list;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tag = dr["Keyword"].ToString();
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        tag_list = tag.TrimEnd().Split(',');
                        foreach (string item in tag_list)
                        {
                            if (!tags.Contains(item))
                            {
                                tags.Add(item);
                            }
                        }
                    }
                }
            }
            return tags;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.NewsInfo GetModelByCache(int Id)
        {
            string CacheKey = "NewsInfoModel-" + Id;
            object objModel = ZGD.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ZGD.Common.ConfigHelper.GetConfigInt("ModelCache");
                        ZGD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZGD.Model.NewsInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetZtList(string strWhere)
        {
            return dal.GetZtList(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <param name="pId">8文章 21专题文章</param>
        /// <returns></returns>
        public DataTable GetList(int Top, string strWhere, string filedOrder, int pId = 8)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder, pId);
            return ds != null ? ds.Tables[0] : null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.NewsInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.NewsInfo> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.NewsInfo> modelList = new List<ZGD.Model.NewsInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.NewsInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.NewsInfo();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[0]["Keyword"] != null && dt.Rows[0]["Keyword"].ToString() != "")
                    {
                        model.Keyword = dt.Rows[0]["Keyword"].ToString();
                    }
                    if (dt.Rows[0]["Description"] != null && dt.Rows[0]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[0]["Description"].ToString();
                    }
                    if (dt.Rows[n]["Author"] != null && dt.Rows[n]["Author"].ToString() != "")
                    {
                        model.Author = dt.Rows[n]["Author"].ToString();
                    }
                    if (dt.Rows[n]["ClassId"] != null && dt.Rows[n]["ClassId"].ToString() != "")
                    {
                        model.ClassId = dt.Rows[n]["ClassId"].ToString();
                    }
                    if (dt.Rows[n]["Content"] != null && dt.Rows[n]["Content"].ToString() != "")
                    {
                        model.Content = dt.Rows[n]["Content"].ToString();
                    }
                    if (dt.Rows[n]["ImgUrl"] != null && dt.Rows[n]["ImgUrl"].ToString() != "")
                    {
                        model.ImgUrl = dt.Rows[n]["ImgUrl"].ToString();
                    }
                    if (dt.Rows[n]["IsImage"] != null && dt.Rows[n]["IsImage"].ToString() != "")
                    {
                        model.IsImage = int.Parse(dt.Rows[n]["IsImage"].ToString());
                    }
                    if (dt.Rows[n]["Click"] != null && dt.Rows[n]["Click"].ToString() != "")
                    {
                        model.Click = int.Parse(dt.Rows[n]["Click"].ToString());
                    }
                    if (dt.Rows[n]["IsTop"] != null && dt.Rows[n]["IsTop"].ToString() != "")
                    {
                        model.IsTop = int.Parse(dt.Rows[n]["IsTop"].ToString());
                    }
                    if (dt.Rows[n]["IsLock"] != null && dt.Rows[n]["IsLock"].ToString() != "")
                    {
                        model.IsLock = int.Parse(dt.Rows[n]["IsLock"].ToString());
                    }
                    if (dt.Rows[n]["PubTime"] != null && dt.Rows[n]["PubTime"].ToString() != "")
                    {
                        model.PubTime = DateTime.Parse(dt.Rows[n]["PubTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得标签数据列表
        /// </summary>
        public List<string> GetTagList(int top = 20)
        {
            DataSet ds = dal.GetTagList(top);
            List<string> list = new List<string>();
            if (ds != null && ds.Tables[0] != null)
            {
                string[] arr;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(item["Tags"].ToString()))
                    {
                        arr = item["Tags"].ToString().Split(',');
                        foreach (string tag in arr)
                        {
                            if (list.Count == 0 || !list.Contains(tag))
                            {
                                list.Add(tag);
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetList(int pIdx, int pageSize, string strWhere, string sortName, string urlParam, string page, out string pager)
        {
            pager = string.Empty;
            int pageCount = 0, rowCount = 0;
            DataSet ds = dal.GetList(pIdx, pageSize, strWhere, sortName, out rowCount, out pageCount);
            pager = DAL.Pager.InitPageFooter(pIdx, pageCount, rowCount, page, urlParam);
            return ds == null ? null : ds.Tables[0];
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}