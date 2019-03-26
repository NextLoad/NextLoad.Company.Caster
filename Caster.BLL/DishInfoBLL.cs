using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class DishInfoBLL
    {
        private DishInfoDAL diDal;

        public DishInfoBLL()
        {
            this.diDal = new DishInfoDAL();
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public List<DishInfo> GetList(Dictionary<string,string> dic)
        {
            return diDal.GetDishInfos(dic);
        }
        /// <summary>
        /// 表示插入数据是否成功
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public bool Add(DishInfo di)
        {
            return diDal.Insert(di) > 0;
        }
        /// <summary>
        /// 表示修改数据是否成功
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public bool Edit(DishInfo di)
        {
            return diDal.Update(di) > 0;
        }
        /// <summary>
        /// 表示删除数据是否成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            return diDal.Delete(id) > 0;
        }
    }
}
