using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace XiamiSig
{
    /// <summary>
    /// 签到主程序。
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 通过得到的网络回应流读出文本。
        /// </summary>
        /// <param name="response">网络回应。</param>
        /// <returns>得到的文本。</returns>
        public static string GetStringByResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 托盘图标。
        /// </summary>
        private static NotifyIcon icon = new NotifyIcon();

        /// <summary>
        /// 冒个泡提示下。
        /// </summary>
        private static void Popup(string MessageText = "虾小米签到中……")
        {
            Console.WriteLine(MessageText);
            icon.ShowBalloonTip(5000, "虾米签到", MessageText, ToolTipIcon.Info);
        }
        //public static string parseEmptyLabel(Match m)
        //{
        //    if (m.Value.TrimEnd().EndsWith('/'))
        //    {
        //        return m.Value;
        //    }
        //    else
        //    {
        //        return m.Value.TrimEnd('/');
        //    }
        //}

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();

            //把托盘图标准备好。
            icon.Icon = XiamiSig.Properties.Resources.favicon;
            icon.Visible = true;

            //确认数据正确性。
            if (!InfoCollector.CheckData())
            {
                Popup("请输入虾米帐户信息。");
                //Application.SetCompatibleTextRenderingDefault(true);
                if (InfoCollector.Collect())
                {
                    //真听话。
                    goto signStart;
                }
                else
                {
                    //用户在捣乱。
                    icon.Visible = false;
                    icon.Dispose();
                    Exit(1);
                }
            }

            //检查参数个数来决定是编辑信息还是直接签到。
            if (argv.Length > 0)
            {
                Console.WriteLine("虾米签到。");
            }
            else
            {
                InfoCollector.Collect();
            }
        signStart:
            string email = InfoCollector.Email;
            string password = InfoCollector.Password;
            Popup();
            //icon.Click += new EventHandler();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //定义一些频繁用到的局部变量。
            Regex r;
            HttpWebRequest request;
            HttpWebResponse response;
            CookieContainer cc = new CookieContainer();

            //去电脑版网页签到，没成功。
            //走弯路浪费这么多，不舍得删啊。
            //request = (HttpWebRequest)HttpWebRequest.Create("https://login.xiami.com/member/login");
            //request.CookieContainer = cc;
            //response = (HttpWebResponse)request.GetResponse();
            //string loginPageTextReader = GetStringByResponse(response);
            //string formText = loginPageTextReader.Split(new string[] { "<form", "</form>" }, StringSplitOptions.RemoveEmptyEntries)[1];
            //formText = "<form" + formText + "</form>";

            //Regex r = new Regex("<!--(.*?)-->");
            //formText = r.Replace(formText, "");
            //r = new Regex("<input(.*?)+(?=>)");
            //formText = r.Replace(formText,"$0 /");
            //formText = formText.Replace("/ /", "/");


            //using (StringReader formTextReader = new StringReader(formText))
            //{
            //    using (XmlReader formReader = XmlReader.Create(formTextReader))
            //    {
            //        while (formReader.Read())
            //        {
            //            string typeAttr = formReader.GetAttribute("type");
            //            if (formReader.NodeType == XmlNodeType.Element
            //                &&
            //                formReader.Name.ToLower() == "input"
            //                &&
            //                (typeAttr != null &&
            //                (
            //                    formReader.GetAttribute("type").ToLower() == "hidden"
            //                    ||
            //                    formReader.GetAttribute("type").ToLower() == "input")
            //                )
            //                )
            //            {
            //                names.Add(formReader.GetAttribute("name"));
            //                values.Add(formReader.GetAttribute("value"));
            //            }
            //        }
            //    }
            //}

            //POST数值对。
            StringCollection names = new StringCollection();
            StringCollection values = new StringCollection();
            names.Add("email");
            values.Add(email);
            names.Add("password");
            values.Add(password);
            names.Add("LoginButton");
            values.Add("登录");

            //把POST数值对变成字符串。
            StringBuilder postTextBuilder = new StringBuilder();
            if (names.Count == 0 || names.Count != values.Count)
            {
                Popup("登录参数错误。");
                Thread.Sleep(10000);
                Exit(1);
            }
            for (int paraIndex = 0; paraIndex < names.Count; paraIndex++)
            {
                postTextBuilder.Append(names[paraIndex].Trim());
                postTextBuilder.Append('=');
                postTextBuilder.Append(values[paraIndex].Trim());
                postTextBuilder.Append('&');
            }
            string postText = postTextBuilder.ToString(0, postTextBuilder.Length - 1);

            //登录。
            request = (HttpWebRequest)HttpWebRequest.Create("https://login.xiami.com/web/login");
            request.CookieContainer = cc;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Referer = "https://www.xiami.com/web"; //一字不漏。
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    using (StreamWriter requestWriter = new StreamWriter(requestStream))
                    {
                        requestWriter.Write(postText);
                        requestWriter.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Popup("登录错误：" + ex.Message);
                Thread.Sleep(10000);
                Exit(1);
            }
            response = (HttpWebResponse)request.GetResponse();

            //得到的响应，正确的话应该是个人页。
            string loginResultText = GetStringByResponse(response);
            //loginResultText = loginResultText.Replace("null(\"", "");
            //loginResultText = loginResultText.Remove(loginResultText.Length - 2);

            //获得首页地址。
            string homeUrl = string.Empty;
            r = new Regex("(?<=<a href=\")(.*?)(?=\">首页)");
            homeUrl = r.Match(loginResultText).Value;
            if (string.IsNullOrEmpty(homeUrl))
            {
                Popup("登陆错误：无法获得用户ID。");
                Thread.Sleep(10000);
                Exit(1);
            }
            
            //Thread.Sleep(5000);

            request = (HttpWebRequest)HttpWebRequest.Create(homeUrl);
            request.CookieContainer = cc;
            response = (HttpWebResponse)request.GetResponse();
            string homeText = GetStringByResponse(response);
            string signResultText = string.Empty;
            if (homeText.Contains("已连续签到"))
            {
                signResultText = homeText;
                goto result;
            }
            string signUrl = string.Empty;
            r = new Regex("(?<=href=\")(.*?)(?=\">每日签到)");
            signUrl = r.Match(homeText).Value;
            signUrl = "http://www.xiami.com" + signUrl;

            //进行签到。
            request = (HttpWebRequest)HttpWebRequest.Create(signUrl);
            request.CookieContainer = cc;
            request.Referer = "http://www.xiami.com/web"; //一字不漏。
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Popup("签到错误：" + ex.Message);
                Thread.Sleep(10000);
                Exit(1);
            }
            //得到签到后跳转的页面。
            signResultText = GetStringByResponse(response);

            //签到正确的话应该能从页面上得到类似“已连续签到xx天”的字符串。
            result:
            string dayCountText = string.Empty;
            r = new Regex("已连续签到.*?天");
            dayCountText = r.Match(signResultText).Value;
            if (string.IsNullOrEmpty(dayCountText))
            {
                Popup("签到错误：无法得到连续签到天数。");
                Thread.Sleep(10000);
                Exit(1);
            }
            else
            {
                Popup(dayCountText + '。');
                Thread.Sleep(5000);
            }
            Exit(0);
        }

        internal static void Exit(int exitCode)
        {
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
