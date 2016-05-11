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
    public class OpponentSetupBoard : Panel
    {
        public static int MAXI = 10;
        public static int MAXJ = 10;
        public Tile[,] Tiles = new Tile[MAXI, MAXJ];
        private Random tileChooseI = new Random();
        private Random tileChooseJ = new Random();
        private Random direction = new Random();
        public List<HashSet<Index>> listOfBoats = new List<HashSet<Index>>();
        private int boatLength = 0;
        public Bitmap[] triImages = new Bitmap[3];
        public Bitmap[] triImagesVer = new Bitmap[3];
        public Bitmap[] fourImages = new Bitmap[4];
        public Bitmap[] fourImagesVer = new Bitmap[4];
        public Bitmap[] fiveImages = new Bitmap[5];
        public Bitmap[] fiveImagesVer = new Bitmap[5];
        public OpponentSetupBoard()
        {
            Height = 400;
            Width = 400;
            init();
        }
        public void init()
        {
            Bitmap img = new Bitmap("../../Textures/water.jpg");
            loadImages();
            int x = 0;
            int y = 0;
            for (int i = 0; i < MAXI; i++)
            {
                for (int j = 0; j < MAXJ; j++)
                { //inicijalizacija na pole
                    Tiles[i, j] = new Tile(i, j);
                    Tiles[i, j].setImage(img);
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

            triImages[0] = new Bitmap("../../Textures/TriPiece1.png");
            triImages[1] = new Bitmap("../../Textures/TriPiece2.png");
            triImages[2] = new Bitmap("../../Textures/TriPiece3.png");
            triImagesVer[0] = new Bitmap("../../Textures/TriPiece1Ver.png");
            triImagesVer[1] = new Bitmap("../../Textures/TriPiece2Ver.png");
            triImagesVer[2] = new Bitmap("../../Textures/TriPiece3Ver.png");
            fourImages[0] = new Bitmap("../../Textures/FourPiece1.png");
            fourImages[1] = new Bitmap("../../Textures/FourPiece2.png");
            fourImages[2] = new Bitmap("../../Textures/FourPiece3.png");
            fourImages[3] = new Bitmap("../../Textures/FourPiece4.png");
            fourImagesVer[0] = new Bitmap("../../Textures/FourPiece1Ver.png");
            fourImagesVer[1] = new Bitmap("../../Textures/FourPiece2Ver.png");
            fourImagesVer[2] = new Bitmap("../../Textures/FourPiece3Ver.png");
            fourImagesVer[3] = new Bitmap("../../Textures/FourPiece4Ver.png");
            fiveImages[0] = new Bitmap("../../Textures/FivePiece1.png");
            fiveImages[1] = new Bitmap("../../Textures/FivePiece2.png");
            fiveImages[2] = new Bitmap("../../Textures/FivePiece3.png");
            fiveImages[3] = new Bitmap("../../Textures/FivePiece4.png");
            fiveImages[4] = new Bitmap("../../Textures/FivePiece5.png");
            fiveImagesVer[0] = new Bitmap("../../Textures/FivePiece1Ver.png");
            fiveImagesVer[1] = new Bitmap("../../Textures/FivePiece2Ver.png");
            fiveImagesVer[2] = new Bitmap("../../Textures/FivePiece3Ver.png");
            fiveImagesVer[3] = new Bitmap("../../Textures/FivePiece4Ver.png");
            fiveImagesVer[4] = new Bitmap("../../Textures/FivePiece5Ver.png");

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
            setBoat(3);
            setBoat(3);
            setBoat(3);
           setBoat(4);
           setBoat(5);
        }
        public void setBoat(int leng) {
            bool boatSet = false;
            boatLength = leng;
            
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
                            HashSet<Index> boat = new HashSet<Index>();
                            while (k > 0)
                            {
                                if (leng == 3)
                                Tiles[i + k, j].setOpBoat(triImagesVer[k-1]);
                                if (leng == 4)
                                    Tiles[i + k, j].setOpBoat(fourImagesVer[k-1]);
                                if (leng == 5)
                                    Tiles[i + k, j].setOpBoat(fiveImagesVer[k-1]);
                                boat.Add(new Index(i + k, j));
                                k--;
                            }
                            listOfBoats.Add(boat);
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
                                HashSet<Index> boat = new HashSet<Index>();
                                while (k > 0)
                                {
                                    if(leng==3)
                                    Tiles[pos + k, j].setOpBoat(triImagesVer[k-1]);
                                    if (leng == 4)
                                        Tiles[pos + k, j].setOpBoat(fourImagesVer[k-1]);
                                    if (leng == 4)
                                        Tiles[pos + k, j].setOpBoat(fiveImagesVer[k-1]);
                                    boat.Add(new Index(pos + k, j));
                                    k--;
                                }
                                listOfBoats.Add(boat);
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
                            HashSet<Index> boat = new HashSet<Index>();
                            while (k > 0)
                            {
                                if(leng==3)
                                Tiles[i, j + k].setOpBoat(triImages[k-1]);
                                if (leng == 4)
                                    Tiles[i, j + k].setOpBoat(fourImages[k-1]);
                                if (leng == 5)
                                    Tiles[i, j + k].setOpBoat(fiveImages[k-1]);
                                boat.Add(new Index(i, j+k));
                                k--;
                            }
                            listOfBoats.Add(boat);
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
                                HashSet<Index> boat = new HashSet<Index>();
                                while (k > 0)
                                {
                                    if(leng==3)
                                    Tiles[i, pos + k].setOpBoat(triImages[k-1]);
                                    if (leng == 4)
                                        Tiles[i, pos + k].setOpBoat(fourImages[k-1]);
                                    if (leng == 5)
                                        Tiles[i, pos + k].setOpBoat(fiveImages[k-1]);
                                    boat.Add(new Index(i, pos + k));
                                    k--;
                                }
                                boatSet = true;
                                listOfBoats.Add(boat);
                            }
                        }
                    }
                }

            }
        }

       public List<HashSet<Index>> getListOfBoats()
        {
            return listOfBoats;
        }
       
    }
}
