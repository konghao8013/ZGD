using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:ProjectImg
    /// </summary>
    public partial class ProjectImg
    {
        public ProjectImg()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("piID", "ProjectImg");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int pID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ProjectImg");
            strSql.Append(" where piID=@piID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@piID", SqlDbType.Int,4)};
            parameters[0].Value = pID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from ProjectImg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProjectImg set " + strValue);
            strSql.Append(" where piID=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.ProjectImg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ProjectImg(");
            strSql.Append("pID,Title,ImgUrl,ImageSmall,PubTime,Type,DesignerId)");
            strSql.Append(" values (");
            strSql.Append("@pID,@Title,@ImgUrl,@ImageSmall,@PubTime,@Type,@DesignerId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@ImageSmall", SqlDbType.NVarChar,250),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@pID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@DesignerId", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.ImgUrl;
            parameters[2].Value = model.ImageSmall;
            parameters[3].Value = model.PubTime;
            parameters[4].Value = model.pID;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.DesignerId;

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
        public bool Update(ZGD.Model.ProjectImg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProjectImg set ");
            strSql.Append("Title=@Title,");
            strSql.Append("ImgUrl=@ImgUrl,");
            strSql.Append("ImageSmall=@ImageSmall,");
            strSql.Append("PubTime=@PubTime,");
            strSql.Append("pID=@pID,");
            strSql.Append("Type=@Type,");
            strSql.Append("DesignerId=@DesignerId");
            strSql.Append(" where piID=@piID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,200),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@ImageSmall", SqlDbType.NVarChar,250),
                    new SqlParameter("@PubTime", SqlDbType.DateTime),
                    new SqlParameter("@piID", SqlDbType.Int,4),
                    new SqlParameter("@pID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@DesignerId", SqlDbType.Int,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.ImgUrl;
            parameters[2].Value = model.ImageSmall;
            parameters[3].Value = model.PubTime;
            parameters[4].Value = model.piID;
            parameters[5].Value = model.pID;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.DesignerId;

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
        public bool Delete(int pID, int pType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProjectImg ");
            strSql.Append(" where pID=@pID and Type=@Type");
            SqlParameter[] parameters = {
                    new SqlParameter("@pID", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4)
            };
            parameters[0].Value = pID;
            parameters[1].Value = pType;

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
        public bool DeleteByDesignerId(int DesignerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProjectImg ");
            strSql.Append(" where DesignerId=@DesignerId and Type=2");
            SqlParameter[] parameters = {
                    new SqlParameter("@DesignerId", SqlDbType.Int,4)
            };
            parameters[0].Value = DesignerId;

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
        public bool DeleteList(string piIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ProjectImg ");
            strSql.Append(" where piID in (" + piIDlist + ")  ");
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
        public ZGD.Model.ProjectImg GetModel(int piID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 piID,pID,Title,ImgUrl,ImageSmall,PubTime,Type,DesignerId from ProjectImg ");
            strSql.Append(" where piID=@piID");
            SqlParameter[] parameters = {
                    new SqlParameter("@piID", SqlDbType.Int,4)
            };
            parameters[0].Value = piID;

            ZGD.Model.ProjectImg model = new ZGD.Model.ProjectImg();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["piID"].ToString() != "")
                {
                    model.piID = int.Parse(ds.Tables[0].Rows[0]["piID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["pID"].ToString() != "")
                {
                    model.pID = int.Parse(ds.Tables[0].Rows[0]["pID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DesignerId"].ToString() != "")
                {
                    model.DesignerId = int.Parse(ds.Tables[0].Rows[0]["DesignerId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();
                model.ImageSmall = ds.Tables[0].Rows[0]["ImageSmall"].ToString();
                if (ds.Tables[0].Rows[0]["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(ds.Tables[0].Rows[0]["PubTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select piID,pID,Title,ImgUrl,ImageSmall,PubTime,Type,DesignerId ");
            strSql.Append(" FROM ProjectImg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetGdStatus(int pID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DesignerId ");
            strSql.Append(" FROM ProjectImg ");
            strSql.Append(" where pID=@pID and Type=5 group by DesignerId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@pID", SqlDbType.Int,4)
            };
            parameters[0].Value = pID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
            strSql.Append(" piID,pID,Title,ImgUrl,ImageSmall,PubTime,Type,DesignerId ");
            strSql.Append(" FROM ProjectImg ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
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
            parameters[0].Value = "ProjectImg";
            parameters[1].Value = "pID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

