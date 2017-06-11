Imports System.Collections.ObjectModel
Imports System.IO
Public Class CacheForm

    Private Sub 关于ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 关于ToolStripMenuItem.Click
        AboutBox1.ShowDialog()

    End Sub
    Dim caches As Collection(Of MusicCache)
    Public Sub New(ByVal cs As Collection(Of MusicCache))

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        caches = cs
    End Sub

    Private Sub CacheForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not Visibility Then
            MsgBox("请关闭导出对话框！")
            e.Cancel = True
        End If
    End Sub

    Private Sub CacheForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ListView1.Items.Clear()
        For Each mc As MusicCache In caches
            If Not mc.State = LoadState.ErrorOcc Then
                mc.Num = ListView1.Items.Count
                ListView1.Items.Add(New ListViewItem({mc.Name, mc.Artist, mc.Size}))
                ListView1.Items(ListView1.Items.Count - 1).Tag = mc
                ListView1.Items(ListView1.Items.Count - 1).Checked = True
                CacheChangedToListView(mc)
                If mc.State = LoadState.Loading Then AddHandler mc.LoadingFinished, AddressOf CacheChangedToListView
            End If
        Next
        ListView1.Items(0).Selected = True
        ListView1_MouseClick(Nothing, Nothing)
    End Sub

    Private Sub CacheChangedToListView(sender As MusicCache)
        RemoveHandler sender.LoadingFinished, AddressOf CacheChangedToListView
        Select Case sender.State
            Case LoadState.Finish
                BeginInvoke(Sub()
                                ListView1.Items(sender.Num).SubItems(0).Text = sender.Name
                                ListView1.Items(sender.Num).SubItems(1).Text = sender.Artist
                                ListView1.Items(sender.Num).SubItems(2).Text = sender.Size
                            End Sub)
                If sender.Completed = False Then
                    BeginInvoke(Sub()
                                    ListView1.Items(sender.Num).Checked = False
                                End Sub)
                End If
        End Select
    End Sub

    Private Sub DisplayingCacheChanged(sender As MusicCache)
        RemoveHandler sender.LoadingFinished, AddressOf DisplayingCacheChanged
        BeginInvoke(Sub(mc As MusicCache) CacheChangedToPanel(mc), sender)
    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As EventArgs) Handles ListView1.MouseClick
        For Each row As ListViewItem In ListView1.Items
            If row.Selected AndAlso TypeOf row.Tag Is MusicCache Then
                Dim cache As MusicCache = row.Tag
                ContextMenuStrip1.Items(0).Tag = cache
                CacheChangedToPanel(cache)
                If cache.State = LoadState.Loading Then AddHandler cache.LoadingFinished, AddressOf DisplayingCacheChanged
                Exit For
            End If
        Next
    End Sub

    Private Sub CacheChangedToPanel(mc As MusicCache)
        Dim pic As Image = mc.AlbumPicture
        If IsNothing(pic) Then
            TitleLabel.Left = 6
            ArtistLabel.Left = 6
            AlbumLabel.Left = 6
            PictureBox1.Hide()
        Else
            TitleLabel.Left = 103
            ArtistLabel.Left = 103
            AlbumLabel.Left = 103
            PictureBox1.Image = pic
            PictureBox1.Show()
        End If
        TitleLabel.Text = mc.Name
        ArtistLabel.Text = mc.Artist
        AlbumLabel.Text = mc.Album
        WarningLabel.Text = mc.LoadingString
        SizeLabel.Text = mc.Size
        Select Case mc.State
            Case LoadState.Loading
                WarningLabel.ForeColor = DefaultForeColor
            Case LoadState.Finish
                With WarningLabel
                    If mc.Completed Then
                        .ForeColor = DefaultForeColor
                    Else
                        .Text = "该文件没有缓冲完毕！"
                        .ForeColor = Color.Red
                    End If
                End With
            Case LoadState.ErrorOcc
                WarningLabel.ForeColor = Color.Red
                SizeLabel.Text = ""
        End Select

        ExCurButton.Tag = mc
    End Sub

    Private Sub 清除缓存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 清除缓存ToolStripMenuItem.Click
        If MsgBox("你确定删除所有缓存文件？", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For Each d As String In Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\Packages\")
                If Path.GetFileName(d).StartsWith("BaiduMusic") Then
                    Try
                        delf(d & "\AC\INetCache\")
                        Application.Restart()
                    Catch ex As Exception
                        MsgBox("无法清除。" & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                    End Try
                End If
            Next
            MsgBox("找不到缓存文件夹！", MsgBoxStyle.Exclamation)
        End If
    End Sub
    Private Sub delf(ByVal d As String)
        For Each f As String In Directory.GetFiles(d)
            Try
                File.Delete(f)
            Catch ex As Exception

            End Try
        Next
        For Each f As String In Directory.GetDirectories(d)
            delf(f)
        Next
        Try
            Directory.Delete(d)
        Catch ex As Exception
            Debug.WriteLine("")
        End Try
    End Sub

    Public Sub Export(cache As MusicCache)
        Select Case cache.State
            Case LoadState.ErrorOcc
                MsgBox("读取错误。" & vbCrLf & cache.LoadingString, MsgBoxStyle.Critical)
            Case LoadState.Loading
                MsgBox("读取未完成，请稍后再试。", MsgBoxStyle.Information)
            Case LoadState.Finish
                If cache.Completed = True Or (cache.Completed = False AndAlso MsgBox(String.Format("该文件没有缓冲完整。{0}是否继续？", vbCrLf), MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes) Then
                    SaveFileDialog1.FileName = cache.Name
                    If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Try
                            cache.WriteToFile(SaveFileDialog1.FileName)
                            MsgBox("导出成功！", MsgBoxStyle.Information)
                        Catch ex As Exception
                            MsgBox(String.Format("导出失败。{0}{1}", vbCrLf, ex.Message), MsgBoxStyle.Critical)
                        End Try
                    End If
                End If
        End Select

    End Sub
    Private Sub ExCurButton_Click(sender As Object, e As EventArgs) Handles ExCurButton.Click
        If (Not IsNothing(ExCurButton.Tag)) And TypeOf ExCurButton.Tag Is MusicCache Then
            Dim mc As MusicCache = ExCurButton.Tag
            Export(mc)
        End If
    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        Dim s As Boolean = False
        If IsNothing(ListView1.Columns(e.Column).Tag) Then
            ListView1.Columns(e.Column).Tag = False
        Else
            s = Not ListView1.Columns(e.Column).Tag
            ListView1.Columns(e.Column).Tag = Not ListView1.Columns(e.Column).Tag
        End If

        ListView1.ListViewItemSorter = New ListViewSorter(s, e.Column)
        ListView1.Sort()
    End Sub
    Private Class ListViewSorter
        Implements IComparer

        Dim desc As Boolean
        Dim col As Integer
        Public Sub New(_desc As Boolean, _col As Integer)
            desc = _desc
            col = _col
        End Sub
        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim result As Integer
            If col = 2 Then
                result = Val(x.SubItems(col).Text) - Val(y.SubItems(col).Text)
            Else
                result = String.Compare(x.SubItems(col).Text, y.SubItems(col).Text)
            End If
            If desc Then Return -result Else Return result
        End Function
    End Class

    Private Sub 导出EToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 导出EToolStripMenuItem.Click
        Dim tag = ContextMenuStrip1.Items(0).Tag
        If (Not IsNothing(tag)) AndAlso TypeOf tag Is MusicCache Then
            Dim mc As MusicCache = tag
            Export(mc)
        End If
    End Sub

    Private Sub ExportCaches(ByVal ExAll As Boolean)
        Dim cs As New Collection(Of MusicCache)
        For Each item As ListViewItem In ListView1.Items
            If (ExAll Or item.Checked) AndAlso TypeOf item.Tag Is MusicCache Then
                Dim mc As MusicCache = item.Tag
                cs.Add(mc)
            End If
        Next
        If cs.Count = 0 Then
            MsgBox("请勾选要导出的歌曲！", MsgBoxStyle.Critical)
        Else
            Dim exp As New Export(cs, Me)
            exp.Show()

            SetControlsEnabled(Me, False)

        End If
    End Sub

    Private Sub ExSelButton_Click(sender As Object, e As EventArgs) Handles ExSelButton.Click
        ExportCaches(False)
    End Sub

    Private Sub 导出选中ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 导出选中ToolStripMenuItem.Click
        ExportCaches(False)
    End Sub

    Public Visibility As Boolean = True
    Friend Shared Sub SetControlsEnabled(ByRef form As CacheForm, ByVal Visibility As Boolean)
        form.导出EToolStripMenuItem.Enabled = Visibility
        form.导出所有ToolStripMenuItem.Enabled = Visibility
        form.导出选中ToolStripMenuItem.Enabled = Visibility
        form.清除缓存ToolStripMenuItem.Enabled = Visibility
        form.ExCurButton.Enabled = Visibility
        form.ExSelButton.Enabled = Visibility
        form.ExAllButton.Enabled = Visibility
        form.Visibility = Visibility
    End Sub
End Class
