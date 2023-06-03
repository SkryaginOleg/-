using System;
using System.Threading;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class AddingPhilatelists : Form
    {
        public AddingPhilatelists()
        {
            InitializeComponent();
            this.FormClosed += Form1_FormClosed;
        }

        Thread th;

        private void button2_Click(object sender, EventArgs e)
        {
            bool right1;
            bool right2;
            bool right3;
            string name = null;
            string country = null;
            string contact_details = null;

            if (Metod.Check_string(textBox2.Text))
            {
                contact_details = textBox2.Text;
                label4.Text = "";
                textBox2.BackColor = System.Drawing.Color.White;
                right1 = true;
            }
            else
            {
                textBox2.BackColor = System.Drawing.Color.Salmon;
                label4.Text = "Ви вели хибне значення.";
                right1 = false;
            }
            //
            if (Metod.Check_string(textBox1.Text))
            {
                name = textBox1.Text;
                label5.Text = "";
                textBox1.BackColor = System.Drawing.Color.White;
                right2 = true;
            }
            else
            {
                textBox1.BackColor = System.Drawing.Color.Salmon;
                label5.Text = "Ви вели хибне значення.";
                right2 = false;
            }
            //
            if (Metod.Check_string(textBox3.Text))
            {
                country = textBox3.Text;
                label6.Text = "";
                textBox3.BackColor = System.Drawing.Color.White;
                right3 = true;
            }
            else
            {
                textBox3.BackColor = System.Drawing.Color.Salmon;
                label6.Text = "Ви вели хибне значення.";
                right3 = false;
            }
            

            if (right1 && right2 && right3 )
            {
                ListPhilatelists.Add(new Philatelist(ListPhilatelists.MaxId+1, name, country, contact_details));
                button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListPhilatelists.SaveInFile();            
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm()
        {
            Application.Run(new ListOfPhilatelists());
        }
    }
}
