using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Caster.Common
{
    public static class MD5Helper
    {
        /// <summary>
        /// 返回MD5加密的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrying(string str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }
    }
}
