using System.Collections.Generic;
using System.IO;

namespace Довідник_філателіста
{
    public class Stamp
    {
        public int id { get; }
        public string country { get; }
        public int year { get; }
        public int circulation { get; }
        public double cost { get; }
        public string features { get; }
        public List<int> ListOfPhilatelists = new List<int>();

        public Stamp(int id, string country, int year, int circulation, 
            double cost, string features)
        {
            this.id = id;
            this.country = country;
            this.year = year;
            this.circulation = circulation;
            this.cost = cost;
            this.features = features;
        }
    }

    public static class ListStamp
    {
        public static bool FirstLaunch = true;
        public static int actual_id;
        public static List<Stamp> Stamps = new List<Stamp>();
        public static int Length;
        public static int MaxId;
        public static double MaxCost;
        public static double MinCost = int.MaxValue;
        public static int MaxYear;
        public static int MinYear = 2023;
        public static int MaxCirculation;
        public static int MinCirculation = int.MaxValue;

        //Додаємо нову марку до довідника та визначаємо макимальні
        //і мінамальні величини таких данних, як ціна, тираж і рік
        public static void Add(Stamp stamp)
        {
            if (stamp.cost > MaxCost)
            {
                MaxCost = stamp.cost;
            }
            if(stamp.cost < MinCost)
            {
                MinCost = stamp.cost;
            }

            if (stamp.year > MaxYear)
            {
                MaxYear = stamp.year;
            }
            if (stamp.year < MinYear)
            {
                MinYear = stamp.year;
            }

            if (stamp.circulation > MaxCirculation)
            {
                MaxCirculation = stamp.circulation;
            }
            if (stamp.circulation < MinCirculation)
            {
                MinCirculation = stamp.circulation;
            }
            if (stamp.id > MaxId)
            {
                MaxId = stamp.id;
            }
            Stamps.Add(stamp);
            Length++;
        }

        //Пошук марки за її ID
        public static Stamp SearchID(int id)
        {
            foreach (Stamp stamp in Stamps)
            {
                if (stamp.id == id)
                {
                    return stamp;
                }
            }
            return null;
        }

        //Збереження всіх марок до файлу
        public static void SaveInFile()
        {
            string filePath = "ListOfStamp.txt";
            File.WriteAllText(filePath, string.Empty);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (Stamp stamp in Stamps)
                {
                    writer.WriteLine($"{stamp.id}/[]{stamp.country}/[]{stamp.year}/[]" +
                        $"{stamp.circulation}/[]{stamp.cost}/[]{stamp.features}");
                    if (stamp.ListOfPhilatelists != null && stamp.ListOfPhilatelists.Count > 0)
                    {
                        writer.WriteLine(string.Join("/|", stamp.ListOfPhilatelists));
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