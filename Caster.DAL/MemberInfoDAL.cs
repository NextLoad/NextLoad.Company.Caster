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
            string sqltext = "select mi.MId,mi.MIsDelete,mi.MMoney,mi.MName,mi.MPhone,mi.MTypeId,mti.Mtitle as Mtitle from MemberInfo as mi,MemberTypeInfo as mti where mi.MTypeId = Mti.MId and mi.MIsDelete = 0";
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
        /// <summary>
        /// 向MemberInfo表中插入数据
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Insert(MemberInfo mi)
        {
            string sqlText =
                "insert into MemberInfo(MTypeId,MName,MPhone,MMoney,MIsDelete) values(@typeId,@Name,@Phone,@Money,0)";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@typeId", DbType.Int32),
                new SQLiteParameter("@Name", DbType.String),
                new SQLiteParameter("@Phone", DbType.String),
                new SQLiteParameter("@Money", DbType.Decimal)
            };
            parameters[0].Value = mi.MTypeId;
            parameters[1].Value = mi.MName;
            parameters[2].Value = mi.MPhone;
            parameters[3].Value = mi.MMoney;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Update(MemberInfo mi)
        {
            string sqlText = "update MemberInfo set MName = @Name,MPhone=@Phone,MMoney=@Money,Mtypeid = @typeid where MId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@Name", DbType.String),
                new SQLiteParameter("@Phone", DbType.String),
                new SQLiteParameter("@Money", DbType.Decimal),
                new SQLiteParameter("@typeid", DbType.Int32),
                new SQLiteParameter("@Id", DbType.Int32)
            };
            parameters[0].Value = mi.MName;
            parameters[1].Value = mi.MPhone;
            parameters[2].Value = mi.MMoney;
            parameters[3].Value = mi.MTypeId;
            parameters[4].Value = mi.MId;
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameters);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sqlText = "update MemberInfo set Misdelete = 1 where MId = @id";
            SQLiteParameter parameter = new SQLiteParameter("@id", Id);
            return SQLiteHelper.ExecuteNonQuery(sqlText, parameter);
        }
    }
}
