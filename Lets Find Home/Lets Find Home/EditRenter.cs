﻿using System;
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
    public partial class EditRenter : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public EditRenter()
        {
            InitializeComponent();
            showPage();
            textpass.PasswordChar = '\0';
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

                    textID.Text = Convert.ToString(dr.GetValue(0));
                    textnid.Text = Convert.ToString(dr.GetValue(1));
                    textname.Text = Convert.ToString(dr.GetValue(2));
                    textphone.Text = Convert.ToString(dr.GetValue(3));
                    textemail.Text = Convert.ToString(dr.GetValue(4));
                    textpass.Text = Convert.ToString(dr.GetValue(5));
                    dbgroup.Text = Convert.ToString(dr.GetValue(6));
                    dbgender.Text = Convert.ToString(dr.GetValue(7));
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

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                RenterDashboard f = new RenterDashboard();
                this.Hide();
                f.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                string query = "update Renters set NID=@NID,Name=@Name,Phone=@Phone,Email=@Email,Password=@Password,BGroup=@BGroup,Gender=@Gender,Picture=@Picture where RID=@RID";
                SqlCommand cmd = new SqlCommand(query, c);
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
                    MessageBox.Show("Data Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Data Updated", "Unchanged", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                c.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Update Information", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();

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

        private void EditRenter_Load(object sender, EventArgs e)
        {
            
        }

        private void clrbutton_Click(object sender, EventArgs e)
        {

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
