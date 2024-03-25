using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSqlDataBase
{
    public partial class Full_Inf_Form : Form
    {
        private MainForm mainForm;


        public Full_Inf_Form()
        {
            InitializeComponent();
            mainForm = new MainForm();
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


        private void Full_Inf_Form_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonNew2_Click(object sender, EventArgs e)
        {
            
            Show();
        }

        private void buttonSave2_Click(object sender, EventArgs e)
        {
            
            Update();
        }

        private void buttonChange2_Click(object sender, EventArgs e)
        {

            mainForm.Change();
            
        }

        private void buttonDelete2_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Ви впевнені що хочете видалити цей запис?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mainForm.deleteRow();
                ClearFields();
            }

        }
    }
}
