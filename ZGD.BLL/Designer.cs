using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// Designer
    /// </summary>
    public partial class Designer
    {
        private readonly ZGD.DAL.Designer dal = new ZGD.DAL.Designer();
        public Designer()
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
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
        public int Add(ZGD.Model.Designer model)
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
        public bool Update(ZGD.Model.Designer model)
        {
            return dal.Update(model);
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
        public ZGD.Model.Designer GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Designer GetModelByWhere(string where)
        {
            return dal.GetModelByWhere(where);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.Designer GetModelByCache(int ID)
        {

            string CacheKey = "DesignerModel-" + ID;
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
            return (ZGD.Model.Designer)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Designer> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Designer> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.Designer> modelList = new List<ZGD.Model.Designer>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.Designer model;
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
        /// 绑定类别DropDownList控制
        /// </summary>
        public void BindDesigner(System.Web.UI.WebControls.DropDownList ddl)
        {
            DataSet ds = GetAllList();
            ddl.DataSource = ds;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择设计师", ""));
        }

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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetList_M(int pIdx, int pageSize, string strWhere, string sortName, string urlParam, string page, out string pager, bool isWeb = true)
        {
            pager = string.Empty;
            int pageCount = 0, rowCount = 0;
            DataSet ds = dal.GetList_M(pIdx, pageSize, strWhere, sortName, out rowCount, out pageCount);
            pager = DAL.Pager.InitPageFooter(pIdx, pageCount, rowCount, page, urlParam, isWeb);
            return ds == null ? null : ds.Tables[0];
        }
        #endregion  ExtensionMethod
    }
}

