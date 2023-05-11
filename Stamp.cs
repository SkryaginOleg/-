using System.Collections.Generic;

namespace Довідник_філателіста
{
    public class Stamp
    {
        public int id;
        public readonly string features;
        public readonly string country;
        public readonly int year;
        public readonly int circulation;
        public readonly double cost;

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
        public static List<Stamp> Stamps = new List<Stamp>();
        public static int Length;

        public static void Add(Stamp stamp)
        {
            Stamps.Add(stamp);
            Length++;
        }
    }

    public static class MetodStamp
    {
        public static List<Stamp> SortCountry(List<Stamp> list, string country)
        {
            country = country.ToLower();
            List<Stamp> List = new List<Stamp>();
            foreach (Stamp stamp in list)
            {
                if (stamp.country.ToLower() == country)
                {
                    List.Add(stamp);
                }
            }
            return List;
        }

        public static List<Stamp> SortYear(List<Stamp> list, int MinY, int MaxY)
        {
            List<Stamp> List = new List<Stamp>();
            foreach (Stamp stamp in list)
            {
                if (stamp.year >= MinY && stamp.year <= MaxY)
                {
                    List.Add(stamp);
                }
            }
            return List;
        }

        public static List<Stamp> SortCirculation(List<Stamp> list, int MinCir, int MaxCir)
        {
            List<Stamp> List = new List<Stamp>();
            foreach (Stamp stamp in list)
            {
                if (stamp.circulation >= MinCir && stamp.circulation <= MaxCir)
                {
                    List.Add(stamp);
                }
            }
            return List;
        }

        public static List<Stamp> SortCost(List<Stamp> list, int MinC, int MaxC)
        {
            List<Stamp> List = new List<Stamp>();
            foreach (Stamp stamp in list)
            {
                if (stamp.cost >= MinC && stamp.cost <= MaxC)
                {
                    List.Add(stamp);
                }
            }
            return List;
        }


    }
}
