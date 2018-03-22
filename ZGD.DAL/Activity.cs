using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Activity
    /// </summary>
    public partial class Activity
    {
        public Activity()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Activity");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Activity");
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
            strSql.Append(" from Activity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Activity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Activity(");
            strSql.Append("Title,sDate,eDate,ImgUrl,aCon,PubTime,Status,aUrl,JoinCount,Keyword,Description,Click)");
            strSql.Append(" values (");
            strSql.Append("@Title,@sDate,@eDate,@ImgUrl,@aCon,@PubTime,@Status,@aUrl,@JoinCount,@Keyword,@Description,@Click)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@sDate", SqlDbType.NVarChar),
					new SqlParameter("@eDate", SqlDbType.NVarChar),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@aCon", SqlDbType.Text),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@aUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@JoinCount", SqlDbType.Int,4),
					new SqlParameter("@Keyword", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Click", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.sDate;
            parameters[2].Value = model.eDate;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.aCon;
            parameters[5].Value = model.PubTime;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.aUrl;
            parameters[8].Value = model.JoinCount;
            parameters[9].Value = model.Keyword;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.Click;

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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Activity set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Activity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Activity set ");
            strSql.Append("Title=@Title,");
            strSql.Append("sDate=@sDate,");
            strSql.Append("eDate=@eDate,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("aCon=@aCon,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("Status=@Status,");
            strSql.Append("aUrl=@aUrl,");
            strSql.Append("JoinCount=@JoinCount,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Description=@Description,");
            strSql.Append("Click=@Click");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@sDate", SqlDbType.NVarChar),
					new SqlParameter("@eDate", SqlDbType.NVarChar),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@aCon", SqlDbType.Text),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@aUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@JoinCount", SqlDbType.Int,4),
					new SqlParameter("@Keyword", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Click", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.sDate;
            parameters[2].Value = model.eDate;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.aCon;
            parameters[5].Value = model.PubTime;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.ID;
            parameters[8].Value = model.aUrl;
            parameters[9].Value = model.JoinCount;
            parameters[10].Value = model.Keyword;
            parameters[11].Value = model.Description;
            parameters[12].Value = model.Click;

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
            strSql.Append("delete from Activity ");
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
            strSql.Append("delete from Activity ");
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
        public ZGD.Model.Activity GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,sDate,eDate,ImgUrl,aCon,PubTime,Status,aUrl,JoinCount,Keyword,Description,Click from Activity ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            ZGD.Model.Activity model = new ZGD.Model.Activity();
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
        public ZGD.Model.Activity DataRowToModel(DataRow row)
        {
            ZGD.Model.Activity model = new ZGD.Model.Activity();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }
                if (row["sDate"] != null)
                {
                    model.sDate = row["sDate"].ToString();
                }
                if (row["eDate"] != null)
                {
                    model.eDate = row["eDate"].ToString();
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["aCon"] != null)
                {
                    model.aCon = row["aCon"].ToString();
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["JoinCount"] != null && row["JoinCount"].ToString() != "")
                {
                    model.JoinCount = int.Parse(row["JoinCount"].ToString());
                }
                if (row["aUrl"] != null)
                {
                    model.aUrl = row["aUrl"].ToString();
                }
                if (row["Keyword"] != null)
                {
                    model.Keyword = row["Keyword"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Click"] != null && row["Click"].ToString() != "")
                {
                    model.Click = int.Parse(row["Click"].ToString());
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
            strSql.Append("select ID,Title,sDate,eDate,ImgUrl,aCon,PubTime,Status,aUrl,JoinCount,Keyword,Description,Click ");
            strSql.Append(" FROM Activity ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Status desc,PubTime desc");
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
            strSql.Append(" ID,Title,sDate,eDate,ImgUrl,aCon,PubTime,Status,aUrl,JoinCount,Keyword,Description,Click ");
            strSql.Append(" FROM Activity ");
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
            strSql.Append("select count(1) FROM Activity ");
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
            parameters[0].Value = "Activity";
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
            pager.tableName = " Activity ";
            pager.fieldsName = " ID,Title,sDate,eDate,ImgUrl,aCon,PubTime,Status,aUrl,JoinCount,Keyword,Description,Click ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " Status desc,PubTime desc";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        #endregion  ExtensionMethod
    }
}

