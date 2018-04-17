using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// ProjectImg
    /// </summary>
    public partial class ProjectImg
    {
        private readonly ZGD.DAL.ProjectImg dal = new ZGD.DAL.ProjectImg();
        public ProjectImg()
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
        public bool Exists(int pID)
        {
            return dal.Exists(pID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.ProjectImg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.ProjectImg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int pID, int pType)
        {

            return dal.Delete(pID, pType);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByDesignerId(int DesignerId)
        {
            return dal.DeleteByDesignerId(DesignerId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string pIDlist)
        {
            return dal.DeleteList(pIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.ProjectImg GetModel(int pID)
        {

            return dal.GetModel(pID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.ProjectImg GetModelByCache(int pID)
        {

            string CacheKey = "ProjectImgModel-" + pID;
            object objModel = ZGD.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(pID);
                    if (objModel != null)
                    {
                        int ModelCache = ZGD.Common.ConfigHelper.GetConfigInt("ModelCache");
                        ZGD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZGD.Model.ProjectImg)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        public List<ZGD.Model.ProjectImg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.ProjectImg> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.ProjectImg> modelList = new List<ZGD.Model.ProjectImg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.ProjectImg model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.ProjectImg();
                    if (dt.Rows[n]["piID"].ToString() != "")
                    {
                        model.piID = int.Parse(dt.Rows[n]["piID"].ToString());
                    }
                    if (dt.Rows[n]["pID"].ToString() != "")
                    {
                        model.pID = int.Parse(dt.Rows[n]["pID"].ToString());
                    }
                    model.Title = dt.Rows[n]["Title"].ToString();
                    model.Description = dt.Rows[n]["Description"].ToString();
                    model.ImgUrl = dt.Rows[n]["ImgUrl"].ToString();
                    model.ImageSmall = dt.Rows[n]["ImageSmall"].ToString();
                    if (dt.Rows[n]["PubTime"].ToString() != "")
                    {
                        model.PubTime = DateTime.Parse(dt.Rows[n]["PubTime"].ToString());
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

