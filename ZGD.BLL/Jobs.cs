using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HX.Model;
namespace HX.BLL
{
	/// <summary>
	/// Jobs
	/// </summary>
	public partial class Jobs
	{
		private readonly HX.DAL.Jobs dal=new HX.DAL.Jobs();
		public Jobs()
		{}
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
		/// ����һ������
		/// </summary>
		public int  Add(HX.Model.Jobs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(HX.Model.Jobs model)
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
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public HX.Model.Jobs GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public HX.Model.Jobs GetModelByCache(int ID)
		{
			
			string CacheKey = "JobsModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HX.Model.Jobs)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<HX.Model.Jobs> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<HX.Model.Jobs> DataTableToList(DataTable dt)
		{
			List<HX.Model.Jobs> modelList = new List<HX.Model.Jobs>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HX.Model.Jobs model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HX.Model.Jobs();
					if(dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					model.WorkAddr=dt.Rows[n]["WorkAddr"].ToString();
					if(dt.Rows[n]["Price"].ToString()!="")
					{
						model.Price=decimal.Parse(dt.Rows[n]["Price"].ToString());
					}
					model.Age=dt.Rows[n]["Age"].ToString();
					model.Education=dt.Rows[n]["Education"].ToString();
					model.WorkYear=dt.Rows[n]["WorkYear"].ToString();
					if(dt.Rows[n]["JobType"].ToString()!="")
					{
						model.JobType=int.Parse(dt.Rows[n]["JobType"].ToString());
					}
					model.Job=dt.Rows[n]["Job"].ToString();
					model.JobMust=dt.Rows[n]["JobMust"].ToString();
					if(dt.Rows[n]["JobCount"].ToString()!="")
					{
						model.JobCount=int.Parse(dt.Rows[n]["JobCount"].ToString());
					}
					if(dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
					}
					if(dt.Rows[n]["UpdateTime"].ToString()!="")
					{
						model.UpdateTime=DateTime.Parse(dt.Rows[n]["UpdateTime"].ToString());
					}
					if(dt.Rows[n]["PubTime"].ToString()!="")
					{
						model.PubTime=DateTime.Parse(dt.Rows[n]["PubTime"].ToString());
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

