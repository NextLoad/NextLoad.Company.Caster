using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class TableInfoBLL
    {
        private TableInfoDAL tiDal;

        public TableInfoBLL()
        {
            this.tiDal = new TableInfoDAL();
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            return tiDal.GeTableInfos(dic);
        }
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public bool Add(TableInfo ti)
        {
            return tiDal.Insert(ti) > 0;
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public bool Edit(TableInfo ti)
        {
            return tiDal.Update(ti) > 0;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            return tiDal.Delete(id) > 0;
        }
    }
}
