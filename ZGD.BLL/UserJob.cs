using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HX.Model;
namespace HX.BLL
{
	/// <summary>
	/// UserJob
	/// </summary>
	public partial class UserJob
	{
		private readonly HX.DAL.UserJob dal=new HX.DAL.UserJob();
		public UserJob()
		{}
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(HX.Model.UserJob model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HX.Model.UserJob model)
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
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HX.Model.UserJob GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HX.Model.UserJob GetModelByCache(int ID)
		{
			
			string CacheKey = "UserJobModel-" + ID;
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
			return (HX.Model.UserJob)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HX.Model.UserJob> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HX.Model.UserJob> DataTableToList(DataTable dt)
		{
			List<HX.Model.UserJob> modelList = new List<HX.Model.UserJob>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HX.Model.UserJob model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HX.Model.UserJob();
					if(dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					model.uName=dt.Rows[n]["uName"].ToString();
					model.Sex=dt.Rows[n]["Sex"].ToString();
					if(dt.Rows[n]["Birthday"].ToString()!="")
					{
						model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
					}
					model.Phone=dt.Rows[n]["Phone"].ToString();
					if(dt.Rows[n]["WorkYear"].ToString()!="")
					{
						model.WorkYear=int.Parse(dt.Rows[n]["WorkYear"].ToString());
					}
					model.Files=dt.Rows[n]["Files"].ToString();
					model.Profession=dt.Rows[n]["Profession"].ToString();
					model.CurJob=dt.Rows[n]["CurJob"].ToString();
					model.City=dt.Rows[n]["City"].ToString();
					if(dt.Rows[n]["CurPrice"].ToString()!="")
					{
						model.CurPrice=decimal.Parse(dt.Rows[n]["CurPrice"].ToString());
					}
					if(dt.Rows[n]["Price"].ToString()!="")
					{
						model.Price=decimal.Parse(dt.Rows[n]["Price"].ToString());
					}
					model.InTime=dt.Rows[n]["InTime"].ToString();
					if(dt.Rows[n]["PubTime"].ToString()!="")
					{
						model.PubTime=DateTime.Parse(dt.Rows[n]["PubTime"].ToString());
					}
					if(dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
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

