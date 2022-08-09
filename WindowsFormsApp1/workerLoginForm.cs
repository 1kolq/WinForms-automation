using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class workerLoginForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public workerLoginForm()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);

            myConnection.Open();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstForm first = new firstForm();
            first.ShowDialog();
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

        private void workerLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM workers WHERE Логин = '" + workerLoginField.Text + "' AND Пароль='" + workerPassField.Text + "'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                query = "SELECT Имя FROM workers WHERE Логин = '" + workerLoginField.Text + "'";
                OleDbCommand commandHello = new OleDbCommand(query, myConnection);
                this.Hide();
                mainWorkerForm mainWorker = new mainWorkerForm();
                mainWorker.mainWorkerHelloLabel.Text = "Добро пожаловать, " + commandHello.ExecuteScalar().ToString();
                mainWorker.ShowDialog();


            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void workerLoginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void workerPassField_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
