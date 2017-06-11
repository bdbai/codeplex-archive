namespace FileReminder
{
    partial class TodayForm
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
            this.todayLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // todayLabel
            // 
            this.todayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.todayLabel.AutoSize = true;
            this.todayLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.todayLabel.Location = new System.Drawing.Point(14, 10);
            this.todayLabel.Name = "todayLabel";
            this.todayLabel.Size = new System.Drawing.Size(55, 15);
            this.todayLabel.TabIndex = 2;
            this.todayLabel.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(14, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 214);
            this.panel1.TabIndex = 5;
            // 
            // TodayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 263);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.todayLabel);
            this.MinimumSize = new System.Drawing.Size(450, 310);
            this.Name = "TodayForm";
            this.ShowIcon = false;
            this.Text = "今日事项";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TodayForm_FormClosing);
            this.Load += new System.EventHandler(this.TodayForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label todayLabel;
        private System.Windows.Forms.Panel panel1;
    }
}