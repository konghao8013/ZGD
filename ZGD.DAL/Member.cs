using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
	/// <summary>
	/// 数据访问类:Member
	/// </summary>
	public partial class Member
	{
		public Member()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "Member"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Member");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZGD.Model.Member model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Member(");
			strSql.Append("Account,Password,UserName,VipCard,OpenId,Phone,Address,Remark,IsLock,AddDate)");
			strSql.Append(" values (");
			strSql.Append("@Account,@Password,@UserName,@VipCard,@OpenId,@Phone,@Address,@Remark,@IsLock,@AddDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.NVarChar,20),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@VipCard", SqlDbType.NVarChar,50),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,100),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime)};
			parameters[0].Value = model.Account;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.VipCard;
			parameters[4].Value = model.OpenId;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.IsLock;
			parameters[9].Value = model.AddDate;

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
		public bool Update(ZGD.Model.Member model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Member set ");
			strSql.Append("Account=@Account,");
			strSql.Append("Password=@Password,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("VipCard=@VipCard,");
			strSql.Append("OpenId=@OpenId,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Address=@Address,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("IsLock=@IsLock,");
			strSql.Append("AddDate=@AddDate");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Account", SqlDbType.NVarChar,20),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@VipCard", SqlDbType.NVarChar,50),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,100),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Account;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.VipCard;
			parameters[4].Value = model.OpenId;
			parameters[5].Value = model.Phone;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.IsLock;
			parameters[9].Value = model.AddDate;
			parameters[10].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Member ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Member ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public ZGD.Model.Member GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Account,Password,UserName,VipCard,OpenId,Phone,Address,Remark,IsLock,AddDate from Member ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			ZGD.Model.Member model=new ZGD.Model.Member();
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
		public ZGD.Model.Member DataRowToModel(DataRow row)
		{
			ZGD.Model.Member model=new ZGD.Model.Member();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Account"]!=null)
				{
					model.Account=row["Account"].ToString();
				}
				if(row["Password"]!=null)
				{
					model.Password=row["Password"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["VipCard"]!=null)
				{
					model.VipCard=row["VipCard"].ToString();
				}
				if(row["OpenId"]!=null)
				{
					model.OpenId=row["OpenId"].ToString();
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["IsLock"]!=null && row["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(row["IsLock"].ToString());
				}
				if(row["AddDate"]!=null && row["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(row["AddDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,Account,Password,UserName,VipCard,OpenId,Phone,Address,Remark,IsLock,AddDate ");
			strSql.Append(" FROM Member ");
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
			strSql.Append(" Id,Account,Password,UserName,VipCard,OpenId,Phone,Address,Remark,IsLock,AddDate ");
			strSql.Append(" FROM Member ");
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
			strSql.Append("select count(1) FROM Member ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from Member T ");
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
			parameters[0].Value = "Member";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

