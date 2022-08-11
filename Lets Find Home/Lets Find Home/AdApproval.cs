using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Lets_Find_Home
{
    public partial class AdApproval : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public AdApproval()
        {
            InitializeComponent();
            ImageProcess();
            ImageProcess2();
        }

        private void AdApproval_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.UnapprovedAds' table. You can move, or remove it, as needed.
            this.unapprovedAdsTableAdapter1.Fill(this.houseDatabaseDataSet1.UnapprovedAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.FinalAds' table. You can move, or remove it, as needed.
            this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet1.FinalAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet.ApprovedAds' table. You can move, or remove it, as needed.
            //this.approvedAdsTableAdapter.Fill(this.houseDatabaseDataSet.ApprovedAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet.UnapprovedAds' table. You can move, or remove it, as needed.
            this.unapprovedAdsTableAdapter.Fill(this.houseDatabaseDataSet.UnapprovedAds);

        }

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            texthid.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textarea.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textaddress.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textrent.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            texthowner.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[5].Value);
            pictureBox2.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[6].Value);
            pictureBox3.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[7].Value);
        }

        private Image GetPhoto(byte[] picture)
        {
            MemoryStream msi = new MemoryStream(picture);
            return System.Drawing.Image.FromStream(msi);
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
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

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into FinalAds values(@ADNo,@HouseID,@HouseArea,@HouseAddress,@Rent,@OwnerID,@HousePicture1,@HousePicture2,@HousePicture3)", c);
                cmd.Parameters.AddWithValue("@ADNo", textadno.Text);
                cmd.Parameters.AddWithValue("@HouseID", texthid.Text);
                cmd.Parameters.AddWithValue("@HouseArea", textarea.Text);
                cmd.Parameters.AddWithValue("@HouseAddress", textaddress.Text);
                cmd.Parameters.AddWithValue("@Rent", textrent.Text);
                cmd.Parameters.AddWithValue("@OwnerID", texthowner.Text);
                cmd.Parameters.AddWithValue("@HousePicture1", SavePhoto(pictureBox1.Image));
                cmd.Parameters.AddWithValue("@HousePicture2", SavePhoto(pictureBox2.Image));
                cmd.Parameters.AddWithValue("@HousePicture3", SavePhoto(pictureBox3.Image));
                cmd.ExecuteNonQuery();                
                MessageBox.Show("Ad has been Approved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SqlCommand cmd1 = new SqlCommand("Delete from UnapprovedAds where HouseID=@HouseID", c);
                cmd1.Parameters.AddWithValue("@HouseID", texthid.Text);
                cmd1.ExecuteNonQuery();
                getRecord();
                getRecord2();
                c.Close();
                ClearData();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Approve Ad", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void ClearData()
        {
            textadno.Clear();
            texthid.Clear();
            textaddress.Clear();
            textarea.Clear();
            textrent.Clear();
            texthowner.Clear();
            pictureBox1.Image = Properties.Resources.No_Image_Available;
            pictureBox2.Image = Properties.Resources.No_Image_Available;
            pictureBox3.Image = Properties.Resources.No_Image_Available;
        }

        private byte[] SavePhoto(Image pic)
        {
            MemoryStream ms = new MemoryStream();

            pic.Save(ms, pic.RawFormat);

            return ms.GetBuffer();
        }

        public void getRecord()
        {
            SqlConnection c = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from UnapprovedAds", c);
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
            SqlCommand cmd = new SqlCommand("select * from FinalAds", c);
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

        public void ImageProcess()
        {
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

        public void ImageProcess2()
        {
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
    }
}
