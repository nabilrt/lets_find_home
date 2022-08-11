using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Lets_Find_Home
{
    public partial class DeleteAds : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public DeleteAds()
        {
            InitializeComponent();
            ImageProcess2();
        }

        private void DeleteAds_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet5.FinalAds' table. You can move, or remove it, as needed.
            this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet5.FinalAds);

        }

        public void ImageProcess2()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from FinalAds", c);
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv1 = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv2 = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)bunifuDataGridView1.Columns[6];
            dgv1 = (DataGridViewImageColumn)bunifuDataGridView1.Columns[7];
            dgv2 = (DataGridViewImageColumn)bunifuDataGridView1.Columns[8];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv2.ImageLayout = DataGridViewImageCellLayout.Stretch;
            bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuDataGridView1.RowTemplate.Height = 80;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
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

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Delete from FinalAds where ADNO=@ADNO", c);
                cmd.Parameters.AddWithValue("@ADNO", (string)bunifuDataGridView1.SelectedRows[0].Cells[0].Value);
                cmd.ExecuteNonQuery();
                MessageBox.Show("AD Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImageProcess2();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Delete AD", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
