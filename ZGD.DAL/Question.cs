using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Question
    /// </summary>
    public partial class Question
    {
        public Question()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Question");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Question");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Question n");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Question(");
            strSql.Append("UserId,Title,Contents,Keyword,Description,ImgUrl,Click,IsTop,IsGood,IsLock,PubTime,Status)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@Title,@Contents,@Keyword,@Description,@ImgUrl,@Click,@IsTop,@IsGood,@IsLock,@PubTime,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.NVarChar),
                    new SqlParameter("@Contents", SqlDbType.NVarChar),
                    new SqlParameter("@Keyword", SqlDbType.NVarChar),
                    new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar),
                    new SqlParameter("@Click", SqlDbType.Int,4),
                    new SqlParameter("@IsTop", SqlDbType.Int,4),
                    new SqlParameter("@IsGood", SqlDbType.Int,4),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.Keyword;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.ImgUrl;
            parameters[6].Value = model.Click;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IsGood;
            parameters[9].Value = model.IsLock;
            parameters[10].Value = model.PubTime;
            parameters[11].Value = model.Status;

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
        public bool Update(ZGD.Model.Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Title=@Title,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Description=@Description,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("Click=@Click,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("IsGood=@IsGood,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Title", SqlDbType.NVarChar),
                    new SqlParameter("@Contents", SqlDbType.NVarChar),
                    new SqlParameter("@Keyword", SqlDbType.NVarChar),
                    new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar),
                    new SqlParameter("@Click", SqlDbType.Int,4),
                    new SqlParameter("@IsTop", SqlDbType.Int,4),
                    new SqlParameter("@IsGood", SqlDbType.Int,4),
                    new SqlParameter("@IsLock", SqlDbType.Int,4),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.Keyword;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.ImgUrl;
            parameters[6].Value = model.Click;
            parameters[7].Value = model.IsTop;
            parameters[8].Value = model.IsGood;
            parameters[9].Value = model.IsLock;
            parameters[10].Value = model.PubTime;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.ID;

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
            strSql.Append("update Question set " + strValue);
            strSql.Append(" where Id=" + Id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Question ");
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
            strSql.Append("delete from Question ");
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
        public ZGD.Model.Question GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 q.ID,q.UserId,q.Title,q.Contents,q.Keyword,q.Description,q.ImgUrl,q.Click,q.IsTop,q.IsGood,q.IsLock,q.PubTime,q.Status,u.Nickname from Question q inner join Users u on q.UserId=u.ID ");
            strSql.Append(" where q.ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            ZGD.Model.Question model = new ZGD.Model.Question();
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
        public ZGD.Model.Question DataRowToModel(DataRow row)
        {
            ZGD.Model.Question model = new ZGD.Model.Question();
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
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
                }
                if (row["Keyword"] != null)
                {
                    model.Keyword = row["Keyword"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["Nickname"] != null)
                {
                    model.Nickname = row["Nickname"].ToString();
                }
                if (row["Click"] != null && row["Click"].ToString() != "")
                {
                    model.Click = int.Parse(row["Click"].ToString());
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if (row["IsGood"] != null && row["IsGood"].ToString() != "")
                {
                    model.IsGood = int.Parse(row["IsGood"].ToString());
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(row["IsLock"].ToString());
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
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
            strSql.Append("select ID,UserId,Title,Contents,Keyword,Description,ImgUrl,Click,IsTop,IsGood,IsLock,PubTime,Status ");
            strSql.Append(" FROM Question ");
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
            strSql.Append(" q.ID,q.UserId,q.Title,q.Contents,q.Keyword,q.Description,q.ImgUrl,q.Click,q.IsTop,q.IsGood,q.IsLock,q.PubTime,q.Status,(select count(1) from Answer where QuestionId=q.ID) as aCount,(select top 1 Contents from Answer where QuestionId=q.ID order by PubTime desc) as AnCon");
            strSql.Append(" FROM Question q");
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
            strSql.Append("select count(1) FROM Question ");
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
            strSql.Append(")AS Row, T.*  from Question T ");
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
			parameters[0].Value = "Question";
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
            pager.tableName = " Question q ";
            pager.fieldsName = " q.*,(select top 1 Contents from Answer where QuestionId=q.ID) as 'AnswerCon',(select COUNT(1) from Answer where QuestionId=q.ID) as 'aCount' ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " q.IsTop desc, q.ID desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and q.IsLock=0";
            else
                pager.sqlWhere = " q.IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        #endregion  ExtensionMethod
    }
}

