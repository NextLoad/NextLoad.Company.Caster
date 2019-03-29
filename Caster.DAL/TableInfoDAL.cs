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
    public class TableInfoDAL
    {
        public TableInfoDAL() { }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GeTableInfos(Dictionary<string, string> dic)
        {
            List<TableInfo> list = new List<TableInfo>();
            string sqlText = "select TId,TTitle,THallId,TIsFree,TIsDelete,HTitle from TableInfo as ti, HallInfo as hi where ti.THallId = hi.HId and TIsDelete = 0";

            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            string sqlWhere = string.Empty;
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                sqlWhere += " and " + keyValuePair.Key + " = @" + keyValuePair.Key;
                parameters.Add(new SQLiteParameter("@" + keyValuePair.Key, keyValuePair.Value));
            }

            sqlText += sqlWhere;
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqlText, parameters.ToArray());
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId = Convert.ToInt32(dataRow[0]),
                    TTitle = dataRow[1].ToString(),
                    THallId = Convert.ToInt32(dataRow[2]),
                    TIsFree = Convert.ToBoolean(dataRow[3]),
                    TIsDelete = Convert.ToBoolean(dataRow[4]),
                    THTitle = dataRow[5].ToString()
                });
            }

            return list;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Insert(TableInfo ti)
        {
            string sqltext = "Insert into TableInfo(TTitle,THallId,TIsFree,TIsDelete) values(@title,@hallId,@isFree,0)";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@hallId", DbType.Int32),
                new SQLiteParameter("@isFree", DbType.Boolean),
            };
            parameters[0].Value = ti.TTitle;
            parameters[1].Value = ti.THallId;
            parameters[2].Value = ti.TIsFree;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Update(TableInfo ti)
        {
            string sqltext = "Update TableInfo set TTitle = @title,THallId = @hallId,TIsFree = @isFree where TId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@hallId", DbType.Int32),
                new SQLiteParameter("@isFree", DbType.Boolean),
                new SQLiteParameter("@Id",DbType.Int32),
            };
            parameters[0].Value = ti.TTitle;
            parameters[1].Value = ti.THallId;
            parameters[2].Value = ti.TIsFree;
            parameters[3].Value = ti.TId;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sqltext = "Update TableInfo set TIsDelete = 1 where TId = @Id";
            SQLiteParameter parameter = new SQLiteParameter("@Id", Id);
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }

    }
}
