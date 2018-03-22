using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
	/// <summary>
	/// 数据访问类:House
	/// </summary>
	public partial class House
	{
		public House()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "House"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from House");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from House ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update House set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZGD.Model.House model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into House(");
            strSql.Append("Title,Keyword,Description,Address,hContent,yhContent,ImgUrl,ImageSmall,Click,IsTop,IsLock,PubTime,cCount,pCount)");
			strSql.Append(" values (");
            strSql.Append("@Title,@Keyword,@Description,@Address,@hContent,@yhContent,@ImgUrl,@ImageSmall,@Click,@IsTop,@IsLock,@PubTime,@cCount,@pCount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@hContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@yhContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageSmall", SqlDbType.NVarChar,200),
					new SqlParameter("@Click", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@cCount", SqlDbType.Int,4),
					new SqlParameter("@pCount", SqlDbType.Int,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Keyword;
			parameters[2].Value = model.Description;
			parameters[3].Value = model.Address;
			parameters[4].Value = model.hContent;
			parameters[5].Value = model.yhContent;
			parameters[6].Value = model.ImgUrl;
			parameters[7].Value = model.ImageSmall;
			parameters[8].Value = model.Click;
			parameters[9].Value = model.IsTop;
            parameters[10].Value = model.IsLock;
            parameters[11].Value = model.PubTime;
            parameters[12].Value = model.cCount;
            parameters[13].Value = model.pCount;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZGD.Model.House model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update House set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Keyword=@Keyword,");
			strSql.Append("Description=@Description,");
			strSql.Append("Address=@Address,");
			strSql.Append("hContent=@hContent,");
			strSql.Append("yhContent=@yhContent,");
			strSql.Append("ImgUrl=@ImgUrl,");
			strSql.Append("ImageSmall=@ImageSmall,");
			strSql.Append("Click=@Click,");
			strSql.Append("IsTop=@IsTop,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("cCount=@cCount,");
            strSql.Append("pCount=@pCount");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@hContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@yhContent", SqlDbType.NVarChar,-1),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageSmall", SqlDbType.NVarChar,200),
					new SqlParameter("@Click", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@cCount", SqlDbType.Int,4),
					new SqlParameter("@pCount", SqlDbType.Int,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Keyword;
			parameters[2].Value = model.Description;
			parameters[3].Value = model.Address;
			parameters[4].Value = model.hContent;
			parameters[5].Value = model.yhContent;
			parameters[6].Value = model.ImgUrl;
			parameters[7].Value = model.ImageSmall;
			parameters[8].Value = model.Click;
			parameters[9].Value = model.IsTop;
			parameters[10].Value = model.IsLock;
            parameters[11].Value = model.PubTime;
            parameters[12].Value = model.ID;
            parameters[13].Value = model.cCount;
            parameters[14].Value = model.pCount;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from House ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from House ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZGD.Model.House GetModel(int ID)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,Keyword,Description,Address,hContent,yhContent,ImgUrl,ImageSmall,Click,IsTop,IsLock,PubTime,(select COUNT(1) from Project where HouseId=h.ID)+cCount as cCount,(select COUNT(1) from NewsInfo where AssId=h.ID and ZxType=2)+pCount as pCount  from House h ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ZGD.Model.House model=new ZGD.Model.House();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZGD.Model.House DataRowToModel(DataRow row)
		{
			ZGD.Model.House model=new ZGD.Model.House();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["Keyword"]!=null)
				{
					model.Keyword=row["Keyword"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["hContent"]!=null)
				{
					model.hContent=row["hContent"].ToString();
				}
				if(row["yhContent"]!=null)
				{
					model.yhContent=row["yhContent"].ToString();
				}
				if(row["ImgUrl"]!=null)
				{
					model.ImgUrl=row["ImgUrl"].ToString();
				}
				if(row["ImageSmall"]!=null)
				{
					model.ImageSmall=row["ImageSmall"].ToString();
                }
                if (row["Click"] != null && row["Click"].ToString() != "")
                {
                    model.Click = int.Parse(row["Click"].ToString());
                }
                if (row["pCount"] != null && row["pCount"].ToString() != "")
                {
                    model.pCount = int.Parse(row["pCount"].ToString());
                }
                if (row["cCount"] != null && row["cCount"].ToString() != "")
                {
                    model.cCount = int.Parse(row["cCount"].ToString());
                }
				if(row["IsTop"]!=null && row["IsTop"].ToString()!="")
				{
					model.IsTop=int.Parse(row["IsTop"].ToString());
				}
				if(row["IsLock"]!=null && row["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(row["IsLock"].ToString());
				}
				if(row["PubTime"]!=null && row["PubTime"].ToString()!="")
				{
					model.PubTime=DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["cCount"] != null && row["cCount"].ToString() != "")
                {
                    model.cCount = int.Parse(row["cCount"].ToString());
                }
                if (row["pCount"] != null && row["pCount"].ToString() != "")
                {
                    model.pCount = int.Parse(row["pCount"].ToString());
                }
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,Keyword,Description,Address,hContent,yhContent,ImgUrl,ImageSmall,Click,IsTop,IsLock,PubTime,(select COUNT(1) from Project where HouseId=h.ID)+cCount as cCount,(select COUNT(1) from NewsInfo where AssId=h.ID and ZxType=2)+pCount as pCount  ");
			strSql.Append(" FROM House h ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
            }
            strSql.Append(" ID,Title,Keyword,Description,Address,hContent,yhContent,ImgUrl,ImageSmall,Click,IsTop,IsLock,PubTime,(select COUNT(1) from Project where HouseId=h.ID)+cCount as cCount,(select COUNT(1) from NewsInfo where AssId=h.ID and ZxType=2)+pCount as pCount  ");
			strSql.Append(" FROM House h ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM House ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from House T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "House";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " House h";
            pager.fieldsName = " *,(select COUNT(1) from Project where HouseId=h.ID)+h.cCount as caseCount,(select COUNT(1) from NewsInfo where AssId=h.ID and ZxType=2)+h.pCount as proCount ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " IsTop desc,PubTime desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and IsLock=0";
            else
                pager.sqlWhere = " IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
		#endregion  ExtensionMethod
	}
}

