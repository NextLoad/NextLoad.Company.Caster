using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class HallInfoBLL
    {
        private HallInfoDAL hiDal;
        public HallInfoBLL()
        {
            hiDal = new HallInfoDAL();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<HallInfo> GetList()
        {
            return hiDal.GetHallInfos();
        }
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public bool Add(HallInfo hi)
        {
            return hiDal.Insert(hi) > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public bool Edit(HallInfo hi)
        {
            return hiDal.Update(hi) > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            return hiDal.Delete(id) > 0;
        }
    }
}
