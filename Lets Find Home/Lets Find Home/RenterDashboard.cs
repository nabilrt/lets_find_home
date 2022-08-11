using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Lets_Find_Home
{
    public partial class RenterDashboard : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public RenterDashboard()
        {
            InitializeComponent();
            showPage();
            bunifuLabel6.Visible = false;
            check();
        }

        public void check()
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Select * from BlockedMember where ID=@ID", c);
                cmd.Parameters.AddWithValue("@ID", LoginForm.rid.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    bunifuLabel6.Visible = true;
                    bunifuButton2.Visible = false;
                    bunifuButton3.Visible = false;
                    bunifuButton4.Visible = false;
                    bunifuButton5.Visible = false;
                    bunifuButton6.Visible = false;
                    bunifuButton7.Visible = false;
                }
                dr.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void showPage()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("select * from Renters where RID=@RID", c);
            cmd.Parameters.AddWithValue("@RID", LoginForm.rid.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    bunifuLabel3.Text = Convert.ToString(dr.GetValue(2));
                    byte[] img = (byte[])(dr.GetValue(8));
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                dr.Close();
            }
            c.Close();
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

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

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RentHouse rh = new RentHouse();
                rh.ShowDialog();
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
                RenterChatting rc = new RenterChatting();
                rc.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RenterTransaction rt = new RenterTransaction();
                rt.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RentedHouses rh = new RentedHouses();
                rh.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                throw;
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                EditRenter er = new EditRenter();
                er.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RenterNotices rno = new RenterNotices();
                rno.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                RenterAdminSupport ras = new RenterAdminSupport();
                ras.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
