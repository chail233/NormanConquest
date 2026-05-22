namespace NormanConquest
{
    public partial class FormMain : Form, IGameUI
    {
        private GameManager gameManager;

        public FormMain()
        {
            InitializeComponent();
            gameManager = new GameManager();
            gameManager.UI = this; // 将界面实例传递给游戏管理器，以便游戏管理器可以调用界面刷新方法
            Logout("窗口实例初始化");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            gameManager.StartGame();
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
            // 刷新界面显示
            // 这里可以根据游戏状态更新玩家信息、牌堆信息等
        }
        public void PromptDefense(Attack attack)
        {
            // 这里可以弹出一个对话框让玩家选择如何防守
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            gameManager.EndTurn();
        }
    }
}
