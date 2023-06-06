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

        //Створюємо таблицю
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

            //Заповнюємо таблицю
            Print(ListPhilatelist.Philatelists);
        }

        //Відкриваємо попередню форму
        private void button1_Click_1(object sender, EventArgs e)
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

        //Виводить у таблицю дані всіх філателістів, дані яких відповідають фільтрам
        private void Print(List<Philatelist> list)
        {
            foreach (Philatelist philatelist in list)
            {
                table.Rows.Add(philatelist.id, philatelist.name, 
                    philatelist.country, philatelist.contact_details);
            }
        }

        //Перевірка, чи відповідають дані філателіста фільтрам
        private bool name(Philatelist philatelist)
        {
            return philatelist.name.ToLower().IndexOf(textBox1.Text.ToLower(), 
                StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool country(Philatelist philatelist)
        {
            return philatelist.country.ToLower().IndexOf(textBox2.Text.ToLower(),
                StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool contact_details(Philatelist philatelist)
        {
            return philatelist.contact_details.ToLower().IndexOf(textBox3.Text.ToLower(), 
                StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private bool List(Philatelist philatelist)
        {
            return name(philatelist) && country(philatelist) 
                && contact_details(philatelist);
        }

        //Оновлює таблицю
        private void update()
        {
            table.Rows.Clear();
            foreach (Philatelist philatelist in ListPhilatelist.Philatelists)
            {
                if (List(philatelist))
                {
                    table.Rows.Add(philatelist.id, philatelist.name, 
                        philatelist.country, philatelist.contact_details);
                }
            }
        }

        //При взаємодією з полем вводу таблиця буде автоматично оновлюватися
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            update();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            update();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            update();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, 
            DataGridViewCellEventArgs e)
        {
            // Перевіряєм що обрана ячійка не є заголовком
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) 
            {
                // Отримуємо вибраний елемент
                var selectedRow = dataGridView1.Rows[e.RowIndex];
                ListPhilatelist.actual_id = Convert.ToInt32
                    (selectedRow.Cells["ID"].Value.ToString());
                OpenDialogForm();
            }
        }

        //Откриваємо у формі діалога нову форму, на якій буде повна
        //інформація про вибраного філателіста
        private void OpenDialogForm()
        {
            using (Philatelists_Info dialogForm = new Philatelists_Info())
            {
                dialogForm.FormClosing += DialogForm_FormClosing;
                dialogForm.ShowDialog(this);
            }
        }
        //Оновлюємо таблицю при закритті форми 
        private void DialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            update();
        }

        //Відкриваємо форму для створення нового екземпляру класа Philatelist
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
