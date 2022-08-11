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
    public partial class RenterTransaction : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        public static string ho, rn, amount,adno,month;

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (bkashButton.Checked)
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
                            //texthowner.Text = Convert.ToString(dr.GetValue(0));
                            ho = Convert.ToString(dr.GetValue(0));
                            //textrenter.Text = Convert.ToString(dr.GetValue(1));
                            rn = Convert.ToString(dr.GetValue(1));
                            //textpayable.Text = Convert.ToString(dr.GetValue(2));
                            amount = Convert.ToString(dr.GetValue(2));
                            adno = bunifuDropdown1.Text;
                            month = bunifuDropdown2.Text;
                        }
                        dr.Close();
                    }
                    BKashGateway bk = new BKashGateway();
                    bk.ShowDialog();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (cardButton.Checked)
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
                            //texthowner.Text = Convert.ToString(dr.GetValue(0));
                            ho = Convert.ToString(dr.GetValue(0));
                            //textrenter.Text = Convert.ToString(dr.GetValue(1));
                            rn = Convert.ToString(dr.GetValue(1));
                            //textpayable.Text = Convert.ToString(dr.GetValue(2));
                            amount = Convert.ToString(dr.GetValue(2));
                            adno = bunifuDropdown1.Text;
                            month = bunifuDropdown2.Text;
                        }
                        dr.Close();
                    }
                    CardGateWay cg = new CardGateWay();
                    cg.ShowDialog();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                MessageBox.Show("Please Choose a payment Method", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ResetControl()
        {
            bunifuDropdown1.Text = "";
            texthowner.Clear();
            textrenter.Clear();
            textpayable.Clear();
            bunifuDropdown2.Text = "";
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        public RenterTransaction()
        {
            InitializeComponent();
            ComboBoxAdd();
        }

        public void ComboBoxAdd()
        {
            bunifuDropdown1.Items.Clear();
            SqlConnection c = new SqlConnection(cs);
            c.Open();
            SqlCommand cmd = new SqlCommand("Select * From BashaVara where RenterID=@RenterID", c);
            cmd.Parameters.AddWithValue("@RenterID", LoginForm.rid.ToString());
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

        private void bunifuButton3_Click(object sender, EventArgs e)
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
                        texthowner.Text = Convert.ToString(dr.GetValue(0));
                        ho= Convert.ToString(dr.GetValue(0));
                        textrenter.Text = Convert.ToString(dr.GetValue(1));
                        rn= Convert.ToString(dr.GetValue(1));
                        textpayable.Text = Convert.ToString(dr.GetValue(2));
                        amount= Convert.ToString(dr.GetValue(2));
                        adno = bunifuDropdown1.Text;
                        month = bunifuDropdown2.Text;
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
    }
}
