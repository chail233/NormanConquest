namespace NormanConquest
{
    public partial class FormMain : Form
    {
        private GameManager gameManager;
        
        public FormMain()
        {
            InitializeComponent();
            gameManager = new GameManager();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }
        public void Logout(string message)
        {
            boxLog.AppendText($"{message}\r\n");
            // 自动滚动到最后一行
            boxLog.SelectionStart = boxLog.TextLength;
            boxLog.ScrollToCaret();
        }
    }
}
