using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class OwnCollection : Form
    {
        Thread th;
        DataTable table = new DataTable();
        public OwnCollection()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        //Виводить у таблицю всі марки, які перебувають у власній колекції
        public void Print()
        {
            table.Rows.Clear();
            foreach (int i in ListPhilatelist.OwnCollection)
            {
                Stamp stamp = ListStamp.SearchID(i);
                table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
            }
        }

        //Відкриває попередню форму
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm()
        {
            Application.Run(new MainForm());
        }

        //Створює таблицю
        private void OwnCollection_Load(object sender, EventArgs e)
        {
            label10.Visible = false;
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            table.Columns.Clear();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Страна", typeof(string));
            table.Columns.Add("Рік", typeof(string));
            table.Columns.Add("Тираж", typeof(int));
            table.Columns.Add("Вартість", typeof(double));
            table.Columns.Add("Назва", typeof(string));

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
            //Перевіряє чи існує хоча б одна марка в власній колекції
            if (ListPhilatelist.OwnCollection != null)
            {
                Print();
            }
        }

        //Очищає поле вводу та знімає фокус з нього
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        //Додає до власної колекції марку із вказаним ID
        private void button2_Click(object sender, EventArgs e)
        {
            if (!Check.Check_int(textBox1.Text))
            {
                DialogResult result = MessageBox.Show("Ви ввеле хибне значення. " +
                    "Спробуйте ще раз.", 
                    "Помилка.", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    textBox1.Text = "";
                }
            }
            else
            {
                int id = Convert.ToInt32(textBox1.Text);

                if (ListPhilatelist.OwnCollection.Contains(id))
                {
                    DialogResult result = MessageBox.Show($"Марка з індексом {id} " +
                        $"вже добавлена до колекції.", 
                        "Помилка.", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
                else if (ListStamp.SearchID(id) != null)
                {
                    ListPhilatelist.OwnCollection.Add(id);
                    ListStamp.SearchID(id).ListOfPhilatelists.Add(0);
                    textBox1.Text = "";
                    Print();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Марка з таким індексом не існує.", 
                        "Помилка.", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
            }
        }

        //Зберігає всі зміни до файлів
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListPhilatelist.SaveInFile();
            ListStamp.SaveInFile();
        }

        //Видаляє марку зі вказаним ID із власної колекції
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                if (!Check.Check_int(textBox2.Text))
                {
                    DialogResult result0 = MessageBox.Show("Ви ввеле хибне значення. Спробуйте ще раз.", 
                        "Помилка", MessageBoxButtons.OK);
                }
                else
                {
                    int id = Convert.ToInt32(textBox2.Text);
                    if (ListPhilatelist.OwnCollection.Contains(id))
                    {
                        ListPhilatelist.OwnCollection.Remove(id);
                        ListStamp.SearchID(id).ListOfPhilatelists.Remove(0);
                        DialogResult result0 = MessageBox.Show($"Марка з id:{id} була видалена.", 
                            "Успіх", MessageBoxButtons.OK);
                        Print();
                    }
                    else
                    {
                        DialogResult result0 = MessageBox.Show($"Марки з id:{id} не " +
                            $"існує в даній колекції.", "Помилка", MessageBoxButtons.OK);
                    }
                }
            }
            textBox2.Text = "";
        }

        //Виводить до таблиці всі марки, які перебувають у довіднику
        private void textBox1_Enter(object sender, EventArgs e)
        {
            dataGridView1.Location = new Point(12, 31);
            dataGridView1.Height = 407;
            table.Rows.Clear();
            label10.Visible = true;
            foreach (Stamp stamp in ListStamp.Stamps)
            {
                table.Rows.Add(stamp.id, stamp.country, stamp.year, 
                    stamp.circulation, stamp.cost, stamp.features);
            }
        }

        //Повертає стан таблиці до початкового
        private void textBox1_Leave(object sender, EventArgs e)
        {
            dataGridView1.Height = 426;
            dataGridView1.Location = new Point(12, 12);
            table.Rows.Clear();
            Print();
            label10.Visible = false;
        }
    }
}
