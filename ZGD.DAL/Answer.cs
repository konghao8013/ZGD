using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references

namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Answer
    /// </summary>
    public partial class Answer
    {
        public Answer()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Answer");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Answer");
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
        public int Add(ZGD.Model.Answer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Answer(");
            strSql.Append("UserId,QuestionId,Contents,IsLock,IsAccept,PubTime,ImgUrl)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@QuestionId,@Contents,@IsLock,@IsAccept,@PubTime,@ImgUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@QuestionId", SqlDbType.Int,4),
                    new SqlParameter("@Contents", SqlDbType.NVarChar),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@IsAccept", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.QuestionId;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.IsLock;
            parameters[4].Value = model.IsAccept;
            parameters[5].Value = model.PubTime;
            parameters[6].Value = model.ImgUrl;

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
        public bool Update(ZGD.Model.Answer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Answer set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("QuestionId=@QuestionId,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("IsAccept=@IsAccept,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("ImgUrl=@ImgUrl");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@QuestionId", SqlDbType.Int,4),
                    new SqlParameter("@Contents", SqlDbType.NVarChar),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@IsAccept", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.QuestionId;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.IsLock;
            parameters[4].Value = model.IsAccept;
            parameters[5].Value = model.PubTime;
            parameters[6].Value = model.ID;
            parameters[7].Value = model.ImgUrl;

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
        public int UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Answer set " + strValue);
            strSql.Append(" where Id=" + Id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Answer ");
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
            strSql.Append("delete from Answer ");
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
        public ZGD.Model.Answer GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserId,ImgUrl,QuestionId,Contents,IsLock,IsAccept,PubTime from Answer ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            ZGD.Model.Answer model = new ZGD.Model.Answer();
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
        public ZGD.Model.Answer DataRowToModel(DataRow row)
        {
            ZGD.Model.Answer model = new ZGD.Model.Answer();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["QuestionId"] != null && row["QuestionId"].ToString() != "")
                {
                    model.QuestionId = int.Parse(row["QuestionId"].ToString());
                }
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(row["IsLock"].ToString());
                }
                if (row["IsAccept"] != null && row["IsAccept"].ToString() != "")
                {
                    model.IsAccept = int.Parse(row["IsAccept"].ToString());
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByUser(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" a.ID,a.UserId,a.ImgUrl,a.QuestionId,a.Contents,a.IsLock,a.IsAccept,a.PubTime,u.Nickname,(select count(1) from Answer a1 where a1.UserId=a.UserId) as aCount,(select count(1) from Answer a2 where a2.UserId=a.UserId and a2.IsAccept=1) as cnCount ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(" FROM Answer a inner join Users u on a.UserId=u.ID");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(filedOrder))
                strSql.Append(" order by " + filedOrder);
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
            strSql.Append(" ID,UserId,ImgUrl,QuestionId,Contents,IsLock,IsAccept,PubTime ");
            strSql.Append(" FROM Answer ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(filedOrder))
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetQaList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" a.ID,a.UserId,a.ImgUrl,a.QuestionId,a.Contents,a.IsLock,a.IsAccept,a.PubTime,q.Title ");
            strSql.Append(" FROM Answer a inner join Question q on a.QuestionId=q.ID");
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
            strSql.Append("select count(1) FROM Answer ");
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
            strSql.Append(")AS Row, T.*  from Answer T ");
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
			parameters[0].Value = "Answer";
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
            pager.tableName = " Answer a inner join Question q on a.QuestionId=q.ID ";
            pager.fieldsName = " a.ID,a.UserId,a.ImgUrl,a.QuestionId,a.Contents,a.IsLock,a.IsAccept,a.PubTime,q.Title,q.Click,(select COUNT(1) from Answer where QuestionId=q.ID) as 'aCount' ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " a.ID desc";
            pager.sqlWhere = strWhere;
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        #endregion  ExtensionMethod
    }
}

