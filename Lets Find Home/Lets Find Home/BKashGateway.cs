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
    public partial class BKashGateway : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["hdb"].ConnectionString;
        string a, b, c, d;

        private void BKashGateway_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font f = new Font("Century Gothic", 18, FontStyle.Bold);
            Font f1 = new Font("Century Gothic", 16, FontStyle.Regular);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("LETS FIND HOME", f, Brushes.Black, new Point(315, 10));
            e.Graphics.DrawString("INVOICE", f1, Brushes.Black, new Point(350, 50));
            e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", f, Brushes.Black, new Point(0, 80));
            e.Graphics.DrawString("NAME", f, Brushes.Black, new Point(100, 140));
            e.Graphics.DrawString("MONTH", f, Brushes.Black, new Point(410, 140));
            e.Graphics.DrawString("RENT", f, Brushes.Black, new Point(660, 140));
            e.Graphics.DrawString(bunifuTextBox3.Text, f1, Brushes.Black, new Point(100, 200));
            e.Graphics.DrawString(bunifuTextBox7.Text, f1, Brushes.Black, new Point(410, 200));
            e.Graphics.DrawString(bunifuTextBox2.Text, f1, Brushes.Black, new Point(665, 200));
            e.Graphics.DrawString("--------------------------------------------------------------------------------------------", f1, Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("TOTAL AMOUNT : ", f1, Brushes.Black, new Point(470, 270));
            e.Graphics.DrawString(bunifuTextBox5.Text, f1, Brushes.Black, new Point(675, 270));
        }

        public BKashGateway()
        {
            InitializeComponent();
            bunifuTextBox2.Text = RenterTransaction.amount;
            bunifuTextBox6.Text = RenterTransaction.adno;
            bunifuTextBox7.Text = RenterTransaction.month;
            bunifuTextBox4.Text = RenterTransaction.ho;
            bunifuTextBox3.Text = RenterTransaction.rn;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                RenterTransaction rt = new RenterTransaction();
                this.Hide();
                rt.ShowDialog();
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to back");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
           try
            {
                SqlConnection c = new SqlConnection(cs);
                c.Open();
                SqlCommand cmd = new SqlCommand("Insert into Payments Values(@ADNO,@OwnerID,@RenterID,@Rent,@PaidAmount,@Month)", c);
                cmd.Parameters.AddWithValue("@ADNO", bunifuTextBox6.Text);
                cmd.Parameters.AddWithValue("@OwnerID", bunifuTextBox4.Text);
                cmd.Parameters.AddWithValue("@RenterID", bunifuTextBox3.Text);
                cmd.Parameters.AddWithValue("@Rent", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@PaidAmount", bunifuTextBox5.Text);
                cmd.Parameters.AddWithValue("@Month", bunifuTextBox7.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Paid Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                c.Close();
                DialogResult dr= MessageBox.Show("Do You Want an Invoice?", "Invoice Printing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        PrintDialog printDialog = new PrintDialog();
                        printDialog.Document = printDocument1;

                        DialogResult dialogResult = printDialog.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to make Invoice", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    RenterTransaction rtc = new RenterTransaction();
                    rtc.ResetControl();
                    this.Close();
                    this.Hide();

                }
                else
                {
                    RenterTransaction rtc = new RenterTransaction();
                    rtc.ResetControl();
                    this.Close();
                    this.Hide();

                }
                //Application.Exit();
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
