using System;
using System.Collections;
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
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;

namespace Довідник_філателіста
{
    public partial class Philatelists_Info : Form
    {
        public Philatelists_Info()
        {
            InitializeComponent();
        }
        Thread th;
        DataTable table = new DataTable();
        Philatelist philatelist = ListPhilatelists.SearchID(ListPhilatelists.actual_id);

        private void DisplayTextOnLabel(string text, int duration)
        {
            label10.Text = text;  // Установка текста на Label

            Timer timer = new Timer();
            timer.Interval = duration;
            timer.Tick += (sender, e) =>
            {
                label10.Text = "";  // Сброс текста на Label
                timer.Stop();  // Остановка таймера
                timer.Dispose();  // Освобождение ресурсов таймера
            };
            timer.Start();  // Запуск таймера
        }

        private void Philatelists_Info_Load(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(philatelist.id); 
            label2.Text = philatelist.name; 
            label3.Text = philatelist.country; 
            label4.Text = philatelist.contact_details;
            label10.Text = "";
            if (philatelist.ListOfStamps != null)
            {
                Print(philatelist);
            }
        }
        public void Print(Philatelist philatelist)
        {
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            table.Columns.Clear();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Страна", typeof(string));
            table.Columns.Add("Рік", typeof(string));
            table.Columns.Add("Тираж", typeof(int));
            table.Columns.Add("Вартість", typeof(double));
            table.Columns.Add("Особливості", typeof(string));

            foreach (int i in philatelist.ListOfStamps)
            {
                Stamp stamp = ListStamps.SearchID(i);
                table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            this.Close();
            th.Start();
        }
        private void openNewForm()
        {
            System.Windows.Forms.Application.Run(new ListOfPhilatelists());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            bool found = false;
            if (!Metod.Check_int(textBox1.Text))
            {
                textBox1.BackColor = System.Drawing.Color.Salmon;
                label10.Text = "Ви ввеле хибне значення.";
            }
            else
            {
                foreach (int element in philatelist.ListOfStamps)
                {
                    if (element == id)
                    {
                        found = true; break;
                    }
                }
                if (found)
                {
                    label10.Text = "Марка з таким індексом вже добавлена до колекції.";
                }
                else if (ListStamps.SearchID(id) != null)
                {
                    philatelist.ListOfStamps.Add(id);
                    label10.Text = "";                   
                    textBox1.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    DisplayTextOnLabel("Марки з таким індексом не існує.", 4000);
                }
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListPhilatelists.Philatelists.Remove(philatelist);
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            this.Close();
            th.Start();
        }
    }

}
