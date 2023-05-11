using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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

        private void button3_Click(object sender, EventArgs e)
        {
            th = new Thread(openNewForm1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }
        private void openNewForm1()
        {
            Application.Run(new ListOfStamps());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ListStamps.FirstLaunch)
            {
                string[] s = File.ReadAllLines("ListOfStamp.txt");

                for (int i = 0; i < s.Length; i++)
                {
                    string[] rpas = s[i].Split(new char[] { ' ' });
                    ListStamps.Add(new Stamp(ListStamps.Length, rpas[0], Convert.ToInt32(rpas[1]), Convert.ToInt32(rpas[2]), Convert.ToDouble(rpas[3]), rpas[4]));
                }
                ListStamps.FirstLaunch = false;
            }
        }
    }
}
