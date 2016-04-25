using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class ShipTile : Tile
    {
        public ShipTile(int i,int j): base(i,j)
        {
            BackColor = Color.Green;
        }
    }
}
