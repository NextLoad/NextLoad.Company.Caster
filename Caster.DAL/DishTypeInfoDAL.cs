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
    public class DishTypeInfoDAL
    {
        public DishTypeInfoDAL()
        {
        }
        /// <summary>
        /// 获取DishTypeInfo数据
        /// </summary>
        /// <returns></returns>
        public List<DishTypeInfo> GetDishTypeInfos()
        {
            List<DishTypeInfo> list = new List<DishTypeInfo>();
            string sqlText = "select DId,DTitle,DIsDelete from DishTypeInfo where DIsDelete = 0";
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqlText);
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    DId = Convert.ToInt32(dataRow[0]),
                    DTitle = dataRow[1].ToString(),
                    DIsDelete = Convert.ToBoolean(dataRow[2])
                });
            }

            return list;
        }
        /// <summary>
        /// 获取类型id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetTypeId(string name)
        {
            string sqltext = "select DId from DishTypeInfo where DTitle = @title";
            SQLiteParameter parameter = new SQLiteParameter("@title", name);
            return SQLiteHelper.ExecuteScalar(sqltext, parameter);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Insert(DishTypeInfo dti)
        {
            string sqltext = "insert into DishTypeInfo (DTitle,DIsDelete) values(@title,0)";
            SQLiteParameter parameter = new SQLiteParameter("@title", dti.DTitle);
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Update(DishTypeInfo dti)
        {
            string sqltext = "update DishTypeInfo set DTitle=@title where DId = @id";
            SQLiteParameter[] parameter =
            {
                new SQLiteParameter("@title", dti.DTitle),
                new SQLiteParameter("@id",dti.DId)
            };
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sqltext = "update DishTypeInfo set DIsDelete=1 where DId = @id";
            SQLiteParameter[] parameter =
            {
                new SQLiteParameter("@id",id)
            };
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);
        }
    }
}
