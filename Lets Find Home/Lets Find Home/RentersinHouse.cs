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
    public partial class RentersinHouse : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public RentersinHouse()
        {
            InitializeComponent();
            getRecord();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * from BashaVara where HOwnerID=@HOwnerID", c);
            cmd.Parameters.AddWithValue("@HOwnerID", LoginForm.rid.ToString());
            DataTable dataTable = new DataTable();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void RentersinHouse_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet4.BashaVara' table. You can move, or remove it, as needed.
            this.bashaVaraTableAdapter.Fill(this.houseDatabaseDataSet4.BashaVara);

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                RenterandAds rna = new RenterandAds();
                this.Hide();
                rna.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            texthid.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textrid.Text= bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textrent.Text= bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textarea.Text= bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textadno.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            texthid.Clear();
            textrid.Clear();
            textrent.Clear();
            textarea.Clear();
            textadno.Clear();
        }
    }
}
