using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShips
{
    public partial class Form1 : Form
    {

        GFX opponent;
        GFX player;
        Sea oppSea;
        Sea playSea;
        bool init = false;
        bool playerInit = true;
        Stack<Point> tilesAIShoot;
        Point lastHit;
        bool gameStarted = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            oppSea = new Sea();
            playSea = new Sea();
            tilesAIShoot = new Stack<Point>();
            lastHit = new Point();
            tbStatus.Text = "Place your ships and prepare for battle!";
            tbStatus.Refresh();
            cbShipLength.SelectedIndex = 0;
            opponentSea.Enabled = false;
            initializeAISea();
            
        }

        private void opponentSea_Paint(object sender, PaintEventArgs e) // Inicijalizira se protivnickoto more
        {
            if(!gameStarted)
                opponent = new GFX(e.Graphics);
        }

        private void playerSea_Paint(object sender, PaintEventArgs e)   // Inicijalizira se moreto na igracot
        {
            if(!gameStarted)
                 player = new GFX(e.Graphics);
        }

        private void opponentSea_Click(object sender, EventArgs e)
        {
            Point mouse = Cursor.Position;              // koordinati na cursor vo odnos na ekran
            mouse = opponentSea.PointToClient(mouse);   // koordinati na cursor vo odnos na opponentSea
            DialogResult res = oppSea.detectHit(mouse, opponentSea.CreateGraphics());
            if (res == DialogResult.Abort)
            {
                tbStatus.Text = "You missed!";
                Thread.Sleep(750);
                AI();
            }
            else if (res == DialogResult.No)
            {
                tbStatus.Text = "H I T!      Fire again!";
            } else if (res == DialogResult.Yes)
            {
                checkSea(oppSea);
            }
        }

        private void checkSea(Sea sea)
        {
            if(!sea.shipsLeft())
            {
                tbStatus.Text = "V I C T O R Y!";
                DialogResult res = MessageBox.Show("Congratulations! You win!!! Start new game?", "V I C T O R Y",
                    MessageBoxButtons.YesNo
                    );
                if (res == DialogResult.Yes) {
                    Application.Restart();
                    Environment.Exit(0);
                }
                else
                {
                    Application.Exit();
                }
            } else
            {
                tbStatus.Text = "You destroyed opponent's ship. Nice job! Fire again!";
            }
        }

        private void opponentSea_MouseMove(object sender, MouseEventArgs e)     // mnogu slicno na gornata funkcija
        {
            if (init)
            {
                Point mouse = Cursor.Position;
                mouse = opponentSea.PointToClient(mouse);
                oppSea.detectHover(mouse, opponentSea.CreateGraphics());
            }
        }

        private void opponentSea_MouseLeave(object sender, EventArgs e)
        {
            Point mouse = Cursor.Position;
            mouse = opponentSea.PointToClient(mouse);
            oppSea.detectHover(mouse, opponentSea.CreateGraphics());
        }


        public void AI()
        {
            Random rand = new Random();
            Graphics g = playerSea.CreateGraphics();
            int x;
            int y;
            

            while (true)
            {

                for (int i = 0; i < 2; i++)     // AI is thinking
                {
                    tbStatus.Text = "Opponent is thinking.";
                    tbStatus.Refresh();
                    Thread.Sleep(250);
                    tbStatus.Text = "Opponent is thinking..";
                    tbStatus.Refresh();
                    Thread.Sleep(250);
                    tbStatus.Text = "Opponent is thinking...";
                    tbStatus.Refresh();
                    Thread.Sleep(250);
                }

                if (tilesAIShoot.Count == 0)    // Ako nema prethodno zacuvani tiles koi treba da gi puka
                {
                    x = rand.Next(0, Sea.SIZE);     // zimame proizvolni koordinati
                    y = rand.Next(0, Sea.SIZE);
                    while (playSea.SeaTiles[x, y].IsAttacked)    // novata izbrana ne smee da e veke napadnata
                    {
                        x = rand.Next(0, Sea.SIZE);
                        y = rand.Next(0, Sea.SIZE);
                    }
                    playSea.SeaTiles[x, y].IsAttacked = true;
                    if (!playSea.SeaTiles[x, y].IsTaken)    // Ako na izbranoto mesto nema brod
                    {
                        g.FillRectangle(new SolidBrush(Color.DimGray), playSea.tiles[x, y]);    // Oboj go sivo
                        tbStatus.Text = "Opponent missed. Your turn!";
                        tbStatus.Refresh();
                        Thread.Sleep(500);
                        break;      // Prekini da pukas na red e igraco
                    }
                    // Ako ima brod na izbranoto mesto

                    // u stack dodaj gi slednite polinja
                    lastHit = new Point(x, y);
                    Point tmp = new Point(Math.Min(Sea.SIZE - 1, x + 1), y);
                    if (!lastHit.Equals(tmp) && !playSea.SeaTiles[tmp.X, tmp.Y].IsAttacked)
                        tilesAIShoot.Push(tmp);             // desno
                    tmp = new Point(Math.Max(0, x - 1), y);
                    if (!lastHit.Equals(tmp) && !playSea.SeaTiles[tmp.X, tmp.Y].IsAttacked)
                        tilesAIShoot.Push(tmp);             // levo
                    tmp = new Point(x, Math.Min(Sea.SIZE - 1, y + 1));
                    if (!lastHit.Equals(tmp) && !playSea.SeaTiles[tmp.X, tmp.Y].IsAttacked)
                        tilesAIShoot.Push(tmp);             // dole
                    tmp = new Point(x, Math.Max(0, y - 1));
                    if (!lastHit.Equals(tmp) && !playSea.SeaTiles[tmp.X, tmp.Y].IsAttacked)
                        tilesAIShoot.Push(tmp);             // gore
                }
                else  // Ako imame prethodno zacuvani tiles
                {
                    Point p;   // gi zimame koordinatite na prvoto
                    do
                    {
                        if (tilesAIShoot.Count == 0)
                        {
                            x = rand.Next(0, Sea.SIZE);
                            y = rand.Next(0, Sea.SIZE);
                            p = new Point(x, y);
                        }
                        else {
                            p = tilesAIShoot.Pop();
                            x = p.X;
                            y = p.Y;
                        }
                        if (x < 0 || x > Sea.SIZE - 1 || y < 0 || y > Sea.SIZE - 1)
                        {
                            x = lastHit.X;
                            y = lastHit.Y;
                        }
                    } while (playSea.SeaTiles[x, y].IsAttacked);   // novata izbrana ne smee da e veke napadnata


                    if (!playSea.SeaTiles[x, y].IsTaken)    // Ako na izbranoto mesto nema brod
                    {
                        g.FillRectangle(new SolidBrush(Color.DimGray), playSea.tiles[x, y]);    // Oboj go sivo
                        tbStatus.Text = "Opponent missed. Your turn!";
                        tbStatus.Refresh();
                        Thread.Sleep(1000);
                        playSea.SeaTiles[x, y].IsAttacked = true;
                        break;      // Prekini da pukas na red e igraco
                    }
                    // Ako ima pogodeno brod
                    
                    adjustStack(ref tilesAIShoot, p);   // adjust the stack tiles

                    int i = p.X + (p.X - lastHit.X);
                    int j = p.Y + (p.Y - lastHit.Y);
                    if (i >= 0 && i < Sea.SIZE && j >= 0 && j < Sea.SIZE)
                    {
                        Point tmp = new Point(i, j);
                        lastHit = p;
                        if (!lastHit.Equals(tmp) && !playSea.SeaTiles[i, j].IsAttacked)
                            tilesAIShoot.Push(tmp);
                    }
                    playSea.SeaTiles[x, y].IsAttacked = true;
                    
                }
                g.FillRectangle(new SolidBrush(Color.Red), playSea.tiles[x, y]);
                tbStatus.Text = "Opponent hits and fires again";
                tbStatus.Refresh();
                Thread.Sleep(1000);
                // proveri koi brod e pogoden i ako e unisten napisi soodvetna poraka u tbStatus
                foreach (Ship s in playSea.ships)
                {
                    if (s.attack(x, y) && !s.isAlive())
                    {
                        tbStatus.Text = "Your ship was destroyed.";
                        tilesAIShoot.Clear();
                        if(playSea.shipsLeft())
                        {
                            tbStatus.Text += "Opponent fires again!";
                            tbStatus.Refresh();
                            Thread.Sleep(750);
                        } else
                        {
                            tbStatus.Text = "You lose!";
                            tbStatus.Refresh();
                            DialogResult res = MessageBox.Show("Do you want a rematch?", "D E F E A T",
                                MessageBoxButtons.YesNo);
                            if(res == DialogResult.Yes)
                            {
                                Application.Restart();
                                Environment.Exit(0);
                            } else
                            {
                                Application.Exit();
                            }
                            break;
                        }

                        adjustStack(ref tilesAIShoot, lastHit);
                        Thread.Sleep(700);
                        break;
                    }
                }
            }
        }

        private void adjustStack(ref Stack<Point> stack, Point p)
        {
            Stack<Point> tempStack = new Stack<Point>();
            while(stack.Count != 0)
            {
                Point tmpPoint = stack.Pop();
                if (!(p.X == tmpPoint.X && (p.Y == tmpPoint.Y - 1 || p.Y == tmpPoint.Y + 1)) || !(p.Y == tmpPoint.Y && (p.X == tmpPoint.X - 1 || p.X == tmpPoint.X + 1)))
                    tempStack.Push(tmpPoint);
            }
            stack = tempStack;
        }

        public void initializeAISea()        // postavuvanje brodovi za opponent
        {
            List<Ship> ships = new List<Ship>();
            int[] lengths = { 2, 3, 4 };
            bool[] isVert = new bool[3];
            List<Point> coords = new List<Point>();
            Random rand = new Random();
            for (int i = 0; i < lengths.Length; ++i)
            {
                int x;
                int y;
                isVert[i] = rand.NextDouble() > 0.5;
                if(isVert[i])
                {
                    x = rand.Next(0, Sea.SIZE - 1);
                    y = rand.Next(0, Sea.SIZE - lengths[i]);
                    for(int j = 0; j < lengths[i]; j++)
                    {
                        coords.Add(new Point(x, y + j));
                    }
                }
                else
                {
                    x = rand.Next(0, Sea.SIZE - lengths[i]);
                    y = rand.Next(0, Sea.SIZE - 1);
                    for(int j = 0; j < lengths[i]; j++)
                    {
                        coords.Add(new Point(x + j, y));
                    }
                }
                Ship toAdd = new Ship(coords, lengths[i], isVert[i]);
                int l;
                for (l = 0; l < ships.Count; ++l)
                {
                    if (toAdd.intersectsWith(ships.ElementAt(l)))
                    {
                        break;
                    }
                }
                if(l != ships.Count)
                {
                    --i;
                    coords.Clear();
                    continue;
                }
                ships.Add(toAdd);
                coords.Clear();
            }
            oppSea.placeShips(ships);
        }

        public void placePlayerShip(Point location, int length, bool isVert)
        {
            List<Point> coords = new List<Point>();
            int x = 0;
            int y = 0;
            playSea.findIndexes(ref x, ref y, location);
            if (!isVert && x > Sea.SIZE - length)
                x = Sea.SIZE - length;
            if (isVert && y > Sea.SIZE - length)
                y = Sea.SIZE - length;
            coords.Add(new Point(x, y));
            if (isVert)
                for (int i = 1; i < length; ++i)
                    coords.Add(new Point(x, y + i));
            else
                for (int i = 1; i < length; ++i)
                    coords.Add(new Point(x + i, y));

            Ship toAdd = new Ship(coords, length, isVert);

            foreach (Ship sh in playSea.ships)
            {
                if(toAdd.intersectsWith(sh))
                {
                    tbStatus.Text = "You can't place a ship there. Try again";
                    tbStatus.Refresh();
                    return;
                }
            }
            tbStatus.Text = "Place the remaining ships and prepare for battle!";
            tbStatus.Refresh();
            playSea.ships.Add(toAdd);
            Graphics g = playerSea.CreateGraphics();
            if (isVert)
                for (int i = 0; i < length; ++i)
                {
                    playSea.SeaTiles[x, y + i].IsTaken = true;
                    g.FillRectangle(new SolidBrush(Color.DarkGoldenrod), playSea.tiles[x, y + i]);
                }
            else
                for (int i = 0; i < length; ++i)
                {
                    playSea.SeaTiles[x + i, y].IsTaken = true;
                    g.FillRectangle(new SolidBrush(Color.DarkGoldenrod), playSea.tiles[x + i, y]);
                }
            g.Dispose();
            if (cbShipLength.Items.Count > 1)
            {
                cbShipLength.Items.RemoveAt(cbShipLength.SelectedIndex);
                cbShipLength.SelectedIndex = 0;
            }
            else {
                cbShipLength.Enabled = false;
            }
            if (playSea.ships.Count == 3)
            {
                playerInit = false;
                rbtnHorizontal.Enabled = false;
                rbtnVertical.Enabled = false;
                init = true;
                opponentSea.Enabled = true;
                tbStatus.Text = "You're first. Fire when ready!";
                gameStarted = true;
                return;
            }
        }

        private void playerSea_MouseMove(object sender, MouseEventArgs e)
        {
            if (playerInit)
            {
                Point mouse = Cursor.Position;
                mouse = playerSea.PointToClient(mouse);
                int len = Int32.Parse(cbShipLength.Text);
                bool isVert = rbtnVertical.Checked;
                playSea.placingShipsHover(mouse, playerSea.CreateGraphics(), len, isVert);
            }
        }

        private void playerSea_MouseClick(object sender, MouseEventArgs e)
        {
            if (!gameStarted)
            {
                Point mouse = Cursor.Position;
                mouse = playerSea.PointToClient(mouse);
                int len = Int32.Parse(cbShipLength.Text);
                bool isVert = rbtnVertical.Checked;
                placePlayerShip(mouse, len, isVert);
            }
        }

        private void playerSea_MouseLeave(object sender, EventArgs e)
        {
            if(!gameStarted)
                playSea.mouseLeft(playerSea.CreateGraphics());
        }
    }
}
