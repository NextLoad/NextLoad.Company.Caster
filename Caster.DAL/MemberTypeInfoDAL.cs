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
    public class MemberTypeInfoDAL
    {
        public MemberTypeInfoDAL() { }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public List<MemberTypeInfo> GetMemberTypeInfos()
        {
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            string sqltext = "select * from MemberTypeInfo where MIsDelete = 0";
            DataTable dt = SQLiteHelper.ExecuteDataTable(sqltext);
            foreach (DataRow dataRow in dt.Rows)
            {
                list.Add(new MemberTypeInfo()
                {
                    MId = Convert.ToInt32(dataRow[0]),
                    MTitle = dataRow[1].ToString(),
                    MDiscount = Convert.ToDecimal(dataRow[2]),
                    MIsDelete = Convert.ToBoolean(dataRow[3])
                });
            }

            return list;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="mti"></param>
        /// <returns></returns>
        public int Insert(MemberTypeInfo mti)
        {
            string sqltext = "insert into MemberTypeInfo(MTitle,MDiscount,MIsDelete) values(@title,@discount,0)";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@discount", DbType.Decimal),
            };
            parameters[0].Value = mti.MTitle;
            parameters[1].Value = mti.MDiscount;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="mti"></param>
        /// <returns></returns>
        public int Update(MemberTypeInfo mti)
        {
            string sqltext = "Update MemberTypeInfo set MTitle = @title,MDiscount = @discount where MId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@title", DbType.String),
                new SQLiteParameter("@discount", DbType.Decimal),
                new SQLiteParameter("@Id", DbType.Int32),
            };
            parameters[0].Value = mti.MTitle;
            parameters[1].Value = mti.MDiscount;
            parameters[2].Value = mti.MId;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sqltext = "Update MemberTypeInfo set MIsDelete = @isDelete where MId = @Id";
            SQLiteParameter[] parameters =
            {
                new SQLiteParameter("@isDelete", DbType.Boolean),
                new SQLiteParameter("@Id", DbType.Int32),
            };
            parameters[0].Value = 1;
            parameters[1].Value = Id;
            return SQLiteHelper.ExecuteNonQuery(sqltext, parameters);
        }
        /// <summary>
        /// 根据会员名称获得会员ID
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns></returns>
        public int? GetMTypeId(string titleName)
        {
            string sqlText = "select MId from MemberTypeInfo where MTitle = @title";
            SQLiteParameter parameter = new SQLiteParameter("@title", titleName);
            object obj = SQLiteHelper.ExecuteScalar(sqlText, parameter);
            return Convert.ToInt32(obj);
        }
    }
}
