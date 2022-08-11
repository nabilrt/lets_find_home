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
    public partial class AdsUploaded : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public AdsUploaded()
        {
            InitializeComponent();
            getRecord();
            getRecord2();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from UnapprovedAds where OwnerName=@OwnerName", c);
            cmd.Parameters.AddWithValue("@OwnerName", LoginForm.rid.ToString());
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView1.DataSource = dataTable;
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv1 = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv2 = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)bunifuDataGridView1.Columns[5];
            dgv1 = (DataGridViewImageColumn)bunifuDataGridView1.Columns[6];
            dgv2 = (DataGridViewImageColumn)bunifuDataGridView1.Columns[7];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv2.ImageLayout = DataGridViewImageCellLayout.Stretch;
            bunifuDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuDataGridView1.RowTemplate.Height = 80;
        }

        public void getRecord2()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from FinalAds where OwnerID=@OwnerID", c);
            cmd.Parameters.AddWithValue("@OwnerID", LoginForm.rid.ToString());
            DataTable dataTable = new DataTable();
            c.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            dataTable.Load(sqlDataReader);
            c.Close();
            bunifuDataGridView2.DataSource = dataTable;
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv1 = new DataGridViewImageColumn();
            DataGridViewImageColumn dgv2 = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)bunifuDataGridView2.Columns[6];
            dgv1 = (DataGridViewImageColumn)bunifuDataGridView2.Columns[7];
            dgv2 = (DataGridViewImageColumn)bunifuDataGridView2.Columns[8];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dgv2.ImageLayout = DataGridViewImageCellLayout.Stretch;
            bunifuDataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bunifuDataGridView2.RowTemplate.Height = 80;
        }

        private void AdsUploaded_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet4.FinalAds' table. You can move, or remove it, as needed.
            this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet4.FinalAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet4.UnapprovedAds' table. You can move, or remove it, as needed.
            this.unapprovedAdsTableAdapter.Fill(this.houseDatabaseDataSet4.UnapprovedAds);

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
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
    }
}
