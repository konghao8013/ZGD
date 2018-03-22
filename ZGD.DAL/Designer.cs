using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Designer
    /// </summary>
    public partial class Designer
    {
        public Designer()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Designer");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Designer");
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
            strSql.Append(" from Designer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Designer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Designer(");
            strSql.Append("Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,Rongyu,School,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Name,@cName,@Photo,@Des,@AddDate,@cyDate,@GoodAt,@QQ,@IsTop,@Rongyu,@School,@GoodAtDes,@Keyword,@Description,@Clicks,@Star,@DeptId,@Sort)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@cName", SqlDbType.NVarChar,50),
					new SqlParameter("@Photo", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NText),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@cyDate", SqlDbType.Int),
					new SqlParameter("@GoodAt", SqlDbType.NVarChar),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@Rongyu", SqlDbType.NVarChar,200),
					new SqlParameter("@School", SqlDbType.NVarChar,200),
					new SqlParameter("@GoodAtDes", SqlDbType.NVarChar),
					new SqlParameter("@Keyword", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Clicks", SqlDbType.Int,4),
					new SqlParameter("@Star", SqlDbType.Int,4),
					new SqlParameter("@DeptId", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.cName;
            parameters[3].Value = model.Photo;
            parameters[4].Value = model.Des;
            parameters[5].Value = model.AddDate;
            parameters[6].Value = model.cyDate;
            parameters[7].Value = model.GoodAt;
            parameters[8].Value = model.QQ;
            parameters[9].Value = model.IsTop;
            parameters[10].Value = model.Rongyu;
            parameters[11].Value = model.School;
            parameters[12].Value = model.GoodAtDes;
            parameters[13].Value = model.Keyword;
            parameters[14].Value = model.Description;
            parameters[15].Value = model.Clicks;
            parameters[16].Value = model.Star;
            parameters[17].Value = model.DeptId;
            parameters[18].Value = model.Sort;

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
            strSql.Append("update Designer set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Designer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Designer set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Name=@Name,");
            strSql.Append("cName=@cName,");
            strSql.Append("Photo=@Photo,");
            strSql.Append("Des=@Des,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("cyDate=@cyDate,");
            strSql.Append("GoodAt=@GoodAt,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("Rongyu=@Rongyu,");
            strSql.Append("School=@School,");
            strSql.Append("GoodAtDes=@GoodAtDes,");
            strSql.Append("Clicks=@Clicks,");
            strSql.Append("Star=@Star,");
            strSql.Append("DeptId=@DeptId,");
            strSql.Append("Sort=@Sort");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@cName", SqlDbType.NVarChar,50),
					new SqlParameter("@Photo", SqlDbType.NVarChar,200),
					new SqlParameter("@Des", SqlDbType.NText),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@cyDate", SqlDbType.Int,4),
					new SqlParameter("@GoodAt", SqlDbType.NVarChar),
					new SqlParameter("@QQ", SqlDbType.NVarChar,50),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@Rongyu", SqlDbType.NVarChar,200),
					new SqlParameter("@School", SqlDbType.NVarChar,200),
					new SqlParameter("@GoodAtDes", SqlDbType.NVarChar),
					new SqlParameter("@Keyword", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@Clicks", SqlDbType.Int,4),
					new SqlParameter("@Star", SqlDbType.Int,4),
					new SqlParameter("@DeptId", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.cName;
            parameters[3].Value = model.Photo;
            parameters[4].Value = model.Des;
            parameters[5].Value = model.AddDate;
            parameters[6].Value = model.ID;
            parameters[7].Value = model.cyDate;
            parameters[8].Value = model.GoodAt;
            parameters[9].Value = model.QQ;
            parameters[10].Value = model.IsTop;
            parameters[11].Value = model.Rongyu;
            parameters[12].Value = model.School;
            parameters[13].Value = model.GoodAtDes;
            parameters[14].Value = model.Keyword;
            parameters[15].Value = model.Description;
            parameters[16].Value = model.Clicks;
            parameters[17].Value = model.Star;
            parameters[18].Value = model.DeptId;
            parameters[19].Value = model.Sort;

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
            strSql.Append("delete from Designer ");
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
            strSql.Append("delete from Designer ");
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
        public ZGD.Model.Designer GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,School,Rongyu,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort from Designer ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            ZGD.Model.Designer model = new ZGD.Model.Designer();
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
        public ZGD.Model.Designer GetModelByWhere(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,School,Rongyu,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort from Designer ");
            strSql.Append(" where " + where);
            ZGD.Model.Designer model = new ZGD.Model.Designer();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        public ZGD.Model.Designer DataRowToModel(DataRow row)
        {
            ZGD.Model.Designer model = new ZGD.Model.Designer();
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
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["cName"] != null)
                {
                    model.cName = row["cName"].ToString();
                }
                if (row["Photo"] != null)
                {
                    model.Photo = row["Photo"].ToString();
                }
                if (row["Des"] != null)
                {
                    model.Des = row["Des"].ToString();
                }
                if (row["AddDate"] != null && row["AddDate"].ToString() != "")
                {
                    model.AddDate = DateTime.Parse(row["AddDate"].ToString());
                }
                if (row["cyDate"] != null && row["cyDate"].ToString() != "")
                {
                    model.cyDate = int.Parse(row["cyDate"].ToString());
                }
                if (row["GoodAt"] != null)
                {
                    model.GoodAt = row["GoodAt"].ToString();
                }
                if (row["QQ"] != null)
                {
                    model.QQ = row["QQ"].ToString();
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if (row["Rongyu"] != null)
                {
                    model.Rongyu = row["Rongyu"].ToString();
                }
                if (row["School"] != null)
                {
                    model.School = row["School"].ToString();
                }
                if (row["GoodAtDes"] != null)
                {
                    model.GoodAtDes = row["GoodAtDes"].ToString();
                }
                if (row["Keyword"] != null)
                {
                    model.Keyword = row["Keyword"].ToString();
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["Clicks"] != null && row["Clicks"].ToString() != "")
                {
                    model.Clicks = int.Parse(row["Clicks"].ToString());
                }
                if (row["Star"] != null && row["Star"].ToString() != "")
                {
                    model.Star = int.Parse(row["Star"].ToString());
                }
                if (row["DeptId"] != null && row["DeptId"].ToString() != "")
                {
                    model.DeptId = int.Parse(row["DeptId"].ToString());
                }
                if (row["Sort"] != null && row["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(row["Sort"].ToString());
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
            strSql.Append("select ID,Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,School,Rongyu,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort ");
            strSql.Append(" FROM Designer ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by AddDate desc");
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
            strSql.Append(" d.ID,d.Title,d.Name,d.cName,d.Photo,d.Des,d.AddDate,d.cyDate,d.GoodAt,d.QQ,d.IsTop,d.School,d.Rongyu,d.GoodAtDes,d.Keyword,d.Description,d.Clicks,d.Star,d.DeptId,d.Sort,(select count(1) from project where DesignerId=d.ID) as pCount ");
            strSql.Append(" FROM Designer d");
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
            strSql.Append("select count(1) FROM Designer ");
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
            strSql.Append(")AS Row, T.*  from Designer T ");
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
            parameters[0].Value = "Designer";
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
            pager.tableName = " Designer ";
            pager.fieldsName = " ID,Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,School,Rongyu,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " AddDate desc";
            pager.sqlWhere = strWhere;
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList_M(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = " Designer d ";
            pager.fieldsName = " ID,Title,Name,cName,Photo,Des,AddDate,cyDate,GoodAt,QQ,IsTop,School,Rongyu,GoodAtDes,Keyword,Description,Clicks,Star,DeptId,Sort,(select count(1) from Project where DesignerId=d.ID) as pCount ";
            if (!string.IsNullOrEmpty(sortName))
                pager.orderField = sortName;
            else
                pager.orderField = " AddDate desc";
            pager.sqlWhere = strWhere;
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }
        #endregion  ExtensionMethod
    }
}

