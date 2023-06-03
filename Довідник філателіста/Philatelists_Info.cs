using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class Philatelists_Info : Form
    {
        public Philatelists_Info()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
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
            table.Rows.Clear();
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
            Application.Run(new ListOfPhilatelists());
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
         
                if (philatelist.SearchStamp(id))
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
                    ListStamps.SearchID(id).ListOfPhilatelists.Add(philatelist.id);
                    textBox1.Text = "";
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
                foreach (int id in philatelist.ListOfStamps)
                {
                    ListStamps.SearchID(id).ListOfPhilatelists.Remove(philatelist.id);
                }
                DialogResult result0 = MessageBox.Show("Інформація про колекціонера було видалено.", "Успіх", MessageBoxButtons.OK);
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                if (!Metod.Check_int(textBox2.Text))
                {
                    DialogResult result0 = MessageBox.Show("Ви ввеле хибне значення. Спробуйте ще раз.", "Помилка", MessageBoxButtons.OK);
                }
                else
                {
                    int id = Convert.ToInt32(textBox2.Text);
                    if (philatelist.SearchStamp(id))
                    {
                        philatelist.ListOfStamps.Remove(id);
                        ListStamps.SearchID(id).ListOfPhilatelists.Remove(philatelist.id);
                        DialogResult result0 = MessageBox.Show($"Марка з id:{id} була видалена.", "Успіх", MessageBoxButtons.OK);
                        Print(philatelist);
                    }
                    else
                    {
                        DialogResult result0 = MessageBox.Show($"Марки з id:{id} не існує в даній колекції.", "Помилка", MessageBoxButtons.OK);
                    }
                }
            }
            textBox2.Text = "";
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListPhilatelists.SaveInFile();
            ListStamps.SaveInFile();
        }
    }

}
