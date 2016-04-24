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
        //del od poseben igrach
        public Tile[,] Tiles = new Tile[8, 24];
        public int numBoats = 0;
        public bool shipRotation = false;
        public int MAXI=8;
        public int MAXJ=24;
        public Board(int numBoats, bool shipRotation, bool isSetup)
        {
            this.numBoats = numBoats;
            this.shipRotation = shipRotation;
            Height = 337;
            Width = 1013;
            int x = 5;
            int y = 5;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 24; j++)
                { //inicijalizacija na pole
                    Tiles[i, j] = new Tile(isSetup,i,j);
                    Tiles[i, j].MouseEnter += Tiles_Enter;//event handlers 
                    Tiles[i, j].MouseLeave += Tiles_Leave;
                    Tiles[i, j].MouseClick += Tiles_Click;
                    Tiles[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                    this.Controls.Add(Tiles[i, j]);
                    x += Tiles[i, j].Width + 2; //se zgolemuva pozicijata za shirinata plus 2 
                }
                y += Tiles[i, 0].Height + 2; //posle j pati se prefrla na drug red
                x = 5;//se vraka nazad na pochetna x
            }
           
        }
       
        private void Tiles_Enter(object sender, EventArgs e)
        {   //pri vleguvanje so glushecot se menuva bojata
            Tile temp = (Tile)sender;
            if (shipRotation)//spoder rotacijata na koja strana da se vrti
            {
                    if (temp.i < MAXI - numBoats) // ova e za da ne izleguva OutOfBounds indeksot
                    {// dolu se objasnati ovie funkcii
                        HighlighEnoughVerticalBoats(temp);
                    }
                    else
                    {
                        HighlightLessVerticalBoats(temp);
                    }
                
            }
            else
            {
                if (temp.j < MAXJ - numBoats)
                {
                    HighlighEnoughHorizontalBoats(temp);
                }
                else {
                    HighlightLessHorizontalBoats(temp);
                }
            }

            
            
        }
        private void Tiles_Leave(object sender, EventArgs e)
        {   //isto kako predhodno samo za brishenje
            Tile temp = (Tile)sender;
            if (shipRotation)
            {
                if (temp.i < MAXI - numBoats)
                {
                    UnHighlighEnoughVerticalBoats(temp);
                }
                else {
                    UnHighlightLessVerticalBoats(temp);
                }
            }
            else
            {
                if (temp.j < MAXJ - numBoats)
                {
                    UnHighlighEnoughHorizontalBoats(temp);
                }
                else {
                    UnHighlightLessHorizontalBoats(temp);
                }

            }

        }
        private void Tiles_Click(object sender , EventArgs e)
        {
            if (numHighlight() == numBoats) //ako ima highligh kolku num boats togash stavaj boat
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        if (Tiles[i, j].isHighLighted)//site shto se obelezhani gi setira kako brod 
                        {
                            Tiles[i, j].setBoat();
                        }
                    }

                }
            }
            shipRotation = true;
            
        }
        public int numHighlight()
        {
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    if (Tiles[i, j].isHighLighted)//site shto se obelezhani gi setira kako brod 
                    {
                        k++; 
                    }
                }

            }
            return k;
        }

        private void HighlighEnoughVerticalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i + k, temp.j].highLight();//k delovi nadolu ako ima dovolno mesto pravi highligh
                k++;
            }

        }
        private void UnHighlighEnoughVerticalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i + k, temp.j].unhighLight(); //isto kako predhodno
                k++;
            }

        }
        private void HighlightLessVerticalBoats(Tile temp)
        {   //TODO ova treba da se modificira bidejki iako praktichno ne izgleda dobro
                 int k = 0;
            int i = MAXI-numBoats;
                while (k < numBoats)
                {
                    Tiles[i + k, temp.j].highLight();//slichna e logikata samo shto namesto nadolu da odi odi nagore 
                    k++;
                }
           
        }
        private void UnHighlightLessVerticalBoats(Tile temp)
        {
            int k = 0;
            int i = MAXI - numBoats;
            while (k < numBoats)
                {
                    Tiles[i + k, temp.j].unhighLight();
                    k++;
                }
           
        }

        private void HighlighEnoughHorizontalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i, temp.j+k].highLight();
                k++;
            }

        }
        private void UnHighlighEnoughHorizontalBoats(Tile temp)
        {
            int k = 0;
            while (k < numBoats)
            {
                Tiles[temp.i, temp.j+k].unhighLight();
                k++;
            }

        }
        private void HighlightLessHorizontalBoats(Tile temp)
        {
            int k = 0;
            int j = MAXJ - numBoats;
            while (k < numBoats)
            {
                Tiles[temp.i, j+k].highLight();//slichna e logikata samo shto namesto nadolu da odi odi nagore 
                k++;
            }


          

        }
        private void UnHighlightLessHorizontalBoats(Tile temp)
        {
            
            int k = 0;
            int j = MAXJ - numBoats;
            while (k < numBoats)
            {
                Tiles[temp.i, j+k].unhighLight();
                k++;
            }

        }



    }
}
