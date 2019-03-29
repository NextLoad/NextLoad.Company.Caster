using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class OrderDetailInfoBLL
    {
        private OrderDetialInfoDAL odiDal;

        public OrderDetailInfoBLL()
        {
            this.odiDal = new OrderDetialInfoDAL();
        }

        public List<OrderDetailInfo> GetList(Dictionary<string, string> dic)
        {
            return odiDal.GetDetailInfos(dic);
        }

        public Decimal GetPayMoney(int orderId)
        {
            return odiDal.GetPayMoney(orderId);
        }
    }
}
