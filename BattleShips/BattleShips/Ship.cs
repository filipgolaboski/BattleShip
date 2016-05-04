using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleShips
{
    public class Ship
    {
        public List<Point> Coordinates { get; set; }
        public int Length { get; set; }
        public bool IsVertical { get; set; }
        private bool[] hits;

        public Ship (List<Point> coords, int l, bool o)
        {
            IsVertical = o;
            if (l > 4)
            {
                Length = 4;
            } else Length = l;

            hits = new bool[Length];
            for (int k = 0; k < Length; ++k)
                hits[k] = false;

            Coordinates = new List<Point>();
            foreach (Point p in coords)
            {
                Coordinates.Add(p);
            }
        }

        public bool isAlive()
        {
            foreach (bool h in hits)
                if (!h)
                    return true;
            return false;
        }

        public bool intersectsWith(Ship s)
        {
            foreach(Point p in Coordinates)
            {
                if (s.Coordinates.Contains(p))
                    return true;
            }
            return false;
        }

        public bool attack(int x, int y)
        {
            Point target = new Point(x, y);
            if (Coordinates.Contains(target))
            {
                hits[Coordinates.IndexOf(target)] = true;
                return true;
            }
            return false;
        }
    }
}
