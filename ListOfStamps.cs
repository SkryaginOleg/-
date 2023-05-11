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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Довідник_філателіста
{
    public partial class ListOfStamps : Form
    {
        public ListOfStamps()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();

        private void button1_Click(object sender, EventArgs e)
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

        public void Print(List<Stamp> list)
        {
            dataGridView1.DataSource = null; 
            table.Rows.Clear();
            table.Columns.Clear();

            table.Columns.Add("№", typeof(int));
            table.Columns.Add("Страна", typeof(string));
            table.Columns.Add("Рік", typeof(string));
            table.Columns.Add("Тираж", typeof(int));
            table.Columns.Add("Вартість", typeof(double));
            table.Columns.Add("Особливості", typeof(string));
            int count = 1;
            foreach (Stamp stamp in list)
            {
                table.Rows.Add(count, stamp.country, stamp.year, stamp.circulation, stamp.cost, stamp.features);
                count++;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        private void ListOfStamps_Load(object sender, EventArgs e)
        {
            Print(ListStamps.Stamps);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Stamp> list = ListStamps.Stamps;

            if (Metod.Check_string(textBox1.Text))
            {
                list = MetodStamp.SortCountry(list, textBox1.Text);
            }            
            Print(list);
        }

    }

}
