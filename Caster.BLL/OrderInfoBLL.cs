using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class OrderInfoBLL
    {
        private OrderInfoDAL odDal;

        public OrderInfoBLL()
        {
            this.odDal = new OrderInfoDAL();
        }

        public OrderInfo GetOrderInfo(int orderId)
        {
            return odDal.GetOrderInfo(orderId);
        }
        public int GetOrderId(int tableId)
        {
            return odDal.GetOrderID(tableId);
        }
        public bool Order(TableInfo ti,OrderInfo oi, List<OrderDetailInfo> odis)
        {
            if (ti.TIsFree)//如果餐桌是空闲状态
            {
                return odDal.Insert(oi, odis) > 0;
            }
            else
            {
                return true;
            }
            
        }

        public bool PayOrder(OrderInfo oi,bool isUseBalance)
        {
            return odDal.PayOrder(oi,isUseBalance) > 0;
        }
    }
}
