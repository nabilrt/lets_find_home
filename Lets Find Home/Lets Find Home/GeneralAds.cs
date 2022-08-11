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

namespace Lets_Find_Home
{
    public partial class GeneralAds : Form
    {
        public GeneralAds()
        {
            InitializeComponent();
            ImageProcess2();
        }

        private void GeneralAds_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet1.FinalAds' table. You can move, or remove it, as needed.
            this.finalAdsTableAdapter.Fill(this.houseDatabaseDataSet1.FinalAds);

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

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Form1 f = new Form1();
                this.Hide();
                f.ShowDialog();
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

        private void bunifuButton1_Click(object sender, EventArgs e)
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
    }
}
