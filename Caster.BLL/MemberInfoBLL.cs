using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class MemberInfoBLL
    {
        private MemberInfoDAL miDal;

        public MemberInfoBLL()
        {
            this.miDal = new MemberInfoDAL();
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public List<MemberInfo> GetList(Dictionary<string, string> dictionary)
        {
            return miDal.GetMemberInfos(dictionary);
        }
        /// <summary>
        /// 表示插入是否成功
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public bool Add(MemberInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }
        /// <summary>
        /// 表示更改是否成功
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public bool Eidt(MemberInfo mi)
        {
            return miDal.Update(mi) > 0;
        }
        /// <summary>
        /// 表示删除数据是否成功
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Remove(int Id)
        {
            return miDal.Delete(Id) > 0;
        }
    }
}
