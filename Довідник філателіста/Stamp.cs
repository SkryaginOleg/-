﻿using System.Collections.Generic;
using System.IO;

namespace Довідник_філателіста
{
    public class Stamp
    {
        public int id;
        public readonly string country;
        public readonly int year;
        public readonly int circulation;
        public readonly double cost;
        public readonly string features;
        public List<int> ListOfPhilatelists = new List<int>();

        public Stamp(int id, string country, int year, int circulation, double cost, string features)
        {
            this.id = id;
            this.country = country;
            this.year = year;
            this.circulation = circulation;
            this.cost = cost;
            this.features = features;
        }
    }

    public static class ListStamps
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

        public static void SaveInFile()
        {
            string filePath = "ListOfStamp.txt";
            File.WriteAllText(filePath, string.Empty);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (Stamp stamp in Stamps)
                {
                    writer.WriteLine($"{stamp.id}/[]{stamp.country}/[]{stamp.year}/[]{stamp.circulation}/[]{stamp.cost}/[]{stamp.features}");
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