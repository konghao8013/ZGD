using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// Channel
    /// </summary>
    public partial class Channel
    {
        private readonly ZGD.DAL.Channel dal = new ZGD.DAL.Channel();
        public Channel()
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetZtYear()
        {
            return dal.GetZtYear();
        }

            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 返回栏目名称
        /// </summary>
        public string GetChannelTitle(string classId)
        {
            return dal.GetChannelTitle(classId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Channel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Channel model)
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
        /// 删除该栏目分类及所有属下分类数据
        /// </summary>
        public void Delete(int Id)
        {
            dal.Delete(Id);
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
        public ZGD.Model.Channel GetModelById(int Id)
        {
            return dal.GetModelById(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Channel GetModelByName(string name)
        {
            return dal.GetModelByName(name);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZGD.Model.Channel GetModelByCache(int Id)
        {

            string CacheKey = "ChannelModel-" + Id;
            object objModel = ZGD.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModelById(Id);
                    if (objModel != null)
                    {
                        int ModelCache = ZGD.Common.ConfigHelper.GetConfigInt("ModelCache");
                        ZGD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZGD.Model.Channel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获取连接类别
        /// </summary>
        public DataTable GetLinkType()
        {
            DataSet ds= dal.GetLinkType();
            return ds != null ? ds.Tables[0] : null;
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
        public List<ZGD.Model.Channel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZGD.Model.Channel> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.Channel> modelList = new List<ZGD.Model.Channel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.Channel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.Channel();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["ParentId"] != null && dt.Rows[n]["ParentId"].ToString() != "")
                    {
                        model.ParentId = int.Parse(dt.Rows[n]["ParentId"].ToString());
                    }
                    if (dt.Rows[n]["ClassList"] != null && dt.Rows[n]["ClassList"].ToString() != "")
                    {
                        model.ClassList = dt.Rows[n]["ClassList"].ToString();
                    }
                    if (dt.Rows[n]["ClassLayer"] != null && dt.Rows[n]["ClassLayer"].ToString() != "")
                    {
                        model.ClassLayer = int.Parse(dt.Rows[n]["ClassLayer"].ToString());
                    }
                    if (dt.Rows[n]["SortId"] != null && dt.Rows[n]["SortId"].ToString() != "")
                    {
                        model.SortId = int.Parse(dt.Rows[n]["SortId"].ToString());
                    }
                    if (dt.Rows[n]["PageUrl"] != null && dt.Rows[n]["PageUrl"].ToString() != "")
                    {
                        model.PageUrl = dt.Rows[n]["PageUrl"].ToString();
                    }
                    if (dt.Rows[n]["KindId"] != null && dt.Rows[n]["KindId"].ToString() != "")
                    {
                        model.KindId = int.Parse(dt.Rows[n]["KindId"].ToString());
                    }
                    if (dt.Rows[n]["IsDelete"] != null && dt.Rows[n]["IsDelete"].ToString() != "")
                    {
                        model.IsDelete = int.Parse(dt.Rows[n]["IsDelete"].ToString());
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

        /// <summary>
        /// 取得所有栏目列表
        /// </summary>
        public DataTable GetList(int PId, int KId)
        {
            return dal.GetList(PId, KId);
        }

        /// <summary>
        /// 取得所有栏目列表
        /// </summary>
        public DataTable BindList(int PId, int KId)
        {
            return dal.BindList(PId, KId);
        }

        /// <summary>
        /// 取得该栏目下的所有子栏目的ID
        /// </summary>
        public DataSet GetChannelListByClassId(int classId)
        {
            return dal.GetChannelListByClassId(classId);
        }

        #endregion  Method
    }
}