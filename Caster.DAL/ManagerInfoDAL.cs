using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.Common;
using Caster.Model;

namespace Caster.DAL
{
    public class ManagerInfoDAL
    {
        public ManagerInfoDAL() { }

        /// <summary>
        /// 获取ManagerInfo列表
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetManagerInfoList()
        {
            List<ManagerInfo> list = new List<ManagerInfo>();
            string sqlString = "select * from ManagerInfo";
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqlString);
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    MId = Convert.ToInt32(dataRow[0]),
                    MName = dataRow[1].ToString(),
                    MPwd = dataRow[2].ToString(),
                    MType = Convert.ToInt32(dataRow[3])
                });
            }

            return list;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Insert(ManagerInfo mi)
        {
            string sqlText = "insert into ManagerInfo(MName,MPwd,MType) values(@Name,@Pwd,@Type)";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@Name", DbType.String),
                new SQLiteParameter("@Pwd", DbType.String),
                new SQLiteParameter("@Type", DbType.Int32),
            };
            parameters[0].Value = mi.MName;
            parameters[1].Value = MD5Helper.Encrying(mi.MPwd);
            parameters[2].Value = mi.MType;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="isMd5Encrying"></param>
        /// <returns></returns>
        public int Update(ManagerInfo mi, bool isMd5Encrying)
        {
            string sqlText = "update ManagerInfo set MName = @Name,MPwd = @Pwd,MType = @Type where MId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@Name", DbType.String),
                new SQLiteParameter("@Pwd", DbType.String),
                new SQLiteParameter("@Type", DbType.Int32),
                new SQLiteParameter("@Id",DbType.Int32)
            };
            parameters[0].Value = mi.MName;
            if (isMd5Encrying)
            {
                parameters[1].Value = MD5Helper.Encrying(mi.MPwd);
            }
            else
            {
                parameters[1].Value = mi.MPwd;
            }

            parameters[2].Value = mi.MType;
            parameters[3].Value = mi.MId;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        public int Update(ManagerInfo mi)
        {
            string sqlText = "update ManagerInfo set MName = @Name,MType = @Type where MId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@Name", DbType.String),
                new SQLiteParameter("@Type", DbType.Int32),
                new SQLiteParameter("@Id",DbType.Int32)
            };
            parameters[0].Value = mi.MName;
            parameters[1].Value = mi.MType;
            parameters[2].Value = mi.MId;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sqltext = "delete from ManagerInfo where MId = @Id";
            SQLiteParameter parameter = new SQLiteParameter("@Id", Id);
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }
        /// <summary>
        /// 获取登录账号的密码
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ManagerInfo GetManagerInfo(string Name)
        {
            string sqlText = "select * from ManagerInfo where MName = @Name";
            SQLiteParameter parameter = new SQLiteParameter("@Name", Name);
            ManagerInfo mi = null;
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqlText, parameter);
            foreach (DataRow dataRow in dt.Rows)
            {
                mi = new ManagerInfo()
                {
                    MId = Convert.ToInt32(dataRow[0]),
                    MName = dataRow[1].ToString(),
                    MPwd = dataRow[2].ToString(),
                    MType = Convert.ToInt32(dataRow[3])
                };
            }

            return mi;
        }
    }
}
