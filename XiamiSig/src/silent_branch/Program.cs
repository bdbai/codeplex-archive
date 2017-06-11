using System.Diagnostics;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace XiamiSigLite
{
    /// <summary>
    /// 提示委托。
    /// </summary>
    /// <param name="messageText">提示内容。</param>
    /// <param name="finished">是否结束。</param>
    internal delegate void MessageHandler(string messageText = "虾小米签到中……", bool finished = false);

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

        /*/// <summary>
        /// 指示是否有新提示。
        /// </summary>
        private static bool newTip = false;

        private static MessageTip _currentTip;

        /// <summary>
        /// 当前的提示。
        /// </summary>
        private static MessageTip currentTip
        {
            get
            {
                return _currentTip;
            }
            set
            {
                _currentTip = value;
                tipHandler.Set();
            }
        }*/

        /// <summary>
        /// 放托盘图标的线程。
        /// </summary>
        private static Thread iconThread;

        /// <summary>
        /// 托盘图标。
        /// </summary>
        private static NotifyIcon icon = new NotifyIcon();

        /*/// <summary>
        /// 等待提示改变的句柄。
        /// </summary>
        private static EventWaitHandle tipHandler = new EventWaitHandle(false, EventResetMode.AutoReset);*/

        /// <summary>
        /// 显示提示的事件。
        /// </summary>
        private static event MessageHandler tipShow;

        /// <summary>
        /// 等待单击图标退出的句柄。
        /// </summary>
        private static EventWaitHandle exitHandler = new EventWaitHandle(false, EventResetMode.AutoReset);

        /// <summary>
        /// 冒个泡提示下。
        /// </summary>
        private static void Popup(string MessageText = "虾小米签到中……", bool finished = false)
        {
            Console.WriteLine(MessageText);
            InfoCollector.WriteLog(MessageText);
            //currentTip = new MessageTip(MessageText, finished);
            if (tipShow != null)
            {
                tipShow(MessageText, finished);
            }
            //newTip = true;
            //icon.ShowBalloonTip(5000, "虾米签到", MessageText, ToolTipIcon.Info);
            /*icon.Text = MessageText;
            if (finished)
            {
                icon.Icon = XiamiSigLite.Properties.Resources.favicon_finished;
            }
            else
            {
                icon.Icon = XiamiSigLite.Properties.Resources.favicon;
            }*/
        }

        /// <summary>
        /// 托盘图标的单击事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void icon_Click(object sender, EventArgs e)
        {
            exitHandler.Set();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();
            //把托盘图标准备好。
            iconThread = new Thread(new ThreadStart(() =>
            {
                icon = new NotifyIcon();
                icon.Click += icon_Click;
                icon.Icon = XiamiSigLite.Properties.Resources.favicon;
                icon.Visible = true;
                tipShow += new MessageHandler((msg, finished) =>
                {
                    icon.Text = msg;
                    if (finished)
                    {
                        icon.Icon = XiamiSigLite.Properties.Resources.favicon_finished;
                    }
                    else
                    {
                        icon.Icon = XiamiSigLite.Properties.Resources.favicon;
                    }
                });
                Application.Run();
                /*while (true)
                {
                    tipHandler.WaitOne();
                    icon.Text = currentTip.MessageText;
                    if (currentTip.Finished)
                    {
                        icon.Icon = XiamiSigLite.Properties.Resources.favicon_finished;
                    }
                    else
                    {
                        icon.Icon = XiamiSigLite.Properties.Resources.favicon;
                    }
                    //newTip = false;
                }
                while (true)
                {
                    exitHandler.WaitOne();
                    Exit(0);
                }*/
            }));
            iconThread.Start();

            /*//杀掉残留的家伙。
            Process[] processes = Process.GetProcessesByName(Application.ExecutablePath.Replace(Application.StartupPath + @"\", ""));
            try
            {
                foreach (Process p in processes)
                {
                    p.Kill();
                    Popup("杀掉一只老家伙。");
                }
            }
            catch (Exception ex)
            {
                Popup("杀错误：" + ex.Message);
            }*/


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
                //Thread.Sleep(10000);
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
                //Thread.Sleep(10000);
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
                //Thread.Sleep(10000);
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
                //Thread.Sleep(10000);
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
                //Thread.Sleep(10000);
                Exit(1);
            }
            else
            {
                Popup(dayCountText + '。', true);
                //Thread.Sleep(5000);
            }
            while (true)
            {
                exitHandler.WaitOne();
                Exit(0);
            }
            //Exit(0);
        }

        /// <summary>
        /// 退出并消除图标。
        /// </summary>
        /// <param name="exitCode">退出码。0代表正常。</param>
        internal static void Exit(int exitCode)
        {
            try
            {
                icon.Visible = false;
                icon.Dispose();
                InfoCollector.WriteLog("退出:" + exitCode.ToString());
            }
            catch (Exception ex)
            {
                InfoCollector.WriteLog("退出错误:" + ex.Message);
            }
            Environment.Exit(exitCode);
        }

        /*private static void icon_Click(object sender, EventArgs e)
        {
            Console.WriteLine("fucj");
        }*/

    }

    /*/// <summary>
    /// 图标的提示。
    /// </summary>
    internal struct MessageTip
    {
        /// <summary>
        /// 初始化一个图标文本。
        /// </summary>
        /// <param name="messageText">提示文本。</param>
        /// <param name="finished">指示是否完成。</param>
        public MessageTip(string messageText = "虾小米签到中……", bool finished = false)
        {
            MessageText = messageText;
            Finished = finished;
        }

        /// <summary>
        /// 提示文本。
        /// </summary>
        public readonly string MessageText;

        /// <summary>
        /// 指示是否完成。
        /// </summary>
        public readonly bool Finished;
    }*/
}
