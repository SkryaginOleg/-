using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    //Філателісти: країна, ім'я, контактні координати, наявність рідкісних марок в колекції
    public class Philatelist
    {
        public int id;
        public readonly string name;
        public readonly string country;
        public readonly string contact_details;
        public List<int> ListOfStamps = new List<int>();

        public Philatelist(int id, string name, string country, string contact_details)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.contact_details = contact_details;
        }

        public bool SearchStamp( int id)
        {
            foreach (int element in ListOfStamps)
            {
                if (element == id)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static class ListPhilatelists
    {
        public static List<Philatelist> Philatelists = new List<Philatelist>();
        public static int MaxId;
        public static int actual_id;

        public static void Add(Philatelist philatelist)
        {
            if(philatelist.id > MaxId)
            {
                MaxId = philatelist.id;
            }
            Philatelists.Add(philatelist);
        }
        
        public static Philatelist SearchID(int id)
        {
            foreach (Philatelist philatelist in Philatelists)
            {
                if (philatelist.id == id)
                {
                    return philatelist;
                }
            }
            return null;
        }


        public static void SaveInFile()
        {
            string filePath = "ListOfPhilatelists.txt";
            File.WriteAllText(filePath, string.Empty);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (Philatelist philatelist in Philatelists)
                {
                    writer.WriteLine($"{philatelist.id}.{philatelist.name}.{philatelist.country}.{philatelist.contact_details}");
                    if (philatelist.ListOfStamps != null && philatelist.ListOfStamps.Count > 0)
                    {
                        writer.WriteLine(string.Join(".", philatelist.ListOfStamps));
                    }
                    else
                    {
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}