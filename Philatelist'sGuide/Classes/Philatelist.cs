using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Довідник_філателіста
{
    //Філателісти: країна, ім'я, контактні координати,
    //наявність рідкісних марок в колекції
    public class Philatelist
    {
        public int id { get; }
        public string name { get; }
        public string country { get; }
        public string contact_details { get; }
        //Список всіх ID марок, які перебувають у колекції філателіста
        public List<int> ListOfStamps = new List<int>();       

        public Philatelist(int id, string name, string country, 
            string contact_details)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.contact_details = contact_details;
        }

        //Пошук марки за її ID
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

    public static class ListPhilatelist
    {
        // Список всіх філателістів
        public static List<Philatelist> Philatelists = new List<Philatelist>();
        //Список ID всіх марок, які перебувають у власній колекції
        public static List<int> OwnCollection = new List<int>();
        public static int MaxId=1;
        public static int actual_id;

        public static void Add(Philatelist philatelist)
        {
            if(philatelist.id > MaxId)
            {
                MaxId = philatelist.id;
            }
            Philatelists.Add(philatelist);
        }
        
        //Пошук філателіста за його ID
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

        //Збереження даних про філателістів до файлу
        public static void SaveInFile()
        {
            string filePath = "ListOfPhilatelists.txt";
            File.WriteAllText(filePath, string.Empty);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(string.Join("/|", OwnCollection));
                foreach (Philatelist philatelist in Philatelists)
                {
                    writer.WriteLine($"{philatelist.id}/[]{philatelist.name}/[]{philatelist.country}" +
                        $"/[]{philatelist.contact_details}");
                    if (philatelist.ListOfStamps != null && philatelist.ListOfStamps.Count > 0)
                    {
                        writer.WriteLine(string.Join("/|", philatelist.ListOfStamps));
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