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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Довідник_філателіста
{
    public partial class ListOfPhilatelists : Form
    {
        public ListOfPhilatelists()
        {
            InitializeComponent();
        }

        Thread th;
        DataTable table = new DataTable();


        private void ListOfPhilatelists_Load(object sender, EventArgs e)
        {
            Text = "Список філателістов";
            dataGridView1.DataSource = null;
            table.Rows.Clear();
            table.Columns.Clear();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Ім'я", typeof(string));
            table.Columns.Add("Країна", typeof(string));
            table.Columns.Add("Контактні_дані", typeof(string));

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            dataGridView1.DataSource = table;
            dataGridView1.Refresh();

            Print(ListPhilatelists.Philatelists);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread th;
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }

        private void openNewForm()
        {
            Application.Run(new MainForm());
        }

        public void Print(List<Philatelist> list)
        {
            foreach (Philatelist philatelist in list)
            {
                table.Rows.Add(philatelist.id, philatelist.name, philatelist.country, philatelist.contact_details);
            }
        }

        private bool name(Philatelist philatelist)
        {
            return philatelist.name.ToLower().IndexOf(textBox1.Text.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool country(Philatelist philatelist)
        {
            return philatelist.country.ToLower().IndexOf(textBox2.Text.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool contact_details(Philatelist philatelist)
        {
            return philatelist.contact_details.ToLower().IndexOf(textBox3.Text.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool List(Philatelist philatelist)
        {
            return name(philatelist) && country(philatelist) && contact_details(philatelist);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            table.Rows.Clear();
            foreach (Philatelist philatelist in ListPhilatelists.Philatelists)
            {
                if (List(philatelist))
                {
                    table.Rows.Add(philatelist.id, philatelist.name, philatelist.country, philatelist.contact_details);
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            table.Rows.Clear();
            foreach (Philatelist philatelist in ListPhilatelists.Philatelists)
            {
                if (List(philatelist))
                {
                    table.Rows.Add(philatelist.id, philatelist.name, philatelist.country, philatelist.contact_details);
                }
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            table.Rows.Clear();
            foreach (Philatelist philatelist in ListPhilatelists.Philatelists)
            {
                if (List(philatelist))
                {
                    table.Rows.Add(philatelist.id, philatelist.name, philatelist.country, philatelist.contact_details);
                }
            }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Проверяем, что ячейка не заголовок
            {
                // Получаем выбранный элемент
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                ListPhilatelists.actual_id = Convert.ToInt32(selectedRow.Cells["ID"].Value.ToString());
                th = new Thread(openNewForm1);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
        }
        private void openNewForm1()
        {
            Application.Run(new Philatelists_Info());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm2()
        {
            Application.Run(new AddingPhilatelists());
        }
    }
}
