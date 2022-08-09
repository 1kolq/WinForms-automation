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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class mainWorkerForm : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";

        private OleDbConnection myConnection;

        public mainWorkerForm()
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

        

        private void addUserButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.usersTableAdapter.Fill(this.databaseDataSet.users);
            workerRegUser workerReg = new workerRegUser();
            workerReg.ShowDialog();
        }

        private void addWorkerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            workerRegWorker workerReg = new workerRegWorker();
            workerReg.ShowDialog();
        }

        private void mainWorkerForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet2.fuel". При необходимости она может быть перемещена или удалена.
            this.fuelTableAdapter.Fill(this.databaseDataSet2.fuel);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet1.workers". При необходимости она может быть перемещена или удалена.
            this.workersTableAdapter.Fill(this.databaseDataSet1.workers);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "databaseDataSet.users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.databaseDataSet.users);
           
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
            comboBox1.SelectedIndex = 0; //

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if
                       (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(searchUserBox.Text
                       ))
                        {
                            dataGridView1.Rows[i].Selected = true; break;
                            
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        if
                       (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(searchUserBox.Text
                       ))
                        {
                            dataGridView1.Rows[i].Selected = false; break;

                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                    {
                        if
                       (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(searchWorkerBox.Text
                       ))
                        {
                            dataGridView2.Rows[i].Selected = true; break;

                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                    {
                        if
                       (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(searchWorkerBox.Text
                       ))
                        {
                            dataGridView2.Rows[i].Selected = false; break;

                        }
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            searchUserBox.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            searchWorkerBox.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {

           string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();



            string query = "DELETE FROM users WHERE id = " + id;
            OleDbCommand command = new OleDbCommand(query, myConnection);

            command.ExecuteNonQuery();

            MessageBox.Show("Данные успешно удалены!");
            this.usersTableAdapter.Fill(this.databaseDataSet.users);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string id = dataGridView2.CurrentRow.Cells[0].Value.ToString();



            string query = "DELETE FROM workers WHERE id = " + id;
            OleDbCommand command = new OleDbCommand(query, myConnection);

            command.ExecuteNonQuery();

            MessageBox.Show("Данные успешно удалены!");
            this.workersTableAdapter.Fill(this.databaseDataSet1.workers);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
           


            try
            {
                this.Validate();
                this.usersBindingSource.EndEdit();
                this.usersTableAdapter.Update(this.databaseDataSet.users);
                MessageBox.Show("Изменения сохранены!");
            }
            catch
            { MessageBox.Show("Update failed"); }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.workersBindingSource.EndEdit();
                this.workersTableAdapter.Update(this.databaseDataSet1.workers);
                MessageBox.Show("Изменения сохранены!");
            }
            catch
            { MessageBox.Show("Update failed"); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
          
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"usersTable.txt");

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
                System.Windows.Forms.MessageBox.Show("Данные таблицы Сотрудники успешно экспортированы!", "Экспорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"workersTable.txt");

            try
            {
                string sLine = "";


                for (int r = 0; r <= dataGridView2.Rows.Count - 1; r++)
                {

                    for (int c = 0; c <= dataGridView2.Columns.Count - 1; c++)
                    {
                        sLine = sLine + dataGridView2.Rows[r].Cells[c].Value;
                        if (c != dataGridView2.Columns.Count - 1)
                        {
                            sLine = sLine + ",";
                        }
                    }

                    file.WriteLine(sLine);
                    sLine = "";
                }

                file.Close();
                System.Windows.Forms.MessageBox.Show("Данные таблицы Сотрудники успешно экспортированы!", "Экспорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                file.Close();
            }
        }

            
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var result =MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

       

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.fuelBindingSource1.EndEdit();
                this.fuelTableAdapter.Update(this.databaseDataSet2.fuel);
                MessageBox.Show("Изменения сохранены!");
            }
            catch
            { MessageBox.Show("Update failed"); }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            dataGridView3.Refresh();
            this.fuelTableAdapter.Fill(this.databaseDataSet2.fuel);
        }

        private void button12_Click(object sender, EventArgs e)
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"fuelTable.txt");

            try
            {
                string sLine = "";


                for (int r = 0; r <= dataGridView3.Rows.Count - 1; r++)
                {

                    for (int c = 0; c <= dataGridView3.Columns.Count - 1; c++)
                    {
                        sLine = sLine + dataGridView3.Rows[r].Cells[c].Value;
                        if (c != dataGridView3.Columns.Count - 1)
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

        private void button13_Click(object sender, EventArgs e)
        {
            string id = dataGridView3.CurrentRow.Cells[0].Value.ToString();



            string query = "DELETE FROM fuel WHERE id = " + id;
            OleDbCommand command = new OleDbCommand(query, myConnection);

            command.ExecuteNonQuery();

            MessageBox.Show("Данные успешно удалены!");
            this.fuelTableAdapter.Fill(this.databaseDataSet2.fuel);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != 0 && textBox1.Text != String.Empty)
            {

                int selectedindex = comboBox1.SelectedIndex;
                string selectedindexStr = selectedindex.ToString();

                string queryFuelorder = "INSERT INTO data ([Вид топлива], [В заказе]) VALUES ('" + selectedindexStr + "', '" + textBox1.Text + "')";
                OleDbCommand command = new OleDbCommand(queryFuelorder, myConnection);
                command.ExecuteNonQuery();

                this.Hide();
                orderFuelVerification orderFuel = new orderFuelVerification();
                orderFuel.ShowDialog();
            }
            else
            {
                MessageBox.Show("Данные для заполнения заявки введены неверно!");
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

       
    }
}
