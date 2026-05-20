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
            mainLayout = new TableLayoutPanel();
            opponentInfoPanel = new Panel();
            labelOpponentDiscardCount = new Label();
            labelOpponentDeckPileCount = new Label();
            labelOpponentHandCount = new Label();
            labelOpponentHP = new Label();
            labelOpponentName = new Label();
            opponentHandPanel = new Panel();
            opponentHandFlow = new FlowLayoutPanel();
            battleInfoPanel = new Panel();
            labelBattleInfo = new Label();
            tableLayoutSelfArea = new TableLayoutPanel();
            selfBuildingFlow = new FlowLayoutPanel();
            selfHandFlow = new FlowLayoutPanel();
            selfInfoPanel = new Panel();
            buttonEndTurn = new Button();
            labelSelfHP = new Label();
            labelSelfName = new Label();
            mainLayout.SuspendLayout();
            opponentInfoPanel.SuspendLayout();
            opponentHandPanel.SuspendLayout();
            battleInfoPanel.SuspendLayout();
            tableLayoutSelfArea.SuspendLayout();
            selfInfoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(opponentInfoPanel, 0, 0);
            mainLayout.Controls.Add(opponentHandPanel, 0, 1);
            mainLayout.Controls.Add(battleInfoPanel, 0, 2);
            mainLayout.Controls.Add(tableLayoutSelfArea, 0, 3);
            mainLayout.Controls.Add(selfInfoPanel, 0, 4);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 5;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            mainLayout.Size = new Size(1175, 824);
            mainLayout.TabIndex = 0;
            // 
            // opponentInfoPanel
            // 
            opponentInfoPanel.BackColor = Color.DarkGray;
            opponentInfoPanel.Controls.Add(labelOpponentDiscardCount);
            opponentInfoPanel.Controls.Add(labelOpponentDeckPileCount);
            opponentInfoPanel.Controls.Add(labelOpponentHandCount);
            opponentInfoPanel.Controls.Add(labelOpponentHP);
            opponentInfoPanel.Controls.Add(labelOpponentName);
            opponentInfoPanel.Dock = DockStyle.Fill;
            opponentInfoPanel.Location = new Point(3, 3);
            opponentInfoPanel.Name = "opponentInfoPanel";
            opponentInfoPanel.Size = new Size(1169, 76);
            opponentInfoPanel.TabIndex = 0;
            // 
            // labelOpponentDiscardCount
            // 
            labelOpponentDiscardCount.AutoSize = true;
            labelOpponentDiscardCount.Location = new Point(643, 12);
            labelOpponentDiscardCount.Name = "labelOpponentDiscardCount";
            labelOpponentDiscardCount.Size = new Size(151, 24);
            labelOpponentDiscardCount.TabIndex = 0;
            labelOpponentDiscardCount.Text = "弃牌堆数量：233";
            // 
            // labelOpponentDeckPileCount
            // 
            labelOpponentDeckPileCount.AutoSize = true;
            labelOpponentDeckPileCount.Location = new Point(471, 12);
            labelOpponentDeckPileCount.Name = "labelOpponentDeckPileCount";
            labelOpponentDeckPileCount.Size = new Size(133, 24);
            labelOpponentDeckPileCount.TabIndex = 0;
            labelOpponentDeckPileCount.Text = "牌堆数量：233";
            // 
            // labelOpponentHandCount
            // 
            labelOpponentHandCount.AutoSize = true;
            labelOpponentHandCount.Location = new Point(301, 12);
            labelOpponentHandCount.Name = "labelOpponentHandCount";
            labelOpponentHandCount.Size = new Size(133, 24);
            labelOpponentHandCount.TabIndex = 0;
            labelOpponentHandCount.Text = "手牌数量：233";
            // 
            // labelOpponentHP
            // 
            labelOpponentHP.AutoSize = true;
            labelOpponentHP.Location = new Point(159, 12);
            labelOpponentHP.Name = "labelOpponentHP";
            labelOpponentHP.Size = new Size(105, 24);
            labelOpponentHP.TabIndex = 0;
            labelOpponentHP.Text = "HP:114514";
            // 
            // labelOpponentName
            // 
            labelOpponentName.AutoSize = true;
            labelOpponentName.Location = new Point(40, 12);
            labelOpponentName.Name = "labelOpponentName";
            labelOpponentName.Size = new Size(82, 24);
            labelOpponentName.TabIndex = 0;
            labelOpponentName.Text = "对手名字";
            // 
            // opponentHandPanel
            // 
            opponentHandPanel.Controls.Add(opponentHandFlow);
            opponentHandPanel.Dock = DockStyle.Fill;
            opponentHandPanel.Location = new Point(3, 85);
            opponentHandPanel.Name = "opponentHandPanel";
            opponentHandPanel.Size = new Size(1169, 158);
            opponentHandPanel.TabIndex = 1;
            // 
            // opponentHandFlow
            // 
            opponentHandFlow.Dock = DockStyle.Fill;
            opponentHandFlow.Location = new Point(0, 0);
            opponentHandFlow.Name = "opponentHandFlow";
            opponentHandFlow.Size = new Size(1169, 158);
            opponentHandFlow.TabIndex = 0;
            // 
            // battleInfoPanel
            // 
            battleInfoPanel.Controls.Add(labelBattleInfo);
            battleInfoPanel.Dock = DockStyle.Fill;
            battleInfoPanel.Location = new Point(3, 249);
            battleInfoPanel.Name = "battleInfoPanel";
            battleInfoPanel.Size = new Size(1169, 241);
            battleInfoPanel.TabIndex = 2;
            // 
            // labelBattleInfo
            // 
            labelBattleInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelBattleInfo.AutoSize = true;
            labelBattleInfo.Font = new Font("华文行楷", 23.9999981F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelBattleInfo.ForeColor = Color.White;
            labelBattleInfo.Location = new Point(471, 79);
            labelBattleInfo.Name = "labelBattleInfo";
            labelBattleInfo.Size = new Size(118, 51);
            labelBattleInfo.TabIndex = 0;
            labelBattleInfo.Text = "信息";
            labelBattleInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutSelfArea
            // 
            tableLayoutSelfArea.ColumnCount = 1;
            tableLayoutSelfArea.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutSelfArea.Controls.Add(selfBuildingFlow, 0, 0);
            tableLayoutSelfArea.Controls.Add(selfHandFlow, 0, 1);
            tableLayoutSelfArea.Dock = DockStyle.Fill;
            tableLayoutSelfArea.Location = new Point(3, 496);
            tableLayoutSelfArea.Name = "tableLayoutSelfArea";
            tableLayoutSelfArea.RowCount = 2;
            tableLayoutSelfArea.RowStyles.Add(new RowStyle(SizeType.Percent, 41.17647F));
            tableLayoutSelfArea.RowStyles.Add(new RowStyle(SizeType.Percent, 58.82353F));
            tableLayoutSelfArea.Size = new Size(1169, 241);
            tableLayoutSelfArea.TabIndex = 3;
            // 
            // selfBuildingFlow
            // 
            selfBuildingFlow.Dock = DockStyle.Fill;
            selfBuildingFlow.Location = new Point(3, 3);
            selfBuildingFlow.Name = "selfBuildingFlow";
            selfBuildingFlow.Size = new Size(1163, 93);
            selfBuildingFlow.TabIndex = 0;
            // 
            // selfHandFlow
            // 
            selfHandFlow.AutoScroll = true;
            selfHandFlow.Dock = DockStyle.Fill;
            selfHandFlow.Location = new Point(3, 102);
            selfHandFlow.Name = "selfHandFlow";
            selfHandFlow.Size = new Size(1163, 136);
            selfHandFlow.TabIndex = 1;
            // 
            // selfInfoPanel
            // 
            selfInfoPanel.BackColor = Color.DarkGray;
            selfInfoPanel.Controls.Add(buttonEndTurn);
            selfInfoPanel.Controls.Add(labelSelfHP);
            selfInfoPanel.Controls.Add(labelSelfName);
            selfInfoPanel.Dock = DockStyle.Fill;
            selfInfoPanel.Location = new Point(3, 743);
            selfInfoPanel.Name = "selfInfoPanel";
            selfInfoPanel.Size = new Size(1169, 78);
            selfInfoPanel.TabIndex = 4;
            // 
            // buttonEndTurn
            // 
            buttonEndTurn.Anchor = AnchorStyles.Right;
            buttonEndTurn.Location = new Point(991, 7);
            buttonEndTurn.Name = "buttonEndTurn";
            buttonEndTurn.Size = new Size(178, 65);
            buttonEndTurn.TabIndex = 1;
            buttonEndTurn.Text = "结束回合";
            buttonEndTurn.UseVisualStyleBackColor = true;
            buttonEndTurn.Click += buttonEndTurn_Click;
            // 
            // labelSelfHP
            // 
            labelSelfHP.AutoSize = true;
            labelSelfHP.Location = new Point(159, 24);
            labelSelfHP.Name = "labelSelfHP";
            labelSelfHP.Size = new Size(50, 24);
            labelSelfHP.TabIndex = 0;
            labelSelfHP.Text = "HP:0";
            // 
            // labelSelfName
            // 
            labelSelfName.AutoSize = true;
            labelSelfName.Location = new Point(40, 24);
            labelSelfName.Name = "labelSelfName";
            labelSelfName.Size = new Size(82, 24);
            labelSelfName.TabIndex = 0;
            labelSelfName.Text = "我的名字";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1175, 824);
            Controls.Add(mainLayout);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NormanConquest";
            Load += FormMain_Load;
            mainLayout.ResumeLayout(false);
            opponentInfoPanel.ResumeLayout(false);
            opponentInfoPanel.PerformLayout();
            opponentHandPanel.ResumeLayout(false);
            battleInfoPanel.ResumeLayout(false);
            battleInfoPanel.PerformLayout();
            tableLayoutSelfArea.ResumeLayout(false);
            selfInfoPanel.ResumeLayout(false);
            selfInfoPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Panel opponentInfoPanel;
        private Label labelOpponentHP;
        private Label labelOpponentName;
        private Label labelOpponentDiscardCount;
        private Label labelOpponentDeckPileCount;
        private Label labelOpponentHandCount;
        private Panel opponentHandPanel;
        private FlowLayoutPanel opponentHandFlow;
        private Panel battleInfoPanel;
        private Label labelBattleInfo;
        private TableLayoutPanel tableLayoutSelfArea;
        private FlowLayoutPanel selfBuildingFlow;
        private FlowLayoutPanel selfHandFlow;
        private Panel selfInfoPanel;
        private Label labelSelfHP;
        private Label labelSelfName;
        private Button buttonEndTurn;
    }
}
