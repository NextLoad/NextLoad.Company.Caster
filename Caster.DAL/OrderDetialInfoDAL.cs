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
    public class OrderDetialInfoDAL
    {
        public OrderDetialInfoDAL() { }
        /// <summary>
        /// 获取订单详细数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> GetDetailInfos(Dictionary<string, string> dic)
        {
            List<OrderDetailInfo> list = new List<OrderDetailInfo>();
            string sqlText =
                "select odi.OId,odi.OrderId,odi.DishId,odi.Count,odi.ODishId,di.Dtitle,di.DPrice from orderdetailinfo as odi,dishinfo as di where odi.DishId = di.DId";
            string sqlWhere = string.Empty;
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                sqlWhere += " and " + keyValuePair.Key + " = @" + keyValuePair.Key;
                parameters.Add(new SQLiteParameter("@" + keyValuePair.Key, keyValuePair.Value));
            }

            sqlText += sqlWhere;
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqlText, parameters.ToArray());
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(dataRow[0]),
                    OrderId = Convert.ToInt32(dataRow[1]),
                    DishId = Convert.ToInt32(dataRow[2]),
                    Count = Convert.ToInt32(dataRow[3]),
                    ODishId = Convert.ToInt32(dataRow[4]),
                    ODTitle = dataRow[5].ToString(),
                    ODPrice = Convert.ToDecimal(dataRow[6])

                });
            }

            return list;
        }
        /// <summary>
        /// 获取订单金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal GetPayMoney(int orderId)
        {
            string sqlText =
                "select sum(odi.count * di.Dprice) from OrderDetailInfo as odi,DishInfo as di where odi.OrderId = @orderId and odi.DishId = di.DId";
            SQLiteParameter parameter = new SQLiteParameter("@orderId", orderId);
            return Convert.ToDecimal(SQLiteHelper.ExecuteScalar(sqlText, parameter));
        }
    }
}
