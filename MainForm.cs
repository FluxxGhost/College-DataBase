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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace MSSqlDataBase
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class MainForm : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id_stud", "id");
            dataGridView1.Columns.Add("name_stud", "Ім'я");
            dataGridView1.Columns.Add("lastname_stud", "Призвіще");
            dataGridView1.Columns.Add("patronymic__stud", "По батькові");
            dataGridView1.Columns.Add("number_stud", "Номер телефону");
            dataGridView1.Columns.Add("email_stud", "Email");
            dataGridView1.Columns.Add("group_stud", "Група");
            dataGridView1.Columns.Add("specialty_stud", "Спеціальність");
            dataGridView1.Columns.Add("data_vstupu", "Дата вступу");
            dataGridView1.Columns.Add("scholarship", "Стипендія");
            dataGridView1.Columns.Add("bal_mat", "Бал ЗНО з Матиматики");
            dataGridView1.Columns.Add("bal_ukr", "Бал ЗНО з Укр. Мови");
            dataGridView1.Columns.Add("bal_eng", "Бал ЗНО з Англійської");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            this.dataGridView1.Columns["IsNew"].Visible = false;
            this.dataGridView1.Columns["id_stud"].Visible = false;
            this.dataGridView1.Columns["number_stud"].Visible = false;
            this.dataGridView1.Columns["data_vstupu"].Visible = false;
            this.dataGridView1.Columns["scholarship"].Visible = false;
            this.dataGridView1.Columns["bal_mat"].Visible = false;
            this.dataGridView1.Columns["bal_ukr"].Visible = false;
            this.dataGridView1.Columns["bal_eng"].Visible = false;

        }

        private void ClearFields()
        {
            textBoxID_Inf.Text = "";
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

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetString(8), record.GetString(9), record.GetInt32(10), record.GetInt32(11), record.GetInt32(12), RowState.ModifiedNew);

        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from students";


            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateColumns1();
            CreateColumns();
            RefreshDataGrid(dataGridView1);
            RefreshDataGrid1(dataGridView2);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;


            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBoxID_Inf.Text = row.Cells[0].Value.ToString();
                textBoxStNa_Inf.Text = row.Cells[1].Value.ToString();
                textBoxLaNa_Inf.Text = row.Cells[2].Value.ToString();
                textBoxStPa_Inf.Text = row.Cells[3].Value.ToString();
                textBoxNumb_Inf.Text = row.Cells[4].Value.ToString();
                textBoxEmail_Inf.Text = row.Cells[5].Value.ToString();
                textBoxGrup_Inf.Text = row.Cells[6].Value.ToString();
                textBoxSpec_Inf.Text = row.Cells[7].Value.ToString();
                textBoxDataVst_Inf.Text = row.Cells[8].Value.ToString();
                textBoxStSt_Inf.Text = row.Cells[9].Value.ToString();
                textBoxBalMat_Inf.Text = row.Cells[10].Value.ToString();
                textBoxBalUkr_Inf.Text = row.Cells[11].Value.ToString();
                textBoxBalEng_Inf.Text = row.Cells[12].Value.ToString();
            }
        }



        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from students where concat (id_stud, name_stud, lastname_stud, patronymic__stud, number_stud, email_stud, group_stud, specialty_stud) like '%" + SearchWrite2.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }


        public void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[13].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[13].Value = RowState.Deleted;

        }

        private void pictureRefresh2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }


        private void buttonSave2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                Update();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                Update1();
            }
        }

        public void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[13].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from students where id_stud = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var lastname = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var patr = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var number = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var email = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var grup = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var spec = dataGridView1.Rows[index].Cells[7].Value.ToString();
                    var data = dataGridView1.Rows[index].Cells[8].Value.ToString();
                    var stypen = dataGridView1.Rows[index].Cells[9].Value.ToString();
                    var bal_mat = dataGridView1.Rows[index].Cells[10].Value.ToString();
                    var bal_ukr = dataGridView1.Rows[index].Cells[11].Value.ToString();
                    var bal_eng = dataGridView1.Rows[index].Cells[12].Value.ToString();

                    var changeQuary = $"update students set name_stud = '{name}', lastname_stud = '{lastname}', patronymic__stud = '{patr}', number_stud = '{number}', email_stud = '{email}', group_stud = '{grup}', specialty_stud = '{spec}', data_vstupu = '{data}', scholarship = '{stypen}', bal_mat = '{bal_mat}', bal_ukr = '{bal_ukr}', bal_eng = '{bal_eng}' where id_stud = '{id}'";

                    var command = new SqlCommand(changeQuary, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }




        public void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBoxID_Inf.Text;
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

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, name, lastname, patr, numb, email, grup, spec, data, sypend, mat, ukr, eng);
                dataGridView1.Rows[selectedRowIndex].Cells[13].Value = RowState.Modified;
            }

        }

        private void Print1()
        {
            string fileName = $"{textBoxStNa_Inf.Text}_{textBoxLaNa_Inf.Text}_{DateTime.Now:yyyy.MM.dd}.docx";
            string folderPath = @"C:\D\Coding\databases\MSSql base\files";
            string filePath = Path.Combine(folderPath, fileName);

            int fileIndex = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{textBoxStNa_Inf.Text}_{textBoxLaNa_Inf.Text}_{DateTime.Now:yyyy.MM.dd}_{fileIndex}.docx";
                filePath = Path.Combine(folderPath, fileName);
                fileIndex++;
            }
            using (WordprocessingDocument document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {

                MainDocumentPart mainPart = document.AddMainDocumentPart();

                // Створення структури документа.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Отримання текстових полів з panel4 і відсортовування їх за значенням тегів.
                var textboxes = panel4.Controls.OfType<TextBox>()
                    .OrderBy(t => int.Parse(t.Tag.ToString()))
                    .ToList();

                // Додавання тексту з кожного текстового поля на panel4 до нового абзацу в документі.
                foreach (TextBox textBox in textboxes)
                {
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text(textBox.Text));
                }

                // Закрити документ.
                mainPart.Document.Save();

            }
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = false;


        }

        private void buttonChange2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                Change();
                ClearFields();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                Change1();
                ClearFields1();
            }
            
        }



        private void buttonNew2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                AddForm addForm = new AddForm();
                addForm.Show();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                Add2Form add2Form = new Add2Form();
                add2Form.Show();
            }
        }

        private void buttonDelete2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви впевнені що хочете видалити цей запис?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[0])
                {
                    deleteRow();
                    ClearFields();
                }
                else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                {
                    deleteRow1();
                    ClearFields1();
                }
            }
        }

        private void SearchWrite2_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void pictureRubber2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Друкувати цей запис?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[0])
                {
                    Print1();
                }
                else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
                {
                    Print2();
                }
            }
        }
        


        /*=========================================================================*/



        private void CreateColumns1()
        {


            dataGridView2.Columns.Add("id_teach", "id");
            dataGridView2.Columns.Add("name_teach", "Ім'я");
            dataGridView2.Columns.Add("lastname_teach", "Призвіще");
            dataGridView2.Columns.Add("patronymic_teach", "По батькові");
            dataGridView2.Columns.Add("number_teach", "Номер телефону");
            dataGridView2.Columns.Add("email_teach", "Email");
            dataGridView2.Columns.Add("position_teach", "посада");
            dataGridView2.Columns.Add("IsNew", String.Empty);
            this.dataGridView2.Columns["IsNew"].Visible = false;
            this.dataGridView2.Columns["id_teach"].Visible = false;

        }

        private void ClearFields1()
        {
            textBoxIDteach.Text = "";
            textBoxNameTeach.Text = "";
            textBoxLastnameTeach.Text = "";
            textBoxPatronomicTeach.Text = "";
            textBoxNumberTeach.Text = "";
            textBoxEmailTeach.Text = "";
            textBoxPosTeach.Text = "";
        }


        private void ReadSingleRow1(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), RowState.ModifiedNew);

        }

        private void RefreshDataGrid1(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select * from teachers";


            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgv, reader);
            }
            reader.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dataGridView2.Rows[selectedRow];

                textBoxIDteach.Text = row.Cells[0].Value.ToString();
                textBoxNameTeach.Text = row.Cells[1].Value.ToString();
                textBoxLastnameTeach.Text = row.Cells[2].Value.ToString();
                textBoxPatronomicTeach.Text = row.Cells[3].Value.ToString();
                textBoxNumberTeach.Text = row.Cells[4].Value.ToString();
                textBoxEmailTeach.Text = row.Cells[5].Value.ToString();
                textBoxPosTeach.Text = row.Cells[6].Value.ToString();

            }
        }


        private void Search1(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string searchString = $"select * from teachers where concat (id_teach, name_teach, lastname_teach, patronymic_teach, number_teach, email_teach, position_teach) like '%" + SearchWrite.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow1(dgv, read);
            }

            read.Close();
        }




        private void deleteRow1()
        {
            int index = dataGridView2.CurrentCell.RowIndex;

            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }

            dataGridView2.Rows[index].Cells[7].Value = RowState.Deleted;

        }



        private void Update1()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from teachers where id_teach = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var lastname = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    var patron = dataGridView2.Rows[index].Cells[3].Value.ToString();
                    var number = dataGridView2.Rows[index].Cells[4].Value.ToString();
                    var email = dataGridView2.Rows[index].Cells[5].Value.ToString();
                    var pos = dataGridView2.Rows[index].Cells[6].Value.ToString();

                    var changeQuary = $"update teachers set name_teach = '{name}', lastname_teach = '{lastname}', patronymic_teach = '{patron}', number_teach = '{number}', email_teach = '{email}', position_teach = '{pos}' where id_teach = '{id}'";

                    var command = new SqlCommand(changeQuary, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }


        private void Change1()
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;

            var id = textBoxIDteach.Text;
            var name = textBoxNameTeach.Text;
            var lastname = textBoxLastnameTeach.Text;
            var patron = textBoxPatronomicTeach.Text;
            var number = textBoxNumberTeach.Text;
            var email = textBoxEmailTeach.Text;
            var pos = textBoxPosTeach.Text;


            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView2.Rows[selectedRowIndex].SetValues(id, name, lastname, patron, number, email, pos);
                dataGridView2.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;

            }
        }


        private void Print2()
        {
            string fileName = $"{textBoxNameTeach.Text}_{textBoxLastnameTeach.Text}_{DateTime.Now:yyyy.MM.dd}.docx";
            string folderPath = @"C:\D\Coding\databases\MSSql base\files";
            string filePath = Path.Combine(folderPath, fileName);

            int fileIndex = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{textBoxNameTeach.Text}_{textBoxLastnameTeach.Text}_{DateTime.Now:yyyy.MM.dd}_{fileIndex}.docx";
                filePath = Path.Combine(folderPath, fileName);
                fileIndex++;
            }



            using (WordprocessingDocument document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {

                MainDocumentPart mainPart = document.AddMainDocumentPart();

                // Create the document structure.
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Get the textboxes from panel4, and sort them by their tag value.
                var textboxes = panel3.Controls.OfType<TextBox>()
                    .OrderBy(t => int.Parse(t.Tag.ToString()))
                    .ToList();

                // Add text from each textbox in panel4 to a new paragraph in the document.
                foreach (TextBox textBox in textboxes)
                {
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text(textBox.Text));
                }

                // Close the document.
                mainPart.Document.Save();

            }
        }




        private void SearchWrite_TextChanged(object sender, EventArgs e)
        {
            Search1(dataGridView2);

        }

        private void pictureRubber_Click(object sender, EventArgs e)
        {
            ClearFields1();
        }

        private void pictureRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1(dataGridView2);
            ClearFields1();

        }


        /*===============================================*/

        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Програму створив Лош Олег Андрійович.\r\n\r\nEmail: losh.oleg.12@gmail.com\r\n\r\nЦя програма надає адміністрації коледжів і шкіл доступ до важливої інформації про студентів, включаючи ім’я, номер телефону, електронну адресу, групу та спеціальність.Користувачі можуть переглядати, редагувати, видаляти та створювати нові записи студентів, а також переглядати інформацію про викладачів.\r\n\r\nVersion: 1.0.0.", "Інформація про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form1 frm_sign = new Form1();

            frm_sign.Show();
        }

        
    }

}
