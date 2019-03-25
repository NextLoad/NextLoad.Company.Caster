using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static int ExecuteScalar(string sqlText,)
    }
}
