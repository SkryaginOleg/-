using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class AddingStamps : Form
    {
        public AddingStamps()
        {
            InitializeComponent();
            FormClosed += Form1_FormClosed;
        }


        Thread th;

        private void AddingStamps_Load(object sender, EventArgs e)
        {
            Text = "Додати марку";
            label6.Text = string.Empty;
            label7.Text = string.Empty;
            label8.Text = string.Empty;
            label9.Text = string.Empty;
            label10.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool right1;
            bool right2;
            bool right3;
            bool right4;
            bool right5;
            string features = null;
            string country = null;
            int year = 0;
            int circulation = 0;
            double cost = 0;

            if (Metod.Check_string(textBox1.Text))
            {
                country = textBox1.Text;
                label6.Text = "";
                textBox1.BackColor = System.Drawing.Color.White;
                right2 = true;
            }
            else
            {
                textBox1.BackColor = System.Drawing.Color.Salmon;
                label6.Text = "Ви вели хибне значення.";
                right2 = false;
            }
            //
            if (Metod.Check_string(textBox2.Text))
            {
                features = textBox2.Text;
                label7.Text = "";
                textBox2.BackColor = System.Drawing.Color.White;
                right1 = true;
            }
            else
            {
                textBox2.BackColor = System.Drawing.Color.Salmon;
                label7.Text = "Ви вели хибне значення.";
                right1 = false;
            }
            //
            if (Metod.Check_int(textBox3.Text))
            {
                year = Convert.ToInt32(textBox3.Text);
                label8.Text = "";
                textBox3.BackColor = System.Drawing.Color.White;
                right3 = true;
            }
            else
            {
                textBox3.BackColor = System.Drawing.Color.Salmon;
                label8.Text = "Ви вели хибне значення.";
                right3 = false;
            }
            //
            if (Metod.Check_int(textBox4.Text))
            {
                circulation = Convert.ToInt32(textBox4.Text);
                label9.Text = "";
                textBox4.BackColor = System.Drawing.Color.White;
                right4 = true;
            }
            else
            {
                textBox4.BackColor = System.Drawing.Color.Salmon;
                label9.Text = "Ви вели хибне значення.";
                right4 = false;
            }
            //
            if (Metod.Check_double(textBox5.Text))
            {
                cost = Convert.ToDouble(textBox5.Text);
                label10.Text = "";
                textBox5.BackColor = System.Drawing.Color.White;
                right5 = true;
            }
            else
            {
                textBox5.BackColor = System.Drawing.Color.Salmon;
                label10.Text = "Ви вели хибне значення.";
                right5 = false;
            }


            if (right1 && right2 && right3 && right4 && right5)
            {
                ListStamps.Add(new Stamp(ListStamps.MaxId + 1, country, year, circulation, cost, features));
                button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
                textBox1.Text = null;
                textBox2.Text = null; 
                textBox3.Text = null; 
                textBox4.Text = null; 
                textBox5.Text = null;
            }  
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListStamps.SaveInFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm()
        {
            Application.Run(new ListOfStamps());
        }

    }
}