<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CacheForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CacheForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.缓存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.导出选中ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.导出所有ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.读取ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清除缓存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.关于ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.导出EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ExAllButton = New System.Windows.Forms.Button()
        Me.ExSelButton = New System.Windows.Forms.Button()
        Me.ExCurButton = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.SizeLabel = New System.Windows.Forms.Label()
        Me.WarningLabel = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.AlbumLabel = New System.Windows.Forms.Label()
        Me.ArtistLabel = New System.Windows.Forms.Label()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.缓存ToolStripMenuItem, Me.读取ToolStripMenuItem, Me.关于ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(624, 26)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '缓存ToolStripMenuItem
        '
        Me.缓存ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.导出选中ToolStripMenuItem, Me.导出所有ToolStripMenuItem})
        Me.缓存ToolStripMenuItem.Name = "缓存ToolStripMenuItem"
        Me.缓存ToolStripMenuItem.Size = New System.Drawing.Size(48, 22)
        Me.缓存ToolStripMenuItem.Text = "音乐"
        '
        '导出选中ToolStripMenuItem
        '
        Me.导出选中ToolStripMenuItem.Name = "导出选中ToolStripMenuItem"
        Me.导出选中ToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.导出选中ToolStripMenuItem.Text = "导出选中…"
        '
        '导出所有ToolStripMenuItem
        '
        Me.导出所有ToolStripMenuItem.Name = "导出所有ToolStripMenuItem"
        Me.导出所有ToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.导出所有ToolStripMenuItem.Text = "导出所有…"
        '
        '读取ToolStripMenuItem
        '
        Me.读取ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.清除缓存ToolStripMenuItem})
        Me.读取ToolStripMenuItem.Name = "读取ToolStripMenuItem"
        Me.读取ToolStripMenuItem.Size = New System.Drawing.Size(48, 22)
        Me.读取ToolStripMenuItem.Text = "缓存"
        '
        '清除缓存ToolStripMenuItem
        '
        Me.清除缓存ToolStripMenuItem.Name = "清除缓存ToolStripMenuItem"
        Me.清除缓存ToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.清除缓存ToolStripMenuItem.Text = "清除缓存"
        '
        '关于ToolStripMenuItem
        '
        Me.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem"
        Me.关于ToolStripMenuItem.Size = New System.Drawing.Size(48, 22)
        Me.关于ToolStripMenuItem.Text = "关于"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 29)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(600, 398)
        Me.SplitContainer1.SplitterDistance = 285
        Me.SplitContainer1.TabIndex = 1
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.FullRowSelect = True
        ListViewItem1.Checked = True
        ListViewItem1.StateImageIndex = 1
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.ShowGroups = False
        Me.ListView1.Size = New System.Drawing.Size(282, 395)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "名称"
        Me.ColumnHeader2.Width = 137
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "艺术家"
        Me.ColumnHeader3.Width = 74
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "大小"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.导出EToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.ShowImageMargin = False
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(107, 26)
        '
        '导出EToolStripMenuItem
        '
        Me.导出EToolStripMenuItem.Name = "导出EToolStripMenuItem"
        Me.导出EToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.导出EToolStripMenuItem.Text = "导出...(&E)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(305, 392)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "详细信息"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.ExAllButton)
        Me.GroupBox4.Controls.Add(Me.ExSelButton)
        Me.GroupBox4.Controls.Add(Me.ExCurButton)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 246)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(293, 140)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "操作"
        '
        'ExAllButton
        '
        Me.ExAllButton.Location = New System.Drawing.Point(6, 91)
        Me.ExAllButton.Name = "ExAllButton"
        Me.ExAllButton.Size = New System.Drawing.Size(90, 29)
        Me.ExAllButton.TabIndex = 3
        Me.ExAllButton.Text = "导出所有"
        Me.ExAllButton.UseVisualStyleBackColor = True
        '
        'ExSelButton
        '
        Me.ExSelButton.Location = New System.Drawing.Point(6, 56)
        Me.ExSelButton.Name = "ExSelButton"
        Me.ExSelButton.Size = New System.Drawing.Size(90, 29)
        Me.ExSelButton.TabIndex = 2
        Me.ExSelButton.Text = "导出选中"
        Me.ExSelButton.UseVisualStyleBackColor = True
        '
        'ExCurButton
        '
        Me.ExCurButton.Location = New System.Drawing.Point(6, 21)
        Me.ExCurButton.Name = "ExCurButton"
        Me.ExCurButton.Size = New System.Drawing.Size(90, 29)
        Me.ExCurButton.TabIndex = 1
        Me.ExCurButton.Text = "导出当前"
        Me.ExCurButton.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.SizeLabel)
        Me.GroupBox3.Controls.Add(Me.WarningLabel)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 148)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(293, 92)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "缓存"
        '
        'SizeLabel
        '
        Me.SizeLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SizeLabel.AutoSize = True
        Me.SizeLabel.Location = New System.Drawing.Point(6, 57)
        Me.SizeLabel.Name = "SizeLabel"
        Me.SizeLabel.Size = New System.Drawing.Size(14, 14)
        Me.SizeLabel.TabIndex = 2
        Me.SizeLabel.Text = "1"
        '
        'WarningLabel
        '
        Me.WarningLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarningLabel.AutoSize = True
        Me.WarningLabel.Font = New System.Drawing.Font("宋体", 8.830189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.WarningLabel.ForeColor = System.Drawing.Color.Red
        Me.WarningLabel.Location = New System.Drawing.Point(6, 28)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Size = New System.Drawing.Size(157, 14)
        Me.WarningLabel.TabIndex = 1
        Me.WarningLabel.Text = "该文件没有缓冲完毕！"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.AlbumLabel)
        Me.GroupBox2.Controls.Add(Me.ArtistLabel)
        Me.GroupBox2.Controls.Add(Me.TitleLabel)
        Me.GroupBox2.Controls.Add(Me.PictureBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(293, 121)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "音乐"
        '
        'AlbumLabel
        '
        Me.AlbumLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlbumLabel.AutoSize = True
        Me.AlbumLabel.Location = New System.Drawing.Point(103, 82)
        Me.AlbumLabel.Name = "AlbumLabel"
        Me.AlbumLabel.Size = New System.Drawing.Size(14, 14)
        Me.AlbumLabel.TabIndex = 3
        Me.AlbumLabel.Text = "2"
        '
        'ArtistLabel
        '
        Me.ArtistLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArtistLabel.AutoSize = True
        Me.ArtistLabel.Location = New System.Drawing.Point(102, 51)
        Me.ArtistLabel.Name = "ArtistLabel"
        Me.ArtistLabel.Size = New System.Drawing.Size(14, 14)
        Me.ArtistLabel.TabIndex = 2
        Me.ArtistLabel.Text = "1"
        '
        'TitleLabel
        '
        Me.TitleLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("宋体", 8.830189!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(103, 22)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(15, 14)
        Me.TitleLabel.TabIndex = 1
        Me.TitleLabel.Text = "0"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(6, 21)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(90, 90)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "mp3"
        Me.SaveFileDialog1.Filter = "MP3 音乐文件|*.mp3"
        '
        'CacheForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 439)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "CacheForm"
        Me.Text = "度娘音乐缓存浏览"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 缓存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 读取ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 关于ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 导出选中ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 导出所有ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 清除缓存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents AlbumLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistLabel As System.Windows.Forms.Label
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents SizeLabel As System.Windows.Forms.Label
    Friend WithEvents WarningLabel As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 导出EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExAllButton As System.Windows.Forms.Button
    Friend WithEvents ExSelButton As System.Windows.Forms.Button
    Friend WithEvents ExCurButton As System.Windows.Forms.Button
End Class
