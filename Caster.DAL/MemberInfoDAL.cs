using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caster.Common;
using Caster.Model;

namespace Caster.DAL
{
    public class MemberInfoDAL
    {
        public MemberInfoDAL() { }
        /// <summary>
        /// 获取memberInfo数据列表
        /// </summary>
        /// <returns></returns>
        public List<MemberInfo> GetMemberInfos(Dictionary<string, string> dictionary)
        {
            List<MemberInfo> list = new List<MemberInfo>();
            string sqltext = "select mi.MId,mi.MIsDelete,mi.MMoney,mi.MName,mi.MPhone,mi.MTypeId,mti.Mtitle as Mtitle from MemberInfo as mi,MemberTypeInfo as mti where mi.MId = Mti.MId and mi.MIsDelete = 0";
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            string sqlWhere = string.Empty;
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                sqlWhere += " and " + keyValuePair.Key + " like @" + keyValuePair.Key;
                parameters.Add(new SQLiteParameter("@" + keyValuePair.Key, "%" + keyValuePair.Value + "%"));
            }

            sqltext += sqlWhere;
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqltext, parameters.ToArray());
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    MId = Convert.ToInt32(dataRow[0]),
                    MIsDelete = Convert.ToBoolean(dataRow[1]),
                    MMoney = Convert.ToDecimal(dataRow[2]),
                    MName = dataRow[3].ToString(),
                    MPhone = dataRow[4].ToString(),
                    MTypeId = Convert.ToInt32(dataRow[5]),
                    Mtitle = dataRow[6].ToString()
                });
            }

            return list;
        }
    }
}
