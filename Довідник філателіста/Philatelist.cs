using System.Collections.Generic;

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
    }
}