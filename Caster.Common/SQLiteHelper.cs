using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caster.Common
{
    public static class SQLiteHelper
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SqliteConn"].ConnectionString;
        }

        /// <summary>
        /// 执行SQL语句，返回第一行第一列的数据
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlText, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sqlText, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sqlText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回一个datatable
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlText, params SQLiteParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlText, conn);
                adapter.SelectCommand.Parameters.AddRange(parameters);
                adapter.Fill(dt);
                return dt;
            }
        }

    }
}
