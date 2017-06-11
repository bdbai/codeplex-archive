Imports System.IO
Imports System.Diagnostics
Imports System.Collections.ObjectModel
Public Class Export
    Dim ParentCacheForm As CacheForm
    Dim caches As Collection(Of MusicCache)
    Public Sub New(cachesToExp As Collection(Of MusicCache), ByRef _ParentCacheForm As CacheForm)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        ParentCacheForm = _ParentCacheForm
        caches = cachesToExp
        ShowCaches(caches)
    End Sub
    Private Sub ShowCaches(cachesToShow As Collection(Of MusicCache))
         ListView1.Items.Clear()
        For Each mc As MusicCache In cachesToShow
            Dim outputname As String
            outputname = String.Format(TextBox2.Text.Replace("%t", "{0}").Replace("%r", "{1}").Replace("%a", "{2}") & ".mp3", mc.Name, mc.Artist, mc.Album)
            ListView1.Items.Add(New ListViewItem({"挂起", outputname, mc.FilePath}) With {.Tag = mc})
            ListView1.Items(ListView1.Items.Count - 1).Checked = True
        Next
    End Sub

    Private Sub Export_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()

    End Sub

    Private Sub Export_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CacheForm.SetControlsEnabled(ParentCacheForm, True)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text.Split(" ".ToCharArray, StringSplitOptions.RemoveEmptyEntries).Count = 0 OrElse TextBox2.Text.Split(" ".ToCharArray, StringSplitOptions.RemoveEmptyEntries).Count = 0 Then
            MsgBox("请选择文件夹并输入文件名表达式！", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim cs As New Collection(Of MusicCache)
        For Each item As ListViewItem In ListView1.Items
            If item.Checked AndAlso TypeOf item.Tag Is MusicCache Then
                Dim mc As MusicCache = item.Tag
                cs.Add(mc)
            End If
        Next
        If cs.Count = 0 Then
            MsgBox("请勾选要导出的歌曲！", MsgBoxStyle.Critical)
        Else
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        BeginInvoke(Sub()
                        Cursor = Cursors.WaitCursor
                        Me.Enabled = False
                    End Sub)
        Dim CachesToExport As New Collection(Of MusicCache)
        Dim ItemsToShow As New Collection(Of ListViewItem)
        Dim outpathpattern As String = ""
        Invoke(Sub(ByRef cte As Collection(Of MusicCache), ByRef its As Collection(Of ListViewItem))
                   outpathpattern = String.Format("{0}\{1}", TextBox1.Text, TextBox2.Text)
                   For Each item As ListViewItem In ListView1.Items
                       If item.Checked AndAlso TypeOf item.Tag Is MusicCache Then
                           its.Add(item)
                           cte.Add(item.Tag)
                       End If
                   Next
               End Sub, CachesToExport, ItemsToShow)
        Dim thisoutpath As String = ""
        For i = 0 To CachesToExport.Count - 1
            thisoutpath = String.Format(outpathpattern.Replace("%t", "{0}").Replace("%r", "{1}").Replace("%a", "{2}") & ".mp3", CachesToExport(i).Name, CachesToExport(i).Artist, CachesToExport(i).Album)
            Try
                CachesToExport(i).WriteToFile(thisoutpath)
                BeginInvoke(Sub(num As Integer)
                                ItemsToShow(num).SubItems(0).Text = "完成"
                                ItemsToShow(num).Checked = False
                            End Sub, i)
            Catch ex As Exception
                BeginInvoke(Sub(num As Integer) ItemsToShow(num).SubItems(0).Text = ex.Message, i)
            End Try
        Next
        Threading.Thread.Sleep(1000)
        BeginInvoke(Sub()
                        Cursor = Cursors.Default
                        Me.Enabled = True
                    End Sub)
        Process.Start(Path.GetDirectoryName(outpathpattern) & "\")
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Return Then ShowCaches(caches)
    End Sub
End Class