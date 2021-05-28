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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
       // SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\viktor\documents\visual studio 2012\Projects\Proekt_USP\Proekt_USP\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");


        private void FillCombobox()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CatNane from Category",con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatNane",typeof(string));
            dt.Load(rdr);
            comboBox1.ValueMember = "CatNane";
            comboBox1.DataSource = dt;

            con.Close();

        }


        private void DisplayAll()
        {
            con.Open();
            string query = "select * from Product;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            FillCombobox();
            DisplayAll();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 log = new Form2();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 log = new Form4();
            this.Hide();
            log.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 log = new Form5();
            this.Hide();
            log.Show();
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "insert into Product values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','"+textBox4.Text+"','"+comboBox1.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("You have added Product");
                
                con.Close();
                DisplayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            comboBox1.SelectedValue = row.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Select Product to Delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from Product where Prodid=" + textBox1.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted succesfully");
                    con.Close();
                    DisplayAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Missing information!");
                }

                con.Open();
                string query = "update Product set ProdName='" + textBox2.Text + "',ProdCity='" + textBox3.Text + "',ProdPrice='" + textBox4.Text + "',ProdCat='" + comboBox1.SelectedValue.ToString() + "' where Prodid= '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("You have updated product");

                con.Close();
                DisplayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
