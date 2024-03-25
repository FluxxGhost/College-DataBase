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


namespace MSSqlDataBase
{
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_pass2.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            textBox_log2.MaxLength = 50;
            textBox_pass2.MaxLength = 50;
        }

        private void btn_Enter_reg_Click(object sender, EventArgs e)
        {

            var login = textBox_log2.Text;
            var pass = md5.hashPassword(textBox_pass2.Text);

            string querystring = $"insert into register(login_user, password_user) values('{login}', '{pass}')";


            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Account successfully created!", "success!");
                log_in frm_login = new log_in(); 
                this.Hide();
                //frm_login.Show(); 
            }
            else
            {
                MessageBox.Show("Account not created!");
            }
            dataBase.closeConnection();

        }

        private Boolean checkUser()
        {
            var loginUser = textBox_log2.Text;
            var passUser = textBox_pass2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select * from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand= command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Such user already exists!");
                return true;
            }
            else { return false; }

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox_pass2.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_pass2.UseSystemPasswordChar = false;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox_log2.Text = "";
            textBox_pass2.Text = "";
        }

        
    }
}
