using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
    /// <summary>
    /// ServiceTools
    /// </summary>
    public partial class ServiceTools
    {
        private readonly ZGD.DAL.ServiceTools dal = new ZGD.DAL.ServiceTools();
        public ServiceTools()
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
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }


        /// <summary>
        /// ������������(��ҳ�õ�)
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(ZGD.Model.ServiceTools model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            dal.UpdateField(Id, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(ZGD.Model.ServiceTools model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public ZGD.Model.ServiceTools GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ�����
        /// </summary>
        public ZGD.Model.ServiceTools GetModelByCache(int ID)
        {

            string CacheKey = "ServiceToolsModel-" + ID;
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
            return (ZGD.Model.ServiceTools)objModel;
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
        public List<ZGD.Model.ServiceTools> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<ZGD.Model.ServiceTools> DataTableToList(DataTable dt)
        {
            List<ZGD.Model.ServiceTools> modelList = new List<ZGD.Model.ServiceTools>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZGD.Model.ServiceTools model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZGD.Model.ServiceTools();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    model.Title = dt.Rows[n]["Title"].ToString();
                    model.Num = dt.Rows[n]["Num"].ToString();
                    if (dt.Rows[n]["PubTime"].ToString() != "")
                    {
                        model.PubTime = DateTime.Parse(dt.Rows[n]["PubTime"].ToString());
                    }
                    if (dt.Rows[n]["Stuats"].ToString() != "")
                    {
                        model.Stuats = int.Parse(dt.Rows[n]["Stuats"].ToString());
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

