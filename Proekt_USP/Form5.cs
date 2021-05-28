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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
       
       // SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectoty|\Proekt_USP\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");
       


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Category Name");
            
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Product Name");
                comboBox3.Items.Add("Product Quantity");
                comboBox3.Items.Add("Product Price");
                
            }
          

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ADD_Click(object sender, EventArgs e)
        {
            
            if (comboBox3.SelectedItem=="Product Name")
            {
              
                try
                {

                    con.Open();
                    string query = "SELECT * From Product Where ProdName Like '" + textBox2.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


           
            else  if (comboBox3.SelectedItem=="Product Quantity")
            {
                try
                {

                    con.Open();
                    string query = " Select * from Product where ProdCity between '"+textBox3.Text+"' and '"+textBox5.Text+"'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (comboBox3.SelectedItem == "Product Price")
            {
                try
                {

                    con.Open();
                    string query = " Select * from Product where ProdPrice between '" + textBox3.Text + "' and '" + textBox5.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (comboBox3.SelectedItem == "Category Name")
            {
                try
                {

                    con.Open();
                    string query = " Select * from Category where CatNane Like'"+textBox2.Text+"'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 log = new Form2();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 log = new Form3();
            this.Hide();
            log.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == "Product Name" || comboBox3.SelectedItem == "Category Name")
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();

                textBox2.Enabled = true;
                textBox3.Enabled = false;
                textBox5.Enabled = false;
               
            }

            if (comboBox3.SelectedItem == "Product Price" || comboBox3.SelectedItem == "Product Quantity")
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();

                textBox2.Enabled = false;
                textBox3.Enabled = true;
                textBox5.Enabled = true;
                
            
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 log = new Form4();
            this.Hide();
            log.Show();
        }
    }
}
