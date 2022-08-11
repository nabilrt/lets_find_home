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
    public partial class BlockRenter : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public BlockRenter()
        {
            InitializeComponent();
        }

        private void BlockRenter_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet6.HOwner' table. You can move, or remove it, as needed.
            this.hOwnerTableAdapter.Fill(this.houseDatabaseDataSet6.HOwner);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet6.BlockedMember' table. You can move, or remove it, as needed.
            this.blockedMemberTableAdapter.Fill(this.houseDatabaseDataSet6.BlockedMember);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet6.Renters' table. You can move, or remove it, as needed.
            this.rentersTableAdapter.Fill(this.houseDatabaseDataSet6.Renters);

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

            try
            {
                this.Close();
                AdminDashboard ad = new AdminDashboard();
                this.Hide();
                ad.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox1.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void bunifuDataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox1.Text = bunifuDataGridView3.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void bunifuDataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox1.Text = bunifuDataGridView2.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into BlockedMember Values (@ID)", c);
                cmd.Parameters.AddWithValue("@ID", bunifuTextBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member Blocked!!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                SqlCommand cmd1 = new SqlCommand("Insert into AdminChatting Values(@ID,@Message,@SenderID,@Time)", c);
                cmd1.Parameters.AddWithValue("@ID", bunifuTextBox1.Text);
                cmd1.Parameters.AddWithValue("@Message", "You are blocked for wrong activities");
                cmd1.Parameters.AddWithValue("@SenderID", LoginForm.rid.ToString());
                cmd1.Parameters.AddWithValue("@Time", DateTime.Now.ToString());
                cmd1.ExecuteNonQuery();
                bunifuTextBox1.Clear();
                getRecord();

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Block");
                throw;
            }
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from BlockedMember", c);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView2.DataSource = dataTable;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Delete from BlockedMember where ID=@ID", c);
                cmd.Parameters.AddWithValue("@ID", bunifuTextBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member Unblocked", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getRecord();
                bunifuTextBox1.Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
