using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Web;

namespace TiebasSig
{
    static class Program
    {

        /// <summary>
        /// 托盘图标。
        /// </summary>
        private static NotifyIcon icon = new NotifyIcon();

        /// <summary>
        /// 冒个泡提示下。
        /// </summary>
        private static void Popup(string messageText = "贴吧签到中……")
        {
            Console.WriteLine(messageText);
            icon.ShowBalloonTip(5000, "贴吧签到", messageText, ToolTipIcon.Info);
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //初始化托盘图标。
            Form iconform = new Form();
            icon.Icon = iconform.Icon;
            icon.Visible = true;

            //读取BDUSS参数。
            string BdussValue = string.Empty;
            if (args.Length > 1)
            {
                //有参数则从参数中读。
                BdussValue = args[1];
            }
            else
            {
                //无参数则读取Cookie。
                BdussValue = HtmlWebHelper.GetBduss();
            }
            if (string.IsNullOrEmpty(BdussValue))
            {
                MessageBox.Show("请用IE浏览器登录你的百度帐户并勾选“下次自动登录”！");
                exit(1);
                return;
            }

            //判断是否需要先确认。
            bool toSign = false;
            toSign = args.Length > 0 && args[0] == "sign";
            if (!toSign)
            {
                Application.EnableVisualStyles();
                DialogResult result = MessageBox.Show(string.Format("是否加至自启？{0}{0}“是”：添加或更新；{0}“否”：删除；{0}“取消”：不做更改。", Environment.NewLine), "百度贴吧签到提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        AutoStartHelper.TaskAdd();
                        break;
                    case DialogResult.No:
                        AutoStartHelper.TaskRemove();
                        break;
                }
            }

            Popup();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            HtmlWebHelper.CookieContainer.Add(new Cookie("BDUSS", BdussValue, "/", "baidu.com"));

            //获取“我关注的贴吧”页面文本。
            string favPageText = string.Empty;
            try
            {
                favPageText = HtmlWebHelper.SendGetRequest("http://tieba.baidu.com/mo/q---B8D06B9EB00241F919F47789D4FD3103%3AFG%3D1--1-1-0--2--wapp_1385540291997_626/m?tn=bdFBW&tab=favorite", 5000);
            }
            catch (Exception ex)
            {
                Popup("获取“我关注的贴吧”页面错误：" + ex.Message);
                exit(1);
                return;
            }
            if (favPageText.Contains("你暂未关注某个吧"))
            {
                MessageBox.Show("请检查是否关注了至少一个贴吧，并用IE浏览器重新登陆一下。");
                exit(1);
                return;
            }

            //获取所要签到贴吧的部分XML。
            string TiebasXhtmlText = string.Empty;
            TiebasXhtmlText = "<div class=\"d\">" + HtmlWebHelper.GetStringBetween(favPageText, "<div class=\"d\">", "</div>", true) + "</div>";

            //处理XML以获取贴吧集合。
            List<TiebaInfo> tiebas = new List<TiebaInfo>();
            using (StringReader sr = new StringReader(TiebasXhtmlText))
            {
                XmlReader tiebasReader;
                try
                {
                    tiebasReader = XmlReader.Create(sr);
                }
                catch (Exception ex)
                {
                    Popup("“我关注的贴吧”页解析出错：" + ex.Message);
                    exit(1);
                    return;
                }
                TiebaInfo tieba = new TiebaInfo();
                while (tiebasReader.Read())
                {
                    if (tiebasReader.NodeType == XmlNodeType.Element &&
                        tiebasReader.GetAttribute("href") != null &&
                        tiebasReader.Name == "a"
                        )
                    {
                        tieba.Href = tiebasReader.GetAttribute("href");
                        continue;
                    }
                    if (tiebasReader.NodeType == XmlNodeType.Text)
                    {
                        tieba.Name = tiebasReader.Value;
                        continue;
                    }
                    if (tiebasReader.NodeType == XmlNodeType.EndElement &&
                        tiebasReader.Name == "a"
                        )
                    {
                        tiebas.Add(tieba);
                        tieba = new TiebaInfo();
                        continue;
                    }
                }
            }

            //签到情况计数。
            int succ = 0, ignore = 0, err = tiebas.Count;

            //最多重试3次并记录上次错误。
            int retryRemain = 3, lastErr = err;
            while (err > 0 && retryRemain > 0)
            {
                //遍历新数组以免破坏迭代器。
                TiebaInfo[] tiebasArray = tiebas.ToArray();
                foreach (TiebaInfo tieba in tiebasArray)
                {
                    //休息一下降低失败率。
                    Thread.Sleep(1000);
                    SignResult result = tieba.SignIn();
                    Popup(result.Tip);
                    switch (result.Status)
                    {
                        case SignStatus.Ignored:
                            ignore++;
                            break;
                        case SignStatus.Successful:
                            succ++;
                            break;
                    }
                    if (result.Status != SignStatus.Error)
                    {
                        err--;
                        tiebas.Remove(tieba);
                    }
                }
                if (lastErr == err)
                {
                    retryRemain--;
                }
                else
                {
                    retryRemain = 2;
                    lastErr = err;
                }
            }

            //完成提示。
            string tip = string.Empty;
            if (succ != 0)
            {
                tip += "成功：" + succ + "个，";
            }
            if (ignore != 0)
            {
                tip += "跳过：" + ignore + "个，";
            }
            if (err != 0)
            {
                tip += "失败：" + err + "个，";
            }
            tip = tip.TrimEnd('，') + "。";
            Popup(tip);

            exit(err);
            return;
        }

        /// <summary>
        /// 退出程序。
        /// </summary>
        /// <param name="exitCode">错误码。</param>
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

    /// <summary>
    /// 贴吧类。
    /// </summary>
    internal class TiebaInfo
    {
        /// <summary>
        /// 贴吧名称。
        /// </summary>
        public string Name;

        /// <summary>
        /// 贴吧相对链接。
        /// </summary>
        public string Href;

        /// <summary>
        /// 签到结果。
        /// </summary>
        public SignResult Status;

        /// <summary>
        /// 立即签到。
        /// </summary>
        /// <returns>签到结果。</returns>
        public SignResult SignIn()
        {
            //获取贴吧文本。
            string tiebaString = string.Empty;
            try
            {
                tiebaString = HtmlWebHelper.SendGetRequest("http://wapp.baidu.com/" + this.Href, 5000);
            }
            catch (Exception ex)
            {
                this.Status = new SignResult(SignStatus.Error, "获取贴吧“" + this.Name + "”失败：" + ex.Message);
                return this.Status;
            }

            //判断签到情况。
            if (tiebaString.Contains("签到</a></td></tr></table>"))
            {
                //需要签到。
                //获取签到地址。
                string signUrl = string.Empty;
                signUrl = "http://wapp.baidu.com" + HtmlWebHelper.GetStringBetween(tiebaString, "href=\"", "\">签到<", rightToLeft: true);
                signUrl = HttpUtility.HtmlDecode(signUrl);

                string signedText = string.Empty;
                try
                {
                    signedText = HtmlWebHelper.SendGetRequest(signUrl, "http://wapp.baidu.com" + this.Href, 5000);
                }
                catch (Exception ex)
                {
                    this.Status = new SignResult(SignStatus.Error, "贴吧“" + this.Name + "”签到失败：" + ex.Message);
                    return this.Status;
                }
                if (signedText.Contains("<span class=\"light\">签到成功"))
                {
                    this.Status = new SignResult(SignStatus.Successful, "已签到：" + this.Name + "。");
                    return this.Status;
                }
                else
                {
                    this.Status = new SignResult(SignStatus.Error, "错误：" + this.Name);
                    return this.Status;
                }
            }

            string signText = string.Empty;
            signText = HtmlWebHelper.GetStringBetween(tiebaString, ">", "</span></td></tr>", rightToLeft: true);

            //已经签到了，跳过。
            if (signText == "已签到")
            {
                this.Status = new SignResult(SignStatus.Ignored, "跳过：" + this.Name);
                return this.Status;
            }

            //出乎意料的情况。
            this.Status = new SignResult(SignStatus.Error, "获取贴吧“" + this.Name + "”失败：" + signText);
            return this.Status;
        }
    }

    /// <summary>
    /// 签到结果。
    /// </summary>
    public struct SignResult
    {
        /// <summary>
        /// 初始化签到结果。
        /// </summary>
        /// <param name="status">签到状态。</param>
        /// <param name="tip">提示文本。</param>
        public SignResult(SignStatus status, string tip)
        {
            Status = status;
            Tip = tip;
        }
        
        /// <summary>
        /// 签到状态。
        /// </summary>
        public readonly SignStatus Status;

        /// <summary>
        /// 提示文本。
        /// </summary>
        public readonly string Tip;
    }

    /// <summary>
    /// 签到状态。
    /// </summary>
    public enum SignStatus
    {
        /// <summary>
        /// 成功。
        /// </summary>
        Successful,

        /// <summary>
        /// 出错。
        /// </summary>
        Error,

        /// <summary>
        /// 跳过。
        /// </summary>
        Ignored
    }
}
