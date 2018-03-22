using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ZGD.Common;
using ZGD.DBUtility;

namespace ZGD.DAL
{
    public class wechat
    {
        #region 基本方法
        public string databaseprefix; //数据库表名前缀
        public wechat(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId(string table)
        {
            return DbHelperSQL.GetMaxID("id", databaseprefix + table);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string table, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + table);
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string table, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + table);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        #endregion

        #region 绑定
        private string bind_table = "wechat_bind";
        public int bind_Edit(Model.wechat_bind model)
        {
            bool flag = false;
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select  top 1 * from " + databaseprefix + bind_table);
            DataSet ds = DbHelperSQL.Query(strSql2.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flag = true;
                }
            }

            if (flag)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update " + databaseprefix + bind_table + " set ");
                strSql.Append("wechat_no=@wechat_no,wechat_name=@wechat_name,wechat_logo=@wechat_logo,wechat_account=@wechat_account,wechat_pwd=@wechat_pwd,wechat_check=@wechat_check,appid=@appid,appsecret=@appsecret,token=@token,wechat_url=@wechat_url ");
                //strSql.Append(" where id=" + model.id);

                SqlParameter[] parameters = {
					            new SqlParameter("@wechat_no", SqlDbType.VarChar,50),
					            new SqlParameter("@wechat_name", SqlDbType.VarChar,50),
                                new SqlParameter("@wechat_logo", SqlDbType.VarChar,250),
                                new SqlParameter("@wechat_account", SqlDbType.VarChar,50),
                                new SqlParameter("@wechat_pwd", SqlDbType.VarChar,50),
                                new SqlParameter("@wechat_check", SqlDbType.Int,4),
                                new SqlParameter("@appid", SqlDbType.VarChar,50),
                                new SqlParameter("@appsecret", SqlDbType.VarChar,200),
                                new SqlParameter("@token", SqlDbType.VarChar,50),
                                new SqlParameter("@wechat_url", SqlDbType.VarChar,250)
				};
                parameters[0].Value = model.wechat_no;
                parameters[1].Value = model.wechat_name;
                parameters[2].Value = model.wechat_logo;
                parameters[3].Value = model.wechat_account;
                parameters[4].Value = model.wechat_pwd;
                parameters[5].Value = model.wechat_check;
                parameters[6].Value = model.appid;
                parameters[7].Value = model.appsecret;
                parameters[8].Value = model.token;
                parameters[9].Value = model.Wechat_url;
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into " + databaseprefix + bind_table + " (");
                strSql.Append("wechat_no,wechat_name,wechat_logo,wechat_account,wechat_pwd,wechat_check,appid,appsecret,token,wechat_url)");
                strSql.Append(" values (");
                strSql.Append("@wechat_no,@wechat_name,@wechat_logo,@wechat_account,@wechat_pwd,@wechat_check,@appid,@appsecret,@token,@wechat_url);SELECT SCOPE_IDENTITY();");

                SqlParameter[] parameters = {
					new SqlParameter("@wechat_no", SqlDbType.VarChar,50),
					new SqlParameter("@wechat_name", SqlDbType.VarChar,50),
                    new SqlParameter("@wechat_logo", SqlDbType.VarChar,250),
                    new SqlParameter("@wechat_account", SqlDbType.VarChar,50),
                    new SqlParameter("@wechat_pwd", SqlDbType.VarChar,50),
                    new SqlParameter("@wechat_check", SqlDbType.Int,4),
                    new SqlParameter("@appid", SqlDbType.VarChar,50),
                    new SqlParameter("@appsecret", SqlDbType.VarChar,200),
                    new SqlParameter("@token", SqlDbType.VarChar,50),
                    new SqlParameter("@wechat_url", SqlDbType.VarChar,250)
				};
                parameters[0].Value = model.wechat_no;
                parameters[1].Value = model.wechat_name;
                parameters[2].Value = model.wechat_logo;
                parameters[3].Value = model.wechat_account;
                parameters[4].Value = model.wechat_pwd;
                parameters[5].Value = model.wechat_check;
                parameters[6].Value = model.appid;
                parameters[7].Value = model.appsecret;
                parameters[8].Value = model.token;
                parameters[9].Value = model.Wechat_url;

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

        }
        public Model.wechat_bind bind_Model()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + bind_table);


            Model.wechat_bind model = new Model.wechat_bind();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }

                model.wechat_no = ds.Tables[0].Rows[0]["wechat_no"].ToString();
                model.wechat_name = ds.Tables[0].Rows[0]["wechat_name"].ToString();
                model.wechat_logo = ds.Tables[0].Rows[0]["wechat_logo"].ToString();
                model.wechat_account = ds.Tables[0].Rows[0]["wechat_account"].ToString();
                model.wechat_pwd = ds.Tables[0].Rows[0]["wechat_pwd"].ToString();
                model.appid = ds.Tables[0].Rows[0]["appid"].ToString();
                model.appsecret = ds.Tables[0].Rows[0]["appsecret"].ToString();
                model.token = ds.Tables[0].Rows[0]["token"].ToString();
                model.Wechat_url = ds.Tables[0].Rows[0]["Wechat_url"].ToString();
                if (ds.Tables[0].Rows[0]["wechat_check"].ToString() != "")
                {
                    model.wechat_check = int.Parse(ds.Tables[0].Rows[0]["wechat_check"].ToString());
                }


                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 素材
        private string fodder_table = "wechat_fodder";
        public bool fodder_Exists(int id)
        {
            return Exists(fodder_table, id);
        }
        /// <summary>
        /// 增加一条数据及其子表
        /// </summary>
        public int fodder_Add(Model.wechat_fodder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + fodder_table + " (");
            strSql.Append("fodder_xml,fodder_type)");
            strSql.Append(" values (");
            strSql.Append("@fodder_xml,@fodder_type); SELECT SCOPE_IDENTITY();");

            SqlParameter[] parameters = {
				new SqlParameter("@fodder_xml", SqlDbType.VarChar),
				new SqlParameter("@fodder_type", SqlDbType.VarChar)
			};
            parameters[0].Value = model.fodder_xml;
            parameters[1].Value = model.fodder_type;
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
        public int fodder_Update(int id, string xml)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + fodder_table + " set fodder_xml=@xml");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@xml", SqlDbType.NVarChar),
					new SqlParameter("@id", SqlDbType.Int)
            };
            parameters[0].Value = xml;
            parameters[1].Value = id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        public DataSet fodder_Query(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + fodder_table + " where fodder_type like '%" + type + "%'");
            strSql.Append(" order by addtime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet fodder_Query()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + fodder_table + " ");
            strSql.Append(" order by addtime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet fodder_Query(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + fodder_table + " where id=" + id);
            strSql.Append(" order by addtime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public int fodder_Delete(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int)
			};
            parameters[0].Value = id;
            return DbHelperSQL.ExecuteSql("delete from " + databaseprefix + fodder_table + "  where id=@id", parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wechat_fodder fodder_model(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + fodder_table);
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.wechat_fodder model = new Model.wechat_fodder();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.fodder_type = ds.Tables[0].Rows[0]["fodder_type"].ToString();
                model.fodder_xml = ds.Tables[0].Rows[0]["fodder_xml"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 回复
        private string reply_table = "wechat_reply";
        public bool reply_Exists(int id)
        {
            return Exists(reply_table, id);
        }
        public bool reply_Exists(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + reply_table);
            strSql.Append(" where reply_type=@type ");
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.VarChar,50)};
            parameters[0].Value = type;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public bool reply_Exists(string type, string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + reply_table);
            strSql.Append(" where reply_type=@type and reply_key=@key");
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@key", SqlDbType.VarChar,50)};
            parameters[0].Value = type;
            parameters[1].Value = key;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public bool reply_Exists(int id, string type, string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + reply_table);
            strSql.Append(" where reply_type=@type and reply_key=@key and id<>@id");
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@key", SqlDbType.VarChar,50),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = type;
            parameters[1].Value = key;
            parameters[2].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public int reply_Add(Model.wechat_reply model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + reply_table + " (");
            strSql.Append("reply_type,reply_key,reply_fodder_id)");
            strSql.Append(" values (");
            strSql.Append("@reply_type,@reply_key,@reply_fodder_id); SELECT SCOPE_IDENTITY();");

            SqlParameter[] parameters = {
				new SqlParameter("@reply_type", SqlDbType.VarChar,50),
				new SqlParameter("@reply_key", SqlDbType.VarChar,50),
                new SqlParameter("@reply_fodder_id", SqlDbType.Int,4)
			};
            parameters[0].Value = model.reply_type;
            parameters[1].Value = model.reply_key;
            parameters[2].Value = model.reply_fodder_id;

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
        public int reply_Update(int id, int fodder_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + reply_table + " set reply_fodder_id=" + fodder_id);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        public int reply_Update(int id, int fodder_id, string key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + reply_table + " set reply_fodder_id=" + fodder_id + ",reply_key='" + key + "'");
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet reply_Query(int pIdx, int pageSize, string strWhere, string sortName, out int rowCount, out int pageCount)
        {
            pageCount = 0;
            rowCount = 0;
            Pager pager = new Pager();
            pager.tableName = databaseprefix + reply_table;
            pager.fieldsName = " * ";
            pager.sqlWhere = strWhere;
            pager.orderField = sortName;
            return pager.GetDataPage(pIdx, pageSize, out rowCount, out pageCount);
        }

        public DataSet reply_Query(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + reply_table + " where reply_type='" + type + "'");
            strSql.Append(" order by addtime desc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public int reply_Delete(int id)
        {
            return DbHelperSQL.ExecuteSql("delete from " + databaseprefix + reply_table + "  where id=" + id);
        }
        public Model.wechat_reply reply_model(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + reply_table);
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.wechat_reply model = new Model.wechat_reply();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.reply_type = ds.Tables[0].Rows[0]["reply_type"].ToString();
                model.reply_key = ds.Tables[0].Rows[0]["reply_key"].ToString();
                model.reply_fodder_id = ds.Tables[0].Rows[0]["reply_fodder_id"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public Model.wechat_reply reply_model(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + reply_table);
            strSql.Append(" where reply_type=@type ");
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.VarChar,50)};
            parameters[0].Value = type;

            Model.wechat_reply model = new Model.wechat_reply();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.reply_type = ds.Tables[0].Rows[0]["reply_type"].ToString();
                model.reply_key = ds.Tables[0].Rows[0]["reply_key"].ToString();
                model.reply_fodder_id = ds.Tables[0].Rows[0]["reply_fodder_id"].ToString();
                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 菜单
        private string menu_table = "wechat_menu";
        public DataTable menu_list(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + databaseprefix + menu_table);
            strSql.Append(" order by menu_sort asc,id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            menu_childs(oldData, newData, parent_id);
            return newData;
        }
        public DataTable menu_list()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + databaseprefix + menu_table);
            strSql.Append(" where menu_parent_id=0 order by menu_sort asc,id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            return oldData;
        }
        private void menu_childs(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("menu_parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["menu_name"] = dr[i]["menu_name"].ToString();
                row["menu_key"] = dr[i]["menu_key"].ToString();
                row["menu_parent_id"] = int.Parse(dr[i]["menu_parent_id"].ToString());
                row["menu_sort"] = int.Parse(dr[i]["menu_sort"].ToString());
                row["menu_content_id"] = int.Parse(dr[i]["menu_content_id"].ToString());
                row["menu_type"] = dr[i]["menu_type"].ToString();
                row["menu_url"] = dr[i]["menu_url"].ToString();
                row["menu_is_show"] = int.Parse(dr[i]["menu_is_show"].ToString());
                row["addtime"] = dr[i]["addtime"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                this.menu_childs(oldData, newData, int.Parse(dr[i]["id"].ToString()));
            }
        }
        public int menu_add(Model.wechat_menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + menu_table + " (");
            strSql.Append("menu_name,menu_key,menu_parent_id,menu_sort,menu_content_id,menu_type,menu_url,menu_is_show)");
            strSql.Append(" values (");
            strSql.Append("@menu_name,@menu_key,@menu_parent_id,@menu_sort,@menu_content_id,@menu_type,@menu_url,@menu_is_show);SELECT SCOPE_IDENTITY();");

            SqlParameter[] parameters = {
			    new SqlParameter("@menu_name", SqlDbType.VarChar,50),
			    new SqlParameter("@menu_key", SqlDbType.VarChar,50),
                new SqlParameter("@menu_parent_id", SqlDbType.Int,4),
                new SqlParameter("@menu_sort", SqlDbType.Int,4),
                new SqlParameter("@menu_content_id", SqlDbType.Int,4),
                new SqlParameter("@menu_type", SqlDbType.VarChar,50),
                new SqlParameter("@menu_url", SqlDbType.VarChar,250),
                new SqlParameter("@menu_is_show", SqlDbType.Int,4)
			};
            parameters[0].Value = model.menu_name;
            parameters[1].Value = model.menu_key;
            parameters[2].Value = model.menu_parent_id;
            parameters[3].Value = model.menu_sort;
            parameters[4].Value = model.menu_content_id;
            parameters[5].Value = model.menu_type;
            parameters[6].Value = model.menu_url;
            parameters[7].Value = model.menu_is_show;

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
        public int menu_update(Model.wechat_menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  " + databaseprefix + menu_table + " set ");
            strSql.Append("menu_name=@menu_name,menu_key=@menu_key,menu_parent_id=@menu_parent_id,menu_sort=@menu_sort,menu_content_id=@menu_content_id,menu_type=@menu_type,menu_url=@menu_url,menu_is_show=@menu_is_show ");
            strSql.Append(" where id=@id");

            SqlParameter[] parameters = {
			    new SqlParameter("@menu_name", SqlDbType.VarChar,50),
			    new SqlParameter("@menu_key", SqlDbType.VarChar,50),
                new SqlParameter("@menu_parent_id", SqlDbType.Int,4),
                new SqlParameter("@menu_sort", SqlDbType.Int,4),
                new SqlParameter("@menu_content_id", SqlDbType.Int,4),
                new SqlParameter("@menu_type", SqlDbType.VarChar,50),
                new SqlParameter("@menu_url", SqlDbType.VarChar,250),
                new SqlParameter("@menu_is_show", SqlDbType.Int,4),
                new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = model.menu_name;
            parameters[1].Value = model.menu_key;
            parameters[2].Value = model.menu_parent_id;
            parameters[3].Value = model.menu_sort;
            parameters[4].Value = model.menu_content_id;
            parameters[5].Value = model.menu_type;
            parameters[6].Value = model.menu_url;
            parameters[7].Value = model.menu_is_show;
            parameters[8].Value = model.id;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        public bool menu_update_fild(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + menu_table + " set " + strValue);
            strSql.Append(" where id=" + id);
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
        public int menu_Delete(int id)
        {
            return DbHelperSQL.ExecuteSql("delete from " + databaseprefix + menu_table + "  where menu_parent_id=" + id + " or id=" + id);
        }
        public int menu_count(int id)
        {
            int num = 0;
            DataSet nds = DbHelperSQL.Query("select * from " + databaseprefix + menu_table + "  where menu_parent_id=" + id + " or id=" + id);
            if (nds.Tables.Count > 0)
            {
                DataTable ndt = nds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    num = ndt.Rows.Count;
                }
            }
            return num;
        }
        public Model.wechat_menu menu_model(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from " + databaseprefix + menu_table);
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.wechat_menu model = new Model.wechat_menu();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.menu_name = ds.Tables[0].Rows[0]["menu_name"].ToString();
                model.menu_key = ds.Tables[0].Rows[0]["menu_key"].ToString();
                model.menu_type = ds.Tables[0].Rows[0]["menu_type"].ToString();
                model.menu_url = ds.Tables[0].Rows[0]["menu_url"].ToString();

                if (ds.Tables[0].Rows[0]["menu_parent_id"].ToString() != "")
                {
                    model.menu_parent_id = int.Parse(ds.Tables[0].Rows[0]["menu_parent_id"].ToString());
                }

                if (ds.Tables[0].Rows[0]["menu_sort"].ToString() != "")
                {
                    model.menu_sort = int.Parse(ds.Tables[0].Rows[0]["menu_sort"].ToString());
                }

                if (ds.Tables[0].Rows[0]["menu_content_id"].ToString() != "")
                {
                    model.menu_content_id = int.Parse(ds.Tables[0].Rows[0]["menu_content_id"].ToString());
                }

                if (ds.Tables[0].Rows[0]["menu_is_show"].ToString() != "")
                {
                    model.menu_is_show = int.Parse(ds.Tables[0].Rows[0]["menu_is_show"].ToString());
                }

                if (ds.Tables[0].Rows[0]["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(ds.Tables[0].Rows[0]["addtime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        public List<Model.wechat_menu> menu_list(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM " + databaseprefix + menu_table);
            strSql.Append(" where menu_is_show=1 " + where + "  order by menu_sort asc,id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                DataTable ndt = ds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    List<Model.wechat_menu> list = new List<Model.wechat_menu>();
                    for (int i = 0; i < ndt.Rows.Count; i++)
                    {
                        Model.wechat_menu model = new Model.wechat_menu();

                        if (ds.Tables[0].Rows[i]["id"].ToString() != "")
                        {
                            model.id = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                        }
                        model.menu_name = ds.Tables[0].Rows[i]["menu_name"].ToString();
                        model.menu_key = ds.Tables[0].Rows[i]["menu_key"].ToString();
                        model.menu_type = ds.Tables[0].Rows[i]["menu_type"].ToString();
                        model.menu_url = ds.Tables[0].Rows[i]["menu_url"].ToString();
                        list.Add(model);
                    }
                    return list;
                }
            }
            return new List<Model.wechat_menu>();
        }

        #endregion

        #region 视图
        public string[] view_wechat_reply_fodder(string type, string key)
        {
            string[] str = new string[2] { "", "" };
            string sql = "select top 1 wr.reply_type,wr.reply_key,wr.reply_fodder_id,wf.fodder_type,wf.fodder_xml from dt_wechat_reply wr inner join dt_wechat_fodder wf on wr.reply_fodder_id=wf.id where wr.reply_type='" + type + "' ";
            if (key != "")
            {
                sql += " and wr.reply_key='" + key + "'";
            }
            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables.Count > 0)
            {
                DataTable ndt = ds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    str[0] = ndt.Rows[0]["fodder_type"].ToString();
                    str[1] = ndt.Rows[0]["fodder_xml"].ToString();
                }
            }
            return str;
        }
        public string[] view_wechat_menu_fodder(string key)
        {
            string[] str = new string[2] { "", "" };
            string sql = "select top 1 wm.menu_key,wf.fodder_type,wf.fodder_xml from dt_wechat_menu wm inner join dt_wechat_fodder wf on wm.menu_content_id=wf.id where menu_key='" + key + "' ";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables.Count > 0)
            {
                DataTable ndt = ds.Tables[0];
                if (ndt.Rows.Count > 0)
                {
                    str[0] = ndt.Rows[0]["fodder_type"].ToString();
                    str[1] = ndt.Rows[0]["fodder_xml"].ToString();
                }
            }
            return str;
        }
        #endregion

        public int write_log(string str)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "wechat_log (pxml) values (@pxml);SELECT SCOPE_IDENTITY();");
            SqlParameter[] parameters = {
					new SqlParameter("@pxml", SqlDbType.VarChar)
			};
            parameters[0].Value = str;
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
        public DataSet log_list()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * FROM " + databaseprefix + "wechat_log");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds;
        }
        public void clearLog()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete  FROM " + databaseprefix + "wechat_log");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
        }

    }
}
