using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Project
    /// </summary>
    public partial class Project
    {
        public Project()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            object obj = DbHelperSQL.GetSingle("select max(id) from Project");
            return obj != null ? Convert.ToInt32(obj) : 0;
        }
        /// <summary>
        /// 得到最小ID
        /// </summary>
        public int GetMinId()
        {
            object obj = DbHelperSQL.GetSingle("select min(id) from Project");
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Project");
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
            strSql.Append(" from Project ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Project(");
            strSql.Append("Title,Keyword,Description,ImgUrl,ImageSmall,ClassId,Remark,Click,IsTop,IsLock,PubTime,pContent,Author,UserId)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Keyword,@Description,@ImgUrl,@ImageSmall,@ClassId,@Remark,@Click,@IsTop,@IsLock,@PubTime,@pContent,@Author,@UserId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@ImageSmall", SqlDbType.NVarChar,250),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@Click", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@pContent", SqlDbType.NVarChar),
					new SqlParameter("@Author", SqlDbType.NVarChar),
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Keyword;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.ImageSmall;
            parameters[5].Value = model.ClassId;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Click;
            parameters[8].Value = model.IsTop;
            parameters[9].Value = model.IsLock;
            parameters[10].Value = model.PubTime;
            parameters[11].Value = model.pContent;
            parameters[12].Value = model.Author;
            parameters[13].Value = model.UserId;

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
            strSql.Append("update Project set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Project set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Keyword=@Keyword,");
            strSql.Append("Description=@Description,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("ImageSmall=@ImageSmall,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Click=@Click,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("pContent=@pContent,");
            strSql.Append("Author=@Author,");
            strSql.Append("UserId=@UserId ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Keyword", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@ImageSmall", SqlDbType.NVarChar,250),
					new SqlParameter("@ClassId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@Click", SqlDbType.Int,4),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@pContent", SqlDbType.NVarChar),
					new SqlParameter("@Author", SqlDbType.NVarChar),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Id", SqlDbType.Int)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Keyword;
            parameters[2].Value = model.Description;
            parameters[3].Value = model.ImgUrl;
            parameters[4].Value = model.ImageSmall;
            parameters[5].Value = model.ClassId;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Click;
            parameters[8].Value = model.IsTop;
            parameters[9].Value = model.IsLock;
            parameters[10].Value = model.PubTime;
            parameters[11].Value = model.pContent;
            parameters[12].Value = model.Author;
            parameters[13].Value = model.UserId;
            parameters[14].Value = model.Id;

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
            strSql.Append("delete from Project ");
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
            strSql.Append("delete from Project ");
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
        public ZGD.Model.Project GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Title,Keyword,Description,ImgUrl,ImageSmall,ClassId,Remark,Click,IsTop,IsLock,PubTime,pContent,Author,UserId from Project ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            ZGD.Model.Project model = new ZGD.Model.Project();
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
        public ZGD.Model.Project DataRowToModel(DataRow row)
        {
            ZGD.Model.Project model = new ZGD.Model.Project();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
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
                if (row["ImageSmall"] != null)
                {
                    model.ImageSmall = row["ImageSmall"].ToString();
                }
                if (row["pContent"] != null)
                {
                    model.pContent = row["pContent"].ToString();
                }
                if (row["Author"] != null)
                {
                    model.Author = row["Author"].ToString();
                }
                if (row["ClassId"] != null && row["ClassId"].ToString() != "")
                {
                    model.ClassId = int.Parse(row["ClassId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["Click"] != null && row["Click"].ToString() != "")
                {
                    model.Click = int.Parse(row["Click"].ToString());
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(row["IsLock"].ToString());
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,Keyword,Description,ImgUrl,ImageSmall,ClassId,Remark,Click,IsTop,IsLock,PubTime,pContent,Author,UserId ");
            strSql.Append(" FROM Project ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by PubTime desc ");
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
            strSql.Append(" p.Id,p.Title,p.Keyword,p.ClassId,p.Description,p.ImgUrl,p.ImageSmall,p.Remark,p.Click,p.IsTop,p.IsLock,p.PubTime,c.Title as lx ");
            strSql.Append(" FROM Project p left join Channel c on p.ClassId=c.Id ");
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
            strSql.Append("select count(1) FROM Project ");
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
            strSql.Append(")AS Row, T.*  from Project T ");
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
            parameters[0].Value = "Project";
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " Project p left join Channel c on p.ClassId=c.Id  ";
            pager.fieldsName = " p.Id,p.Title,p.Keyword,p.ClassId,p.Description,p.Author,p.ImgUrl,p.ImageSmall,p.Remark,p.Click,p.IsTop,p.IsLock,p.PubTime,c.Title as typename ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " p.PubTime desc";
            if (!string.IsNullOrEmpty(strWhere))
                pager.sqlWhere = strWhere + " and p.IsLock=0";
            else
                pager.sqlWhere = " p.IsLock=0";
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        #endregion  ExtensionMethod
    }
}

