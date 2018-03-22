using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// Links
    /// </summary>
    public partial class Links
    {
        private readonly ZGD.DAL.Links dal = new ZGD.DAL.Links();
        public Links()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
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
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Links model)
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
        public bool Update(ZGD.Model.Links model)
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
        public ZGD.Model.Links GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.Links GetModelByCache(int Id)
        {

            string CacheKey = "LinksModel-" + Id;
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
            return (ZGD.Model.Links)objModel;
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
        public DataSet GetListPage(string strWhere)
        {
            return dal.GetListPage(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return ds != null ? ds.Tables[0] : null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Links> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Links> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.Links> modelList = new List<ZGD.Model.Links>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.Links model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.Links();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["UserTel"] != null && dt.Rows[n]["UserTel"].ToString() != "")
                    {
                        model.UserTel = dt.Rows[n]["UserTel"].ToString();
                    }
                    if (dt.Rows[n]["UserMail"] != null && dt.Rows[n]["UserMail"].ToString() != "")
                    {
                        model.UserMail = dt.Rows[n]["UserMail"].ToString();
                    }
                    if (dt.Rows[n]["WebUrl"] != null && dt.Rows[n]["WebUrl"].ToString() != "")
                    {
                        model.WebUrl = dt.Rows[n]["WebUrl"].ToString();
                    }
                    if (dt.Rows[n]["ImgUrl"] != null && dt.Rows[n]["ImgUrl"].ToString() != "")
                    {
                        model.ImgUrl = dt.Rows[n]["ImgUrl"].ToString();
                    }
                    if (dt.Rows[n]["IsImage"] != null && dt.Rows[n]["IsImage"].ToString() != "")
                    {
                        model.IsImage = int.Parse(dt.Rows[n]["IsImage"].ToString());
                    }
                    if (dt.Rows[n]["SortId"] != null && dt.Rows[n]["SortId"].ToString() != "")
                    {
                        model.SortId = int.Parse(dt.Rows[n]["SortId"].ToString());
                    }
                    if (dt.Rows[n]["IsRed"] != null && dt.Rows[n]["IsRed"].ToString() != "")
                    {
                        model.IsRed = int.Parse(dt.Rows[n]["IsRed"].ToString());
                    }
                    if (dt.Rows[n]["IsLock"] != null && dt.Rows[n]["IsLock"].ToString() != "")
                    {
                        model.IsLock = int.Parse(dt.Rows[n]["IsLock"].ToString());
                    }
                    if (dt.Rows[n]["ClassId"] != null && dt.Rows[n]["ClassId"].ToString() != "")
                    {
                        model.ClassId = int.Parse(dt.Rows[n]["ClassId"].ToString());
                    }
                    if (dt.Rows[n]["AddTime"] != null && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
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
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}