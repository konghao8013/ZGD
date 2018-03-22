using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Feedback
    /// </summary>
    public partial class Feedback
    {
        public Feedback()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Feedback");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Feedback");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Feedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Feedback model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Feedback(");
            strSql.Append("ClassId,UserName,UserTel,UserQQ,House,Area,YaoQiu,Remark,Sex,AddTime,ClassId2,fType)");
            strSql.Append(" values (");
            strSql.Append("@ClassId,@UserName,@UserTel,@UserQQ,@House,@Area,@YaoQiu,@Remark,@Sex,@AddTime,@ClassId2,@fType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserTel", SqlDbType.NVarChar,200),
					new SqlParameter("@UserQQ", SqlDbType.NVarChar,200),
					new SqlParameter("@House", SqlDbType.NVarChar,200),
					new SqlParameter("@Area", SqlDbType.Decimal),
					new SqlParameter("@YaoQiu", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.NVarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@ClassId2", SqlDbType.Int,4),
					new SqlParameter("@fType", SqlDbType.Int,4)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserTel;
            parameters[3].Value = model.UserQQ;
            parameters[4].Value = model.House;
            parameters[5].Value = model.Area;
            parameters[6].Value = model.YaoQiu;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.AddTime;
            parameters[10].Value = model.ClassId2;
            parameters[11].Value = model.fType;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(ZGD.Model.Feedback model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Feedback set ");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserTel=@UserTel,");
            strSql.Append("UserQQ=@UserQQ,");
            strSql.Append("House=@House,");
            strSql.Append("Area=@Area,");
            strSql.Append("YaoQiu=@YaoQiu,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("ClassId2=@ClassId2,");
            strSql.Append("fType=@fType");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserTel", SqlDbType.NVarChar,200),
					new SqlParameter("@UserQQ", SqlDbType.NVarChar,200),
					new SqlParameter("@House", SqlDbType.NVarChar,200),
					new SqlParameter("@Area", SqlDbType.NVarChar,200),
					new SqlParameter("@YaoQiu", SqlDbType.NVarChar,200),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Sex", SqlDbType.NVarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@ClassId2", SqlDbType.Int,4),
					new SqlParameter("@fType", SqlDbType.Int,4)};
            parameters[0].Value = model.ClassId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserTel;
            parameters[3].Value = model.UserQQ;
            parameters[4].Value = model.House;
            parameters[5].Value = model.Area;
            parameters[6].Value = model.YaoQiu;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.Sex;
            parameters[9].Value = model.AddTime;
            parameters[10].Value = model.Id;
            parameters[11].Value = model.ClassId2;
            parameters[12].Value = model.fType;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Feedback ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Feedback ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public ZGD.Model.Feedback GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,ClassId,UserName,UserTel,UserQQ,House,Area,YaoQiu,Remark,Sex,AddTime,ClassId2,fType from Feedback ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            ZGD.Model.Feedback model = new ZGD.Model.Feedback();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public ZGD.Model.Feedback DataRowToModel(DataRow row)
        {
            ZGD.Model.Feedback model = new ZGD.Model.Feedback();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["ClassId2"] != null && row["ClassId2"].ToString() != "")
                {
                    model.ClassId2 = int.Parse(row["ClassId2"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["UserTel"] != null)
                {
                    model.UserTel = row["UserTel"].ToString();
                }
                if (row["UserQQ"] != null)
                {
                    model.UserQQ = row["UserQQ"].ToString();
                }
                if (row["House"] != null)
                {
                    model.House = row["House"].ToString();
                }
                if (row["Area"] != null)
                {
                    model.Area = decimal.Parse(row["Area"].ToString());
                }
                if (row["YaoQiu"] != null)
                {
                    model.YaoQiu = row["YaoQiu"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["Sex"] != null)
                {
                    model.Sex = row["Sex"].ToString();
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["fType"] != null && row["fType"].ToString() != "")
                {
                    model.fType = int.Parse(row["fType"].ToString());
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
            strSql.Append("select Id,ClassId,UserName,UserTel,UserQQ,House,Area,YaoQiu,Remark,Sex,AddTime,ClassId2,fType ");
            strSql.Append(" FROM Feedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by AddTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,ClassId,UserName,UserTel,UserQQ,House,Area,YaoQiu,Remark,Sex,AddTime,ClassId2,fType ");
            strSql.Append(" FROM Feedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Feedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Feedback T ");
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
            parameters[0].Value = "Feedback";
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

