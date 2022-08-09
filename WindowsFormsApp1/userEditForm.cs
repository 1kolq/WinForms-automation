using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class userEditForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public userEditForm()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);

            myConnection.Open();
        }

        private void userLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();

        }

        private void userEditForm_Load(object sender, EventArgs e)
        {

            string queryID = "SELECT userID FROM userID";
            OleDbCommand commandID = new OleDbCommand(queryID, myConnection);

            string queryName = "SELECT Имя FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandName = new OleDbCommand(queryName, myConnection);
            textBox1.Text = commandName.ExecuteScalar().ToString();

            string querySurname = "SELECT Фамилия FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandSurname = new OleDbCommand(querySurname, myConnection);
            textBox2.Text = commandSurname.ExecuteScalar().ToString();

            string querySecondname = "SELECT Отчество FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandSecondname = new OleDbCommand(querySecondname, myConnection);
            textBox3.Text = commandSecondname.ExecuteScalar().ToString();

            string queryBirthdate = "SELECT [Дата рождения] FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandBirthdate = new OleDbCommand(queryBirthdate, myConnection);
            textBox4.Text = commandBirthdate.ExecuteScalar().ToString();

            string queryPhonenumber = "SELECT [Номер телефона] FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandPhonenumber = new OleDbCommand(queryPhonenumber, myConnection);
            textBox5.Text = commandPhonenumber.ExecuteScalar().ToString();

            string queryLogin = "SELECT Логин FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandLogin = new OleDbCommand(queryLogin, myConnection);
            textBox6.Text = commandLogin.ExecuteScalar().ToString();

            string queryPass = "SELECT Пароль FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandPass = new OleDbCommand(queryPass, myConnection);
            textBox7.Text = commandPass.ExecuteScalar().ToString();

            textBox6.Enabled = false;

            string queryPoints = "SELECT Бонусы FROM users WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandPoints = new OleDbCommand(queryPoints, myConnection);
            label9.Text = "Баланс: " + commandPoints.ExecuteScalar().ToString();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainUserForm mainForm = new mainUserForm();
            mainForm.ShowDialog();
        }

        Point lastPoint;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string queryID = "SELECT userID FROM userID";
            OleDbCommand commandID = new OleDbCommand(queryID, myConnection);


            string queryName = "UPDATE users SET Имя = '" + textBox1.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandName = new OleDbCommand(queryName, myConnection);
            commandName.ExecuteNonQuery();

            string querySurname = "UPDATE users SET Фамилия = '" + textBox2.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandSurname = new OleDbCommand(querySurname, myConnection);
            commandSurname.ExecuteNonQuery();

            string querySecondname = "UPDATE users SET Отчество = '" + textBox3.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandSecondname = new OleDbCommand(querySecondname, myConnection);
            commandSecondname.ExecuteNonQuery();

            string queryBirthdate = "UPDATE users SET [Дата рождения] = '" + textBox4.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandBirthdate = new OleDbCommand(queryBirthdate, myConnection);
            commandBirthdate.ExecuteNonQuery();

            string queryPhonenumber = "UPDATE users SET [Номер телефона] = '" + textBox5.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandPhonenumber = new OleDbCommand(queryPhonenumber, myConnection);
            commandPhonenumber.ExecuteNonQuery();

            string queryPass = "UPDATE users SET Пароль = '" + textBox7.Text + "' WHERE id = " + commandID.ExecuteScalar();
            OleDbCommand commandPass = new OleDbCommand(queryPass, myConnection);
            commandPass.ExecuteNonQuery();

            MessageBox.Show("Изменения сохранены!");
            this.Hide();
            mainUserForm mainUser = new mainUserForm();
            mainUser.ShowDialog();
        }
    }
}
