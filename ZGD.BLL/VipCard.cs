using System;
using System.Data;
using System.Collections.Generic;
using ZGD.Common;
using ZGD.Model;
namespace ZGD.BLL
{
	/// <summary>
	/// VipCard
	/// </summary>
	public partial class VipCard
	{
		private readonly ZGD.DAL.VipCard dal=new ZGD.DAL.VipCard();
		public VipCard()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CardNo)
		{
			return dal.Exists(CardNo);
		}
        
        /// <summary>
        /// 批量新增数据
        /// </summary>
        public int AddList(List<ZGD.Model.VipCard> models)
        {
            return dal.AddList(models);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZGD.Model.VipCard model)
		{
			return dal.Add(model);
		}
        

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(string CardNo, string strValue)
        {
            dal.UpdateField(CardNo, strValue);
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZGD.Model.VipCard model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string CardNo)
		{
			
			return dal.Delete(CardNo);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CardNolist )
		{
			return dal.DeleteList(CardNolist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZGD.Model.VipCard GetModel(string CardNo)
		{
			
			return dal.GetModel(CardNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZGD.Model.VipCard GetModelByCache(string CardNo)
		{
			
			string CacheKey = "VipCardModel-" + CardNo;
			object objModel = ZGD.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CardNo);
					if (objModel != null)
					{
						int ModelCache = ZGD.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZGD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZGD.Model.VipCard)objModel;
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
		public List<ZGD.Model.VipCard> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZGD.Model.VipCard> DataTableToList(DataTable dt)
		{
			List<ZGD.Model.VipCard> modelList = new List<ZGD.Model.VipCard>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZGD.Model.VipCard model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        /// 绑定VIP
        /// </summary>
        public int BindVip(string vipNo, string openId)
        {
            return dal.BindVip(vipNo, openId);
        }
		#endregion  ExtensionMethod
	}
}

