using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
   public class Board : Panel
    {
        public Tile[,] Tiles = new Tile[8, 24];
        public Board()
        {
            Height = 337;
            Width = 1013;
            int x = 5;
            int y = 5;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    Tiles[i, j] = new Tile();
                    Tiles[i, j].Location = new System.Drawing.Point(x, y);
                    this.Controls.Add(Tiles[i, j]);
                    x += Tiles[i, j].Width + 2;
                }
                y += Tiles[i, 0].Height + 2;
                x = 5;
            }
        }
    }
}
