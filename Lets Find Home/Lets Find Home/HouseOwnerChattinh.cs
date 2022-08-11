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
    public partial class HouseOwnerChatting : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        string rentamount, housearea;

        public HouseOwnerChatting()
        {
            InitializeComponent();
            getRecord();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Chat where HOwnerID=@HOwnerID", c);
            cmd.Parameters.AddWithValue("@HOwnerID", LoginForm.rid);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
        }

        public void getRenterDetails()
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            textreply.Clear();
            textmsg.Clear();
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textreqNo.Text= bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textownerid.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textrenterid.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textmsg.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textreply.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textadNo.Text = bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Update Chat set HOwnerID=@HOwnerID, RenterID=@RenterID,HMessage=@HMessage,RMessage=@RMessage,ADNO=@ADNO where RQNO=@RQNO ", c);
                cmd.Parameters.AddWithValue("@RQNO", textreqNo.Text);
                cmd.Parameters.AddWithValue("@HOwnerID", textownerid.Text);
                cmd.Parameters.AddWithValue("@RenterID", textrenterid.Text);
                cmd.Parameters.AddWithValue("@HMessage", textreply.Text);
                cmd.Parameters.AddWithValue("@RMessage", textmsg.Text);
                cmd.Parameters.AddWithValue("@ADNO", textadNo.Text);
                cmd.ExecuteNonQuery();
                c.Close();
                MessageBox.Show("Reply Sent", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getRecord();
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

        private bool isAvailable()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * from BashaVara where ADNo=@ADNo", c);
            cmd.Parameters.AddWithValue("@ADNo", textadNo.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                return true;
            }
            dr.Close();
            return false;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (!isAvailable())
            {
                try
                {
                    SqlConnection c = new SqlConnection(cs);
                    c.Open();
                    SqlCommand cmd = new SqlCommand("Select * from FinalAds where ADNo=@ADNo", c);
                    cmd.Parameters.AddWithValue("@ADNo", textadNo.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rentamount = Convert.ToString(dr.GetValue(4));
                            housearea = Convert.ToString(dr.GetValue(2));
                        }
                    }
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand("Insert into BashaVara values(@HOwnerID,@RenterID,@Rent,@HouseArea,@ADNO)", c);
                    cmd1.Parameters.AddWithValue("@HOwnerID", textownerid.Text);
                    cmd1.Parameters.AddWithValue("@RenterID", textrenterid.Text);
                    cmd1.Parameters.AddWithValue("@Rent", rentamount);
                    cmd1.Parameters.AddWithValue("@HouseArea", housearea);
                    cmd1.Parameters.AddWithValue("@ADNO", textadNo.Text);
                    int x = cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("Delete from FinalAds where ADNo=@ADNo", c);
                    cmd2.Parameters.AddWithValue("@ADNo", textadNo.Text);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Renter Added to Your House Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*if (x > 0)
                    {
                        MessageBox.Show("Renter Added to Your House Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }*/
                    /*else
                    {
                        MessageBox.Show("Renter Already Added", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to Add Renter", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    //throw;
                }
            }
            else
            {
                MessageBox.Show("Renter Already in the house", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
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

        private void HouseOwnerChatting_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet3.Chat' table. You can move, or remove it, as needed.
            this.chatTableAdapter.Fill(this.houseDatabaseDataSet3.Chat);

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
    }
}
