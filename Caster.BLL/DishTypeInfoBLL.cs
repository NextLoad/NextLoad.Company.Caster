using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class DishTypeInfoBLL
    {
        private DishTypeInfoDAL dtiDal;
        public DishTypeInfoBLL()
        {
            this.dtiDal = new DishTypeInfoDAL();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<DishTypeInfo> GetList()
        {
            return dtiDal.GetDishTypeInfos();
        }
        /// <summary>
        /// 根据名称获取类型id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int? GetTypeId(string name)
        {
            object obj = dtiDal.GetTypeId(name);
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 表示添加数据是否成功
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public bool Add(DishTypeInfo dti)
        {
            return dtiDal.Insert(dti) > 0;
        }
        /// <summary>
        /// 表示修改数据是否成功
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public bool Edit(DishTypeInfo dti)
        {
            return dtiDal.Update(dti) > 0;
        }
        /// <summary>
        /// 表示删除数据是否成功
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Remove(int Id)
        {
            return dtiDal.Delete(Id) > 0;
        }
    }
}
