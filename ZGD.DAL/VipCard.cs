using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;
using System.Collections.Generic;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:VipCard
    /// </summary>
    public partial class VipCard
    {
        public VipCard()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CardNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from VipCard");
            strSql.Append(" where CardNo=@CardNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CardNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 批量新增数据
        /// </summary>
        public int AddList(List<ZGD.Model.VipCard> models)
        {
            if (models.Count > 0)
            {
                StringBuilder strSql = new StringBuilder();
                foreach (var item in models)
                {
                    strSql.Append("insert into VipCard(CardNo,Status,PubTime) values ('" + item.CardNo + "',0,getdate());");
                }
                return DbHelperSQL.ExecuteSql(strSql.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ZGD.Model.VipCard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into VipCard(");
            strSql.Append("CardNo,OpenId,Status,PubTime,BindTime)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@OpenId,@Status,@PubTime,@BindTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@BindTime", SqlDbType.DateTime)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.OpenId;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.PubTime;
            parameters[4].Value = model.BindTime;

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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(string CardNo, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VipCard set " + strValue);
            strSql.Append(" where CardNo='" + CardNo + "'");
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.VipCard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VipCard set ");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("Status=@Status,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("BindTime=@BindTime");
            strSql.Append(" where CardNo=@CardNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@BindTime", SqlDbType.DateTime),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.OpenId;
            parameters[1].Value = model.Status;
            parameters[2].Value = model.PubTime;
            parameters[3].Value = model.BindTime;
            parameters[4].Value = model.CardNo;

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
        public bool Delete(string CardNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from VipCard ");
            strSql.Append(" where CardNo=@CardNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CardNo;

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
        public bool DeleteList(string CardNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from VipCard ");
            strSql.Append(" where CardNo in (" + CardNolist + ")  ");
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
        public ZGD.Model.VipCard GetModel(string CardNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CardNo,OpenId,Status,PubTime,BindTime from VipCard ");
            strSql.Append(" where CardNo=@CardNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50)			};
            parameters[0].Value = CardNo;

            ZGD.Model.VipCard model = new ZGD.Model.VipCard();
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
        public ZGD.Model.VipCard DataRowToModel(DataRow row)
        {
            ZGD.Model.VipCard model = new ZGD.Model.VipCard();
            if (row != null)
            {
                if (row["CardNo"] != null)
                {
                    model.CardNo = row["CardNo"].ToString();
                }
                if (row["OpenId"] != null)
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["BindTime"] != null && row["BindTime"].ToString() != "")
                {
                    model.BindTime = DateTime.Parse(row["BindTime"].ToString());
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
            strSql.Append("select CardNo,OpenId,Status,PubTime,BindTime ");
            strSql.Append(" FROM VipCard ");
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
            strSql.Append(" CardNo,OpenId,Status,PubTime,BindTime ");
            strSql.Append(" FROM VipCard ");
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
            strSql.Append("select count(1) FROM VipCard ");
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
                strSql.Append("order by T.CardNo desc");
            }
            strSql.Append(")AS Row, T.*  from VipCard T ");
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
            parameters[0].Value = "VipCard";
            parameters[1].Value = "CardNo";
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
        /// 绑定VIP
        /// </summary>
        public int BindVip(string vipNo, string openId)
        {
            string sql = "update VipCard set OpenId=@OpenId,BindTime=getdate()  where CardNo=@CardNo";
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@OpenId", SqlDbType.NVarChar,100)			
            };
            parameters[0].Value = vipNo;
            parameters[1].Value = openId;
            return DbHelperSQL.ExecuteSql(sql, parameters);
        }

        #endregion  ExtensionMethod
    }
}

