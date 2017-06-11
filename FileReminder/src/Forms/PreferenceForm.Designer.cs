namespace FileReminder
{
    partial class PreferenceForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferenceForm));
            this.todayGroupBox = new System.Windows.Forms.GroupBox();
            this.todayLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.reminderListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dayComboBox = new System.Windows.Forms.ComboBox();
            this.settingGroupBox = new System.Windows.Forms.GroupBox();
            this.modifyButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.todayGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.settingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // todayGroupBox
            // 
            this.todayGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.todayGroupBox.Controls.Add(this.todayLabel);
            this.todayGroupBox.Controls.Add(this.label1);
            this.todayGroupBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.todayGroupBox.Location = new System.Drawing.Point(14, 5);
            this.todayGroupBox.Name = "todayGroupBox";
            this.todayGroupBox.Size = new System.Drawing.Size(432, 76);
            this.todayGroupBox.TabIndex = 0;
            this.todayGroupBox.TabStop = false;
            this.todayGroupBox.Text = "FileReminder";
            this.todayGroupBox.Click += new System.EventHandler(this.todayLabel_Click);
            // 
            // todayLabel
            // 
            this.todayLabel.AutoSize = true;
            this.todayLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.todayLabel.Location = new System.Drawing.Point(7, 52);
            this.todayLabel.Name = "todayLabel";
            this.todayLabel.Size = new System.Drawing.Size(55, 15);
            this.todayLabel.TabIndex = 1;
            this.todayLabel.Text = "label2";
            this.todayLabel.Click += new System.EventHandler(this.todayLabel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "用一个文件纪念特殊的日子。";
            this.label1.Click += new System.EventHandler(this.todayLabel_Click);
            // 
            // monthComboBox
            // 
            this.monthComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Items.AddRange(new object[] {
            "月份",
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月"});
            this.monthComboBox.Location = new System.Drawing.Point(3, 3);
            this.monthComboBox.MaxDropDownItems = 13;
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(199, 23);
            this.monthComboBox.TabIndex = 2;
            this.monthComboBox.SelectedIndexChanged += new System.EventHandler(this.monthComboBox_SelectedIndexChanged);
            // 
            // reminderListView
            // 
            this.reminderListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reminderListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.reminderListView.FullRowSelect = true;
            this.reminderListView.LabelWrap = false;
            this.reminderListView.Location = new System.Drawing.Point(14, 60);
            this.reminderListView.MultiSelect = false;
            this.reminderListView.Name = "reminderListView";
            this.reminderListView.Size = new System.Drawing.Size(407, 116);
            this.reminderListView.TabIndex = 3;
            this.reminderListView.UseCompatibleStateImageBehavior = false;
            this.reminderListView.View = System.Windows.Forms.View.Details;
            this.reminderListView.DoubleClick += new System.EventHandler(this.reminderListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "月";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日";
            this.columnHeader2.Width = 40;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "标题";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "已启用";
            this.columnHeader4.Width = 68;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "文件";
            this.columnHeader5.Width = 80;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dayComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.monthComboBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 30);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // dayComboBox
            // 
            this.dayComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayComboBox.FormattingEnabled = true;
            this.dayComboBox.Items.AddRange(new object[] {
            "月份",
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月"});
            this.dayComboBox.Location = new System.Drawing.Point(208, 3);
            this.dayComboBox.MaxDropDownItems = 32;
            this.dayComboBox.Name = "dayComboBox";
            this.dayComboBox.Size = new System.Drawing.Size(200, 23);
            this.dayComboBox.TabIndex = 3;
            this.dayComboBox.SelectedIndexChanged += new System.EventHandler(this.dayComboBox_SelectedIndexChanged);
            // 
            // settingGroupBox
            // 
            this.settingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingGroupBox.Controls.Add(this.modifyButton);
            this.settingGroupBox.Controls.Add(this.deleteButton);
            this.settingGroupBox.Controls.Add(this.addButton);
            this.settingGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.settingGroupBox.Controls.Add(this.reminderListView);
            this.settingGroupBox.Location = new System.Drawing.Point(14, 88);
            this.settingGroupBox.Name = "settingGroupBox";
            this.settingGroupBox.Size = new System.Drawing.Size(432, 221);
            this.settingGroupBox.TabIndex = 5;
            this.settingGroupBox.TabStop = false;
            this.settingGroupBox.Text = "设置提醒";
            // 
            // modifyButton
            // 
            this.modifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modifyButton.Location = new System.Drawing.Point(243, 183);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(86, 25);
            this.modifyButton.TabIndex = 7;
            this.modifyButton.Text = "修改";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(336, 183);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(86, 25);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "删除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(151, 183);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(86, 25);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "添加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // PreferenceForm
            // 
            this.AcceptButton = this.modifyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 321);
            this.Controls.Add(this.settingGroupBox);
            this.Controls.Add(this.todayGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(409, 318);
            this.Name = "PreferenceForm";
            this.Text = "FileReminder 设置";
            this.Load += new System.EventHandler(this.PreferenceForm_Load);
            this.todayGroupBox.ResumeLayout(false);
            this.todayGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.settingGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox todayGroupBox;
        private System.Windows.Forms.Label todayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.ListView reminderListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox dayComboBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox settingGroupBox;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;

    }
}

