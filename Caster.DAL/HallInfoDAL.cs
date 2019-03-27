using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Caster.Common;
using Caster.Model;

namespace Caster.DAL
{
    public class HallInfoDAL
    {
        public HallInfoDAL() { }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public List<HallInfo> GetHallInfos()
        {
            List<HallInfo> list = new List<HallInfo>();
            string sqltext = "select HId,HTitle,HIsDelete from HallInfo where HIsDelete = 0";
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqltext);
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId = Convert.ToInt32(dataRow[0]),
                    HTitle = dataRow[1].ToString(),
                    HIsDelete = Convert.ToBoolean(dataRow[2])
                });
            }

            return list;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Insert(HallInfo hi)
        {
            string sqltext = "insert into HallInfo (HTitle,HIsDelete) values (@title,0)";
            SQLiteParameter parameter = new SQLiteParameter("@title", hi.HTitle);
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Update(HallInfo hi)
        {
            string sqltext = "update HallInfo set HTitle = @title where HId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", hi.HTitle),
                new SQLiteParameter("@Id", hi.HId)
            };
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sqltext = "update HallInfo set HIsDelete = @isDelete where HId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@isDelete", 1),
                new SQLiteParameter("@Id", id)
            };
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }

    }
}
