namespace NormanConquest
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            labelStart1 = new Label();
            labelStart2 = new Label();
            label1 = new Label();
            linkLabelRule = new LinkLabel();
            SuspendLayout();
            // 
            // labelStart1
            // 
            labelStart1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelStart1.AutoSize = true;
            labelStart1.BackColor = Color.Transparent;
            labelStart1.Cursor = Cursors.Hand;
            labelStart1.Font = new Font("隶书", 22F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelStart1.ForeColor = Color.White;
            labelStart1.Image = Properties.Resources.选项滑块_Options_slider__中图_爱给网_aigei_com;
            labelStart1.Location = new Point(1164, 435);
            labelStart1.Name = "labelStart1";
            labelStart1.Size = new Size(239, 44);
            labelStart1.TabIndex = 0;
            labelStart1.Text = "以威廉开始";
            labelStart1.Click += labelStart1_Click;
            // 
            // labelStart2
            // 
            labelStart2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelStart2.AutoSize = true;
            labelStart2.BackColor = Color.Transparent;
            labelStart2.Cursor = Cursors.Hand;
            labelStart2.Font = new Font("隶书", 22F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelStart2.ForeColor = Color.White;
            labelStart2.Image = Properties.Resources.选项滑块_Options_slider__中图_爱给网_aigei_com;
            labelStart2.Location = new Point(1164, 520);
            labelStart2.Name = "labelStart2";
            labelStart2.Size = new Size(283, 44);
            labelStart2.TabIndex = 0;
            labelStart2.Text = "以哈罗德开始";
            labelStart2.Click += labelStart2_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Cursor = Cursors.Hand;
            label1.Font = new Font("隶书", 22F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Image = Properties.Resources.选项滑块_Options_slider__中图_爱给网_aigei_com;
            label1.Location = new Point(1164, 672);
            label1.Name = "label1";
            label1.Size = new Size(107, 44);
            label1.TabIndex = 0;
            label1.Text = "退出";
            label1.Click += label1_Click;
            // 
            // linkLabelRule
            // 
            linkLabelRule.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            linkLabelRule.AutoSize = true;
            linkLabelRule.BackColor = Color.Transparent;
            linkLabelRule.Cursor = Cursors.Hand;
            linkLabelRule.Font = new Font("隶书", 22F, FontStyle.Regular, GraphicsUnit.Point, 134);
            linkLabelRule.Image = Properties.Resources.选项滑块_Options_slider__中图_爱给网_aigei_com;
            linkLabelRule.Location = new Point(1164, 600);
            linkLabelRule.Name = "linkLabelRule";
            linkLabelRule.Size = new Size(195, 44);
            linkLabelRule.TabIndex = 1;
            linkLabelRule.TabStop = true;
            linkLabelRule.Text = "游戏规则";
            linkLabelRule.LinkClicked += linkLabelRule_LinkClicked;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1578, 844);
            Controls.Add(linkLabelRule);
            Controls.Add(label1);
            Controls.Add(labelStart2);
            Controls.Add(labelStart1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMenu";
            Text = "NormanConquest";
            WindowState = FormWindowState.Maximized;
            Load += FormMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStart1;
        private Label labelStart2;
        private Label label1;
        private LinkLabel linkLabelRule;
    }
}