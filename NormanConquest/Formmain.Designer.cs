namespace NormanConquest
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            boxLog = new TextBox();
            SuspendLayout();
            // 
            // boxLog
            // 
            boxLog.Location = new Point(3, 3);
            boxLog.Multiline = true;
            boxLog.Name = "boxLog";
            boxLog.ScrollBars = ScrollBars.Vertical;
            boxLog.Size = new Size(381, 235);
            boxLog.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1175, 824);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NormanConquest";
            Load += FormMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private TextBox boxLog;
    }
}
