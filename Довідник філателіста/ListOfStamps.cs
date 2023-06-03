using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Довідник_філателіста
{
    public partial class ListOfStamps : Form
    {
        public ListOfStamps()
        {
            InitializeComponent();
        }
        Thread th;
        DataTable table = new DataTable();

        public void Print(List<Stamp> list)
        {
            foreach (Stamp stamp in list)
            {
                table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
            }           
        }

        private void ListOfStamps_Load(object sender, EventArgs e)
        {
            Text = "Список марок";
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

            Print(ListStamps.Stamps);
            label3.Text = Convert.ToString(ListStamps.MaxCost);
            label4.Text = Convert.ToString(ListStamps.MinCost);
            label8.Text = Convert.ToString(ListStamps.MaxYear);
            label9.Text = Convert.ToString(ListStamps.MinYear);
            textBox2.Text = "0";
            textBox3.Text = $"{ListStamps.MaxCirculation}";
            label13.Text = Convert.ToString(ListStamps.Length);
            label14.Text = Convert.ToString(ListStamps.Length);
            label17.Text = Convert.ToString(ListStamps.Length);
            label18.Text = Convert.ToString(ListStamps.Length);
        }

        private bool List(Stamp stamp)
        {
            return Country(stamp) && Cost(stamp) && Year(stamp) && Circulation(stamp);
        }

        private bool Country(Stamp stamp)
        {
            return stamp.country.ToLower().IndexOf(textBox1.Text.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool Cost(Stamp stamp)
        {
            bool MinCost = stamp.cost >= trackBar2.Value;
            bool MaxCost = stamp.cost <= ListStamps.MaxCost - trackBar1.Value;
            return MinCost && MaxCost;
        }

        private bool Year(Stamp stamp)
        {
            bool MinYear = stamp.year >= trackBar4.Value;
            bool MaxYear = stamp.year <= ListStamps.MaxYear - trackBar3.Value;
            return MinYear && MaxYear;
        }

        private bool Circulation(Stamp stamp)
        {
            bool MinCirculation = stamp.circulation >= Convert.ToInt32(textBox2.Text);
            bool MaxCirculation = stamp.circulation <= Convert.ToInt32(textBox3.Text);
            return MinCirculation && MaxCirculation && Metod.Check_int(textBox2.Text) && Metod.Check_int(textBox3.Text);
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            table.Rows.Clear();
            int count = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (List(stamp))
                {
                    count++;
                    table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
                }
            }
            label18.Text = Convert.ToString(count++);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = Convert.ToInt32(ListStamps.MaxCost - ListStamps.MinCost);
            trackBar1.SmallChange = 10;
            label3.Text = Convert.ToString(ListStamps.MaxCost - trackBar1.Value);
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (Cost(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label13.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar2.Minimum = Convert.ToInt32(ListStamps.MinCost);
            trackBar2.Maximum = Convert.ToInt32(ListStamps.MaxCost);
            trackBar2.SmallChange = 10;
            label4.Text = Convert.ToString(trackBar2.Value);
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (Cost(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label13.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackBar3.Minimum = 0;
            trackBar3.Maximum = Convert.ToInt32(ListStamps.MaxYear - ListStamps.MinYear);
            trackBar3.SmallChange = 10;
            label8.Text = Convert.ToString(ListStamps.MaxYear - trackBar3.Value);
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (Year(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label14.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            trackBar4.Minimum = Convert.ToInt32(ListStamps.MinYear);
            trackBar4.Maximum = Convert.ToInt32(ListStamps.MaxYear);
            trackBar4.SmallChange = 10;
            label9.Text = Convert.ToString(trackBar4.Value);
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (Year(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label14.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!Metod.Check_int(textBox2.Text))
            {
                textBox2.Text = "0";
            }
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (Circulation(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label17.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (!Metod.Check_int(textBox3.Text))
            {
                textBox3.Text = $"{ListStamps.MaxCirculation}";
            }
            int count = 0;
            int count1 = 0;
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (List(stamp))
                {
                    count++;
                }
                if (List(stamp))
                {
                    count1++;
                }
            }
            label17.Text = Convert.ToString(count);
            label18.Text = Convert.ToString(count1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
            foreach (Stamp stamp in ListStamps.Stamps)
            {
                if (List(stamp))
                {
                    table.Rows.Add(stamp.id, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
                }
            }
        }

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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Проверяем, что ячейка не заголовок
            {
                // Получаем выбранный элемент
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                ListStamps.actual_id = Convert.ToInt32(selectedRow.Cells["ID"].Value.ToString());
                th = new Thread(openNewForm2);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
        }
        private void openNewForm2()
        {
            Application.Run(new Stamps_Info());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm1()
        {
            Application.Run(new AddingStamps());
        }
    }
}