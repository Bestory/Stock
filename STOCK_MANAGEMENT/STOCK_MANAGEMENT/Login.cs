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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_MouseClick(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //To-Do: Check User Name & Password to Invoke StockMain Page//
            SqlConnection conn = new SqlConnection("Data Source=TZ_BESTORYA-01;Initial Catalog=STOCK;User ID=sa;Password=African@2019");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [dbo].[Login] where [User Name]='" +textBox1.Text+ "' and Password='"+ textBox2.Text +"'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else 
            {
                MessageBox.Show("Invalid Username or Password....!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_MouseClick(sender,e);
            }
        }
    }
}
