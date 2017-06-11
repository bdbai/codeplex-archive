# XiamiSig

虾米自动签到程序。

看到网上有各种语言做虾米签到的，自己也用 C# 做一个。

## Usage

将程序（.exe）和COM组件（.dll）放在同一个文件夹里面，运行程序，输入你的虾米邮箱和密码，加至自启即可。这样第一次配置就完成了，系统启动的时候会自动为你签到，并且图标会弹出气泡来提醒你签到完成与否。
若要编辑虾米帐户信息，重新运行一次该程序。程序放置于任意位置均可，但同目录下必须有COM组件。

详细内容请见博客：

[http://bdbai.22web.org/blog/archives/167](http://bdbai.22web.org/blog/archives/167)

## System Requirements

- Windows 8 或以上
- Windows 8 以下的系统需要 .NET Framework 4.0
- Visual Studio 2013 (dev only)

