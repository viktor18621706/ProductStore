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
      
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DB_proekt_usp.mdf;Integrated Security=True;Connect Timeout=30");


        private void FillCombobox()       //функция за попълване на combobox списъка с категории създадени в форма 2
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CatNane from Category",con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();   //създава нова таблица 
            dt.Columns.Add("CatNane",typeof(string));  //добавя колона само с името на категорията
            dt.Load(rdr);           //зарежда данните в таблицата
            comboBox1.ValueMember = "CatNane";   //добавя член променливите от колоната за име на категория
            comboBox1.DataSource = dt;  //за да взима данни от новосъздадената таблица за попълване на combobox

            con.Close();

        }


        private void DisplayAll()       //фунция за визуализиране на данните от Product
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
            FillCombobox();           //при зареждане на формата, да се визуализират данните в Combobox
            DisplayAll();             //при зареждане на формата, да се визуализират данните от SQL в Datagridview
        }

        private void button5_Click(object sender, EventArgs e)    //функция за препращане от тази форма към Category(form2)
        {
            Form2 log = new Form2();
            this.Hide();
            log.Show();
        }

        private void button6_Click(object sender, EventArgs e)  //функция за препращане от тази форма към Sales(form4)
        {
            Form4 log = new Form4();
            this.Hide();
            log.Show();

        }

        private void button7_Click(object sender, EventArgs e)  //функция за препращане от тази форма към Reference(form5)
        {
            Form5 log = new Form5();
            this.Hide();
            log.Show();
        }

        private void ADD_Click(object sender, EventArgs e)      //бутон за добавяне на продукт
        {
            try
            {
                con.Open();
                string query = "insert into Product values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','"+textBox4.Text+"','"+comboBox1.SelectedValue.ToString()+"')";  //в таблица Product се добавят ID, Name, Quantity, Price и Category
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)   // функция за избиране на ред в таблицата от формата
        {
            if (e.RowIndex == -1) return;    //за да не дава изключение при натискане в хедъра на таблицата
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];   //променлива за реда на таблицата  
            textBox1.Text = row.Cells[0].Value.ToString();    //зареждане на текстбокса с данните от избрания ред за ID
            textBox2.Text = row.Cells[1].Value.ToString();    //зареждане на текстбокса с данните от избрания ред за Name
            textBox3.Text = row.Cells[2].Value.ToString();    //зареждане на текстбокса с данните от избрания ред за Quantity
            textBox4.Text = row.Cells[3].Value.ToString();    //зареждане на текстбокса с данните от избрания ред за Price
            comboBox1.SelectedValue = row.Cells[4].Value.ToString();  //зареждане на комбобокса с данните от избрания ред за Category
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();    // Бутон за изход
        }

        private void button3_Click(object sender, EventArgs e)   //Бутон Delete 
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
                    string query = "delete from Product where Prodid=" + textBox1.Text + ";";     //изтрива избраните данни от ID
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

        private void button2_Click(object sender, EventArgs e)    //бутон Edit
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedValue.ToString() == "")   //проверка за празни полета
                {
                    MessageBox.Show("Missing information!");
                }

                con.Open();
                string query = "update Product set ProdName='" + textBox2.Text + "',ProdCity='" + textBox3.Text + "',ProdPrice='" + textBox4.Text + "',ProdCat='" + comboBox1.SelectedValue.ToString() + "' where Prodid= '" + textBox1.Text + "'";   // ъпдейтва таблица Category като взима въведеното в текстбоксовете
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

