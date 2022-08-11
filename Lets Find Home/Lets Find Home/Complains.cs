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
    public partial class Complains : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;

        public Complains()
        {
            InitializeComponent();
            onlyComplains();
        }

        public void onlyComplains()
        {
            try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Select * from AdminChatting where ID=@ID", c);
                cmd.Parameters.AddWithValue("@ID", LoginForm.rid.ToString());
                DataTable dataTable = new DataTable();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                dataTable.Load(sqlDataReader);
                c.Close();
                bunifuDataGridView1.DataSource = dataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Complains_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'houseDatabaseDataSet7.AdminChatting' table. You can move, or remove it, as needed.
            this.adminChattingTableAdapter.Fill(this.houseDatabaseDataSet7.AdminChatting);

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
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
    }
}
