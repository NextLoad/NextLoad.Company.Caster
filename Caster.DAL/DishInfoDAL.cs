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
    public class DishInfoDAL
    {
        public DishInfoDAL() { }
        /// <summary>
        /// 获取菜单信息列表
        /// </summary>
        /// <returns></returns>
        public List<DishInfo> GetDishInfos(Dictionary<string, string> dic)
        {
            List<DishInfo> list = new List<DishInfo>();
            string sqltext = "select di.DId,di.DTitle,di.DTypeId,di.DPrice,di.DChar,di.DIsDelete,dti.DTitle as DTypeTitle from DishInfo as di,DishTypeInfo as dti where di.DTypeId = dti.DId and di.DIsDelete = 0";
            string sqlWhere = string.Empty;
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                sqlWhere += " and di." + keyValuePair.Key + " like @" + keyValuePair.Key;
                parameters.Add(new SQLiteParameter("@" + keyValuePair.Key, "%" + keyValuePair.Value + "%"));
            }

            sqltext += sqlWhere;
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqltext, parameters.ToArray());
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId = Convert.ToInt32(dataRow[0]),
                    DTitle = dataRow[1].ToString(),
                    DTypeId = Convert.ToInt32(dataRow[2]),
                    DPrice = Convert.ToDecimal(dataRow[3]),
                    DChar = dataRow[4].ToString(),
                    DIsDelete = Convert.ToBoolean(dataRow[5]),
                    DTypeTitle = dataRow[6].ToString()
                });
            }

            return list;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public int Insert(DishInfo di)
        {
            string sqlText = "insert into DishInfo (DTitle,DTypeId,DPrice,DChar,DIsDelete) values (@title,@typeId,@price,@char,0)";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@typeId", DbType.Int32),
                new SQLiteParameter("@price", DbType.Decimal),
                new SQLiteParameter("@char", DbType.String),
            };
            parameters[0].Value = di.DTitle;
            parameters[1].Value = di.DTypeId;
            parameters[2].Value = di.DPrice;
            parameters[3].Value = di.DChar;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public int Update(DishInfo di)
        {
            string sqltext =
                "update DishInfo set DTitle = @title,DTypeId = @typeId,DPrice=@price,DChar=@char where DId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@typeId", DbType.Int32),
                new SQLiteParameter("@price", DbType.Decimal),
                new SQLiteParameter("@char", DbType.String),
                new SQLiteParameter("@Id", DbType.Int32),
            };
            parameters[0].Value = di.DTitle;
            parameters[1].Value = di.DTypeId;
            parameters[2].Value = di.DPrice;
            parameters[3].Value = di.DChar;
            parameters[4].Value = di.DId;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sqltext = "update DishInfo set DisDelete = 1 where Did=@id";
            SQLiteParameter parameter = new SQLiteParameter("@id", id);
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameter);

        }
    }
}
