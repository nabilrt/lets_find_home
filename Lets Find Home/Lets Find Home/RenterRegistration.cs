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
    public partial class RenterRegistration : Form
    {
        int count;

        public RenterRegistration()
        {
            InitializeComponent();
            textpass.PasswordChar = '\0';
        }

        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Form1 f = new Form1();
                this.Hide();
                f.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        public void resetControls()
        {
            textID.Text = getID();
            textemail.Clear();
            textname.Clear();
            textnid.Clear();
            textphone.Clear();
            textpass.Clear();
            dbgender.Text = "";
            dbgroup.Text = "";
            pictureBox1.Image = Properties.Resources.No_Image_Available;
        }

        private void clrbutton_Click(object sender, EventArgs e)
        {
            textID.Text = getID();
            textemail.Clear();
            textname.Clear();
            textnid.Clear();
            textphone.Clear();
            textpass.Clear();
            dbgender.Text = "";
            dbgroup.Text = "";
            pictureBox1.Image = Properties.Resources.No_Image_Available;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (isvalid())
                {
                    SqlConnection c = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("Insert into Renters Values(@RID,@NID,@Name,@Phone,@Email,@Password,@BGroup,@Gender,@Picture)", c);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@RID", textID.Text);
                    cmd.Parameters.AddWithValue("@NID", textnid.Text);
                    cmd.Parameters.AddWithValue("@Name", textname.Text);
                    cmd.Parameters.AddWithValue("@Phone", textphone.Text);
                    cmd.Parameters.AddWithValue("@Email", textemail.Text);
                    cmd.Parameters.AddWithValue("@Password", textpass.Text);
                    cmd.Parameters.AddWithValue("@BGroup", dbgroup.Text);
                    cmd.Parameters.AddWithValue("@Gender", dbgender.Text);
                    cmd.Parameters.AddWithValue("@Picture", SavePhoto());
                    c.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Account Opened Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        resetControls();
                    }
                    c.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Insert", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public bool isvalid()
        {
            if (textpass.Text == String.Empty)
            {
                MessageBox.Show("Password is Required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Choose Photo";
            OFD.Filter = "Image File(*.png;*.jpg) |*.png;*.jpg";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(OFD.FileName);
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        public string getID()
        {
            SqlConnection c = new SqlConnection(cs);
            string query = "Select count(*) from Renters";
            SqlCommand cmd = new SqlCommand(query, c);
            c.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            //textID.Text = "RN-" + count;
            c.Close();
            return "RN-" + count;
        }

        private void RenterRegistration_Load(object sender, EventArgs e)
        {
            SqlConnection c = new SqlConnection(cs);
            string query = "Select count(*) from Renters";
            SqlCommand cmd = new SqlCommand(query, c);
            c.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            textID.Text = "RN-" + count;
            c.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {          
                textpass.UseSystemPasswordChar = false;
            }
            else
            {
                textpass.UseSystemPasswordChar = true;
            }
        }
    }
}
