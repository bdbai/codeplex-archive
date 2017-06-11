namespace FileReminder
{
    partial class TodayReminderView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.doButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.Controls.Add(this.doButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fileLabel, 0, 1);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.raiseClick);
            this.tableLayoutPanel1.MouseEnter += new System.EventHandler(this.c_MouseEnter);
            this.tableLayoutPanel1.MouseLeave += new System.EventHandler(this.c_MouseLeave);
            // 
            // doButton
            // 
            this.doButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.doButton.Location = new System.Drawing.Point(212, 32);
            this.doButton.Name = "doButton";
            this.tableLayoutPanel1.SetRowSpan(this.doButton, 2);
            this.doButton.Size = new System.Drawing.Size(75, 23);
            this.doButton.TabIndex = 0;
            this.doButton.Text = "打开";
            this.doButton.UseVisualStyleBackColor = true;
            this.doButton.Click += new System.EventHandler(this.raiseClick);
            this.doButton.MouseEnter += new System.EventHandler(this.c_MouseEnter);
            this.doButton.MouseLeave += new System.EventHandler(this.c_MouseLeave);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.BackColor = System.Drawing.Color.Transparent;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel.Font = new System.Drawing.Font("微软雅黑", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(5, 2);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(197, 41);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "label1";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.nameLabel.Click += new System.EventHandler(this.raiseClick);
            this.nameLabel.MouseEnter += new System.EventHandler(this.c_MouseEnter);
            this.nameLabel.MouseLeave += new System.EventHandler(this.c_MouseLeave);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileLabel.Location = new System.Drawing.Point(5, 45);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(197, 41);
            this.fileLabel.TabIndex = 2;
            this.fileLabel.Text = "label1";
            this.fileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileLabel.Click += new System.EventHandler(this.raiseClick);
            this.fileLabel.MouseEnter += new System.EventHandler(this.c_MouseEnter);
            this.fileLabel.MouseLeave += new System.EventHandler(this.c_MouseLeave);
            // 
            // TodayReminderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TodayReminderView";
            this.Size = new System.Drawing.Size(294, 88);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button doButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label fileLabel;
    }
}
