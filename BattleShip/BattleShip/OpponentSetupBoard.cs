using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class OpponentSetupBoard : Panel
    {
        public static int MAXI = 8;
        public static int MAXJ = 26;
        public Tile[,] Tiles = new Tile[MAXI, MAXJ];
        private Random tileChooseI = new Random();
        private Random tileChooseJ = new Random();
        private Random direction = new Random();
        private int maxBoats = 5;
        private int boatLength = 0;
        private Random randBoatLength = new Random();
        public OpponentSetupBoard()
        {
            Height = 337;
            Width = 1100;
            int x = 5;
            int y = 5;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
                { //inicijalizacija na pole
                    Tiles[i, j] = new Tile(i, j);
                    Tiles[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                    this.Controls.Add(Tiles[i, j]);
                    x += Tiles[i, j].Width + 2; //se zgolemuva pozicijata za shirinata plus 2 
                }
                y += Tiles[i, 0].Height + 2; //posle j pati se prefrla na drug red
                x = 5;//se vraka nazad na pochetna x
            }
        }
        public bool checkTrue(int dir,Tile t)
        {
            int k = boatLength;
            bool b = true;
            if (dir == 0)
            {
                while (k > 0)
                {
                    if (Tiles[t.i + k, t.j].boatHere)
                    {
                        b = false;
                    }
                    k--;
                }
            }
            else
            {
                while (k > 0)
                {
                    if (Tiles[t.i, t.j+k].boatHere)
                    {
                        b = false;
                    }
                    k--;
                }
            }
            

            return b;
        }
        public void setBoats()
        {
            int max = maxBoats;
            boatLength = randBoatLength.Next(3, 6);
            while (max > 0)
            {
                boatLength = randBoatLength.Next(3, 6);
                int i = tileChooseI.Next(7);
                int j = tileChooseJ.Next(24);
                int k = boatLength;
                int dir = direction.Next(2);
                if ( dir== 0)
                {
                    if (MAXI - i > boatLength && !Tiles[i,j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            while (k > 0)
                            {
                                Tiles[i + k, j].setBoat();
                                if (i + k == i + 1)
                                {
                                    Tiles[i + k, j].setStartAndDir(dir);
                                }
                                k--;
                            }
                            max--;
                        }
                    }
                    if (MAXI - i < boatLength )
                    {
                        int pos = MAXI - i;
                        if (!Tiles[i, j].boatHere)
                        {
                            if (checkTrue(dir, Tiles[pos, j]))
                            {
                                while (k > 0)
                                {
                                    Tiles[pos + k, j].setBoat();
                                    if (pos + k == pos + 1)
                                    {
                                        Tiles[pos + k, j].setStartAndDir(dir);
                                    }
                                    k--;
                                }
                                max--;
                            }
                        }
                    }

                }
                else
                {
                    if (MAXJ - j > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            while (k > 0)
                            {
                                Tiles[i, j + k].setBoat();
                                if (j + k == j + 1)
                                {
                                    Tiles[i, j+k].setStartAndDir(dir);
                                }
                                k--;
                            }
                            max--;
                        }
                    }
                    if (MAXI - j < boatLength)
                    {
                        int pos = MAXJ - j;
                        
                            if (!Tiles[i, pos].boatHere)
                            {
                            if (checkTrue(dir, Tiles[i, pos]))
                            {
                                while (k > 0)
                                {
                                    Tiles[i, pos + k].setBoat();
                                    if (pos + k == pos + 1)
                                    {
                                        Tiles[i , pos + k].setStartAndDir(dir);
                                    }
                                    k--;
                                }
                                max--;
                            }
                        }
                    }
                }
                
            }
        }
    }
}
