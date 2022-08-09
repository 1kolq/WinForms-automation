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
    public partial class userLoginForm : Form
    {

         public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

         private OleDbConnection myConnection;

       


        public userLoginForm()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);

           myConnection.Open();

        }

        private void userLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
       
        }




        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstForm first = new firstForm();
            first.ShowDialog();
        }

        Point lastPoint;

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
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

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            userRegistrationForm userRegistration = new userRegistrationForm();
            userRegistration.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM users WHERE Логин = '" + loginField.Text + "' AND Пароль='" + passField.Text + "'";
             OleDbCommand command = new OleDbCommand(query, myConnection);
             OleDbDataReader reader = command.ExecuteReader();

             if (reader.Read())
             {
                query = "SELECT Имя FROM users WHERE Логин = '" + loginField.Text + "'";
                OleDbCommand commandHello = new OleDbCommand(query, myConnection);
                mainUserForm mainUser = new mainUserForm();
                mainUser.mainUserHelloLabel.Text = "Добро пожаловать, " + commandHello.ExecuteScalar().ToString();

                string queryid = "SELECT id FROM users WHERE Логин = '" + loginField.Text + "'";
                OleDbCommand commandid = new OleDbCommand(queryid, myConnection);

                string queryIntoId = "UPDATE userID SET userID = " + Convert.ToInt32(commandid.ExecuteScalar());
                OleDbCommand commandIntoId = new OleDbCommand(queryIntoId, myConnection);
                commandIntoId.ExecuteNonQuery();

                

                


                this.Hide();
                mainUser.ShowDialog();


             }
             else
             {
                 MessageBox.Show("Неверный логин или пароль");
             }
           

            
        }



    }
}
