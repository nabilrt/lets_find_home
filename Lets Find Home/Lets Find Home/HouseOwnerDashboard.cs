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
using System.IO;
using System.Data.SqlClient;

namespace Lets_Find_Home
{
    public partial class HouseOwnerDashboard : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        int count;

        int sum = 0;

        public HouseOwnerDashboard()
        {
            InitializeComponent();
            showPage();
            bunifuLabel7.Text = getID();
            bunifuLabel8.Visible = false;
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
                    bunifuLabel8.Visible = true;
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

        public string calculations()
        {
            
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * From Payments where RenterID=@RenterID", c);
            cmd.Parameters.AddWithValue("@RenterID", LoginForm.rid.ToString());
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                sum+=Convert.ToInt32(dr["PaidAmount"]);
            }
            c.Close();

            return sum.ToString();
        }

        public void showPage()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("select * from HOwner where HID=@HID", c);
            cmd.Parameters.AddWithValue("@HID", LoginForm.rid.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    bunifuLabel3.Text = Convert.ToString(dr.GetValue(2));
                    byte[] img = (byte[])(dr.GetValue(7));
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

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                PostAds p=new PostAds();
                p.ShowDialog();
            }
            catch (Exception) { MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                MessageBox.Show("Unable to be Back", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            try {
                this.Hide();
                EditHOwner eh = new EditHOwner();
                eh.ShowDialog();
            } 
            catch (Exception) { MessageBox.Show("Unable to Open Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                HouseOwnerChatting hc = new HouseOwnerChatting();
                hc.ShowDialog();
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
                OwnerPaymentHistory oph = new OwnerPaymentHistory();
                oph.ShowDialog();
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
                RenterandAds rna = new RenterandAds();
                rna.ShowDialog();
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
                HouseOwnerNotice hon = new HouseOwnerNotice();
                hon.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string getID()
        {
            SqlConnection c = new SqlConnection(cs);
            string query = "Select count(*) from BashaVara where HOwnerID=@HOwnerID";
            SqlCommand cmd = new SqlCommand(query, c);
            cmd.Parameters.AddWithValue("@HOwnerID", LoginForm.rid.ToString());
            c.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
            //textID.Text = "RN-" + count;
            c.Close();
            return count.ToString();
        }

        private void HouseOwnerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                HOAdminSupport hoas = new HOAdminSupport();
                hoas.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
