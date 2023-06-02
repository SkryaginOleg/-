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


        private void Philatelists_Info_Load(object sender, EventArgs e)
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

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            dataGridView1.DataSource = table;
            dataGridView1.Refresh();

            label1.Text = Convert.ToString(philatelist.id); 
            label2.Text = philatelist.name; 
            label3.Text = philatelist.country; 
            label4.Text = philatelist.contact_details;
            label10.Visible = false;
            if (philatelist.ListOfStamps != null)
            {
                Print(philatelist);
            }
        }

        public void Print(Philatelist philatelist)
        {
            foreach (int i in philatelist.ListOfStamps)
            {
                Stamp stamp = ListStamps.SearchID(i);
                table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
            }
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
            bool found = false;
            if (!Metod.Check_int(textBox1.Text))
            {
                DialogResult result = MessageBox.Show("Ви ввеле хибне значення. Спробуйте ще раз.", "Помилка.", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    textBox1.Text = "";
                }
            }
            else
            {
                int id = Convert.ToInt32(textBox1.Text);
                foreach (int element in philatelist.ListOfStamps)
                {
                    if (element == id)
                    {
                        found = true; break;
                    }
                }
                if (found)
                {
                    DialogResult result = MessageBox.Show($"Марка з індексом {id} вже добавлена до колекції.", "Помилка.", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
                else if (ListStamps.SearchID(id) != null)
                {
                    philatelist.ListOfStamps.Add(id);
                    DialogResult result = MessageBox.Show($"Марка з індексом {id} добавлена до колекції.", "Успіх.", MessageBoxButtons.OK); ;
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                    Print(philatelist);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Марка з таким індексом не існує.", "Помилка.", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити всю інформацію про даного колекціонера?", "Підтвердження", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ListPhilatelists.Philatelists.Remove(philatelist);
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
                DialogResult result0 = MessageBox.Show("Інформація про колекціонера було видалено.", "Успіх", MessageBoxButtons.OK);
                this.Close();
                th.Start();
            }
            else if (result == DialogResult.Cancel) { }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            dataGridView1.Location = new Point(35, 89);
            dataGridView1.Height = 349;
            table.Rows.Clear();
            label10.Visible = true;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                    table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            dataGridView1.Height = 368;
            dataGridView1.Location = new Point(35, 70);
            table.Rows.Clear();
            Print(philatelist);
            label10.Visible = false;
        }
    }

}
