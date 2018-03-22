using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZGD.DBUtility;//Please add references

namespace ZGD.DAL
{
    /// <summary>
    /// 数据访问类:Admin
    /// </summary>
    public partial class Admin
    {
        public Admin()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Admin");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Admin");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool Exists(string UserName)
        {
            string strSql = "select count(*) from Admin where UserName=@UserName";
            SqlParameter[] parameters = {
                new SqlParameter("@UserName",SqlDbType.NVarChar,30)};
            parameters[0].Value = UserName;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查登录用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="UserPwd">密码</param>
        /// <returns></returns>
        public bool chkAdminLogin(string UserName, string UserPwd)
        {
            string strSql = "select count(*) from Admin where UserName=@UserName and UserPwd=@UserPwd and IsLock=0";
            SqlParameter[] parameters = {
                new SqlParameter("@UserName",SqlDbType.NVarChar,30),
                new SqlParameter("@UserPwd", SqlDbType.NVarChar,50)};
            parameters[0].Value = UserName;
            parameters[1].Value = UserPwd;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZGD.Model.Admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Admin(");
            strSql.Append("UserName,UserPwd,Address,Tel,UserEmail,UserType,UserLevel,IsLock,AddTime,LoginTime)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@UserPwd,@Address,@Tel,@UserEmail,@UserType,@UserLevel,@IsLock,@AddTime,@LoginTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,30),
					new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@Tel", SqlDbType.NVarChar,100),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,30),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@UserLevel", SqlDbType.NText),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@LoginTime", SqlDbType.DateTime)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.UserPwd;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.UserEmail;
            parameters[5].Value = model.UserType;
            parameters[6].Value = model.UserLevel;
            parameters[7].Value = model.IsLock;
            parameters[8].Value = model.AddTime;
            parameters[9].Value = model.LoginTime;

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
        public bool Update(ZGD.Model.Admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Admin set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPwd=@UserPwd,");
            strSql.Append("Address=@Address,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("UserEmail=@UserEmail,");
            strSql.Append("UserType=@UserType,");
            strSql.Append("UserLevel=@UserLevel,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("LoginTime=@LoginTime");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,30),
					new SqlParameter("@UserPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@Tel", SqlDbType.NVarChar,100),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar,30),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@UserLevel", SqlDbType.NText),
					new SqlParameter("@IsLock", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.UserPwd;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.UserEmail;
            parameters[5].Value = model.UserType;
            parameters[6].Value = model.UserLevel;
            parameters[7].Value = model.IsLock;
            parameters[8].Value = model.AddTime;
            parameters[9].Value = model.LoginTime;
            parameters[10].Value = model.Id;

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
            strSql.Append("delete from Admin ");
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
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
        /// 根据用户名取得一行数据给Model
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ZGD.Model.Admin GetModel1(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserName,UserPwd,Address,Tel,UserEmail,UserType,UserLevel,IsLock,AddTime,LoginTime from Admin ");
            strSql.Append(" where UserName=@UserName ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,30)
                                        };
            parameters[0].Value = UserName;

            ZGD.Model.Admin model = new ZGD.Model.Admin();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPwd"] != null && ds.Tables[0].Rows[0]["UserPwd"].ToString() != "")
                {
                    model.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserEmail"] != null && ds.Tables[0].Rows[0]["UserEmail"].ToString() != "")
                {
                    model.UserEmail = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserLevel"] != null && ds.Tables[0].Rows[0]["UserLevel"].ToString() != "")
                {
                    model.UserLevel = ds.Tables[0].Rows[0]["UserLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsLock"] != null && ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"] != null && ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginTime"] != null && ds.Tables[0].Rows[0]["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LoginTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZGD.Model.Admin GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,UserName,UserPwd,Address,Tel,UserEmail,UserType,UserLevel,IsLock,AddTime,LoginTime from Admin ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;

            ZGD.Model.Admin model = new ZGD.Model.Admin();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPwd"] != null && ds.Tables[0].Rows[0]["UserPwd"].ToString() != "")
                {
                    model.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserEmail"] != null && ds.Tables[0].Rows[0]["UserEmail"].ToString() != "")
                {
                    model.UserEmail = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserType"] != null && ds.Tables[0].Rows[0]["UserType"].ToString() != "")
                {
                    model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserLevel"] != null && ds.Tables[0].Rows[0]["UserLevel"].ToString() != "")
                {
                    model.UserLevel = ds.Tables[0].Rows[0]["UserLevel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsLock"] != null && ds.Tables[0].Rows[0]["IsLock"].ToString() != "")
                {
                    model.IsLock = int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"] != null && ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginTime"] != null && ds.Tables[0].Rows[0]["LoginTime"].ToString() != "")
                {
                    model.LoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LoginTime"].ToString());
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
            strSql.Append("select Id,UserName,UserPwd,Address,Tel,UserEmail,UserType,UserLevel,IsLock,AddTime,LoginTime ");
            strSql.Append(" FROM Admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Id desc");
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
            strSql.Append(" Id,UserName,UserPwd,Address,Tel,UserEmail,UserType,UserLevel,IsLock,AddTime,LoginTime ");
            strSql.Append(" FROM Admin ");
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
            parameters[0].Value = "Admin";
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