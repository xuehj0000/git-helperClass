using System.Text.RegularExpressions;

namespace HD.Helper.Common
{
    /// <summary>
    /// 正则表达式的公共类
    /// </summary>    
    public class RegexHelper
    {
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配（忽略大小写），匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }


        /// <summary>
        /// 替换第一个位置的复合条件的数
        /// </summary>
        public string ReplaceFirst(string str)
        {
            return new Regex("号").Replace(str, "号1单元", 1);
        }
        
    }
}
