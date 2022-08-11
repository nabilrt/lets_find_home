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
    public partial class PostAds : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        string n;

        public PostAds()
        {
            InitializeComponent();
            getInfo();
        }

        public void getInfo()
        {
            try
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
                        texthowner.Text = Convert.ToString(dr.GetValue(0));
                        n= Convert.ToString(dr.GetValue(0));
                    }
                    dr.Close();
                }
                c.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error to Open the Form", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            texthid.Clear();
            textaddress.Clear();
            textarea.Clear();
            textrent.Clear();
            pictureBox1.Image = Properties.Resources.No_Image_Available;
            pictureBox2.Image = Properties.Resources.No_Image_Available;
            pictureBox3.Image = Properties.Resources.No_Image_Available;
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                HouseOwnerDashboard h = new HouseOwnerDashboard();
                this.Hide();
                h.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to be Back", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into UnapprovedAds values(@HouseID,@HouseArea,@HouseAddress,@Rent,@OwnerName,@HousePicture1,@HousePicture2,@HousePicture3)", c);
                cmd.Parameters.AddWithValue("@HouseID", texthid.Text);
                cmd.Parameters.AddWithValue("@HouseArea", textarea.Text);
                cmd.Parameters.AddWithValue("@HouseAddress", textaddress.Text);
                cmd.Parameters.AddWithValue("@Rent", textrent.Text);
                cmd.Parameters.AddWithValue("@OwnerName", n);
                cmd.Parameters.AddWithValue("@HousePicture1", SavePhoto(pictureBox1.Image));
                cmd.Parameters.AddWithValue("@HousePicture2", SavePhoto(pictureBox2.Image));
                cmd.Parameters.AddWithValue("@HousePicture3", SavePhoto(pictureBox3.Image));
                cmd.ExecuteNonQuery();
                c.Close();
                MessageBox.Show("Ad has been posted for Approval, Check Status on Renters and Ads->Ads", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Post Ad", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private byte[] SavePhoto(Image pic)
        {
            MemoryStream ms = new MemoryStream();

            pic.Save(ms, pic.RawFormat);

            return ms.GetBuffer();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "SELECT IMAGE";
            of.Filter = "SELECT IMAGE (ALL FILES) *.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(of.FileName);
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "SELECT IMAGE";
            of.Filter = "SELECT IMAGE (ALL FILES) *.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(of.FileName);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "SELECT IMAGE";
            of.Filter = "SELECT IMAGE (ALL FILES) *.*)|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = new Bitmap(of.FileName);
            }
        }
    }
}
