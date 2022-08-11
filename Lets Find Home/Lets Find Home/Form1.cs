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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParams = base.CreateParams;
                handleParams.ExStyle |= 0x02000000;
                return handleParams;
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                GeneralAds ga = new GeneralAds();
                ga.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RenterRegistration r = new RenterRegistration();
                r.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuLabel3_MouseHover(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.DarkViolet;
        }

        private void bunifuLabel3_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel3.ForeColor = Color.Black;
        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_MouseHover(object sender, EventArgs e)
        {
            bunifuLabel5.ForeColor = Color.DarkViolet;
        }

        private void bunifuLabel5_MouseLeave(object sender, EventArgs e)
        {
            bunifuLabel5.ForeColor = Color.Black;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                LoginForm l= new LoginForm();
                l.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                HouseOwnerRegistration h = new HouseOwnerRegistration();
                h.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuToggleSwitch1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {

        }
    }
}
