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
    public partial class RenterChatting : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        int count;

        public RenterChatting()
        {
            InitializeComponent();
            getRecord();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Chat where RenterID=@RenterID", c);
            cmd.Parameters.AddWithValue("@RenterID", LoginForm.rid);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void RenterChatting_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet3.Chat' table. You can move, or remove it, as needed.
            this.chatTableAdapter.Fill(this.houseDatabaseDataSet3.Chat);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet2.Chatting' table. You can move, or remove it, as needed.
            this.chattingTableAdapter.Fill(this.houseDatabaseDataSet2.Chatting);

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

        public bool isFull()
        {
            if(textownerid.Text!= string.Empty && textrenterid.Text!=string.Empty && textadNo.Text != string.Empty)
            {
                return true;
            }
            return false;
        }

        public string getID()
        {
            SqlConnection c = new SqlConnection(cs);
            string query = "Select count(*) from Chat";
            SqlCommand cmd = new SqlCommand(query, c);
            c.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            count++;
            c.Close();

            return "RQ-" + count;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            textreply.Clear();
            textmsg.Clear();
            textreqNo.Text = getID();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFull())
                {
                    SqlConnection c = new SqlConnection(cs);
                    c.Open();
                    SqlCommand cmd = new SqlCommand("Insert into Chat Values(@RQNO,@HOwnerID,@RenterID,@HMessage,@RMessage,@ADNO)", c);
                    cmd.Parameters.AddWithValue("@RQNO", textreqNo.Text);
                    cmd.Parameters.AddWithValue("@HOwnerID", textownerid.Text);
                    cmd.Parameters.AddWithValue("@RenterID", textrenterid.Text);
                    cmd.Parameters.AddWithValue("@HMessage", "");
                    cmd.Parameters.AddWithValue("@RMessage", textmsg.Text);
                    cmd.Parameters.AddWithValue("@ADNO", textadNo.Text);
                    cmd.ExecuteNonQuery();
                    c.Close();
                    MessageBox.Show("Message Sent Please wait for reply", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getRecord();
                }
                else
                {
                    MessageBox.Show("Select the Previous Message which you want to reply first", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Send Message", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textreqNo.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textownerid.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textrenterid.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textmsg.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textreply.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textadNo.Text= bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Delete from Chat where RQNO=@RQNO", c);
                cmd.Parameters.AddWithValue("@RQNO", (string)bunifuDataGridView1.SelectedRows[0].Cells[0].Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Message Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getRecord();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Delete Message", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void bunifuDataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textreqNo.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textownerid.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textrenterid.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textmsg.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textreply.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textadNo.Text = bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            textadNo.Clear();
            textmsg.Clear();
            textownerid.Clear();
            textrenterid.Clear();
            textreqNo.Clear();
        }
    }
}
