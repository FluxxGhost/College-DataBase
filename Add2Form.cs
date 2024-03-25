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
    public partial class Add2Form : Form
    {
        DataBase dataBase = new DataBase();
        public Add2Form()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private void buttonAdd2_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var name = textBoxTeNaNew.Text;
            var lastname = textBoxTeLaNaNew.Text;
            var patron = textBoxTePatNew.Text;
            var number = textBoxTeNumNew.Text;
            var email = textBoxTeEmNew.Text;
            var pos = textBoxTePosNew.Text;

            var addQuary = $"insert into teachers (name_teach, lastname_teach, patronymic_teach, number_teach, email_teach, position_teach) values ('{name}', '{lastname}', '{patron}', '{number}', '{email}', '{pos}')";

            var comand = new SqlCommand(addQuary, dataBase.getConnection());
            comand.ExecuteNonQuery();

            MessageBox.Show("Entry was successfully created!", "success!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            dataBase.closeConnection();
        }


        private void ClearFieldsAdd2()
        {
            textBoxTeNaNew.Text = "";
            textBoxTeLaNaNew.Text = "";
            textBoxTePatNew.Text = "";
            textBoxTeNumNew.Text = "";
            textBoxTeEmNew.Text = "";
            textBoxTePosNew.Text = "";
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClearFieldsAdd2();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ведіть дані", "!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
