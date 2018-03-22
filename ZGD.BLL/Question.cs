using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// Question
    /// </summary>
    public partial class Question
    {
        private readonly ZGD.DAL.Question dal = new ZGD.DAL.Question();
        public Question()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Question model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Question model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            dal.UpdateField(Id, strValue);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Question GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.Question GetModelByCache(int ID)
        {

            string CacheKey = "QuestionModel-" + ID;
            object objModel = ZGD.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = ZGD.Common.ConfigHelper.GetConfigInt("ModelCache");
                        ZGD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZGD.Model.Question)objModel;
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
        public List<ZGD.Model.Question> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Question> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.Question> modelList = new List<ZGD.Model.Question>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.Question model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetList(int pIdx, int pageSize, string strWhere, string sortName, string urlParam, string page, out string pager, bool isWeb = true)
        {
            pager = string.Empty;
            int pageCount = 0, rowCount = 0;
            DataSet ds = dal.GetList(pIdx, pageSize, strWhere, sortName, out rowCount, out pageCount);
            pager = DAL.Pager.InitPageFooter(pIdx, pageCount, rowCount, page, urlParam, isWeb);
            return ds == null ? null : ds.Tables[0];
        }
        #endregion  ExtensionMethod
    }
}

