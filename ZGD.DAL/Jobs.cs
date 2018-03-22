using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HX.DAL
{
	/// <summary>
	/// 数据访问类:Jobs
	/// </summary>
	public partial class Jobs
	{
		public Jobs()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Jobs"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Jobs");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(HX.Model.Jobs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Jobs(");
			strSql.Append("WorkAddr,Price,Age,Education,WorkYear,JobType,Job,JobMust,JobCount,Status,UpdateTime,PubTime)");
			strSql.Append(" values (");
			strSql.Append("@WorkAddr,@Price,@Age,@Education,@WorkYear,@JobType,@Job,@JobMust,@JobCount,@Status,@UpdateTime,@PubTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@WorkAddr", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Age", SqlDbType.NVarChar,50),
					new SqlParameter("@Education", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkYear", SqlDbType.NVarChar,50),
					new SqlParameter("@JobType", SqlDbType.Int,4),
					new SqlParameter("@Job", SqlDbType.NVarChar,500),
					new SqlParameter("@JobMust", SqlDbType.NVarChar,500),
					new SqlParameter("@JobCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PubTime", SqlDbType.DateTime)};
			parameters[0].Value = model.WorkAddr;
			parameters[1].Value = model.Price;
			parameters[2].Value = model.Age;
			parameters[3].Value = model.Education;
			parameters[4].Value = model.WorkYear;
			parameters[5].Value = model.JobType;
			parameters[6].Value = model.Job;
			parameters[7].Value = model.JobMust;
			parameters[8].Value = model.JobCount;
			parameters[9].Value = model.Status;
			parameters[10].Value = model.UpdateTime;
			parameters[11].Value = model.PubTime;

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
		public bool Update(HX.Model.Jobs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Jobs set ");
			strSql.Append("WorkAddr=@WorkAddr,");
			strSql.Append("Price=@Price,");
			strSql.Append("Age=@Age,");
			strSql.Append("Education=@Education,");
			strSql.Append("WorkYear=@WorkYear,");
			strSql.Append("JobType=@JobType,");
			strSql.Append("Job=@Job,");
			strSql.Append("JobMust=@JobMust,");
			strSql.Append("JobCount=@JobCount,");
			strSql.Append("Status=@Status,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("PubTime=@PubTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@WorkAddr", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Age", SqlDbType.NVarChar,50),
					new SqlParameter("@Education", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkYear", SqlDbType.NVarChar,50),
					new SqlParameter("@JobType", SqlDbType.Int,4),
					new SqlParameter("@Job", SqlDbType.NVarChar,500),
					new SqlParameter("@JobMust", SqlDbType.NVarChar,500),
					new SqlParameter("@JobCount", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.WorkAddr;
			parameters[1].Value = model.Price;
			parameters[2].Value = model.Age;
			parameters[3].Value = model.Education;
			parameters[4].Value = model.WorkYear;
			parameters[5].Value = model.JobType;
			parameters[6].Value = model.Job;
			parameters[7].Value = model.JobMust;
			parameters[8].Value = model.JobCount;
			parameters[9].Value = model.Status;
			parameters[10].Value = model.UpdateTime;
			parameters[11].Value = model.PubTime;
			parameters[12].Value = model.ID;

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
			strSql.Append("delete from Jobs ");
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
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Jobs ");
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
		public HX.Model.Jobs GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,WorkAddr,Price,Age,Education,WorkYear,JobType,Job,JobMust,JobCount,Status,UpdateTime,PubTime from Jobs ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			HX.Model.Jobs model=new HX.Model.Jobs();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.WorkAddr=ds.Tables[0].Rows[0]["WorkAddr"].ToString();
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				model.Age=ds.Tables[0].Rows[0]["Age"].ToString();
				model.Education=ds.Tables[0].Rows[0]["Education"].ToString();
				model.WorkYear=ds.Tables[0].Rows[0]["WorkYear"].ToString();
				if(ds.Tables[0].Rows[0]["JobType"].ToString()!="")
				{
					model.JobType=int.Parse(ds.Tables[0].Rows[0]["JobType"].ToString());
				}
				model.Job=ds.Tables[0].Rows[0]["Job"].ToString();
				model.JobMust=ds.Tables[0].Rows[0]["JobMust"].ToString();
				if(ds.Tables[0].Rows[0]["JobCount"].ToString()!="")
				{
					model.JobCount=int.Parse(ds.Tables[0].Rows[0]["JobCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PubTime"].ToString()!="")
				{
					model.PubTime=DateTime.Parse(ds.Tables[0].Rows[0]["PubTime"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,WorkAddr,Price,Age,Education,WorkYear,JobType,Job,JobMust,JobCount,Status,UpdateTime,PubTime ");
			strSql.Append(" FROM Jobs ");
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
			strSql.Append(" ID,WorkAddr,Price,Age,Education,WorkYear,JobType,Job,JobMust,JobCount,Status,UpdateTime,PubTime ");
			strSql.Append(" FROM Jobs ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[0].Value = "Jobs";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

