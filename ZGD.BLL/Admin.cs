using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;

namespace ZGD.BLL
{
    /// <summary>
    /// Admin
    /// </summary>
    public partial class Admin
    {
        private readonly ZGD.DAL.Admin dal = new ZGD.DAL.Admin();
        public Admin()
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
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool Exists(string UserName)
        {
            return dal.Exists(UserName);
        }

        /// <summary>
        /// 检查登录用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        public bool chkAdminLogin(string UserName, string UserPwd)
        {
            return dal.chkAdminLogin(UserName, UserPwd);
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
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Admin model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Admin model)
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
        /// 根据用户名取得一行数据给Model
        /// </summary>
        public ZGD.Model.Admin GetModel1(string UserName)
        {
            return dal.GetModel1(UserName);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Admin GetModel(int Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.Admin GetModelByCache(int Id)
        {

            string CacheKey = "AdminModel-" + Id;
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
            return (ZGD.Model.Admin)objModel;
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
        public List<ZGD.Model.Admin> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Admin> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.Admin> modelList = new List<ZGD.Model.Admin>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.Admin model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.Admin();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["UserPwd"] != null && dt.Rows[n]["UserPwd"].ToString() != "")
                    {
                        model.UserPwd = dt.Rows[n]["UserPwd"].ToString();
                    }
                    if (dt.Rows[n]["Address"] != null && dt.Rows[n]["Address"].ToString() != "")
                    {
                        model.Address = dt.Rows[n]["Address"].ToString();
                    }
                    if (dt.Rows[n]["Tel"] != null && dt.Rows[n]["Tel"].ToString() != "")
                    {
                        model.Tel = dt.Rows[n]["Tel"].ToString();
                    }
                    if (dt.Rows[n]["UserEmail"] != null && dt.Rows[n]["UserEmail"].ToString() != "")
                    {
                        model.UserEmail = dt.Rows[n]["UserEmail"].ToString();
                    }
                    if (dt.Rows[n]["UserType"] != null && dt.Rows[n]["UserType"].ToString() != "")
                    {
                        model.UserType = int.Parse(dt.Rows[n]["UserType"].ToString());
                    }
                    if (dt.Rows[n]["UserLevel"] != null && dt.Rows[n]["UserLevel"].ToString() != "")
                    {
                        model.UserLevel = dt.Rows[n]["UserLevel"].ToString();
                    }
                    if (dt.Rows[n]["IsLock"] != null && dt.Rows[n]["IsLock"].ToString() != "")
                    {
                        model.IsLock = int.Parse(dt.Rows[n]["IsLock"].ToString());
                    }
                    if (dt.Rows[n]["AddTime"] != null && dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    if (dt.Rows[n]["LoginTime"] != null && dt.Rows[n]["LoginTime"].ToString() != "")
                    {
                        model.LoginTime = DateTime.Parse(dt.Rows[n]["LoginTime"].ToString());
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