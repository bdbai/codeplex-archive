Imports System.IO
Imports System.Collections.ObjectModel
Imports TagLib
Imports System.Diagnostics
Imports System.Threading
Public Class Load
    Dim caches As New Collection(Of MusicCache)
    Dim loadthr As New Thread(AddressOf LoadMusicCaches)
    Private Sub Load_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadthr.Start()
    End Sub
    Private Sub LoadMusicCaches()
startload:
        BeginInvoke(Sub()
                        Label1.Text = "正在进入缓存"
                    End Sub)
        If Not Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Packages\") Then
            MsgBox("瞎凑热闹")
            loadthr.Abort()
            Application.Exit()
        End If
        For Each d As String In Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Packages\")
            If Path.GetFileName(d).StartsWith("BaiduMusic") Then
                If Not Directory.Exists(d & "\AC\INetCache\") Then Directory.CreateDirectory(d & "\AC\INetCache\")
                BeginInvoke(Sub()
                                Label1.Text = "正在枚举文件"
                            End Sub)
                MusicCache.LoadCaches(d & "\AC\INetCache\", caches)
                If caches.Count = 0 Then
                    Invoke(Sub()
                               MsgBox(String.Format("您还没有缓存哦，{0}打开百度音乐win8版听点歌吧。", vbCrLf), MsgBoxStyle.Information)
                               loadthr.Abort()
                               Application.Exit()
                           End Sub)
                End If
                Dim cf As New CacheForm(caches)
                BeginInvoke(Sub()
                                cf.Show()
                                Me.Close()
                                Me.Dispose()
                                loadthr.Abort()
                            End Sub)
                Exit Sub
            End If
        Next

        If MsgBox("看起来你没装度娘音乐Win8版哦" & vbCrLf & "现在去装？", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Process.Start("ms-windows-store:PDP?PFN=BaiduMusic.13872B549AB1A_mnx5vnspd98s0")
            If MsgBox("我装好了？", MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                GoTo startload
            End If
        Else
            loadthr.Abort()
            Application.Exit()
        End If
    End Sub

End Class
