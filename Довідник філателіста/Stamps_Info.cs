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
        public Stamps_Info()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }

        Thread th;
        DataTable table = new DataTable();
        Stamp stamp = ListStamps.SearchID(ListStamps.actual_id);


        private void Stamps_Info_Load(object sender, EventArgs e)
        {
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

        private void Print()
        {
            table.Rows.Clear();
            foreach (int i in stamp.ListOfPhilatelists)
            {
                Philatelist philatelist = ListPhilatelists.SearchID(i);
                table.Rows.Add(philatelist.id, philatelist.name, philatelist.country, philatelist.contact_details);
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
            Application.Run(new ListOfStamps());
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListPhilatelists.SaveInFile();
            ListStamps.SaveInFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити всю інформацію про даного колекціонера?", "Підтвердження", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ListStamps.Stamps.Remove(stamp);
                foreach (int id in stamp.ListOfPhilatelists)
                {
                    ListPhilatelists.SearchID(id).ListOfStamps.Remove(stamp.id);
                }
                DialogResult result0 = MessageBox.Show("Інформація про колекціонера було видалено.", "Успіх", MessageBoxButtons.OK);
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
                this.Close();
                th.Start();
            }
            else if (result == DialogResult.Cancel) { }
        }
    }
}
