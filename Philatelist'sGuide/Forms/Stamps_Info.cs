using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class Stamps_Info : Form
    {
        //додаємо нову подію для закриття форми
        public Stamps_Info()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        DataTable table = new DataTable();
        Stamp stamp = ListStamp.SearchID(ListStamp.actual_id);

        //Виводимо дані всхі філателістів, в колекції яких знаходиться дана марка
        private void Print()
        {
            table.Rows.Clear();
            foreach (int i in stamp.ListOfPhilatelists)
            {
                if (i != 0)
                {
                    Philatelist philatelist = ListPhilatelist.SearchID(i);
                    table.Rows.Add(philatelist.id, philatelist.name, 
                        philatelist.country, 
                        philatelist.contact_details);
                }
            }
        }

        //створюємо таблицю
        private void Stamps_Info_Load(object sender, EventArgs e)
        {
            Text = "Інформація про марку";
            label1.Text = Convert.ToString(stamp.id);
            label2.Text = stamp.country;
            label3.Text = Convert.ToString(stamp.year);
            label4.Text = Convert.ToString(stamp.circulation);
            label10.Text = Convert.ToString(stamp.cost);
            label12.Text = stamp.features;

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
            Print();
        }

        //Закриваємо форму
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            OnFormClosing(new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        //Зберігаємо всі дані до файлів
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListPhilatelist.SaveInFile();
            ListStamp.SaveInFile();
        }

        //Видаляємо марку із довідника
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити " +
                "всю інформацію про дану марку?", 
                "Підтвердження", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ListStamp.Stamps.Remove(stamp);
                foreach (int id in stamp.ListOfPhilatelists)
                {
                    if (stamp.ListOfPhilatelists.Contains(0))
                    {
                        ListPhilatelist.OwnCollection.Remove(stamp.id);
                    }
                    if(id != 0)
                    {
                        ListPhilatelist.SearchID(id).ListOfStamps.Remove(stamp.id);
                    }
                }
                DialogResult result0 = MessageBox.Show("Інформація про марку було видалено.", 
                    "Успіх", MessageBoxButtons.OK);
                this.Close();
            }
            else if (result == DialogResult.Cancel) { }
        }
    }
}
