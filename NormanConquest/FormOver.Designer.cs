namespace NormanConquest
{
    partial class FormOver
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
            labelIcon = new Label();
            labelDesc = new Label();
            buttonRestart = new Button();
            SuspendLayout();
            // 
            // labelIcon
            // 
            labelIcon.AutoSize = true;
            labelIcon.Font = new Font("Microsoft YaHei UI", 72F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelIcon.ForeColor = Color.Gold;
            labelIcon.Location = new Point(449, 79);
            labelIcon.Name = "labelIcon";
            labelIcon.Size = new Size(271, 188);
            labelIcon.TabIndex = 0;
            labelIcon.Text = "🏆";
            // 
            // labelDesc
            // 
            labelDesc.AutoSize = true;
            labelDesc.Font = new Font("Microsoft YaHei UI", 72F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelDesc.Location = new Point(276, 296);
            labelDesc.Name = "labelDesc";
            labelDesc.Size = new Size(655, 188);
            labelDesc.TabIndex = 1;
            labelDesc.Text = "你赢了！";
            // 
            // buttonRestart
            // 
            buttonRestart.Font = new Font("Microsoft YaHei UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 134);
            buttonRestart.Location = new Point(409, 520);
            buttonRestart.Name = "buttonRestart";
            buttonRestart.Size = new Size(322, 106);
            buttonRestart.TabIndex = 2;
            buttonRestart.Text = "重来";
            buttonRestart.UseVisualStyleBackColor = true;
            buttonRestart.Click += buttonRestart_Click;
            // 
            // FormOver
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1188, 708);
            Controls.Add(buttonRestart);
            Controls.Add(labelDesc);
            Controls.Add(labelIcon);
            Cursor = Cursors.Cross;
            DoubleBuffered = true;
            Name = "FormOver";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormOver";
            WindowState = FormWindowState.Maximized;
            Resize += FormOver_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelIcon;
        private Label labelDesc;
        private Button buttonRestart;
    }
}