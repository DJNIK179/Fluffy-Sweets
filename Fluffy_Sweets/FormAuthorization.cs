using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fluffy_Sweets
{
    public struct Authorization
    {
        public string login;
        public string password;
        public string type;
    }
    public partial class FormAuthorization : Form
    {
            public static Authorization authorization = new Authorization();
        
            public FormAuthorization()
            {
                InitializeComponent();
            }

            private void label3_Click(object sender, EventArgs e)
            {

            }

            private void FormAuthorization_Load(object sender, EventArgs e)
            {

            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                if (textBoxLogin.Text == "" && textBoxPassword.Text == " ")
                {
                    MessageBox.Show("Введите данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool key = false;
                    foreach (AuthorizationSet authorizationSet in Program.wftDb.AuthorizationSet)
                    {
                        if (textBoxLogin.Text == authorizationSet.Login && textBoxPassword.Text == authorizationSet.Password)
                        {
                            key = true;
                        authorization.login = authorizationSet.Login;
                        authorization.password = authorizationSet.Password;
                        authorization.type = authorizationSet.Type;
                        }
                    }
                    if (!key)
                    {
                        MessageBox.Show("Проверьте данные", "Пользователь не найден", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxLogin.Text = "";
                        textBoxPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Данные введены верно", "Пользователь найден", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormMenu formMenu = new FormMenu();
                        formMenu.Show();
                        this.Hide();
                    }
                }

            }

            private void buttonCancel_Click(object sender, EventArgs e)
            {
                Close();
            }
    }
}
