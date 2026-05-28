using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NormanConquest
{
    public partial class FormOver : Form
    {
        private int width;
        private int height;
        public FormOver(Player winner)
        {
            InitializeComponent();
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
            labelIcon.Text = winner.Name == "玩家" ? "👑" : "💀";
            labelIcon.ForeColor = winner.Name == "玩家" ? Color.Gold : Color.Silver;
            labelIcon.Location = new Point(width / 2 - labelIcon.Width / 2, height / 3 - labelIcon.Height / 2);
            labelDesc.Text = winner.Name == "玩家" ? "你赢了！" : "你输了！";
            labelDesc.Location = new Point(width / 2 - labelDesc.Width / 2, height * 2 / 3 - labelDesc.Height / 2);
            buttonRestart.Location = new Point(width / 2 - buttonRestart.Width / 2, height * 6 / 7 - buttonRestart.Height / 2);
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            this.Tag = new FormMain();
            this.Close();
        }

        private void FormOver_Resize(object sender, EventArgs e)
        {
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
            labelIcon.Location = new Point(width / 2 - labelIcon.Width / 2, height / 3 - labelIcon.Height / 2);
            labelDesc.Location = new Point(width / 2 - labelDesc.Width / 2, height * 2 / 3 - labelDesc.Height / 2);
            buttonRestart.Location = new Point(width / 2 - buttonRestart.Width / 2, height * 6 / 7 - buttonRestart.Height / 2);
        }
    }
}
