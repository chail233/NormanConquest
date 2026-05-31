using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NormanConquest
{
    public partial class FormMenu : Form
    {
        private AudioPlayer bgmplayer = new AudioPlayer(@"bgms\bgm1.wav");
        public FormMenu()
        {
            InitializeComponent();
        }

        private void linkLabelRule_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://chail233.github.io/NormanConquest/",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Tag = null;
            this.Close();
        }

        private void labelStart1_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.gameManager.player.CharacterIndex = 0;
            this.Tag = formMain;
            bgmplayer.Dispose();
            this.Close();
        }

        private void labelStart2_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.gameManager.player.CharacterIndex = 1;
            this.Tag = formMain;
            bgmplayer.Dispose();
            this.Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            bgmplayer.Play(loop: true);
        }
    }
}
