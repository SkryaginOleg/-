using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Thread th;

        //Відкриває формі з таблицею всіх марок
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

        //Відкриває форму з таблицею всіх філателістів
        private void button4_Click_1(object sender, EventArgs e)
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

        //Відкриває форму з таблицею марок, які знаходятся
        //у власній колекції
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm4);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm4()
        {
            Application.Run(new OwnCollection());
        }

        //Загружаємо всі дані з файлів
        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "Меню";
            if (ListStamp.FirstLaunch)
            {
                string[] s = File.ReadAllLines("ListOfStamp.txt");
                for (int i = 0; i < s.Length; i++)
                {
                    string[] rpas = s[i].Split(new string[] { "/[]" }, 
                        StringSplitOptions.None);
                    ListStamp.Add(new Stamp(Convert.ToInt32(rpas[0]), rpas[1], 
                        Convert.ToInt32(rpas[2]), Convert.ToInt32(rpas[3]), 
                        Convert.ToDouble(rpas[4]), rpas[5]));
                    if (++i < s.Length && s[i] != "")
                    {
                        int[] rpas1 = Array.ConvertAll(s[i].Split(new string[] { "/|" }, 
                            StringSplitOptions.None), int.Parse);
                        ListStamp.Stamps[ListStamp.Stamps.Count - 1]
                            .ListOfPhilatelists = new List<int>(rpas1);
                    }
                }

                string[] t = File.ReadAllLines("ListOfPhilatelists.txt");
                int[] rpas0 = Array.ConvertAll(t[0].Split(new string[] { "/|" }, 
                    StringSplitOptions.None), int.Parse);
                ListPhilatelist.OwnCollection = new List<int>(rpas0);

                for (int i = 1; i < t.Length; i++)
                {
                    string[] rpas1 = t[i].Split(new string[] { "/[]" }, 
                        StringSplitOptions.None);
                    ListPhilatelist.Add(new Philatelist(Convert.ToInt32(rpas1[0]),
                        rpas1[1], (rpas1[2]), (rpas1[3])));
                    if (++i < t.Length && t[i] != "")
                    {                       
                        int[] rpas2 = Array.ConvertAll(t[i].Split(new string[] { "/|" }, 
                            StringSplitOptions.None), int.Parse);
                        ListPhilatelist.Philatelists[ListPhilatelist.Philatelists.Count-1]
                            .ListOfStamps = new List<int>(rpas2);
                    }
                }
                ListStamp.FirstLaunch = false;
            }
        }
    }
}