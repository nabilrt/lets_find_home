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
    public partial class RenterNotices : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public RenterNotices()
        {
            InitializeComponent();
            getRecord();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from NoticeTable where RenterID=@RenterID", c);
            cmd.Parameters.AddWithValue("@RenterID", LoginForm.rid);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void RenterNotices_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet5.NoticeTable' table. You can move, or remove it, as needed.
            this.noticeTableTableAdapter.Fill(this.houseDatabaseDataSet5.NoticeTable);

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox4.Clear();
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox4.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            bunifuTextBox1.Text= bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            bunifuTextBox2.Text= bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            bunifuTextBox3.Text= bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
