﻿using System;
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
        public MainForm()
        {
            InitializeComponent();
        }

        Thread th;


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


        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "Меню";
            if (ListStamps.FirstLaunch)
            {
                string[] s = File.ReadAllLines("ListOfStamp.txt");
                for (int i = 0; i < s.Length; i++)
                {
                    string[] rpas = s[i].Split(new string[] { "/[]" }, StringSplitOptions.None);
                    ListStamps.Add(new Stamp(Convert.ToInt32(rpas[0]), rpas[1], Convert.ToInt32(rpas[2]), Convert.ToInt32(rpas[3]), Convert.ToDouble(rpas[4]), rpas[5]));
                    if (++i < s.Length && s[i] != "")
                    {
                        int[] rpas1 = Array.ConvertAll(s[i].Split(new string[] { "/|" }, StringSplitOptions.None), int.Parse);
                        ListStamps.Stamps[ListStamps.Stamps.Count - 1].ListOfPhilatelists = new List<int>(rpas1);
                    }
                }

                string[] t = File.ReadAllLines("ListOfPhilatelists.txt");
                int[] rpas0 = Array.ConvertAll(t[0].Split(new string[] { "/|" }, StringSplitOptions.None), int.Parse);
                ListPhilatelists.OwnCollection = new List<int>(rpas0);

                for (int i = 1; i < t.Length; i++)
                {
                    string[] rpas1 = t[i].Split(new string[] { "/[]" }, StringSplitOptions.None);
                    ListPhilatelists.Add(new Philatelist(Convert.ToInt32(rpas1[0]), rpas1[1], (rpas1[2]), (rpas1[3])));
                    if (++i < t.Length && t[i] != "")
                    {                       
                        int[] rpas2 = Array.ConvertAll(t[i].Split(new string[] { "/|" }, StringSplitOptions.None), int.Parse);
                        ListPhilatelists.Philatelists[ListPhilatelists.Philatelists.Count-1].ListOfStamps = new List<int>(rpas2);
                    }
                }
                ListStamps.FirstLaunch = false;
            }
        }
    }
}