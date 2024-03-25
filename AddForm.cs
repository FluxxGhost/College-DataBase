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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace MSSqlDataBase
{
    public partial class AddForm : Form
    {
        DataBase dataBase = new DataBase();
        public AddForm()
        {
            InitializeComponent();
            StartPosition= FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

           
            
            var name = textBoxStNa_Inf.Text;
            var lastname = textBoxLaNa_Inf.Text;
            var patr = textBoxStPa_Inf.Text;
            var numb = textBoxNumb_Inf.Text;
            var email = textBoxEmail_Inf.Text;
            var grup = textBoxGrup_Inf.Text;
            var spec = textBoxSpec_Inf.Text;
            var data = textBoxDataVst_Inf.Text;
            var sypend = textBoxStSt_Inf.Text;
            var mat = textBoxBalMat_Inf.Text;
            var ukr = textBoxBalUkr_Inf.Text;
            var eng = textBoxBalEng_Inf.Text;

            var addQuary = $"insert into students (name_stud, lastname_stud, patronymic__stud, number_stud, email_stud, group_stud, specialty_stud, data_vstupu, scholarship, bal_mat, bal_ukr, bal_eng) values ('{name}', '{lastname}', '{patr}', '{numb}', '{email}', '{grup}', '{spec}', '{data}', '{sypend}', '{mat}', '{ukr}', '{eng}')";

            var comand = new SqlCommand(addQuary, dataBase.getConnection());
            comand.ExecuteNonQuery();

            MessageBox.Show("Entry was successfully created!", "success!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            dataBase.closeConnection();
        }


        private void ClearFieldsAdd()
        {
            textBoxStNa_Inf.Text = "";
            textBoxLaNa_Inf.Text = "";
            textBoxStPa_Inf.Text = "";
            textBoxNumb_Inf.Text = "";
            textBoxEmail_Inf.Text = "";
            textBoxGrup_Inf.Text = "";
            textBoxSpec_Inf.Text = "";
            textBoxDataVst_Inf.Text = "";
            textBoxStSt_Inf.Text = "";
            textBoxBalMat_Inf.Text = "";
            textBoxBalUkr_Inf.Text = "";
            textBoxBalEng_Inf.Text = "";
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClearFieldsAdd();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ведіть дані", "!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
