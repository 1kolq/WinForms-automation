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
    public partial class orderFuelVerification : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public orderFuelVerification()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);

            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM workers WHERE Логин = '" + workerLoginField.Text + "' AND Пароль='" + workerPassField.Text + "'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();

            

            if (reader.Read())
            {

                query = "SELECT Имя FROM workers WHERE Логин = '" + workerLoginField.Text + "'";

                OleDbCommand commandVerification = new OleDbCommand(query, myConnection);
                mainWorkerForm mainWorker = new mainWorkerForm();
                MessageBox.Show("Внимание, " + commandVerification.ExecuteScalar().ToString() + ", Ваша заявка оформлена!");
                this.Hide();


                string queryFuelOut = "SELECT [Вид топлива] FROM data";
                OleDbCommand commandFuel = new OleDbCommand(queryFuelOut, myConnection);
                commandFuel.ExecuteNonQuery();


                string queryFuelCountOut = "SELECT [В заказе] FROM data";
                OleDbCommand commandFuelCount = new OleDbCommand(queryFuelCountOut, myConnection);
                commandFuelCount.ExecuteNonQuery();

           
                int id = Convert.ToInt32(commandFuel.ExecuteScalar());

                string queryAdd = "SELECT [В заказе] FROM fuel WHERE id = " + id;
                OleDbCommand commandAddFuel = new OleDbCommand(queryAdd, myConnection);
                commandAddFuel.ExecuteNonQuery();
                int queryAddInt = Convert.ToInt32(commandAddFuel.ExecuteScalar());

                int inOrder = queryAddInt +  Convert.ToInt32(commandFuelCount.ExecuteScalar()); 
                string queryFuelIn = "UPDATE fuel SET [В заказе] = " + inOrder + " WHERE id = " + id;
                OleDbCommand commandFuelUpdate = new OleDbCommand(queryFuelIn, myConnection);
                commandFuelUpdate.ExecuteNonQuery();


                string queryDeleteRow = "DELETE * FROM data";
                OleDbCommand commandDeleteRow = new OleDbCommand(queryDeleteRow, myConnection);
                commandDeleteRow.ExecuteNonQuery();

                mainWorker.mainWorkerHelloLabel.Text = "Сервис-меню";

                mainWorker.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainWorkerForm mainWorker = new mainWorkerForm();
            mainWorker.ShowDialog();
        }
    }
}
