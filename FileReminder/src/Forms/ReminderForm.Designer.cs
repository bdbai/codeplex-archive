namespace FileReminder
{
    partial class ReminderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.dayComboBox = new System.Windows.Forms.ComboBox();
            this.filePickerButton = new System.Windows.Forms.Button();
            this.isFileLockedCheckBox = new System.Windows.Forms.CheckBox();
            this.isEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.acceptButton, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(120, 235);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(161, 30);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(83, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(3, 3);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(74, 23);
            this.acceptButton.TabIndex = 0;
            this.acceptButton.Text = "确定";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Location = new System.Drawing.Point(60, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(221, 23);
            this.nameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "标题";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "月";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "日";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "文件";
            // 
            // monthComboBox
            // 
            this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Items.AddRange(new object[] {
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
            this.monthComboBox.Location = new System.Drawing.Point(60, 52);
            this.monthComboBox.MaxDropDownItems = 13;
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(218, 22);
            this.monthComboBox.TabIndex = 6;
            this.monthComboBox.SelectedIndexChanged += new System.EventHandler(this.monthComboBox_SelectedIndexChanged);
            // 
            // dayComboBox
            // 
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
            this.dayComboBox.Location = new System.Drawing.Point(60, 91);
            this.dayComboBox.MaxDropDownItems = 32;
            this.dayComboBox.Name = "dayComboBox";
            this.dayComboBox.Size = new System.Drawing.Size(218, 22);
            this.dayComboBox.TabIndex = 7;
            // 
            // filePickerButton
            // 
            this.filePickerButton.Location = new System.Drawing.Point(60, 130);
            this.filePickerButton.Name = "filePickerButton";
            this.filePickerButton.Size = new System.Drawing.Size(218, 23);
            this.filePickerButton.TabIndex = 8;
            this.filePickerButton.Text = "选择一个有意义的文件。";
            this.filePickerButton.UseVisualStyleBackColor = true;
            this.filePickerButton.Click += new System.EventHandler(this.filePickerButton_Click);
            // 
            // isFileLockedCheckBox
            // 
            this.isFileLockedCheckBox.AutoSize = true;
            this.isFileLockedCheckBox.Checked = true;
            this.isFileLockedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isFileLockedCheckBox.Location = new System.Drawing.Point(15, 171);
            this.isFileLockedCheckBox.Name = "isFileLockedCheckBox";
            this.isFileLockedCheckBox.Size = new System.Drawing.Size(96, 18);
            this.isFileLockedCheckBox.TabIndex = 11;
            this.isFileLockedCheckBox.Text = "锁定该文件";
            this.isFileLockedCheckBox.UseVisualStyleBackColor = true;
            // 
            // isEnableCheckBox
            // 
            this.isEnableCheckBox.AutoSize = true;
            this.isEnableCheckBox.Checked = true;
            this.isEnableCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isEnableCheckBox.Location = new System.Drawing.Point(15, 199);
            this.isEnableCheckBox.Name = "isEnableCheckBox";
            this.isEnableCheckBox.Size = new System.Drawing.Size(96, 18);
            this.isEnableCheckBox.TabIndex = 12;
            this.isEnableCheckBox.Text = "备忘已启用";
            this.isEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "选择文件";
            // 
            // ReminderForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(293, 277);
            this.Controls.Add(this.isEnableCheckBox);
            this.Controls.Add(this.isFileLockedCheckBox);
            this.Controls.Add(this.filePickerButton);
            this.Controls.Add(this.dayComboBox);
            this.Controls.Add(this.monthComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReminderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "修改备忘";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReminderForm_FormClosing);
            this.Load += new System.EventHandler(this.ReminderForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.ComboBox dayComboBox;
        private System.Windows.Forms.Button filePickerButton;
        private System.Windows.Forms.CheckBox isFileLockedCheckBox;
        private System.Windows.Forms.CheckBox isEnableCheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}