﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class OpponentWarBoard : Panel
    {
        public static int MAXI = 10;
        public static int MAXJ = 10;
        public BattleBoard BB;
        public Tile[,] WarTile = new Tile[MAXI, MAXJ];
        
       
        public List<HashSet<Index>> listOfBoats = new List<HashSet<Index>>();
        public OpponentWarBoard(Tile[,] t)
        {
            Height = 400;
            Width = 400;
            int x = 0;
            int y = 0;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
                {
                    if (t[i, j].boatHere)
                    {
                        WarTile[i, j] = new OpponentShipTile(i, j);
                        WarTile[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                       // WarTile[i, j].MouseClick += ShipTile_Click;
                        WarTile[i, j].Image = t[i, j].Image;
                        WarTile[i, j].img = t[i, j].img;
                        this.Controls.Add(WarTile[i, j]);
                        x += WarTile[i, j].Width;
                    }
                    else
                    {
                        WarTile[i, j] = new WaterTile(i, j);
                        WarTile[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                       // WarTile[i, j].MouseClick += Tile_Click;
                        WarTile[i, j].Image = t[i, j].Image;
                        this.Controls.Add(WarTile[i, j]);
                        x += WarTile[i, j].Width;
                    }
                    
                }
                y += WarTile[i, 0].Height; //posle j pati se prefrla na drug red
                x = 0;
                
            }
        }
        public void setListOfBoats(List<HashSet<Index>> oppBoats)
        {
            listOfBoats = oppBoats;
        }
        public void setBattleBoard(BattleBoard bb)
        {
            this.BB = bb;
        }
        
      

    }
}
