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
    public partial class RenterAdminSupport : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public RenterAdminSupport()
        {
            InitializeComponent();
            bunifuButton2.Visible = false;
            loadData();
            checkStatus();
        }

        public void loadData()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * from AdminChatting where ID=@ID", c);
            cmd.Parameters.AddWithValue("@ID", LoginForm.rid.ToString());
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;

        }

        public void checkStatus()
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
                    bunifuButton1.Visible = false;
                    bunifuButton2.Visible = true;
                    bunifuTextBox1.ReadOnly = true;
                }
                dr.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RenterAdminSupport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet7.AdminChatting' table. You can move, or remove it, as needed.
            this.adminChattingTableAdapter.Fill(this.houseDatabaseDataSet7.AdminChatting);

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                RenterDashboard rd = new RenterDashboard();
                this.Hide();
                rd.ShowDialog();
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
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into AdminChatting Values(@ID,@Message,@SenderID,@Time)", c);
                cmd.Parameters.AddWithValue("@ID", "394");
                cmd.Parameters.AddWithValue("@Message", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@SenderID", LoginForm.rid.ToString());
                cmd.Parameters.AddWithValue("@Time", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Mailcious Activity Reported, Thanks for Helping us", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into AdminChatting Values(@ID,@Message,@SenderID,@Time)", c);
                cmd.Parameters.AddWithValue("@ID", "394");
                cmd.Parameters.AddWithValue("@Message", "Please Unblock My Account");
                cmd.Parameters.AddWithValue("@SenderID", LoginForm.rid.ToString());
                cmd.Parameters.AddWithValue("@Time", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Request Sent, Please Wait", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
