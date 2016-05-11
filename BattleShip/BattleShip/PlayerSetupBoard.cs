using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
   public class PlayerSetupBoard : Panel
    {
        //del od poseben igrach
        public static int MAXI = 10;
        public static int MAXJ = 10;
        public Tile[,] Tiles = new Tile[MAXI, MAXJ];
        public int numBoats = 0;
        public int shipRotation = 0 ;
        public List<HashSet<Index>> listOFBoats = new List<HashSet<Index>>();
        public Bitmap[] triImages = new Bitmap[3];
        public Bitmap[] triImagesVer = new Bitmap[3];
        public Bitmap[] fourImages = new Bitmap[4];
        public Bitmap[] fourImagesVer = new Bitmap[4];
        public Bitmap[] fiveImages = new Bitmap[5];
        public Bitmap[] fiveImagesVer = new Bitmap[5];
        public int[] numBoatsOnTile = { 3, 1, 1 };
       
        public PlayerSetupBoard(int numBoats, int shipRotation)
        {
            //Bitmap img = new Bitmap("../../Textures/water.jpg");
           
            loadImages();
            this.numBoats = numBoats;
            this.shipRotation = shipRotation;
            init();
           
        }
        public  void init()
        {
            Bitmap img = Properties.Resources.water;
            Height = 400;
            Width = 400;
            int x = 0;
            int y = 0;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
                { //inicijalizacija na pole
                    Tiles[i, j] = new Tile(i, j);
                    Tiles[i, j].setImage(img);
                    Tiles[i, j].MouseEnter += Tiles_Enter;//event handlers 
                    Tiles[i, j].MouseLeave += Tiles_Leave;
                    Tiles[i, j].MouseClick += Tiles_Click;
                    Tiles[i, j].Location = new System.Drawing.Point(x, y);//lokacija na pole
                    this.Controls.Add(Tiles[i, j]);
                    x += Tiles[i, j].Width; //se zgolemuva pozicijata za shirinata plus 2

                }
                y += Tiles[i, 0].Height; //posle j pati se prefrla na drug red
                x = 0;//se vraka nazad na pochetna x
                
            }
        }
       public void loadImages()
        {

            triImages[0] = Properties.Resources.TriPiece1;
            triImages[1] = Properties.Resources.TriPiece2;
            triImages[2] = Properties.Resources.TriPiece3;
            triImagesVer[0] = Properties.Resources.TriPiece1Ver;
            triImagesVer[1] = Properties.Resources.TriPiece2Ver;
            triImagesVer[2] = Properties.Resources.TriPiece3Ver;
            fourImages[0] = Properties.Resources.FourPiece1;
            fourImages[1] = Properties.Resources.FourPiece2;
            fourImages[2] = Properties.Resources.FourPiece3;
            fourImages[3] = Properties.Resources.FourPiece4;
            fourImagesVer[0] = Properties.Resources.FourPiece1Ver;
            fourImagesVer[1] = Properties.Resources.FourPiece2Ver;
            fourImagesVer[2] = Properties.Resources.FourPiece3Ver;
            fourImagesVer[3] = Properties.Resources.FourPiece4Ver;
            fiveImages[0] = Properties.Resources.FivePiece1;
            fiveImages[1] = Properties.Resources.FivePiece2;
            fiveImages[2] = Properties.Resources.FivePiece3;
            fiveImages[3] = Properties.Resources.FivePiece4;
            fiveImages[4] = Properties.Resources.FivePiece5;
            fiveImagesVer[0] = Properties.Resources.FivePiece1Ver;
            fiveImagesVer[1] = Properties.Resources.FivePiece2Ver;
            fiveImagesVer[2] = Properties.Resources.FivePiece3Ver;
            fiveImagesVer[3] = Properties.Resources.FivePiece4Ver;
            fiveImagesVer[4] = Properties.Resources.FivePiece5Ver;

        }
        private void Tiles_Enter(object sender, EventArgs e)
        {   //pri vleguvanje so glushecot se menuva bojata
            Tile temp = (Tile)sender;
            if (shipRotation==0)//spoder rotacijata na koja strana da se vrti
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
            if (shipRotation==0)
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
            this.OnClick(e);
            Tile t = (Tile)sender;
            int pos = 0;
            if (numHighlight() == numBoats) //ako ima highligh kolku num boats togash stavaj boat
            {
                if (numBoats == 3) numBoatsOnTile[0]--;
                if (numBoats == 4) numBoatsOnTile[1]--;
                if (numBoats == 5) numBoatsOnTile[2]--;
                
                if (shipRotation==0)
                {
                    while (t.isHighLighted)
                    {
                        pos = t.i - 1;
                        if (t.i != 0)
                        {
                            t = Tiles[t.i - 1, t.j];
                        }
                        else
                        {
                            break;
                        }
                    }
                   
                    int k = numBoats;
                    HashSet<Index> temp = new HashSet<Index>();
                    while (k > 0)
                    {
                        Tiles[pos + k, t.j].unhighLight();
                        Tiles[pos + k, t.j].setBoat();
                        temp.Add(new Index(pos + k, t.j));
                        if(numBoats==3)
                        Tiles[pos+k, t.j].setImage(triImagesVer[k - 1]);
                        if (numBoats == 4)
                            Tiles[pos + k, t.j].setImage(fourImagesVer[k - 1]);
                        if (numBoats == 5)
                            Tiles[pos + k, t.j].setImage(fiveImagesVer[k - 1]);
                        k--;
                    }
                    listOFBoats.Add(temp);
                   
                }
                else
                {
                    while (t.isHighLighted)
                    {
                        pos = t.j - 1;
                        if (t.j != 0)
                        {
                            t = Tiles[t.i, t.j - 1];
                        }
                        else
                        {
                            break;
                        }
                    
                    }

                    int k = numBoats;
                    HashSet<Index> temp = new HashSet<Index>();
                    while (k > 0)
                    {
                        Tiles[t.i, pos+k].unhighLight();
                        Tiles[t.i, pos+k].setBoat();
                        if(numBoats==3)
                        Tiles[t.i, pos + k].setImage(triImages[k-1]);
                        if (numBoats == 4)
                            Tiles[t.i, pos + k].setImage(fourImages[k - 1]);
                        if (numBoats == 5)
                            Tiles[t.i, pos + k].setImage(fiveImages[k - 1]);
                        temp.Add(new Index(t.i, pos+k));
                        k--;
                    }
                    listOFBoats.Add(temp);
                   
                }
                if (numBoats == 3 && numBoatsOnTile[0] < 1) this.Enabled = false;
                if (numBoats == 4 && numBoatsOnTile[1] < 1) this.Enabled = false;
                if (numBoats == 5 && numBoatsOnTile[2] < 1) this.Enabled = false;
            }
            
            
        }

        public List<HashSet<Index>> getBoatList()
        {
            return listOFBoats;
        }

        public int numHighlight()
        {
            int k = 0;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
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
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }


    }
}
