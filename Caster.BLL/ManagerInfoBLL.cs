using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.Common;
using Caster.DAL;
using Caster.Model;

namespace Caster.BLL
{
    public class ManagerInfoBLL
    {
        public ManagerInfoBLL()
        {
            this.miDal = new ManagerInfoDAL();
        }

        private ManagerInfoDAL miDal;
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList()
        {
            return miDal.GetManagerInfoList();
        }
        /// <summary>
        /// 表示增加数据是否成功
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public bool Add(ManagerInfo mi)
        {
            return miDal.Insert(mi) > 0;
        }
        /// <summary>
        /// 表示编辑数据是否成功
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public bool Edit(ManagerInfo mi)
        {
            if (mi.MPwd.Equals("这是一个不可能被设置的密码"))
            {
                return miDal.Update(mi) > 0;
            }
            else
            {
                return miDal.Update(mi, true) > 0;
            }
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
        /// <summary>
        /// 表示是否能成功登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Login(string name, string pwd, out int type)
        {
            type = -1;
            ManagerInfo mi = miDal.GetManagerInfo(name);

            if (mi == null)
            {
                return false;
            }

            string correctPwd = mi.MPwd;
            if (MD5Helper.Encrying(pwd) != correctPwd)
            {
                return false;
            }

            type = Convert.ToInt32(mi.MType);
            return true;

        }

    }
}
