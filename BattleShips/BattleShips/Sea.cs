using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace BattleShips
{
    public class Sea
    {
        public const int SIZE = 5;
        public Rectangle[,] tiles;
        public SeaTile[,] SeaTiles;
        public List<Ship> ships;

        Graphics gObj;  //  Graphics objekt so koj ke gi obojuvame Tiles

        public Sea()   //  inicijaliziranje na celoto more
        {
            ships = new List<Ship>();
            tiles = new Rectangle[SIZE, SIZE];
            SeaTiles = new SeaTile[SIZE, SIZE];

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    tiles[i, j] = new Rectangle(i * 80 + 1, j * 80 + 1, 80 + 1, 80 + 1);
                    tiles[i, j].Height = 78;
                    tiles[i, j].Width = 78;
                    SeaTiles[i, j] = new SeaTile();
                }
            }
        }

        public void placeShips(List<Ship> ships)
        {
            this.ships = ships;
            foreach(Ship sh in ships)
            {
                for(int i = 0; i < sh.Length; ++i)
                {
                    Point p = sh.Coordinates[i];
                    int x = p.X;
                    int y = p.Y;

                    SeaTiles[x, y].IsTaken = true;
                }
            }
        }

        public DialogResult detectHit(Point l, Graphics g)
        {
            //  indeksite na SeaTiles so gi cuvame u matrica
            int i = 0;  
            int j = 0;

            findIndexes(ref i, ref j, l);

            if (!SeaTiles[i,j].IsAttacked)
            {
                SeaTiles[i, j].IsAttacked = true;
                gObj = g;
                if (SeaTiles[i, j].IsTaken) // zavisno od toa dali e HIT ili MISS go boeme so razlicna boja
                {   
                    gObj.FillRectangle(new SolidBrush(Color.Red), tiles[i, j]);

                    foreach(Ship s in ships)
                        if (s.attack(i, j) && !s.isAlive())
                            return DialogResult.Yes;

                    return DialogResult.No;
                }
                else
                {
                    gObj.FillRectangle(new SolidBrush(Color.DimGray), tiles[i, j]);
                    return DialogResult.Abort;
                }
            }
            return DialogResult.OK;
        }

        public void detectHover(Point l, Graphics g) 
        {
            for (int x = 0; x < 5; ++x)
                for (int y = 0; y < 5; ++y)
                    if (!SeaTiles[x, y].IsAttacked)
                        g.FillRectangle(new SolidBrush(Color.RoyalBlue), tiles[x, y]);

            //  indeksite na SeaTiles so gi cuvame u matrica
            if (l.X <= 0 || l.X >= 400 || l.Y <= 0 || l.Y >= 400)
                return;
            int i = 0;
            int j = 0;
            findIndexes(ref i, ref j, l);

            gObj = g;

            if (!SeaTiles[i, j].IsAttacked)
                gObj.FillRectangle(new SolidBrush(Color.DeepSkyBlue), tiles[i,j]);
            gObj.Dispose();

        }

        public void placingShipsHover(Point loc, Graphics g, int length, bool isVert)
        {
            for (int x = 0; x < 5; ++x)
                for (int y = 0; y < 5; ++y)
                    if (!SeaTiles[x, y].IsTaken)
                        g.FillRectangle(new SolidBrush(Color.RoyalBlue), tiles[x, y]);

            //  indeksite na SeaTiles so gi cuvame u matrica
            if (loc.X <= 0 || loc.X >= 400 || loc.Y <= 0 || loc.Y >= 400)
                return;
            int i = 0;
            int j = 0;
            findIndexes(ref i, ref j, loc);

            if(!isVert && i > Sea.SIZE - length) {
                i = Sea.SIZE - length;
            }
            else if (isVert && j > Sea.SIZE - length) {
                j = Sea.SIZE - length;
            }

            gObj = g;
            if (!SeaTiles[i, j].IsTaken)
            {
                for(int k = 0; k < length; ++k)
                {
                    if (isVert)
                    {
                        if (!SeaTiles[i, j + k].IsTaken)
                            gObj.FillRectangle(new SolidBrush(Color.SeaGreen), tiles[i, j + k]);
                    }
                    else if (!isVert && !SeaTiles[i + k, j].IsTaken)
                        gObj.FillRectangle(new SolidBrush(Color.SeaGreen), tiles[i + k, j]);
                }
            }
            gObj.Dispose();

        }

        public void findIndexes(ref int i, ref int j, Point l)
        {
            /*--- Trazeme go i ---*/
            if (l.X > 80 && l.X <= 160)
                i = 1;
            else if (l.X > 160 && l.X <= 240)
                i = 2;
            else if (l.X > 240 && l.X <= 320)
                i = 3;
            else if (l.X > 320 && l.X < 400)
                i = 4;

            /*--- Trazeme go j ---*/
            if (l.Y > 80 && l.Y <= 160)
                j = 1;
            else if (l.Y > 160 && l.Y <= 240)
                j = 2;
            else if (l.Y > 240 && l.Y <= 320)
                j = 3;
            else if (l.Y > 320 && l.Y <= 400)
                j = 4;
        }

        public void mouseLeft(Graphics g)
        {
            gObj = g;
            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    if (!SeaTiles[i, j].IsTaken && !SeaTiles[i, j].IsAttacked)
                        gObj.FillRectangle(new SolidBrush(Color.RoyalBlue), tiles[i, j]);
            gObj.Dispose();
        }

        public bool shipsLeft()
        {
            foreach (Ship s in ships)
                if (s.isAlive())
                    return true;
            return false;
        }
    }
}
