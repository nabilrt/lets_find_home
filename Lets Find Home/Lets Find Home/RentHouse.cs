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
    public partial class RentHouse : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        string renterid;
        int count;
        public RentHouse()
        {
            InitializeComponent();
            ImageProcess2();
            showPage();
        }

        public void showPage()
        {
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("select * from Renters where RID=@RID", c);
            cmd.Parameters.AddWithValue("@RID", LoginForm.rid.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    renterid = Convert.ToString(dr.GetValue(0));
                }
                dr.Close();
            }
            c.Close();
        }

        public void ImageProcess2()
        {
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

        private void RentHouse_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.FinalAds' table. You can move, or remove it, as needed.
            this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet1.FinalAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.FinalAds' table. You can move, or remove it, as needed.
            // this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet1.FinalAds);
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.ApprovedAds' table. You can move, or remove it, as needed.
            //this.approvedAdsTableAdapter.Fill(this.houseDatabaseDataSet1.ApprovedAds);

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
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

        private void bunifuDataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textadno.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            texthid.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textarea.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textaddress.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textrent.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            texthowner.Text = bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[6].Value);
            pictureBox2.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[7].Value);
            pictureBox3.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[8].Value);
        }

        private Image GetPhoto(byte[] picture)
        {
            MemoryStream msi = new MemoryStream(picture);
            return System.Drawing.Image.FromStream(msi);
        }

        private void bunifuDataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textadno.Text = bunifuDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            texthid.Text = bunifuDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textarea.Text = bunifuDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textaddress.Text = bunifuDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textrent.Text = bunifuDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            texthowner.Text = bunifuDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[6].Value);
            pictureBox2.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[7].Value);
            pictureBox3.Image = GetPhoto((byte[])bunifuDataGridView1.SelectedRows[0].Cells[8].Value);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("select * from FinalAds where HouseArea=@HouseArea", c);
                cmd.Parameters.AddWithValue("@HouseArea", bunifuTextBox1.Text);
                DataTable dataTable = new DataTable();
                c.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.HasRows == true)
                {
                    dataTable.Load(sqlDataReader);
                    //c.Close();
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
                    c.Close();
                }
                else
                {
                    MessageBox.Show("No Such Area's Ad Found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    getRecord();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Search", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getRecord()
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

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            getRecord();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into Chat Values(@RQNO,@HOwnerID,@RenterID,@HMessage,@RMessage,@ADNO)", c);
                cmd.Parameters.AddWithValue("@RQNO", getID());
                cmd.Parameters.AddWithValue("@HOwnerID", texthowner.Text);
                cmd.Parameters.AddWithValue("@RenterID", LoginForm.rid);
                cmd.Parameters.AddWithValue("@HMessage", "");
                cmd.Parameters.AddWithValue("@RMessage", "I want to talk with you");
                cmd.Parameters.AddWithValue("@ADNO", textadno.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Request Sent to the Owner go to Dashboard->Chat to continue the conversation", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                c.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Send Request", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                string query = "select * from FinalAds where HouseArea like '" + bunifuTextBox1.Text + "%'";
                SqlCommand cmd = new SqlCommand(query, c);
                cmd.Parameters.AddWithValue("@HouseArea", bunifuTextBox1.Text);
                DataTable dataTable = new DataTable();
                c.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.HasRows == true)
                {
                    dataTable.Load(sqlDataReader);
                    //c.Close();
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
                    c.Close();
                }
                else
                {
                    MessageBox.Show("No Such Area's Ad Found", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    getRecord();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to Search", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}