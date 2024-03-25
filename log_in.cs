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
    public partial class log_in : Form
    {
        DataBase dataBase = new DataBase();

        public log_in()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            textBox_log.MaxLength= 50;
            textBox_pass.MaxLength = 50;
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_log.Text;
            var passUser = md5.hashPassword(textBox_pass.Text);

            SqlDataAdapter adapter= new SqlDataAdapter();
            DataTable table= new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand= command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                //MessageBox.Show("Ви успішно увійшли!", "Вхід", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого акаунта не існує!", "Вхід", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

     
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = true;
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = false;
            pictureBox3.Visible = true;
            pictureBox2.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox_log.Text = "";
            textBox_pass.Text = "";
        }
    }
}
