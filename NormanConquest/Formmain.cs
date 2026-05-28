namespace NormanConquest
{
    public partial class FormMain : Form, GameManager.IGameUI
    {
        private GameManager gameManager;
        private int CardWidth = 90;
        private int CardHeight = 140;
        private int CardSpace = 10;
        private bool needDefense = false;
        private int width;
        private int height;
        public FormMain()
        {
            InitializeComponent();
            gameManager = new GameManager();
            gameManager.UI = this; // 将界面实例传递给游戏管理器
            Logout("窗口实例初始化");
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
            CardHeight = (int)(Height / 6);
            CardWidth = (int)(CardHeight * 0.66);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            gameManager.StartGame();
            labelPlayerName.Text = gameManager.player.Name;
            labelPlayerHP.Text = $"HP: {gameManager.player.HP}";
            labelPileCount.Text = $"牌堆:{gameManager.player.Deck.Count}张";
            labelOpponentName.Text = gameManager.opponent.Name;
            labelOpponentHP.Text = $"HP: {gameManager.opponent.HP}";
            labelOpponentPileCount.Text = $"牌堆:{gameManager.opponent.Deck.Count}张";
        }
        public void Logout(string message)
        {
            textBoxLog.AppendText($"{message}\r\n");
            // 自动滚动到最后一行
            textBoxLog.SelectionStart = textBoxLog.TextLength;
            textBoxLog.ScrollToCaret();
        }
        public override void Refresh()
        {
            labelPlayerHP.Text = $"HP: {gameManager.player.HP}";
            labelPileCount.Text = $"牌堆:{gameManager.player.Deck.Count}张";
            labelOpponentHP.Text = $"HP: {gameManager.opponent.HP}";
            labelOpponentPileCount.Text = $"牌堆:{gameManager.opponent.Deck.Count}张";
            RefreshOpponentHand();
            RefreshPlayerHand();
            RefreshOpponentBuildings();
            RefreshPlayerBuildings();
        }
        //刷新对手手牌
        private void RefreshOpponentHand()
        {
            flowOpponentHand.Controls.Clear();

            int cardWidth = CardWidth;
            int cardHeight = CardHeight;
            int spacing = CardSpace;

            for (int i = 0; i < gameManager.opponent.Hand.Count; i++)
            {
                Panel cardBack = new Panel
                {
                    Size = new Size(cardWidth, cardHeight),
                    BackColor = Color.FromArgb(60, 60, 120),  // 深蓝紫色牌背
                    Margin = new Padding(spacing, 0, 0, 0)
                };

                // 可选：画个简单边框或文字
                Label lbl = new Label
                {
                    Text = "?",
                    ForeColor = Color.White,
                    Font = new Font("微软雅黑", 14, FontStyle.Bold),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                cardBack.Controls.Add(lbl);

                flowOpponentHand.Controls.Add(cardBack);
            }
        }
        //刷新玩家手牌
        private void RefreshPlayerHand()
        {
            flowPlayerHand.Controls.Clear();
            int spacing = CardSpace;
            for (int i = 0; i < gameManager.player.Hand.Count; i++)
            {
                Card card = gameManager.player.Hand[i];
                Panel cardPanel = CreateCardPanel(card, CardWidth, CardHeight);
                //用tag标记索引
                cardPanel.Tag = i;
                cardPanel.Margin = new Padding(spacing, 0, 0, 0);
                flowPlayerHand.Controls.Add(cardPanel);
                if (card.CardType == CardType.Unit)
                {
                    cardPanel.Click += (sender, e) => UnitCard_Click(sender, e);
                }
                else if (card.CardType == CardType.Order)
                {
                    cardPanel.Click += (sender, e) => OrderCard_Click(sender, e);
                }
                else if (card.CardType == CardType.Building)
                {
                    cardPanel.Click += (sender, e) => BuildingCard_Click(sender, e);
                }
            }
        }
        public void UnitCard_Click(object sender, EventArgs e)
        {
            if (needDefense)
            {
                Panel cardPanel = sender as Panel;
                int cardIndex = (int)cardPanel.Tag;
                UnitCard unitCard = (UnitCard)gameManager.player.Hand[cardIndex];
                gameManager.currentAttack.DefenderUnit = unitCard;
                needDefense = false;
                buttonPassDefense.Visible = false;
                gameManager.TakeAttack(cardIndex);
            }
            else if (gameManager.currentPlayer != gameManager.player)
            {
                Logout("当前不是你的回合，无法行动");
            }
            else
            {
                Panel cardPanel = sender as Panel;
                int cardIndex = (int)cardPanel.Tag;
                UnitCard unitCard = (UnitCard)gameManager.player.Hand[cardIndex];
                gameManager.TryAttack(gameManager.player, gameManager.opponent, unitCard, cardIndex);
            }
        }
        public void OrderCard_Click(object sender, EventArgs e)
        {
            if (needDefense)
            {
                Logout("正在等待防守，无法使用指令牌");
                return;
            }
            Panel cardPanel = sender as Panel;
            int orderIndex = (int)cardPanel.Tag;
            gameManager.TakeOrder(gameManager.player, orderIndex);
        }
        public void BuildingCard_Click(object sender, EventArgs e)
        {
            if (needDefense)
            {
                Logout("正在等待防守，无法使用建筑牌");
                return;
            }
            Panel cardPanel = sender as Panel;
            int cardIndex = (int)cardPanel.Tag;
            gameManager.TakeBuilding(gameManager.player, cardIndex);
        }
        //刷新对手建筑
        private void RefreshOpponentBuildings()
        {
            flowOpponentBuilding.Controls.Clear();
            int spacing = CardSpace;
            foreach (var building in gameManager.opponent.BuildingZone)
            {
                Panel buildingPanel = CreateCardPanel(building, CardWidth, CardHeight);
                buildingPanel.Margin = new Padding(spacing, 0, 0, 0);
                flowOpponentBuilding.Controls.Add(buildingPanel);
            }
        }
        //刷新玩家建筑
        private void RefreshPlayerBuildings()
        {
            flowPlayerBuilding.Controls.Clear();
            int spacing = CardSpace;
            foreach (var building in gameManager.player.BuildingZone)
            {
                Panel buildingPanel = CreateCardPanel(building, CardWidth, CardHeight);
                buildingPanel.Margin = new Padding(spacing, 0, 0, 0);
                flowPlayerBuilding.Controls.Add(buildingPanel);
            }
        }
        // 创建带悬停边框效果的卡牌Panel
        private Panel CreateCardPanel(Card card, int width, int height)
        {
            Panel panel = new Panel
            {
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None
            };

            // 根据类型设置背景色
            switch (card.CardType)
            {
                case CardType.Unit:
                    panel.BackColor = Color.FromArgb(180, 60, 50);
                    break;
                case CardType.Order:
                    panel.BackColor = Color.FromArgb(50, 100, 180);
                    break;
                case CardType.Building:
                    panel.BackColor = Color.FromArgb(50, 150, 70);
                    break;
            }

            // 卡牌名称
            Label nameLabel = new Label
            {
                Text = card.Name,
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 15, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(width - 4, height / 4),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(2, 2),
                Enabled = false          // 不接收鼠标事件
            };
            panel.Controls.Add(nameLabel);

            // 部队牌显示兵种
            if (card is UnitCard unit)
            {
                Label typeLabel = new Label
                {
                    Text = unit.UnitType.ToString(),
                    ForeColor = Color.LightGray,
                    Font = new Font("微软雅黑", 12),
                    AutoSize = false,
                    Size = new Size(width - 4, height / 4),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(2, height / 4 + 35),
                    Enabled = false
                };
                panel.Controls.Add(typeLabel);
            }

            // 卡牌描述
            if (!string.IsNullOrEmpty(card.Description))
            {
                Label descLabel = new Label
                {
                    Text = card.Description,
                    ForeColor = Color.WhiteSmoke,
                    Font = new Font("微软雅黑", 10),
                    AutoSize = false,
                    Size = new Size(width - 8, height / 4),
                    TextAlign = ContentAlignment.TopLeft,
                    Location = new Point(4, height * 3 / 4 - 25),
                    Enabled = false
                };
                panel.Controls.Add(descLabel);
            }

            //悬停边框效果
            bool isHovered = false;

            panel.MouseEnter += (s, e) =>
            {
                isHovered = true;
                panel.Invalidate();
            };

            panel.MouseLeave += (s, e) =>
            {
                isHovered = false;
                panel.Invalidate();
            };

            panel.Paint += (s, e) =>
            {
                if (isHovered)
                {
                    using (Pen pen = new Pen(Color.Gold, 2))
                    {
                        e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                    }
                }
            };

            return panel;
        }
        public void PromptDefense(Attack attack)
        {
            needDefense = true;
            buttonPassDefense.Visible = true;
            Logout($"{attack.Defender.Name}需要防守");
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            if (gameManager.processing)
            {
                Logout("正在处理上一个操作，当前无法结束回合，请稍等...");
                return;
            }
            if (gameManager.currentAttack != null && !gameManager.currentAttack.finish)
            {
                Logout($"正在处理进攻，无法结束回合。");
                return;
            }
            gameManager.EndTurn();
            buttonEndTurn.Enabled = false;
        }
        public void EnableEndTurnButton()
        {
            buttonEndTurn.Enabled = true;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void buttonPassDefense_Click(object sender, EventArgs e)
        {
            gameManager.TakeAttackWithoutDefense();
            buttonPassDefense.Visible = false;
            needDefense = false;
        }
        public void GameOver(Player winner)
        {
            this.Tag = new FormOver(winner);
            this.Close();
        }


        private void FormMain_Resize(object sender, EventArgs e)
        {
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
            CardHeight = (int)(Height / 5);
            CardWidth = (int)(CardHeight * 0.66);
            Refresh();
        }
    }
}
