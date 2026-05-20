namespace NormanConquest
{
    public partial class FormMain : Form, IGameUI
    {
        private GameManager gameManager;
        public FormMain()
        {
            InitializeComponent();
            gameManager = new GameManager();
            gameManager.UI = this;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            gameManager.StartGame();
        }
        //更新界面
        public void RefreshAll()
        {
            Player self = gameManager.CurrentPlayer;
            Player opponent = gameManager.OpponentPlayer;

            //对手信息
            labelOpponentName.Text = opponent.Name;
            labelOpponentHP.Text = $"HP: {opponent.HP}";
            labelOpponentHandCount.Text = $"手牌: {opponent.Hand.Count}";
            labelOpponentDeckPileCount.Text = $"牌堆: {opponent.Deck.Count}";
            labelOpponentDiscardCount.Text = $"弃牌: {opponent.DiscardPile.Count}";
            //对手手牌
            RefreshOpponentHand(opponent);

            //对手建筑区
            // 后续实现：显示对手建筑

            //己方建筑区
            RefreshBuildingFlow(selfBuildingFlow, self.BuildingZone);

            //己方手牌
            RefreshSelfHand(self);

            //己方信息
            labelSelfName.Text = gameManager.CurrentPlayer.Name;
            labelSelfHP.Text = $"HP: {self.HP}";

            //战场状态
            if (gameManager.ActiveBattle != null)
            {
                var ctx = gameManager.ActiveBattle;
                string attackerName = ctx.Attacker.Name;
                string defenderName = ctx.Defender.Name;
                string unitName = ctx.CurrentAttackUnit.Name;

                if (ctx.Phase == BattlePhase.WaitingForDefense)
                {
                    labelBattleInfo.Text = $"{attackerName} 用 [{unitName}] 进攻！{defenderName} 请选择如何应对";
                }
                else if (ctx.Phase == BattlePhase.WaitingForPursuit)
                {
                    labelBattleInfo.Text = $"{attackerName} 反制成功！是否追击？";
                }
            }
            else
            {
                labelBattleInfo.Text = $"{self.Name} 的行动回合";
            }
        }
        // 刷新对手手牌显示
        private void RefreshOpponentHand(Player opponent)
        {
            opponentHandFlow.Controls.Clear();

            int cardWidth = 60;
            int cardHeight = 85;
            int spacing = 5;

            for (int i = 0; i < opponent.Hand.Count; i++)
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

                opponentHandFlow.Controls.Add(cardBack);
            }
        }
        //刷新建筑区显示
        private void RefreshBuildingFlow(FlowLayoutPanel panel, List<BuildingCard> buildings)
        {
            panel.Controls.Clear();

            int buildingWidth = 80;
            int buildingHeight = 100;

            foreach (var building in buildings)
            {
                Panel buildingCard = new Panel
                {
                    Size = new Size(buildingWidth, buildingHeight),
                    BackColor = Color.FromArgb(80, 130, 80),  // 绿色调
                    Margin = new Padding(3)
                };

                Label lbl = new Label
                {
                    Text = building.Name,
                    ForeColor = Color.White,
                    Font = new Font("微软雅黑", 9),
                    AutoSize = true,
                    Location = new Point(3, 4)
                };
                buildingCard.Controls.Add(lbl);

                panel.Controls.Add(buildingCard);
            }
        }
        //刷新己方手牌显示
        private void RefreshSelfHand(Player self)
        {
            selfHandFlow.Controls.Clear();
            int cardWidth = 100;
            int cardHeight = 140;
            int spacing = 8;

            for (int i = 0; i < self.Hand.Count; i++)
            {
                Card card = self.Hand[i];
                Panel cardPanel = CreateCardPanel(card, cardWidth, cardHeight);
                // 外层容器
                Panel wrapper = new Panel
                {
                    Size = new Size(cardWidth + spacing, cardHeight + 15),
                    Margin = new Padding(0)
                };
                cardPanel.Location = new Point(spacing, 15);
                wrapper.Controls.Add(cardPanel);

                // 悬停效果
                cardPanel.MouseEnter += (s, e) =>
                {
                    cardPanel.Location = new Point(spacing, 0);
                };
                cardPanel.MouseLeave += (s, e) =>
                {
                    cardPanel.Location = new Point(spacing, 15);
                };

                selfHandFlow.Controls.Add(wrapper);
            }
        }

        // 根据卡牌类型创建不同外观的卡牌面板
        private Panel CreateCardPanel(Card card, int width, int height)
        {
            Panel panel = new Panel
            {
                Size = new Size(width, height),
                BorderStyle = BorderStyle.FixedSingle
            };

            // 根据类型设置背景色
            switch (card.CardType)
            {
                case CardType.Unit:
                    panel.BackColor = Color.FromArgb(180, 60, 50);  // 红色调
                    break;
                case CardType.Order:
                    panel.BackColor = Color.FromArgb(50, 100, 180);  // 蓝色调
                    break;
                case CardType.Building:
                    panel.BackColor = Color.FromArgb(50, 150, 70);   // 绿色调
                    break;
            }

            // 卡牌名称
            Label nameLabel = new Label
            {
                Text = card.Name,
                ForeColor = Color.White,
                Font = new Font("微软雅黑", 10, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(width - 4, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(2, 2)
            };
            panel.Controls.Add(nameLabel);

            // 如果是部队牌，显示兵种
            if (card is UnitCard unit)
            {
                Label typeLabel = new Label
                {
                    Text = unit.UnitType.ToString(),
                    ForeColor = Color.LightGray,
                    Font = new Font("微软雅黑", 8),
                    AutoSize = false,
                    Size = new Size(width - 4, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(2, 55)
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
                    Font = new Font("微软雅黑", 7),
                    AutoSize = false,
                    Size = new Size(width - 8, 50),
                    TextAlign = ContentAlignment.TopLeft,
                    Location = new Point(4, 80)
                };
                panel.Controls.Add(descLabel);
            }

            return panel;
        }
        // 提示玩家进行行动选择
        public void PromptPlayerAction(Player player)
        {
            // 隐藏战斗相关按钮

            // 结束回合按钮只在当前玩家行动时可用
            buttonEndTurn.Enabled = true;

            // 给手牌绑定点击事件
            BindHandCardEvents(player);
        }

        // 为当前手牌绑定点击事件
        private void BindHandCardEvents(Player player)
        {
            for (int i = 0; i < selfHandFlow.Controls.Count; i++)
            {
                Panel wrapper = (Panel)selfHandFlow.Controls[i];
                Panel cardPanel = (Panel)wrapper.Controls[0];
                Card card = player.Hand[i];
                // 清除之前的绑定
                cardPanel.Click -= CardPanel_Click;
                cardPanel.Click += CardPanel_Click;
                // 存卡牌索引
                cardPanel.Tag = i;
            }
        }

        // 手牌点击处理：根据卡牌类型执行不同操作
        private void CardPanel_Click(object sender, EventArgs e)
        {
            Panel cardPanel = (Panel)sender;
            int index = (int)cardPanel.Tag;
            Player self = gameManager.CurrentPlayer;
            if (index >= self.Hand.Count) return;
            Card card = self.Hand[index];
            switch (card.CardType)
            {
                case CardType.Unit:
                    // 尝试通常进攻
                    if (gameManager.ActiveBattle != null)
                    {
                        // 有连锁进行中，不能发起新进攻
                        return;
                    }
                    if (self.RemainingNormalAttacks <= 0)
                    {
                        MessageBox.Show("本回合通常进攻次数已用完");
                        return;
                    }
                    gameManager.TryNormalAttack((UnitCard)card);
                    break;

                case CardType.Order:
                    // 打出指令牌
                    if (gameManager.ActiveBattle != null)
                    {
                        // 有连锁进行中，不能打指令牌（除非以后允许在连锁中使用）
                        return;
                    }
                    gameManager.PlayOrderCard((OrderCard)card);
                    break;

                case CardType.Building:
                    // 放置建筑牌
                    if (gameManager.ActiveBattle != null)
                    {
                        return;
                    }
                    gameManager.PlayBuildingCard((BuildingCard)card);
                    break;
            }
        }
        public void PromptDefense(BattleContext context)
        {
            // 后续实现：显示抵御选择
        }

        public void PromptPursuit(BattleContext context)
        {
            // 后续实现：显示追击选择
        }

        public void OnGameOver(Player winner)
        {
            // 后续实现：游戏结束
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            if (gameManager.ActiveBattle != null)
            {
                MessageBox.Show("请先完成当前战斗连锁");
                return;
            }
            gameManager.EndTurn();
        }
    }
}
