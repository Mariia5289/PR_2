using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using PR_2.DBContext;

namespace PR_2.DBContext
{
    public partial class FormAddUsers : Form
    {
        public FormAddUsers()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        ModelEF model = new ModelEF();
        private void FormAddUsers_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = model.Roles.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //проверка входных данных
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$",
RegexOptions.IgnoreCase);
            if (!reg.IsMatch(textBoxEmail.Text))
            {
                MessageBox.Show("Почта не соотвествует требованиям!");
                return;
            }
            if (!textBoxPass.Text.Equals(textBoxPass2.Text))
            {
                MessageBox.Show("Пароли не равны!");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBoxLog.Text) ||
            String.IsNullOrWhiteSpace(textBoxPass.Text) ||
            String.IsNullOrWhiteSpace(textBoxFName.Text) ||
            String.IsNullOrWhiteSpace(textBoxSName.Text) ||
            ! maskedTextBox1.MaskCompleted)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            //Заполнение данных о новом пользователе
            Users users = new Users();
            users.ID = 0;
            users.Login = textBoxLog.Text;
            users.Password = textBoxPass.Text;
            users.Email = textBoxEmail.Text;
            users.Phone = maskedTextBox1.Text;
            users.First_Name = textBoxFName.Text;
            users.Second_Name = textBoxSName.Text;
            users.RoleID = (int)comboBoxRole.SelectedValue;
            users.Gender = radioButtonM.Checked ? "Мужской" : "Женский";
            try
            {
                //сохранение данных в БД
                model.Users.Add(users);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Данные добавленны!");
            Close();

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
