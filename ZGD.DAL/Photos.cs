using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Photos
    /// </summary>
    public partial class Photos
    {
        public Photos()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Photos");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Photos");
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
        public int Add(ZGD.Model.Photos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Photos(");
            strSql.Append("ImgUrl,PhotoName,PhotoCon,Sort,PubTime,IsTop,Year,Type,pType)");
            strSql.Append(" values (");
            strSql.Append("@ImgUrl,@PhotoName,@PhotoCon,@Sort,@PubTime,@IsTop,@Year,@Type,@pType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@PhotoName", SqlDbType.NVarChar),
					new SqlParameter("@PhotoCon", SqlDbType.NVarChar),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@Year", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@pType", SqlDbType.Int,4)};
            parameters[0].Value = model.ImgUrl;
            parameters[1].Value = model.PhotoName;
            parameters[2].Value = model.PhotoCon;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.PubTime;
            parameters[5].Value = model.IsTop;
            parameters[6].Value = model.Year;
            parameters[7].Value = model.Type;
            parameters[8].Value = model.pType;

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
            strSql.Append("update Photos set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZGD.Model.Photos model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Photos set ");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("PhotoName=@PhotoName,");
            strSql.Append("PhotoCon=@PhotoCon,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("IsTop=@IsTop,");
            strSql.Append("Year=@Year,");
            strSql.Append("Type=@Type,");
            strSql.Append("pType=@pType");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
					new SqlParameter("@PhotoName", SqlDbType.NVarChar),
					new SqlParameter("@PhotoCon", SqlDbType.NVarChar),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@PubTime", SqlDbType.DateTime),
					new SqlParameter("@IsTop", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Year", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@pType", SqlDbType.Int,4)};
            parameters[0].Value = model.ImgUrl;
            parameters[1].Value = model.PhotoName;
            parameters[2].Value = model.PhotoCon;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.PubTime;
            parameters[5].Value = model.IsTop;
            parameters[6].Value = model.ID;
            parameters[7].Value = model.Year;
            parameters[8].Value = model.Type;
            parameters[9].Value = model.pType;

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
            strSql.Append("delete from Photos ");
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
            strSql.Append("delete from Photos ");
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
        public ZGD.Model.Photos GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,ImgUrl,PhotoName,PhotoCon,Sort,PubTime,IsTop,Year,Type,pType from Photos ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            ZGD.Model.Photos model = new ZGD.Model.Photos();
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
        public ZGD.Model.Photos DataRowToModel(DataRow row)
        {
            ZGD.Model.Photos model = new ZGD.Model.Photos();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ImgUrl"] != null)
                {
                    model.ImgUrl = row["ImgUrl"].ToString();
                }
                if (row["PhotoName"] != null)
                {
                    model.PhotoName = row["PhotoName"].ToString();
                }
                if (row["PhotoCon"] != null)
                {
                    model.PhotoCon = row["PhotoCon"].ToString();
                }
                if (row["Sort"] != null && row["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(row["Sort"].ToString());
                }
                if (row["PubTime"] != null && row["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(row["PubTime"].ToString());
                }
                if (row["IsTop"] != null && row["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(row["IsTop"].ToString());
                }
                if (row["Year"] != null && row["Year"].ToString() != "")
                {
                    model.Year = int.Parse(row["Year"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["pType"] != null && row["pType"].ToString() != "")
                {
                    model.pType = int.Parse(row["pType"].ToString());
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
            strSql.Append("select ID,ImgUrl,PhotoName,PhotoCon,Sort,PubTime,IsTop,Year,Type,pType ");
            strSql.Append(" FROM Photos ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by PubTime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetYears()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct Year FROM Photos where Type=3 order by Year asc");
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
            strSql.Append(" ID,ImgUrl,PhotoName,PhotoCon,Sort,PubTime,IsTop,Year,Type,pType ");
            strSql.Append(" FROM Photos ");
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
            strSql.Append("select count(1) FROM Photos ");
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
            strSql.Append(")AS Row, T.*  from Photos T ");
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
            parameters[0].Value = "Photos";
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

