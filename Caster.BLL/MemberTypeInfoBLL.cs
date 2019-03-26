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
    public class MemberTypeInfoBLL
    {
        private MemberTypeInfoDAL mtiDal;

        public MemberTypeInfoBLL()
        {
            this.mtiDal = new MemberTypeInfoDAL();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<MemberTypeInfo> GetList()
        {
            return mtiDal.GetMemberTypeInfos();
        }
        /// <summary>
        /// 表示增加是否成功
        /// </summary>
        /// <param name="mti"></param>
        /// <returns></returns>
        public bool Add(MemberTypeInfo mti)
        {
            return mtiDal.Insert(mti) > 0;
        }
        /// <summary>
        /// 表示修改是否成功
        /// </summary>
        /// <param name="mti"></param>
        /// <returns></returns>
        public bool Edit(MemberTypeInfo mti)
        {
            return mtiDal.Update(mti) > 0;
        }
        /// <summary>
        /// 表示删除是否成功
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Remove(int Id)
        {
            return mtiDal.Delete(Id) > 0;
        }
        /// <summary>
        /// 获取会员ID
        /// </summary>
        /// <param name="selectedText"></param>
        /// <returns></returns>
        public int? GetMTypeId(string titleName)
        {
            return mtiDal.GetMTypeId(titleName);
        }
    }
}
