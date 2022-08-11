using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lets_Find_Home
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                LoginForm f = new LoginForm();
                this.Hide();
                f.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                AdApproval ada = new AdApproval();
                ada.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                DeleteAds dad = new DeleteAds();
                dad.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                BlockRenter br = new BlockRenter();
                br.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Complains c = new Complains();
                c.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
