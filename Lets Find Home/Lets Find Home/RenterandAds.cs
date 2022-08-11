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
    public partial class RenterandAds : Form
    {
        public RenterandAds()
        {
            InitializeComponent();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                HouseOwnerDashboard ho = new HouseOwnerDashboard();
                this.Hide();
                ho.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RentersinHouse rih = new RentersinHouse();
                rih.ShowDialog();
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
                AdsUploaded adu = new AdsUploaded();
                adu.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
