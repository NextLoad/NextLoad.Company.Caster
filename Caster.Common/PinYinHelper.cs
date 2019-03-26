using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.International.Converters.PinYinConverter;
using Microsoft.SqlServer.Server;

namespace Caster.Common
{
    public static class PinYinHelper
    {
        /// <summary>
        /// 获取中文汉字的拼音首字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetPinYin(string text)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
            if (!regex.IsMatch(text))
            {
                throw new Exception("输入的字符串不是中文");
            }
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                ChineseChar cc = new ChineseChar(c);
                sb.Append(cc.Pinyins[0][0]);
            }

            return sb.ToString();
        }
    }
}
