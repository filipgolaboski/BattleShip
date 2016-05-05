using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public class OpponentSetupBoard : Panel
    {
        public static int MAXI = 10;
        public static int MAXJ = 10;
        public Tile[,] Tiles = new Tile[MAXI, MAXJ];
        private Random tileChooseI = new Random();
        private Random tileChooseJ = new Random();
        private Random direction = new Random();
        private int maxBoats = 5;
        private int boatLength = 0;
        private Random randBoatLength = new Random();
        public OpponentSetupBoard()
        {
            Height = 400;
            Width = 400;
            init();
        }
        public void init()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
                { //inicijalizacija na pole
                    Tiles[i, j] = new Tile(i, j);
                    Tiles[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                    this.Controls.Add(Tiles[i, j]);
                    x += Tiles[i, j].Width ; //se zgolemuva pozicijata za shirinata plus 2
                    
                }
                y += Tiles[i, 0].Height; //posle j pati se prefrla na drug red
                x = 0;//se vraka nazad na pochetna x
                

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
            setTriBoat();
            setTriBoat();
            setTriBoat();
           setFourBoat();
           setFiveBoat();
        }
        public void setTriBoat() {
            bool boatSet = false;
            boatLength = 3;
            
            int k = boatLength;
            int dir = direction.Next(2);
            while (!boatSet)
            {
                int i = tileChooseI.Next(10);
                int j = tileChooseJ.Next(10);
                if (dir == 0)
                {
                    if (MAXI - i > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            while (k > 0)
                            {
                                Tiles[i + k, j].setOpBoat();
                                k--;
                            }
                            boatSet = true;
                        }
                    }
                    if (MAXI - i < boatLength)
                    {
                        int pos = MAXI - boatLength - 1;
                        if (!Tiles[pos, j].boatHere)
                        {
                            if (checkTrue(dir, Tiles[pos, j]))
                            {
                                while (k > 0)
                                {
                                    Tiles[pos + k, j].setOpBoat();
                                    k--;
                                }
                                boatSet = true;
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
                                Tiles[i, j + k].setOpBoat();
                                k--;
                            }
                            boatSet = true;
                        }
                    }
                    if (MAXI - j < boatLength)
                    {
                        int pos = MAXJ - boatLength-1;

                        if (!Tiles[i, pos].boatHere)
                        {
                            if (checkTrue(dir, Tiles[i, pos]))
                            {
                                while (k > 0)
                                {
                                    Tiles[i, pos + k].setOpBoat();

                                    k--;
                                }
                                boatSet = true;
                            }
                        }
                    }
                }

            }
        }

        public void setFourBoat()
        {
            bool boatSet = false;
            boatLength = 4;
            
            int k = boatLength;
            int dir = direction.Next(2);
            while (!boatSet)
            {
                int i = tileChooseI.Next(10);
                int j = tileChooseJ.Next(10);
                if (dir == 0)
                {
                    if (MAXI - i > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            while (k > 0)
                            {
                                Tiles[i + k, j].setOpBoat();
                                k--;
                            }
                            boatSet = true;
                        }
                    }
                    if (MAXI - i < boatLength)
                    {
                        int pos = MAXI - i;
                        if (!Tiles[pos, j].boatHere)
                        {
                            if (checkTrue(dir, Tiles[pos, j]))
                            {
                                while (k > 0)
                                {
                                    Tiles[pos + k, j].setOpBoat();
                                    k--;
                                }
                                boatSet = true;
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
                                Tiles[i, j + k].setOpBoat();
                                k--;
                            }
                            boatSet = true;
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
                                    Tiles[i, pos + k].setOpBoat();

                                    k--;
                                }
                                boatSet = true;
                            }
                        }
                    }
                }

            }
        }
        public void setFiveBoat()
        {
            bool boatSet = false;
            boatLength = 5;
          
            int k = boatLength;
            int dir = direction.Next(2);
            while (!boatSet)
            {
                int i = tileChooseI.Next(10);
                int j = tileChooseJ.Next(10);
                if (dir == 0)
                {
                    if (MAXI - i > boatLength && !Tiles[i, j].boatHere)
                    {
                        if (checkTrue(dir, Tiles[i, j]))
                        {
                            while (k > 0)
                            {
                                Tiles[i + k, j].setOpBoat();
                                k--;
                            }
                            boatSet = true;
                        }
                    }
                    if (MAXI - i < boatLength)
                    {
                        int pos = MAXI - i;
                        if (!Tiles[pos, j].boatHere)
                        {
                            if (checkTrue(dir, Tiles[pos, j]))
                            {
                                while (k > 0)
                                {
                                    Tiles[pos + k, j].setOpBoat();
                                    k--;
                                }
                                boatSet = true;
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
                                Tiles[i, j + k].setOpBoat();
                                k--;
                            }
                            boatSet = true;
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
                                    Tiles[i, pos + k].setOpBoat();

                                    k--;
                                }
                                boatSet = true;
                            }
                        }
                    }
                }

            }
        }
    }
}
