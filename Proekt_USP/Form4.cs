using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proekt_USP
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

      //  SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\viktor\documents\visual studio 2012\Projects\Proekt_USP\Proekt_USP\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayAll()
        {
            con.Open();
            string query = "select ProdName, ProdCity from Product;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }

        private void FillCombobox()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CatNane from Category", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatNane", typeof(string));
            dt.Load(rdr);
            comboBox1.ValueMember = "CatNane";
            comboBox1.DataSource = dt;

            con.Close();

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            label6.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
            textBox2.Text = row.Cells[0].Value.ToString();
            textBox3.Text = row.Cells[1].Value.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
            DisplayAll();
            FillCombobox();
        }

        int Grdtotal = 0;
        private void button5_Click(object sender, EventArgs e)
        {
           
            int total = Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox3.Text);
            
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dataGridView3);
           
            newRow.Cells[0].Value = label6.Text;
            newRow.Cells[1].Value = textBox2.Text;
            newRow.Cells[2].Value = textBox3.Text;
            newRow.Cells[3].Value = textBox4.Text;
            newRow.Cells[4].Value = comboBox1.SelectedValue.ToString();
            newRow.Cells[5].Value = Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox3.Text);
            dataGridView3.Rows.Add(newRow);
            Grdtotal = Grdtotal + total;
            label8.Text = Grdtotal + " lv.";

            try
            {
                con.Open();
                string query = "update Product set ProdCity='" + textBox3.Text + "' where ProdName= '" + textBox2.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                con.Close();
                DisplayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap bm = new Bitmap(this.dataGridView3.Width, this.dataGridView3.Height);
            dataGridView3.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView3.Width, this.dataGridView3.Height));
            e.Graphics.DrawImage(bm,0,0);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                dataGridView3.Rows.RemoveAt(row.Index);
               
            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                dataGridView3.Rows.RemoveAt(row.Index);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayAll();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form2 log = new Form2();
            this.Hide();
            log.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 log = new Form5();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 log = new Form3();
            this.Hide();
            log.Show();
        }
    }
}
