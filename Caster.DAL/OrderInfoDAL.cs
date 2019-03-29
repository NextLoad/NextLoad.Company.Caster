using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Caster.Common;
using Caster.Model;

namespace Caster.DAL
{
    public class OrderInfoDAL
    {
        public OrderInfoDAL()
        { }
        /// <summary>
        /// 获取订单ID
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetOrderID(int tableId)
        {
            string sqlText = "select OId from OrderInfo where tableid = @tableId and IsPay = 0";
            SQLiteParameter parameter = new SQLiteParameter("@tableId", tableId);
            return Convert.ToInt32(SQLiteHelper.ExecuteScalar(sqlText, parameter));
        }
        /// <summary>
        /// 插入订单信息
        /// </summary>
        /// <param name="oi"></param>
        /// <returns></returns>
        public int Insert(OrderInfo oi, List<OrderDetailInfo> odis)
        {
            string sqlText = "Insert into OrderInfo(ODate,IsPay,OMoney,TableId) values(datetime('now'),0,@Money,@tableId);" +
                             "select last_insert_rowid();" +
                             "Update TableInfo set TIsFree = 0 where Tid = @tableId";
            SQLiteParameter[] parameter =
            {
                new SQLiteParameter("@tableId", oi.TableId),
                new SQLiteParameter("@Money", oi.OMoney),

            };
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqliteConn"].ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.CommandText = sqlText;
                        cmd.Parameters.AddRange(parameter);
                        int Oid = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.CommandText = "Insert into OrderDetailInfo(OrderId,DishId,count,OdishID) values(@OrderId,@dishId,@count,@OdishId)";
                        foreach (OrderDetailInfo odi in odis)
                        {
                            cmd.Parameters.Clear();
                            SQLiteParameter[] parameters =
                            {
                                new SQLiteParameter("@OrderId", Oid),
                                new SQLiteParameter("@dishId", odi.DishId),
                                new SQLiteParameter("@count", odi.Count),
                                new SQLiteParameter("@OdishID", odi.ODishId),
                            };
                            cmd.Parameters.AddRange(parameters);
                            cmd.ExecuteNonQuery();
                        }
                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        return 0;
                    }
                }
            }

            return 1;
        }

        public int PayOrder(OrderInfo oi, bool isUseBalance)
        {
            //1、将餐桌置为空闲状态
            //2、根据是否使用余额付款进行结账
            string sqltext = "update OrderInfo set Ispay = 1 where OId= @oid;" +
                             "update TableInfo set TIsfree = 1 where TId = @tId;";
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            if (isUseBalance)
            {
                sqltext += "update OrderInfo set MemberId = @memberId,Discount = @discount where OId= @oid;" +
                   "update MemberInfo set MMoney = MMoney - @money where MId = @memberId";
                parameters.Add(new SQLiteParameter("@money", oi.OMoney));
                parameters.Add(new SQLiteParameter("@memberId", oi.MemberId));
                parameters.Add(new SQLiteParameter("@discount", oi.Discount));
            }
            parameters.Add(new SQLiteParameter("@oid", oi.OId));
            parameters.Add(new SQLiteParameter("@tId", oi.TableId));
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SqliteConn"].ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.CommandText = sqltext;
                        cmd.Parameters.AddRange(parameters.ToArray());
                        cmd.ExecuteNonQuery();
                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        return 0;
                    }
                }
            }

            return 1;
        }
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfo(int orderId)
        {
            OrderInfo oi = null;
            string sqltext = "select OId,MemberId,ODate,OMoney,IsPay,TableId,Discount from OrderInfo where OId = @oid";
            SQLiteParameter parameter = new SQLiteParameter("@oid", orderId);
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqltext, parameter);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return oi;
            }
            oi = new OrderInfo();
            oi.OId = Convert.ToInt32(dt.Rows[0][0]);
            oi.MemberId = dt.Rows[0][1] == DBNull.Value ? (int?)null : Convert.ToInt32(dt.Rows[0][1]);
            oi.ODate = Convert.ToDateTime(dt.Rows[0][2]);
            oi.OMoney = Convert.ToDecimal(dt.Rows[0][3]);
            oi.IsPay = Convert.ToBoolean(dt.Rows[0][4]);
            oi.TableId = Convert.ToInt32(dt.Rows[0][5]);
            oi.Discount = dt.Rows[0][6] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dt.Rows[0][6]);
            return oi;
        }
    }
}
