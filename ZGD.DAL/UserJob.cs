using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace HX.DAL
{
	/// <summary>
	/// 数据访问类:UserJob
	/// </summary>
	public partial class UserJob
	{
		public UserJob()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "UserJob"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserJob");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(HX.Model.UserJob model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserJob(");
			strSql.Append("uName,Sex,Birthday,Phone,WorkYear,Files,Profession,CurJob,City,CurPrice,Price,InTime,PubTime,Status)");
			strSql.Append(" values (");
			strSql.Append("@uName,@Sex,@Birthday,@Phone,@WorkYear,@Files,@Profession,@CurJob,@City,@CurPrice,@Price,@InTime,@PubTime,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@uName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,1),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkYear", SqlDbType.Int,4),
					new SqlParameter("@Files", SqlDbType.NVarChar,250),
					new SqlParameter("@Profession", SqlDbType.NVarChar,200),
					new SqlParameter("@CurJob", SqlDbType.NVarChar,250),
					new SqlParameter("@City", SqlDbType.NVarChar,250),
					new SqlParameter("@CurPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@InTime", SqlDbType.NVarChar,50),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.uName;
			parameters[1].Value = model.Sex;
			parameters[2].Value = model.Birthday;
			parameters[3].Value = model.Phone;
			parameters[4].Value = model.WorkYear;
			parameters[5].Value = model.Files;
			parameters[6].Value = model.Profession;
			parameters[7].Value = model.CurJob;
			parameters[8].Value = model.City;
			parameters[9].Value = model.CurPrice;
			parameters[10].Value = model.Price;
			parameters[11].Value = model.InTime;
			parameters[12].Value = model.PubTime;
			parameters[13].Value = model.Status;

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
		public bool Update(HX.Model.UserJob model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserJob set ");
			strSql.Append("uName=@uName,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Birthday=@Birthday,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("WorkYear=@WorkYear,");
			strSql.Append("Files=@Files,");
			strSql.Append("Profession=@Profession,");
			strSql.Append("CurJob=@CurJob,");
			strSql.Append("City=@City,");
			strSql.Append("CurPrice=@CurPrice,");
			strSql.Append("Price=@Price,");
			strSql.Append("InTime=@InTime,");
			strSql.Append("PubTime=@PubTime,");
			strSql.Append("Status=@Status");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@uName", SqlDbType.NVarChar,50),
					new SqlParameter("@Sex", SqlDbType.NVarChar,1),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Phone", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkYear", SqlDbType.Int,4),
					new SqlParameter("@Files", SqlDbType.NVarChar,250),
					new SqlParameter("@Profession", SqlDbType.NVarChar,200),
					new SqlParameter("@CurJob", SqlDbType.NVarChar,250),
					new SqlParameter("@City", SqlDbType.NVarChar,250),
					new SqlParameter("@CurPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@InTime", SqlDbType.NVarChar,50),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.uName;
			parameters[1].Value = model.Sex;
			parameters[2].Value = model.Birthday;
			parameters[3].Value = model.Phone;
			parameters[4].Value = model.WorkYear;
			parameters[5].Value = model.Files;
			parameters[6].Value = model.Profession;
			parameters[7].Value = model.CurJob;
			parameters[8].Value = model.City;
			parameters[9].Value = model.CurPrice;
			parameters[10].Value = model.Price;
			parameters[11].Value = model.InTime;
			parameters[12].Value = model.PubTime;
			parameters[13].Value = model.Status;
			parameters[14].Value = model.ID;

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
			strSql.Append("delete from UserJob ");
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
			strSql.Append("delete from UserJob ");
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
		public HX.Model.UserJob GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,uName,Sex,Birthday,Phone,WorkYear,Files,Profession,CurJob,City,CurPrice,Price,InTime,PubTime,Status from UserJob ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			HX.Model.UserJob model=new HX.Model.UserJob();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.uName=ds.Tables[0].Rows[0]["uName"].ToString();
				model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
				if(ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				if(ds.Tables[0].Rows[0]["WorkYear"].ToString()!="")
				{
					model.WorkYear=int.Parse(ds.Tables[0].Rows[0]["WorkYear"].ToString());
				}
				model.Files=ds.Tables[0].Rows[0]["Files"].ToString();
				model.Profession=ds.Tables[0].Rows[0]["Profession"].ToString();
				model.CurJob=ds.Tables[0].Rows[0]["CurJob"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				if(ds.Tables[0].Rows[0]["CurPrice"].ToString()!="")
				{
					model.CurPrice=decimal.Parse(ds.Tables[0].Rows[0]["CurPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				model.InTime=ds.Tables[0].Rows[0]["InTime"].ToString();
				if(ds.Tables[0].Rows[0]["PubTime"].ToString()!="")
				{
					model.PubTime=DateTime.Parse(ds.Tables[0].Rows[0]["PubTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
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
			strSql.Append("select ID,uName,Sex,Birthday,Phone,WorkYear,Files,Profession,CurJob,City,CurPrice,Price,InTime,PubTime,Status ");
			strSql.Append(" FROM UserJob ");
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
			strSql.Append(" ID,uName,Sex,Birthday,Phone,WorkYear,Files,Profession,CurJob,City,CurPrice,Price,InTime,PubTime,Status ");
			strSql.Append(" FROM UserJob ");
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
			parameters[0].Value = "UserJob";
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

