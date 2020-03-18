using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Boats
{
    public partial class Form1 : Form
    {
        string ConnectionString;
        string Login;
        string Password;

        public Form1()
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["Boats.Properties.Settings._16is22ConnectionString"].ConnectionString;

        }

        private void GOBtn_Click(object sender, EventArgs e)
        {
            Login = loginTB.Text.Trim();
            Password = passwordTB.Text.Trim();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                Int32 countUser = 0;
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT COUNT (*) FROM [dbo].[User] WHERE Login = @Login AND Password = @Password";
                command.Connection = connection;

                command.Parameters.Add("@Login", SqlDbType.VarChar);
                command.Parameters["@Login"].Value = Login;

                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = Password;


                try
                {
                    connection.Open();
                    countUser = (Int32)command.ExecuteScalar();
                    if (countUser == 1)
                    {
                        MessageBox.Show("Авторизация успешна.");
                                             
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
