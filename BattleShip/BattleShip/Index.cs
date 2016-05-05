using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Index
    {
        public int i;
        public int j;
        public Index(int i,int j)
        {
            this.i = i;
            this.j = j;
        }
        public bool compare (Index ind)
        {
            if(ind.i==i && ind.j == j)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {

            return i*31+j*7;
        }
        public override bool Equals(object obj)
        {
            Index objectA = (Index)obj;
            return i == objectA.i && j == objectA.j;
        }
    }
}
