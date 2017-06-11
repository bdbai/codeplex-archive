using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FuckZhidaoSig
{
    class Program
    {

        /// <summary>
        /// 托盘图标。
        /// </summary>
        private static NotifyIcon icon = new NotifyIcon();

        /// <summary>
        /// 冒个泡提示下。
        /// </summary>
        private static void Popup(string messageText = "知道签到中……")
        {
            Console.WriteLine(messageText);
            icon.ShowBalloonTip(5000, "知道签到", messageText, ToolTipIcon.Info);
        }

        /// <summary>
        /// 从IE的Cookie文件夹中获取BDUSS参数。
        /// </summary>
        /// <param name="path">IE的Cookie文件夹。</param>
        /// <returns>BDUSS参数</returns>
        public static string GetBduss(string path)
        {
            Regex r;
            string ret;
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    Console.WriteLine(Path.GetFileName(file));
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
                return GetBduss(dir);
            }
            return string.Empty;
        }
        /// <summary>  
        /// Unicode字符串转为正常字符串  
        /// </summary>  
        /// <param name="srcText">含Unicode字符（即\uxxxx字符）</param>  
        /// <returns></returns>  
        public static string UnicodeToString(string srcText)
        {
            string ret = "";
            string src = srcText;
            //int len = (src.Length - srcText.Replace(@"\u", "").Length) / 2;
            while (src.Length != 0)
            {
                if (src.StartsWith(@"\u") && src.Length >= 6)
                {
                    string str = string.Empty;
                    str = src.Substring(0, 6).Substring(2);
                    src = src.Substring(6);
                    byte[] bytes = new byte[2];
                    try
                    {
                        bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                        bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                        ret += Encoding.Unicode.GetString(bytes);
                    }
                    catch (Exception)
                    {
                        ret += @"\u" + str;
                    }
                }
                else
                {
                    ret += src.Substring(0, 1);
                    src = src.Substring(1);
                }
            }
            return ret;
        }
        static void Main(string[] args)
        {
            icon.Icon = FuckZhidaoSig.Properties.Resources.favicon;
            icon.Visible = true;
            string BdussValue = string.Empty;
            string cookieDir = string.Empty;
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows\INetCookies"))
            {
                BdussValue = GetBduss(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Windows\INetCookies");
                if (!string.IsNullOrEmpty(BdussValue))
                {
                    goto start;
                }
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Cookies\"))
            {
                BdussValue = GetBduss(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Cookies\");
                if (!string.IsNullOrEmpty(BdussValue))
                {
                    goto start;
                }
            }

            MessageBox.Show("请用IE浏览器登录你的百度帐户并勾选“下次自动登录”！");
            exit(1);
            return;

        start:
            if (!AutoStartHelper.dirPrepare())
            {
                if (MessageBox.Show("是否加至自启？", "百度知道签到提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    AutoStartHelper.TaskAdd();
                }
            }

            Popup();
            Regex r;
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create("http://wapiknow.baidu.com");
            //HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create("http://zhidao.baidu.com/submit/user");
            CookieContainer cc = new CookieContainer();
            HttpWebRequest request;
            HttpWebResponse response;

            string cm = string.Empty;
            request = (HttpWebRequest)HttpWebRequest.Create("http://cdn.iknow.bdimg.com/static/common/pkg/module_8f2d9f9.js");
            request.CookieContainer = cc;
            //cc.Add(new Cookie("BDUSS", "EdTbTZ6dEtBd3VUb2RKOGdpMDNiTGx6RTQ2TGFNcUtXWkcwZjc5RDZ-dExZU3BVQVFBQUFBJCQAAAAAAAAAAAEAAADGCQQDsPyyvLahAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEvUAlRL1AJUU", "/", "baidu.com"));
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Popup("获取参数“cm”失败：" + ex.Message);
                exit(1);
                return;
            }
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader rr = new StreamReader(s, Encoding.Default))
                {
                    string rrr = rr.ReadToEnd();
                    r = new Regex("(?<=url:\"/submit/user\",params:{cm:)(.*?)(?=})");
                    cm = r.Match(rrr).Value;
                }
            }
            if (string.IsNullOrEmpty(cm))
            {
                Popup("获取参数“cm”失败。");
                exit(1);
                return;
            }

            string stoken = string.Empty;
            request = (HttpWebRequest)HttpWebRequest.Create("http://zhidao.baidu.com");
            request.CookieContainer = cc;
            cc.Add(new Cookie("BDUSS", BdussValue, "/", "baidu.com"));
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Popup("获取参数“stoken”失败：" + ex.Message);
                exit(1);
                return;
            }
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader rr = new StreamReader(s, Encoding.Default))
                {
                    string rrr = rr.ReadToEnd();
                    r = new Regex("(?<=stoken\":\")(.*?)(?=\")");
                    stoken = r.Match(rrr).Value;
                }
            }
            if (string.IsNullOrEmpty(stoken))
            {
                Popup("获取参数“stoken”失败。");
                exit(1);
                return;
            }

            string errorMsg = string.Empty;
            string signInDataNum = string.Empty;
            request = (HttpWebRequest)HttpWebRequest.Create("http://zhidao.baidu.com/submit/user");
            request.CookieContainer = cc;
            //cc.Add(new Cookie("BDUSS", "EdTbTZ6dEtBd3VUb2RKOGdpMDNiTGx6RTQ2TGFNcUtXWkcwZjc5RDZ-dExZU3BVQVFBQUFBJCQAAAAAAAAAAAEAAADGCQQDsPyyvLahAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEvUAlRL1AJUU", "/", "baidu.com"));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            using (Stream s = request.GetRequestStream())
            {
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(string.Format("cm={0}&stoken={1}", cm, stoken));
                    sw.Flush();
                }
            }
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Popup("签到失败：" + ex.Message);
                exit(1);
                return;
            }
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader rr = new StreamReader(s, Encoding.Default))
                {
                    string rrr = rr.ReadToEnd();
                    r = new Regex("(?<=\"errorMsg\":\")(.*?)(?=\")");
                    //byte[] errorMsgBytes = Encoding.Unicode.GetBytes(r.Match(rrr).Value);
                    string errorMsgRaw = r.Match(rrr).Value;
                    errorMsg = UnicodeToString(errorMsgRaw);

                    if (errorMsg == "success")
                    {
                        r = new Regex("(?<=\"signInDataNum\":)(.*?)(?=,)");
                        signInDataNum = r.Match(rrr).Value;
                        Popup("已连续签到" + signInDataNum + "天。");
                    }
                    else if (errorMsg == "已签到")
                    {
                        r = new Regex("(?<=\"signInDataNum\":)(.*?)(?=,)");
                        signInDataNum = r.Match(rrr).Value;
                        Popup("已连续签到" + signInDataNum + "天。");
                    }
                    else
                    {
                        Popup("签到失败：" + errorMsg);
                        exit(1);
                        return;
                    }

                }
            }

            exit(0);
            
        }


        private static void exit(int exitCode)
        {
            Thread.Sleep(5000);
            try
            {
                icon.Visible = false;
                icon.Dispose();
            }
            catch (Exception)
            {
            }
            Environment.Exit(exitCode);
        }
    }
}
