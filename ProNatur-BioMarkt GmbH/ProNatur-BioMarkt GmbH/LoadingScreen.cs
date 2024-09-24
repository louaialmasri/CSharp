using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNatur_BioMarkt_GmbH
{
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue;
        
        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadingbarTimer.Start();
        }

        private void loadingbarTimer_Tick(object sender, EventArgs e)
        {
            loadingBarValue += 1;

            loadingProgressbar.Value = loadingBarValue;

            lblLoadingprogress.Text = loadingProgressbar.Value.ToString() + "%";

            if (loadingBarValue >= loadingProgressbar.Maximum) { 
                loadingbarTimer.Stop();

                MainMenuScreen mainMenuScreen = new MainMenuScreen();
                mainMenuScreen.Show();

                this.Hide();
            }

        }
    }
}
