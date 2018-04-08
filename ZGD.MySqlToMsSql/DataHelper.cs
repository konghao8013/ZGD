using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ZGD.MySqlToMsSql
{
    public class DataHelper
    {

        public static MySqlConnection CreateMySql()
        {
            var connString = ConfigurationManager.AppSettings["MySql"].ToString();
            var conn = new MySqlConnection(connString);
            conn.Open();
            return conn;
        }

        public static SqlConnection Create()
        {
            var connString = ConfigurationManager.AppSettings["MsSql"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// sql执行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Execute(string sql, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    int msg = conn.Execute(sql);
                    conn.Close();
                    return msg;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    int msg = conn.Execute(sql);
                    conn.Close();
                    return msg;
                }
            }
        }

        /// <summary>
        /// sql执行 返回第一行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    int msg = conn.Execute(sql);
                    conn.Close();
                    return msg;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    int msg = conn.Execute(sql);
                    conn.Close();
                    return msg;
                }
            }
        }

        /// <summary>
        /// sql执行 返回第一行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    var obj = conn.ExecuteScalar(sql);
                    conn.Close();
                    return obj;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    var obj = conn.ExecuteScalar(sql);
                    conn.Close();
                    return obj;
                }
            }
        }

        /// <summary>
        /// sql执行 返回第一行
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    var obj = conn.ExecuteScalar(sql, param);
                    conn.Close();
                    return obj;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    var obj = conn.ExecuteScalar(sql, param);
                    conn.Close();
                    return obj;
                }
            }
        }

        /// <summary>
        /// 返回集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    var data = conn.Query<T>(sql);
                    conn.Close();
                    return data != null ? data.ToList() : null;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    var data = conn.Query<T>(sql);
                    conn.Close();
                    return data != null ? data.ToList() : null;
                }
            }
        }

        /// <summary>
        /// 返回集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, object param, bool isMySql = false)
        {
            if (isMySql)
            {
                using (var conn = CreateMySql())
                {
                    var data = conn.Query<T>(sql, param);
                    conn.Close();
                    return data != null ? data.ToList() : null;
                }
            }
            else
            {
                using (var conn = Create())
                {
                    var data = conn.Query<T>(sql, param);
                    conn.Close();
                    return data != null ? data.ToList() : null;
                }
            }
        }
    }
}
