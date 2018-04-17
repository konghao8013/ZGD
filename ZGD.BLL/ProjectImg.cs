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
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int pID)
        {
            return dal.Exists(pID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ZGD.Model.ProjectImg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ZGD.Model.ProjectImg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int pID, int pType)
        {

            return dal.Delete(pID, pType);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteByDesignerId(int DesignerId)
        {
            return dal.DeleteByDesignerId(DesignerId);
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteList(string pIDlist)
        {
            return dal.DeleteList(pIDlist);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ZGD.Model.ProjectImg GetModel(int pID)
        {

            return dal.GetModel(pID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ�����
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return ds != null ? ds.Tables[0] : null;
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<ZGD.Model.ProjectImg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
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
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}

