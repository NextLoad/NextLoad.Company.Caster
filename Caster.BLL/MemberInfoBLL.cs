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

        public List<MemberInfo> GetList(Dictionary<string,string> dictionary)
        {
            return miDal.GetMemberInfos(dictionary);
        }
    }
}
