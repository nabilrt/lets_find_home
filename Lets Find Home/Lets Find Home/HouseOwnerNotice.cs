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
    public partial class HouseOwnerNotice : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public HouseOwnerNotice()
        {
            InitializeComponent();
            AddComboBoxItems();
            getRecord();
        }

        public void AddComboBoxItems()
        {
            bunifuDropdown1.Items.Clear();
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * From BashaVara where HOwnerID=@HOwnerID", c);
            cmd.Parameters.AddWithValue("@HOwnerID", LoginForm.rid.ToString());
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bunifuDropdown1.Items.Add(dr["ADNO"].ToString());
            }
            c.Close();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from NoticeTable where HOwnerID=@HOwnerID", c);
            cmd.Parameters.AddWithValue("@HOwnerID", LoginForm.rid);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void HouseOwnerNotice_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet5.NoticeTable' table. You can move, or remove it, as needed.
            this.noticeTableTableAdapter.Fill(this.houseDatabaseDataSet5.NoticeTable);

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

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Select * from BashaVara where ADNO=@ADNO", c);
                cmd.Parameters.AddWithValue("@ADNO", bunifuDropdown1.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        bunifuTextBox1.Text = Convert.ToString(dr.GetValue(0));
                        bunifuTextBox2.Text = Convert.ToString(dr.GetValue(1));
                    }
                    dr.Close();
                }
                c.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Fetch Info", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void ResetControl()
        {
            bunifuDropdown1.Text = "";
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into NoticeTable values(@ADNO,@HOwnerID,@RenterID,@Notice)", c);
                cmd.Parameters.AddWithValue("@ADNO", bunifuDropdown1.Text);
                cmd.Parameters.AddWithValue("@HOwnerID", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@RenterID", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@Notice", bunifuTextBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Notice Posted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getRecord();
                ResetControl();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Post Notice", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
