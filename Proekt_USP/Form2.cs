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
            Application.Exit();      // Бутон за изход
        }
                                                
      


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");  // стринг за връзка с SQL Database



        private void Form2_Load(object sender, EventArgs e)
        {
            DisplayAll();      //при зареждане на формата, да се визуализират данните от SQL в Datagridview
        }

        private void ADD_Click(object sender, EventArgs e)   //бутон за добавяне на категория
        {

            try
            {
                con.Open();
                string query = "insert into Category values("+textBox1.Text+",'"+textBox2.Text+"','"+textBox3.Text+"')";   //в таблица Category се добавят ID, Name и Description
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
        private void DisplayAll()                     //фунция за визуализиране на данните от Category
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

        

        private void button3_Click(object sender, EventArgs e)        //Бутон Delete 
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
                    string query = "delete from Category where Catid="+textBox1.Text+"";    //изтрива избраните данни от ID
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

        private void button2_Click(object sender, EventArgs e)      //бутон Edit
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")    //проверка за празни полета
                {
                    MessageBox.Show("Missing information!");
                }

                con.Open();
                string query = "update Category set CatNane='" + textBox2.Text + "',CatDesc='" + textBox3.Text + "' where Catid= '" + textBox1.Text + "'"; // ъпдейтва таблица Category като взима въведеното в текстбоксовете
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)  // функция за избиране на ред в таблицата от формата
        {
            if (e.RowIndex == -1) return;  //за да не дава изключение при натискане в хедъра на таблицата
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];  //променлива за реда на таблицата
            textBox1.Text = row.Cells[0].Value.ToString();   //зареждане на текстбокса с данните от избрания ред за ID
            textBox2.Text = row.Cells[1].Value.ToString();   //зареждане на текстбокса с данните от избрания ред за Name
            textBox3.Text = row.Cells[2].Value.ToString();   //зареждане на текстбокса с данните от избрания ред за Description
        }

        private void button5_Click(object sender, EventArgs e)   //функция за препращане от тази форма към Product(form3)
        {
            Form3 log = new Form3();        
            this.Hide();
            log.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)  //функция за препращане от тази форма към Reference(form5)
        {
            Form5 log = new Form5();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e) //функция за препращане от тази форма към Sales(form4)
        {
            Form4 log = new Form4();
            this.Hide();
            log.Show();
        }
    
    }
}

