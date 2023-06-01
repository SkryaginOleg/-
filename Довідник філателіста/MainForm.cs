using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Довідник_філателіста
{
    public partial class MainForm : Form
    {
        Thread th;
        public MainForm()
        {
            InitializeComponent();
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
            Application.Run(new AddingStamps());
        }


        private void button2_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm1()
        {
            Application.Run(new AddingPhilatelists());
        }


        private void button3_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm2()
        {
            Application.Run(new ListOfStamps());
        }


        private void button4_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm3);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm3()
        {
            Application.Run(new ListOfPhilatelists());
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ListStamps.FirstLaunch)
            {
                string[] s = File.ReadAllLines("ListOfStamp.txt");
                for (int i = 0; i < s.Length; i++)
                {
                    string[] rpas = s[i].Split(new char[] { ' ' });
                    ListStamps.Add(new Stamp(Convert.ToInt32(rpas[0]), rpas[1], Convert.ToInt32(rpas[2]), Convert.ToInt32(rpas[3]), Convert.ToDouble(rpas[4]), rpas[5]));
                }
                ListStamps.FirstLaunch = false;


                string[] t = File.ReadAllLines("ListOfPhilatelists.txt");
                for (int i = 0; i < t.Length; i++)
                {
                    string[] rpas = t[i].Split(new char[] { '.' });
                    ListPhilatelists.Add(new Philatelist(Convert.ToInt32(rpas[0]), rpas[1], (rpas[2]), (rpas[3])));
                    if (++i < t.Length && t[i] != "")
                    {                       
                        int[] rpas1 = Array.ConvertAll(t[i].Split(new char[] { '.' }), int.Parse);
                        ListPhilatelists.Philatelists[ListPhilatelists.Philatelists.Count-1].ListOfStamps = new List<int>(rpas1);
                    }
                }
                ListStamps.FirstLaunch = false;
            }
        }
    }
}