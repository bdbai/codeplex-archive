using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;

namespace TiebasSig
{
    /// <summary>
    /// HTTP请求、HTML处理相关辅助类。
    /// </summary>
    public abstract class HtmlWebHelper
    {
        #region 正则文本相关。
        /// <summary>
        /// 正则表达式中需要转义的字符。
        /// </summary>
        private static char[] regexChars = { '\\', '*', '.', '?', '+', '$', '^', '[', ']', '(', ')', '{', '}', '|', '/' };
        /// <summary>
        /// 获得原字符串中某一对字符串的中间字符串。
        /// </summary>
        /// <param name="orig">原字符串。</param>
        /// <param name="left">左侧字符串。</param>
        /// <param name="right">右侧字符串。</param>
        /// <param name="AsMuchAsPossible">贪婪模式，即“?”标志。</param>
        /// <returns>所有匹配项。</returns>
        public static IEnumerable<string> GetStringsBetween(string orig, string left, string right, bool AsMuchAsPossible = true, bool singleLine = false, bool rightToLeft = false)
        {
            //* . ? + $ ^ [ ] ( ) { } | \ /
            string formattedLeft = left, formattedRight = right;
            foreach (char rc in regexChars)
            {
                formattedLeft = formattedLeft.Replace(rc.ToString(), "\\" + rc.ToString());
                formattedRight = formattedRight.Replace(rc.ToString(), "\\" + rc.ToString());
            }

            StringBuilder patternBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(left))
            {
                patternBuilder.Append("(?<=" + formattedLeft + ")");
            }
            patternBuilder.Append(".*");
            if (AsMuchAsPossible)
            {
                patternBuilder.Append('?');
            }
            if (!string.IsNullOrEmpty(right))
            {
                patternBuilder.Append("(?=" + formattedRight + ")");
            }
            RegexOptions ro = new RegexOptions();
            if (singleLine)
            {
                ro |= RegexOptions.Singleline;
            }
            if (rightToLeft)
            {
                ro |= RegexOptions.RightToLeft;
            }
            Regex r = new Regex(patternBuilder.ToString(), ro);
            MatchCollection mc = r.Matches(orig);
            foreach (Match m in mc)
            {
                yield return m.Value;
            }
        }

        /// <summary>
        /// 获得原字符串中某一对字符串的中间字符串。
        /// </summary>
        /// <param name="orig">原字符串。</param>
        /// <param name="left">左侧字符串。</param>
        /// <param name="right">右侧字符串。</param>
        /// <param name="AsMuchAsPossible">贪婪模式，即“?”标志。</param>
        /// <returns>第一个匹配项。</returns>
        public static string GetStringBetween(string orig, string left, string right, bool AsMuchAsPossible = true, bool singleLine = false, bool rightToLeft = false)
        {
            //* . ? + $ ^ [ ] ( ) { } | \ /
            string formattedLeft = string.Empty, formattedRight = string.Empty;
            foreach (char rc in regexChars)
            {
                formattedLeft = left.Replace(rc.ToString(), "\\" + rc.ToString());
                formattedRight = right.Replace(rc.ToString(), "\\" + rc.ToString());
            }
            StringBuilder patternBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(left))
            {
                patternBuilder.Append("(?<=" + formattedLeft + ")");
            }
            patternBuilder.Append(".*");
            if (AsMuchAsPossible)
            {
                patternBuilder.Append('?');
            }
            if (!string.IsNullOrEmpty(right))
            {
                patternBuilder.Append("(?=" + formattedRight + ")");
            }
            RegexOptions ro = new RegexOptions();
            if (singleLine)
            {
                ro |= RegexOptions.Singleline;
            }
            if (rightToLeft)
            {
                ro |= RegexOptions.RightToLeft;
            }
            Regex r = new Regex(patternBuilder.ToString(), ro);
            //Regex r = new Regex(string.Format("(?<={0}).*(?={1})", formattedLeft, formattedRight)); ;
            Match m = r.Match(orig);
            return m.Value;
        }
        #endregion

        #region 网络请求相关。
        public static CookieContainer CookieContainer = new CookieContainer();
        public static string GetStringByResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        #endregion

        #region GET请求相关。
        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <returns></returns>
        public static string SendGetRequest(string url)
        {
            return SendGetRequest(url, null, null, 0);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="timeout">请求与超时时间。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, int timeout)
        {
            return SendGetRequest(url, null, null, timeout);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="referer">Referer标头的值。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, string referer)
        {
            return SendGetRequest(url, referer, null, 0);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="referer">Referer标头的值。</param>
        /// <param name="timeout">请求与超时时间。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, string referer, int timeout)
        {
            return SendGetRequest(url, referer, null, timeout);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="cookieContainer">与此请求关联的CookieContainer。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, CookieContainer cookieContainer)
        {
            return SendGetRequest(url, null, cookieContainer, 0);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="cookieContainer">与此请求关联的CookieContainer。</param>
        /// <param name="timeout">请求与超时时间。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, CookieContainer cookieContainer, int timeout)
        {
            return SendGetRequest(url, null, cookieContainer, timeout);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="referer">Referer标头的值。</param>
        /// <param name="cookieContainer">与此请求关联的CookieContainer。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, string referer, CookieContainer cookieContainer)
        {
            return SendGetRequest(url, referer, cookieContainer, 0);
        }

        /// <summary>
        /// 向指定的URL发送GET请求。
        /// </summary>
        /// <param name="url">指定的URL。</param>
        /// <param name="referer">Referer标头的值。</param>
        /// <param name="cookieContainer">与此请求关联的CookieContainer。</param>
        /// <param name="timeout">请求与超时时间。</param>
        /// <returns>回应文本。</returns>
        public static string SendGetRequest(string url, string referer, CookieContainer cookieContainer, int timeout)
        {
            Console.WriteLine(string.Format("正在向{0}发送请求……", url));
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.Referer = referer ?? string.Empty;
            request.CookieContainer = cookieContainer ?? CookieContainer;
            request.Timeout = timeout == 0 ? request.Timeout : timeout;
            request.ReadWriteTimeout = request.Timeout;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string responseText = string.Empty;
                    responseText = sr.ReadToEnd();
                    return responseText;
                }
            }

        }

        #endregion

        #region 百度帐户相关。
        private static string cookiePathWin8 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows\INetCookies";
        private static string cookiePathWin7 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Cookies\";
        
        /// <summary>
        /// 从IE的Cookie文件夹中获取BDUSS参数。
        /// </summary>
        /// <param name="path">IE的Cookie文件夹。</param>
        /// <returns>BDUSS参数</returns>
        public static string GetBduss()
        {
            string ret = string.Empty;
            if (Directory.Exists(cookiePathWin8))
            {
                ret = findBduss(cookiePathWin8);
                if (!string.IsNullOrEmpty(ret))
                {
                    return ret;
                }
            }
            if (Directory.Exists(cookiePathWin7))
            {
                ret = findBduss(cookiePathWin7);
                if (!string.IsNullOrEmpty(ret))
                {
                    return ret;
                }
            }
            return string.Empty;
        }

        private static string findBduss(string path)
        {
            Regex r;
            string ret;
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    string fileText = File.ReadAllText(file);
                    if (fileText.Contains("BDUSS") && fileText.Contains("baidu.com"))
                    {
                        r = new Regex("(?<=BDUSS\\n)(.*?)(?=\\nbaidu.com)");
                        ret = r.Match(fileText).Value;
                        if (!string.IsNullOrEmpty(ret))
                        {
                            return ret;
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            foreach (string dir in Directory.GetDirectories(path))
            {
                return findBduss(dir);
            }
            return string.Empty;
        }
        #endregion
    }
}
