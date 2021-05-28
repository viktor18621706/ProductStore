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
using System.Configuration;

namespace Proekt_USP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
                                                
      // SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\viktor\documents\visual studio 2012\Projects\Proekt_USP\Proekt_USP\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");



        private void Form2_Load(object sender, EventArgs e)
        {
            DisplayAll();
        }

        private void ADD_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                string query = "insert into Category values("+textBox1.Text+",'"+textBox2.Text+"','"+textBox3.Text+"')";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("You have added category");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                con.Close();
                DisplayAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void DisplayAll()
        {
            con.Open();
            string query = "select * from Category;";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Select Category to Delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from Category where Catid="+textBox1.Text+"";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted succesfully");
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
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Missing information!");
                }

                con.Open();
                string query = "update Category set CatNane='" + textBox2.Text + "',CatDesc='" + textBox3.Text + "' where Catid= '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("You have updated category");

                con.Close();
                DisplayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 log = new Form3();
            this.Hide();
            log.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form5 log = new Form5();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 log = new Form4();
            this.Hide();
            log.Show();
        }
    
    }
}
