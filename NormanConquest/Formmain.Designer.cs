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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            boxLog = new TextBox();
            tableLayoutMain = new TableLayoutPanel();
            panelOpponentInfo = new Panel();
            labelOpponentHP = new Label();
            labelOpponentPileCount = new Label();
            labelOpponentName = new Label();
            panelPlayerInfo = new Panel();
            buttonRefresh = new Button();
            buttonPassDefense = new Button();
            buttonEndTurn = new Button();
            labelPileCount = new Label();
            labelPlayerHP = new Label();
            labelPlayerName = new Label();
            panelGameInfo = new Panel();
            textBoxLog = new TextBox();
            flowOpponentHand = new FlowLayoutPanel();
            flowOpponentBuilding = new FlowLayoutPanel();
            flowPlayerHand = new FlowLayoutPanel();
            flowPlayerBuilding = new FlowLayoutPanel();
            tableLayoutMain.SuspendLayout();
            panelOpponentInfo.SuspendLayout();
            panelPlayerInfo.SuspendLayout();
            panelGameInfo.SuspendLayout();
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
            // tableLayoutMain
            // 
            tableLayoutMain.AutoScroll = true;
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(panelOpponentInfo, 0, 0);
            tableLayoutMain.Controls.Add(panelPlayerInfo, 0, 6);
            tableLayoutMain.Controls.Add(panelGameInfo, 0, 3);
            tableLayoutMain.Controls.Add(flowOpponentHand, 0, 1);
            tableLayoutMain.Controls.Add(flowOpponentBuilding, 0, 2);
            tableLayoutMain.Controls.Add(flowPlayerHand, 0, 5);
            tableLayoutMain.Controls.Add(flowPlayerBuilding, 0, 4);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 7;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 22.2222214F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutMain.Size = new Size(1395, 892);
            tableLayoutMain.TabIndex = 0;
            // 
            // panelOpponentInfo
            // 
            panelOpponentInfo.BackColor = Color.DarkGray;
            panelOpponentInfo.Controls.Add(labelOpponentHP);
            panelOpponentInfo.Controls.Add(labelOpponentPileCount);
            panelOpponentInfo.Controls.Add(labelOpponentName);
            panelOpponentInfo.Dock = DockStyle.Fill;
            panelOpponentInfo.Location = new Point(3, 3);
            panelOpponentInfo.Name = "panelOpponentInfo";
            panelOpponentInfo.Size = new Size(1389, 74);
            panelOpponentInfo.TabIndex = 0;
            // 
            // labelOpponentHP
            // 
            labelOpponentHP.AutoSize = true;
            labelOpponentHP.Location = new Point(136, 19);
            labelOpponentHP.Name = "labelOpponentHP";
            labelOpponentHP.Size = new Size(35, 24);
            labelOpponentHP.TabIndex = 0;
            labelOpponentHP.Text = "HP";
            // 
            // labelOpponentPileCount
            // 
            labelOpponentPileCount.AutoSize = true;
            labelOpponentPileCount.Location = new Point(202, 19);
            labelOpponentPileCount.Name = "labelOpponentPileCount";
            labelOpponentPileCount.Size = new Size(64, 24);
            labelOpponentPileCount.TabIndex = 0;
            labelOpponentPileCount.Text = "牌堆：";
            // 
            // labelOpponentName
            // 
            labelOpponentName.AutoSize = true;
            labelOpponentName.Location = new Point(37, 19);
            labelOpponentName.Name = "labelOpponentName";
            labelOpponentName.Size = new Size(82, 24);
            labelOpponentName.TabIndex = 0;
            labelOpponentName.Text = "对手名字";
            // 
            // panelPlayerInfo
            // 
            panelPlayerInfo.BackColor = Color.DarkGray;
            panelPlayerInfo.Controls.Add(buttonRefresh);
            panelPlayerInfo.Controls.Add(buttonPassDefense);
            panelPlayerInfo.Controls.Add(buttonEndTurn);
            panelPlayerInfo.Controls.Add(labelPileCount);
            panelPlayerInfo.Controls.Add(labelPlayerHP);
            panelPlayerInfo.Controls.Add(labelPlayerName);
            panelPlayerInfo.Dock = DockStyle.Fill;
            panelPlayerInfo.Location = new Point(3, 812);
            panelPlayerInfo.Name = "panelPlayerInfo";
            panelPlayerInfo.Size = new Size(1389, 77);
            panelPlayerInfo.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRefresh.Location = new Point(940, 1);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(145, 76);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "刷新";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonPassDefense
            // 
            buttonPassDefense.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPassDefense.Location = new Point(1091, 1);
            buttonPassDefense.Name = "buttonPassDefense";
            buttonPassDefense.Size = new Size(145, 76);
            buttonPassDefense.TabIndex = 2;
            buttonPassDefense.Text = "放弃抵御";
            buttonPassDefense.UseVisualStyleBackColor = true;
            buttonPassDefense.Visible = false;
            buttonPassDefense.Click += buttonPassDefense_Click;
            // 
            // buttonEndTurn
            // 
            buttonEndTurn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonEndTurn.BackgroundImageLayout = ImageLayout.Stretch;
            buttonEndTurn.Location = new Point(1239, 1);
            buttonEndTurn.Name = "buttonEndTurn";
            buttonEndTurn.Size = new Size(147, 76);
            buttonEndTurn.TabIndex = 1;
            buttonEndTurn.Text = "结束回合";
            buttonEndTurn.UseVisualStyleBackColor = true;
            buttonEndTurn.Click += buttonEndTurn_Click;
            // 
            // labelPileCount
            // 
            labelPileCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelPileCount.AutoSize = true;
            labelPileCount.Location = new Point(202, 30);
            labelPileCount.Name = "labelPileCount";
            labelPileCount.Size = new Size(64, 24);
            labelPileCount.TabIndex = 0;
            labelPileCount.Text = "牌堆：";
            // 
            // labelPlayerHP
            // 
            labelPlayerHP.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelPlayerHP.AutoSize = true;
            labelPlayerHP.Location = new Point(136, 30);
            labelPlayerHP.Name = "labelPlayerHP";
            labelPlayerHP.Size = new Size(39, 24);
            labelPlayerHP.TabIndex = 0;
            labelPlayerHP.Text = "HP:";
            // 
            // labelPlayerName
            // 
            labelPlayerName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelPlayerName.AutoSize = true;
            labelPlayerName.Location = new Point(37, 30);
            labelPlayerName.Name = "labelPlayerName";
            labelPlayerName.Size = new Size(82, 24);
            labelPlayerName.TabIndex = 0;
            labelPlayerName.Text = "玩家名字";
            // 
            // panelGameInfo
            // 
            panelGameInfo.BackColor = Color.LightGray;
            panelGameInfo.Controls.Add(textBoxLog);
            panelGameInfo.Dock = DockStyle.Fill;
            panelGameInfo.Location = new Point(3, 407);
            panelGameInfo.Name = "panelGameInfo";
            panelGameInfo.Size = new Size(1389, 75);
            panelGameInfo.TabIndex = 2;
            // 
            // textBoxLog
            // 
            textBoxLog.Dock = DockStyle.Fill;
            textBoxLog.Font = new Font("华文仿宋", 13.9999981F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBoxLog.Location = new Point(0, 0);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(1389, 75);
            textBoxLog.TabIndex = 1;
            // 
            // flowOpponentHand
            // 
            flowOpponentHand.AutoScroll = true;
            flowOpponentHand.BackgroundImage = Properties.Resources.纸顶_2_paper_top_2__爱给网_aigei_com;
            flowOpponentHand.BackgroundImageLayout = ImageLayout.Stretch;
            flowOpponentHand.Dock = DockStyle.Fill;
            flowOpponentHand.Location = new Point(3, 83);
            flowOpponentHand.Name = "flowOpponentHand";
            flowOpponentHand.Size = new Size(1389, 156);
            flowOpponentHand.TabIndex = 3;
            // 
            // flowOpponentBuilding
            // 
            flowOpponentBuilding.AutoScroll = true;
            flowOpponentBuilding.BackgroundImage = Properties.Resources.纸顶_2_paper_top_2__爱给网_aigei_com;
            flowOpponentBuilding.BackgroundImageLayout = ImageLayout.Stretch;
            flowOpponentBuilding.Dock = DockStyle.Fill;
            flowOpponentBuilding.Location = new Point(3, 245);
            flowOpponentBuilding.Name = "flowOpponentBuilding";
            flowOpponentBuilding.Size = new Size(1389, 156);
            flowOpponentBuilding.TabIndex = 4;
            // 
            // flowPlayerHand
            // 
            flowPlayerHand.AutoScroll = true;
            flowPlayerHand.BackgroundImage = Properties.Resources.纸顶_2_paper_top_2__爱给网_aigei_com;
            flowPlayerHand.BackgroundImageLayout = ImageLayout.Stretch;
            flowPlayerHand.Dock = DockStyle.Fill;
            flowPlayerHand.Location = new Point(3, 650);
            flowPlayerHand.Name = "flowPlayerHand";
            flowPlayerHand.Size = new Size(1389, 156);
            flowPlayerHand.TabIndex = 5;
            // 
            // flowPlayerBuilding
            // 
            flowPlayerBuilding.AutoScroll = true;
            flowPlayerBuilding.BackgroundImage = Properties.Resources.纸顶_2_paper_top_2__爱给网_aigei_com;
            flowPlayerBuilding.BackgroundImageLayout = ImageLayout.Stretch;
            flowPlayerBuilding.Dock = DockStyle.Fill;
            flowPlayerBuilding.Location = new Point(3, 488);
            flowPlayerBuilding.Name = "flowPlayerBuilding";
            flowPlayerBuilding.Size = new Size(1389, 156);
            flowPlayerBuilding.TabIndex = 6;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1395, 892);
            Controls.Add(tableLayoutMain);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NormanConquest";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            Resize += FormMain_Resize;
            tableLayoutMain.ResumeLayout(false);
            panelOpponentInfo.ResumeLayout(false);
            panelOpponentInfo.PerformLayout();
            panelPlayerInfo.ResumeLayout(false);
            panelPlayerInfo.PerformLayout();
            panelGameInfo.ResumeLayout(false);
            panelGameInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox boxLog;
        private TableLayoutPanel tableLayoutMain;
        private Panel panelOpponentInfo;
        private Panel panelPlayerInfo;
        private Panel panelGameInfo;
        private Label labelPlayerName;
        private FlowLayoutPanel flowOpponentHand;
        private FlowLayoutPanel flowOpponentBuilding;
        private FlowLayoutPanel flowPlayerHand;
        private FlowLayoutPanel flowPlayerBuilding;
        private Label labelOpponentHP;
        private Label labelOpponentPileCount;
        private Label labelOpponentName;
        private Label labelPileCount;
        private Label labelPlayerHP;
        private Button buttonEndTurn;
        private Button buttonPassDefense;
        private TextBox textBoxLog;
        private Button buttonRefresh;
    }
}
