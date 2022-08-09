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
    public partial class mainUserForm : Form
    {

        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public mainUserForm()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {

            }
            else
            {
                this.Hide();
                firstForm first = new firstForm();
                first.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainUserForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet3.fuel". При необходимости она может быть перемещена или удалена.
            this.fuelTableAdapter.Fill(this.databaseDataSet3.fuel);

            label1.Text = label1.Text + " " + DateTime.Now.ToString("dd/MM/yyyy"); ;
            timer1.Start();

            textBox1.Text = "Объём топлива (л)";
            textBox1.ForeColor = Color.Gray;

            comboBox1.Items.Add("Выберите топливо");
            comboBox1.Items.Add("АИ-92");
            comboBox1.Items.Add("АИ-95");
            comboBox1.Items.Add("98");
            comboBox1.Items.Add("100");
            comboBox1.Items.Add("ДТ");
            comboBox1.Items.Add("Газ");
            comboBox1.SelectedIndex = 0;

            string queryID = "SELECT userID FROM userID";
            OleDbCommand commandID = new OleDbCommand(queryID, myConnection);
            labelid.Text = commandID.ExecuteScalar().ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        private void mainUserForm_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"fuelTable.txt");

            try
            {
                string sLine = "";


                for (int r = 0; r <= dataGridView1.Rows.Count - 1; r++)
                {

                    for (int c = 0; c <= dataGridView1.Columns.Count - 1; c++)
                    {
                        sLine = sLine + dataGridView1.Rows[r].Cells[c].Value;
                        if (c != dataGridView1.Columns.Count - 1)
                        {
                            sLine = sLine + ",";
                        }
                    }

                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
                System.Windows.Forms.MessageBox.Show("Данные таблицы Топливо успешно экспортированы!", "Экспорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            this.fuelTableAdapter.Fill(this.databaseDataSet3.fuel);
            MessageBox.Show("Данные обновлены");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            userEditForm userEdit = new userEditForm();
            userEdit.ShowDialog();
            
        }

        private void mainUserHelloLabel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fuelBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0 && textBox1.Text != String.Empty)
            {

                int selectedindex = comboBox1.SelectedIndex;
                string selectedindexStr = selectedindex.ToString();

                string queryCost = "SELECT Стоимость FROM fuel WHERE id = " + selectedindexStr;
                OleDbCommand commandCost = new OleDbCommand(queryCost, myConnection);
                
                int textboxCount = Convert.ToInt32(textBox1.Text) * Convert.ToInt32(commandCost.ExecuteScalar());
                string textboxCountString = textboxCount.ToString();
                label4.Text = "Стоимость " + textBox1.Text + " литра(ов) по курсу " + commandCost.ExecuteScalar().ToString() + " рублей за литр \n будет составлять " + textboxCountString + " рубля(ей)";


            }
            else
            {
                MessageBox.Show("Данные для покупки заполнены неверно!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex != 0 && textBox1.Text != String.Empty)
            {

                int selectedindex = comboBox1.SelectedIndex;
                string selectedindexStr = selectedindex.ToString();
                string queryStock = "SELECT Запасы FROM fuel WHERE id = " + selectedindexStr;
                OleDbCommand commandStock = new OleDbCommand(queryStock, myConnection);
                int stockInt = Convert.ToInt32(commandStock.ExecuteScalar()) - Convert.ToInt32(textBox1.Text);
                string stockString = stockInt.ToString();

                int labelidInt = Convert.ToInt32(labelid.Text);
                string queryPoints = "SELECT Бонусы FROM users WHERE id = " + labelidInt;
                OleDbCommand commandPoints = new OleDbCommand(queryPoints, myConnection);
                int pointsInt = Convert.ToInt32(commandPoints.ExecuteScalar());

                if (stockInt >= 0)
                {
                    if (pointsInt > 0)
                    {
                        string pointsString = pointsInt.ToString();
                        DialogResult dialogResult = MessageBox.Show("У вас " + pointsString + " бонуса(ов), Списать их?", "Бонусы", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string queryUpdatePoints = "UPDATE users SET Бонусы = 0 WHERE id = " + labelid.Text;
                            OleDbCommand commandUpdatePoints = new OleDbCommand(queryUpdatePoints, myConnection);
                            commandUpdatePoints.ExecuteNonQuery();

                            string queryCost = "SELECT Стоимость FROM fuel WHERE id = " + selectedindexStr;
                            OleDbCommand commandCost = new OleDbCommand(queryCost, myConnection);
                            int textboxCount = Convert.ToInt32(textBox1.Text) * Convert.ToInt32(commandCost.ExecuteScalar());
                            int textboxCountNew = textboxCount - pointsInt;
                            string textboxCountStringNew = textboxCountNew.ToString();
                            MessageBox.Show("Стоимость покупки после списания бонусов составит " + textboxCountStringNew + " рубля(ей)");
                            string queryNewStock = "UPDATE fuel SET Запасы = " + stockString + " WHERE id = " + selectedindexStr;
                            OleDbCommand commandNewStock = new OleDbCommand(queryNewStock, myConnection);
                            commandNewStock.ExecuteNonQuery();
                            this.fuelTableAdapter.Fill(this.databaseDataSet3.fuel);
                            this.Hide();
                            userProgressForm userProgress = new userProgressForm();
                            userProgress.ShowDialog();



                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.Hide();
                            mainUserForm mainUser = new mainUserForm();
                            mainUser.ShowDialog();
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Недостаточно топлива на заправочной станции!");
                }



            }
            else
            {
                MessageBox.Show("Данные для покупки не заполнены!");
            }

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }

        
    }
}
