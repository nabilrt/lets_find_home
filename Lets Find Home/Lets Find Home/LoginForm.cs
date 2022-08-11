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

namespace Lets_Find_Home
{
    public partial class LoginForm : Form
    {
        public static string rid, hid;

        public LoginForm()
        {
            InitializeComponent();
            textpass.PasswordChar = '\0';
            textuserID.Focus();
        }

        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
        }

        private void textuserID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textuserID.Text))
            {
                textuserID.Focus();
                errorProvider1.Icon = Properties.Resources.Paomedia_Small_N_Flat_Sign_error;
                errorProvider1.SetError(textuserID, "Enter your ID");
            }
            else
            {
                errorProvider1.Icon = Properties.Resources.Custom_Icon_Design_Pretty_Office_8_Accept;
                //errorProvider1.Clear();
            }
        }

        private void textpass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textpass.Text))
            {
                textpass.Focus();
                errorProvider2.Icon = Properties.Resources.Paomedia_Small_N_Flat_Sign_error;
                errorProvider2.SetError(textpass, "Enter your password");
            }
            else
            {
                errorProvider2.Icon = Properties.Resources.Custom_Icon_Design_Pretty_Office_8_Accept;
                //errorProvider2.Clear();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                rid = textuserID.Text;
                SqlConnection c = new SqlConnection(cs);

                if (renterButton.Checked)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Renters WHERE RID='" + textuserID.Text + "' AND Password='" + textpass.Text + "'", c);                   
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                       // MessageBox.Show("Login Successful", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        RenterDashboard rd = new RenterDashboard();
                        rd.ShowDialog();

                    }
                    else
                        MessageBox.Show("Invalid username or password", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (houseOwner.Checked)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM HOwner WHERE HID='" + textuserID.Text + "' AND Password='" + textpass.Text + "'", c);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                       // MessageBox.Show("Login Successful", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        HouseOwnerDashboard ho = new HouseOwnerDashboard();
                        ho.ShowDialog();
                    }
                    else
                        MessageBox.Show("Invalid username or password", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (adminButton.Checked)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Admin WHERE AID='" + textuserID.Text + "' AND Password='" + textpass.Text + "'", c);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        //MessageBox.Show("Login Successful", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        AdminDashboard admin = new AdminDashboard();
                        admin.ShowDialog();

                    }
                    else
                        MessageBox.Show("Invalid username or password", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Choose an option to login", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Login", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            textuserID.Clear();
            textpass.Clear();
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
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
