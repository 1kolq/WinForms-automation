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
    public partial class userProgressForm : Form
    {

        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public userProgressForm()
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
            this.Hide();
            mainUserForm mainUser = new mainUserForm();
            string queryUpdatePoints = "UPDATE users SET Бонусы = 0 WHERE id = " + mainUser.labelid.Text;
            OleDbCommand commandUpdatePoints = new OleDbCommand(queryUpdatePoints, myConnection);
            //commandUpdatePoints.ExecuteNonQuery();
            mainUser.ShowDialog();

        }

        private void userProgressForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Visible = false;
            closeButton.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if(progressBar1.Value == 100)
            {
                timer1.Stop();
                closeButton.Visible = true;
                label1.Visible = true;
            }
            else
            {
                progressBar1.Value = progressBar1.Value + 1;
            }

        }

       
    }
}
