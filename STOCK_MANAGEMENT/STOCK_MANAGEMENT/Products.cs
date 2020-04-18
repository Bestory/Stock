using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STOCK_MANAGEMENT
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            load_data();
        }
        private void products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=TZ_BESTORYA-01;Initial Catalog=STOCK;User ID=sa;Password=African@2019");

            //Insert :Logic
            conn.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlquery = "";
            if (ifproductexist(conn, textBox2.Text))
            {
                sqlquery = @"UPDATE [Products] SET [ProductName] = '" + textBox1.Text + "',[ProductStatus] = '" + status + "' WHERE [ProductCode]='"+textBox2.Text + "'";
            }
            else 
            {
                sqlquery = @"INSERT INTO [dbo].[Products]([ProductCode],[ProductName],[ProductStatus])VALUES ('"+ textBox2.Text + "','" + textBox1.Text + "','" + status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlquery, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            //Restrive data from dataBase table

            load_data();
        }
        private bool ifproductexist(SqlConnection conn, string ProductCode) 
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 from [dbo].[Products] where ProductCode='"+ ProductCode + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else 
            {
                return false;
            }
        }


        public void load_data()
        {
            SqlConnection conn = new SqlConnection("Data Source=TZ_BESTORYA-01;Initial Catalog=STOCK;User ID=sa;Password=African@2019");
            SqlDataAdapter sda = new SqlDataAdapter("Select * from [dbo].[Products]", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow items in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = items["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = items["ProductName"].ToString();
                if ((bool)items["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Inactive";
                };
            }
        }


        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
          if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=TZ_BESTORYA-01;Initial Catalog=STOCK;User ID=sa;Password=African@2019");
            var sqlquery = "";
            if (ifproductexist(conn, textBox2.Text))
            {
                sqlquery = @"DELETE FROM [Products] WHERE [ProductCode]='" + textBox2.Text + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlquery, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                //Restrive data from dataBase table
                load_data();
            }
            else
            {
                MessageBox.Show("No specified record found......!!");
            }
        }
    } 
 
}

