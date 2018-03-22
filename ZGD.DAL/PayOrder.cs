using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:PayOrder
    /// </summary>
    public partial class PayOrder
    {
        public PayOrder()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "PayOrder");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PayOrder");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.PayOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PayOrder(");
            strSql.Append("AssID,PayMoney,PayMethod,UserName,Phone,Remark,PubTime,Status,AssType,PayTime,PayID)");
            strSql.Append(" values (");
            strSql.Append("@AssID,@PayMoney,@PayMethod,@UserName,@Phone,@Remark,@PubTime,@Status,@AssType,@PayTime,@PayID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AssID", SqlDbType.Int,4),
					new SqlParameter("@PayMoney", SqlDbType.Decimal,9),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@AssType", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@PayID", SqlDbType.VarChar)};
            parameters[0].Value = model.AssID;
            parameters[1].Value = model.PayMoney;
            parameters[2].Value = model.PayMethod;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.PubTime;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.AssType;
            parameters[9].Value = model.PayTime;
            parameters[10].Value = model.PayID;

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
        public bool Update(ZGD.Model.PayOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayOrder set ");
            strSql.Append("AssID=@AssID,");
            strSql.Append("PayMoney=@PayMoney,");
            strSql.Append("PayMethod=@PayMethod,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("AssType=@AssType,");
            strSql.Append("PayTime=@PayTime,");
            strSql.Append("PayID=@PayID");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@AssID", SqlDbType.Int,4),
					new SqlParameter("@PayMoney", SqlDbType.Decimal,9),
					new SqlParameter("@PayMethod", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@AssType", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@PayTime", SqlDbType.DateTime),
					new SqlParameter("@PayID", SqlDbType.VarChar)};
            parameters[0].Value = model.AssID;
            parameters[1].Value = model.PayMoney;
            parameters[2].Value = model.PayMethod;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.PubTime;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.AssType;
            parameters[9].Value = model.ID;
            parameters[10].Value = model.PayTime;
            parameters[11].Value = model.PayID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayOrder ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public ZGD.Model.PayOrder GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,AssID,PayMoney,PayMethod,UserName,Phone,Remark,PubTime,Status,AssType,PayTime,PayID from PayOrder ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            ZGD.Model.PayOrder model = new ZGD.Model.PayOrder();
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
        public ZGD.Model.PayOrder DataRowToModel(DataRow row)
        {
            ZGD.Model.PayOrder model = new ZGD.Model.PayOrder();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["AssID"] != null && row["AssID"].ToString() != "")
                {
                    model.AssID = int.Parse(row["AssID"].ToString());
                }
                if (row["PayMoney"] != null && row["PayMoney"].ToString() != "")
                {
                    model.PayMoney = decimal.Parse(row["PayMoney"].ToString());
                }
                if (row["PayMethod"] != null)
                {
                    model.PayMethod = row["PayMethod"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Phone"] != null)
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["PayTime"] != null && row["PayTime"].ToString() != "")
                {
                    model.PayTime = DateTime.Parse(row["PayTime"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["AssType"] != null && row["AssType"].ToString() != "")
                {
                    model.AssType = int.Parse(row["AssType"].ToString());
                }
                if (row["PayID"] != null)
                {
                    model.PayID = row["PayID"].ToString();
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
            strSql.Append("select ID,AssID,PayMoney,PayMethod,UserName,Phone,Remark,PubTime,Status,AssType,PayTime,PayID ");
            strSql.Append(" FROM PayOrder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" ID,AssID,PayMoney,PayMethod,UserName,Phone,Remark,PubTime,Status,AssType,PayTime,PayID ");
            strSql.Append(" FROM PayOrder ");
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
            strSql.Append("select count(1) FROM PayOrder ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from PayOrder T ");
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
            parameters[0].Value = "PayOrder";
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

        #endregion  ExtensionMethod
    }
}

