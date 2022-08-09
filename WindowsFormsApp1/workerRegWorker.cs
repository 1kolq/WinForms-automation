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
    public partial class workerRegWorker : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public workerRegWorker()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);

            myConnection.Open();
        }

        private void userLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();

        }
    
            private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainWorkerForm workerForm = new mainWorkerForm();
            workerForm.mainWorkerHelloLabel.Text = "Сервис-меню";
            workerForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "SELECT * FROM workers WHERE Логин = '" + textBox7.Text + "'";
            OleDbCommand command1 = new OleDbCommand(query1, myConnection);
            OleDbDataReader reader = command1.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Сотрудник с таким логином уже существует!");
            }
            else
            {
                string query = "INSERT INTO workers (Имя, Фамилия, Отчество, [Дата рождения], Зарплата, [Номер телефона], [Дата найма], Логин, Пароль, Должность)" +
               " VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', " +
               "'" + textBox10.Text + "', '" + textBox5.Text + "', '" + textBox8.Text + "', '" + textBox7.Text + "', '" + textBox6.Text + "', '" + textBox9.Text + "')";

                OleDbCommand command = new OleDbCommand(query, myConnection);

                command.ExecuteNonQuery();


                MessageBox.Show("Регистрация прошла успешно!");
                mainWorkerForm workerForm = new mainWorkerForm();
                workerForm.mainWorkerHelloLabel.Text = "Сервис-меню";
                this.Hide();
                workerForm.ShowDialog();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) 
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
