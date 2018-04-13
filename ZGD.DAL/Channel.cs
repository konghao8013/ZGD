using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references
namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Channel
    /// </summary>
    public partial class Channel
    {
        public Channel()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Channel");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Channel");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回栏目名称
        /// </summary>
        public string GetChannelTitle(string classId)
        {
            if (string.IsNullOrWhiteSpace(classId))
            {
                return "";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Title from Channel");
            strSql.Append(" where Id in (" + classId + ")");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                string title = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    title += dr["Title"].ToString() + ",";
                }
                return string.IsNullOrWhiteSpace(title) ? "" : title.TrimEnd(',');
            }
            return "";
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Channel(");
            strSql.Append("Title,ParentId,ClassList,ClassLayer,SortId,PageUrl,KindId,IsDelete,ImgUrl)");
            strSql.Append(" values (");
            strSql.Append("@Title,@ParentId,@ClassList,@ClassLayer,@SortId,@PageUrl,@KindId,@IsDelete,@ImgUrl)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,50),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ClassList", SqlDbType.NVarChar,500),
                    new SqlParameter("@ClassLayer", SqlDbType.Int,4),
                    new SqlParameter("@SortId", SqlDbType.Int,4),
                    new SqlParameter("@PageUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@KindId", SqlDbType.Int,4),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.ClassList;
            parameters[3].Value = model.ClassLayer;
            parameters[4].Value = model.SortId;
            parameters[5].Value = model.PageUrl;
            parameters[6].Value = model.KindId;
            parameters[7].Value = model.IsDelete;
            parameters[8].Value = model.ImgUrl;

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
        public bool Update(ZGD.Model.Channel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Channel set ");
            strSql.Append("Title=@Title,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("ClassList=@ClassList,");
            strSql.Append("ClassLayer=@ClassLayer,");
            strSql.Append("SortId=@SortId,");
            strSql.Append("PageUrl=@PageUrl,");
            strSql.Append("KindId=@KindId,");
            strSql.Append("IsDelete=@IsDelete,");
            strSql.Append("ImgUrl=@ImgUrl");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar,50),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ClassList", SqlDbType.NVarChar,500),
                    new SqlParameter("@ClassLayer", SqlDbType.Int,4),
                    new SqlParameter("@SortId", SqlDbType.Int,4),
                    new SqlParameter("@PageUrl", SqlDbType.NVarChar,250),
                    new SqlParameter("@KindId", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@ImgUrl", SqlDbType.NVarChar)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.ClassList;
            parameters[3].Value = model.ClassLayer;
            parameters[4].Value = model.SortId;
            parameters[5].Value = model.PageUrl;
            parameters[6].Value = model.KindId;
            parameters[7].Value = model.Id;
            parameters[8].Value = model.IsDelete;
            parameters[9].Value = model.ImgUrl;

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
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Channel set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除该栏目分类及所有属下分类数据
        /// </summary>
        public void Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Channel ");
            strSql.Append(" where ClassList like '%," + Id + ",%' ");

            DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Channel ");
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
        public ZGD.Model.Channel GetModelById(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Title,ParentId,ClassList,ClassLayer,SortId,PageUrl,KindId,IsDelete,ImgUrl from Channel ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int, 4)
            };
            parameters[0].Value = Id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return GetModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Channel GetModelByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Title,ParentId,ClassList,ClassLayer,SortId,PageUrl,KindId,IsDelete,ImgUrl from Channel ");
            strSql.Append(" where Title=@Title");
            SqlParameter[] parameters = {
                    new SqlParameter("@Title", SqlDbType.NVarChar, 50)
            };
            parameters[0].Value = name;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return GetModel(ds);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Channel GetModel(DataSet ds)
        {
            ZGD.Model.Channel model = new ZGD.Model.Channel();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ClassList"] != null && ds.Tables[0].Rows[0]["ClassList"].ToString() != "")
                {
                    model.ClassList = ds.Tables[0].Rows[0]["ClassList"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClassLayer"] != null && ds.Tables[0].Rows[0]["ClassLayer"].ToString() != "")
                {
                    model.ClassLayer = int.Parse(ds.Tables[0].Rows[0]["ClassLayer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SortId"] != null && ds.Tables[0].Rows[0]["SortId"].ToString() != "")
                {
                    model.SortId = int.Parse(ds.Tables[0].Rows[0]["SortId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PageUrl"] != null && ds.Tables[0].Rows[0]["PageUrl"].ToString() != "")
                {
                    model.PageUrl = ds.Tables[0].Rows[0]["PageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["KindId"] != null && ds.Tables[0].Rows[0]["KindId"].ToString() != "")
                {
                    model.KindId = int.Parse(ds.Tables[0].Rows[0]["KindId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsDelete"] != null && ds.Tables[0].Rows[0]["IsDelete"].ToString() != "")
                {
                    model.IsDelete = int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ImgUrl"] != null && ds.Tables[0].Rows[0]["ImgUrl"].ToString() != "")
                {
                    model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();
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
        public DataSet GetZtYear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct convert(varchar(4), PubTime,111) y from Channel where ParentId=21 and KindId=2 order by y desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,ParentId,ClassList,ClassLayer,SortId,PageUrl,KindId,IsDelete,ImgUrl ");
            strSql.Append(" FROM Channel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获取连接类别
        /// </summary>
        public DataSet GetLinkType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct c.Id,c.Title from links l inner join Channel c on l.ClassId=c.Id where l.IsLock=0 and c.Id<>38");
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
            strSql.Append(" Id,Title,ParentId,ClassList,ClassLayer,SortId,PageUrl,KindId,IsDelete,ImgUrl ");
            strSql.Append(" FROM Channel ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(filedOrder))
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 取得所有栏目列表
        /// </summary>
        /// <param name="PId">父ID</param>
        /// <param name="KId">种类ID</param>
        /// <returns></returns>
        public DataTable GetList(int PId, int KId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,ParentId,ClassList,ClassLayer,SortId,KindId,IsDelete,ImgUrl from Channel");
            strSql.Append(" where KindId=" + KId + " order by SortId asc,Id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }

            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChannelChild(oldData, newData, PId, KId);
            return newData;
        }

        /// <summary>
        /// 取得所有栏目列表
        /// </summary>
        /// <param name="PId">父ID</param>
        /// <param name="KId">种类ID</param>
        /// <returns></returns>
        public DataTable BindList(int PId, int KId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,ParentId,ClassList,ClassLayer,SortId,KindId,IsDelete,ImgUrl from Channel");
            strSql.Append(" where KindId=" + KId + " and IsDelete=0 order by SortId asc,Id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }

            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChannelChild(oldData, newData, PId, KId);
            return newData;
        }

        /// <summary>
        /// 从内存中取得所有下级栏目列表（自身迭代）
        /// </summary>
        private void GetChannelChild(DataTable oldData, DataTable newData, int PId, int KId)
        {
            DataRow[] dr = oldData.Select("ParentId=" + PId);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["Id"] = int.Parse(dr[i]["Id"].ToString());
                row["Title"] = dr[i]["Title"].ToString();
                row["ParentId"] = int.Parse(dr[i]["ParentId"].ToString());
                row["ClassList"] = dr[i]["ClassList"].ToString();
                row["ClassLayer"] = int.Parse(dr[i]["ClassLayer"].ToString());
                row["SortId"] = int.Parse(dr[i]["SortId"].ToString());
                row["KindId"] = int.Parse(dr[i]["KindId"].ToString());
                row["IsDelete"] = int.Parse(dr[i]["IsDelete"].ToString());
                row["ImgUrl"] = dr[i]["ImgUrl"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChannelChild(oldData, newData, int.Parse(dr[i]["Id"].ToString()), KId);
            }
        }

        /// <summary>
        /// 取得该栏目下的所有子栏目的ID
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataSet GetChannelListByClassId(int classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ClassList,ClassLayer from Channel");
            strSql.Append(" where Id=" + classId + " ");
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
            parameters[0].Value = "Channel";
            parameters[1].Value = "Id";
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