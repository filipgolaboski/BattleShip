using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BattleShip
{
    public class BattleBoard : Panel
    {
        Stack<Tile> possibleClicks = new Stack<Tile>();
        public PlayerWarBoard playerBoard;
        public OpponentWarBoard opponentBoard;
        public int lasti = 0;
        public int lastj = 0;
        public bool huntIsOn = false;
        private int shipHit = 0;
        public System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private int c = 0;
        public Label l = new Label();
        public List<HashSet<Index>> playerListBoats;
        public List<HashSet<Index>> opponentListBoats;
        private bool firstTime = true;
        public Button newGame = new Button();
        private BattleContainer bt = null;
        private Label lbTitle = new Label();
        private Label lbOpponent = new Label();
        private Label lbPlayer = new Label();
        private int sTilesHit = 0;
        private Random r = new Random();
        public BattleBoard(PlayerSetupBoard pt, OpponentSetupBoard st , List<HashSet<Index>> playerBoatList, List<HashSet<Index>> opponentBoatList)
        {
            playerBoard = new PlayerWarBoard(pt.Tiles);
            opponentBoard = new OpponentWarBoard(st.Tiles);
            setPlayerControls();
            playerListBoats = playerBoatList;
            opponentListBoats=opponentBoatList;
            opponentBoard.Location = st.Location;
            playerBoard.Location = pt.Location;
            playerBoard.Enabled = false;
            this.Controls.Add(playerBoard);
            this.Controls.Add(opponentBoard);
            l.Size = new Size(900, 30);
            l.Location = new Point(0, playerBoard.Height + 120);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Bold);
            l.ForeColor = Color.AntiqueWhite;
            this.Controls.Add(l);
            l.Text = "Cannons are ready, captain! Fire when ready!";
            playerBoard.setBattleBoard(this);
            opponentBoard.setBattleBoard(this);
            t.Interval = 400;
            t.Start();
            t.Tick += tick;
            newGame.Size = new Size(80, 50);
            newGame.Location = new Point(opponentBoard.Width + opponentBoard.Location.X - newGame.Width, playerBoard.Height + 105);
            newGame.Font = new Font("Arial", 10, FontStyle.Italic);
            newGame.Text = "New Game";
            this.Controls.Add(newGame);
            this.Controls[this.Controls.Count - 1].BringToFront();
            lbTitle.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Bold | FontStyle.Italic);
            lbTitle.Size = new Size(550, 30);
            lbTitle.Location = new Point(380, 10);
            lbTitle.Text = "B A T T L E";
            lbTitle.ForeColor = Color.AntiqueWhite;
            this.Controls.Add(lbTitle);
            lbOpponent.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Italic | FontStyle.Bold);
            lbOpponent.Size = new Size(150, 30);
            lbOpponent.Location = new Point(140, 70);
            lbOpponent.Text = "Your sea";
            lbOpponent.ForeColor = Color.AntiqueWhite;
            this.Controls.Add(lbOpponent);
            lbPlayer.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Italic | FontStyle.Bold);
            lbPlayer.Size = new Size(250, 30);
            lbPlayer.Location = new Point(opponentBoard.Location.X + 110, 70);
            lbPlayer.Text = "Opponent sea";
            lbPlayer.ForeColor = Color.AntiqueWhite;
            lbPlayer.BackColor = Color.Transparent;
            this.Controls.Add(lbPlayer);

        }
        public void setBattleContainer(BattleContainer bt)
        {
            this.bt = bt;
        }
        private void tick(object sender, EventArgs e)
        {
            if (huntIsOn)
            {
                if (c == 10)
                {
                    c = 0;
                    AIclick();
                }
                else
                {

                    if (c > 3)
                    {
                        if (c % 2 == 0)
                        {
                            l.Text = "Enemy is thinking.";
                        }
                        else {

                            l.Text = "Enemy is thinking..";
                        }
                    }
                    
                    c++;
                    

                }
            }
        }
        public  void AIclick()
        {

            if (possibleClicks.Count == 0)
            {
                firstTime = true;
                int i = calcI();
                int j = calcJ(i);
                while (playerBoard.WarTile[i, j].clicked)
                {
                    i = calcI();
                    j = calcJ(i);
                }

                playerBoard.WarTile[i, j].click();
                lasti = i;
                lastj = j;
                if (playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    shipHit++;
                    l.Text = "Enemy hit our ship!";
                    addToPossible();
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            possibleClicks.Clear();
                            l.Text = "Enemy destroyed one of out battleships!";
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }
                    }
                }
                else
                {
                    l.Text = "Enemy missed! Fire when ready, captain";
                    opponentBoard.Enabled = true;
                    huntIsOn = false;
                    return;
                }
            }
            else
            {

                Tile t = possibleClicks.Pop();
                while(t.clicked && possibleClicks.Count > 0)
                {
                    t = possibleClicks.Pop();
                }
                playerBoard.WarTile[t.i, t.j].click();
                if (playerBoard.WarTile[t.i, t.j].boatHere)
                {
                    if (firstTime)
                    {
                        possibleClicks.Clear();
                        if (lastj - 1 == t.j)
                            if (lastj + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj + 1]);
                        if (lasti + 1 == t.i)
                            if (lasti - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                        if (lastj + 1 == t.j)
                            if (lastj - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj - 1]);
                        if (lasti - 1 == t.i)
                            if (lasti + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                        
                    }
                    changePossible(t);
                    firstTime = false;      
                    shipHit++;
                    l.Text = "Enemy hit our ship!";
                    lasti = t.i;
                    lastj = t.j;
                    
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            l.Text = "Enemy destroyed one of our battleships";
                            possibleClicks.Clear();
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }
                        

                    }

                }
                else
                {
                    l.Text = "Enemy missed! Fire when ready, captain";
                    opponentBoard.Enabled = true;
                    huntIsOn = false;
                    return;
                }

            }


                
            if (shipHit == 18)
            {
                l.Text = "Enemy is victorious";
                DialogResult res = MessageBox.Show("Enemy wins! Do you want a rematch?", "D E F E A T",
                    MessageBoxButtons.YesNo
                    );
                t.Stop();
                if (res == DialogResult.Yes)
                {
                    startNewGame();
                } else
                {
                    Application.Exit();
                }
            }
          
            
        }
       
        public void changePossible(Tile t)
        {
            if (t.i - 1 >= 0 && playerBoard.WarTile[t.i - 1, t.j].clicked && playerBoard.WarTile[t.i - 1, t.j].boatHere)
                if (t.i + 1 < 10 && !playerBoard.WarTile[t.i + 1, t.j].clicked)
                    possibleClicks.Push(playerBoard.WarTile[t.i + 1, t.j]);
            if(t.i+1 < 10 && playerBoard.WarTile[t.i + 1, t.j].clicked && playerBoard.WarTile[t.i + 1, t.j].boatHere)
                if(t.i-1>=0 && !playerBoard.WarTile[t.i - 1, t.j].clicked)
                    possibleClicks.Push(playerBoard.WarTile[t.i - 1, t.j]);
            if (t.j - 1 >= 0 && playerBoard.WarTile[t.i, t.j - 1].clicked && playerBoard.WarTile[t.i, t.j - 1].boatHere)
                if (t.j + 1 < 10 && !playerBoard.WarTile[t.i, t.j + 1].clicked)
                    possibleClicks.Push(playerBoard.WarTile[t.i, t.j + 1]);
            if (t.j + 1 < 10 && playerBoard.WarTile[t.i, t.j + 1].clicked && playerBoard.WarTile[t.i, t.j + 1].boatHere)
                if (t.j - 1 >= 0 && !playerBoard.WarTile[t.i, t.j - 1].clicked)
                    possibleClicks.Push(playerBoard.WarTile[t.i, t.j - 1]);
        }
        public void addToPossible()
        {
            List<Tile> list = new List<Tile>();
            if (lasti > 0)
                if (!playerBoard.WarTile[lasti - 1, lastj].clicked)
                    list.Add(playerBoard.WarTile[lasti - 1, lastj]);
            if (lastj > 0)
                if (!playerBoard.WarTile[lasti, lastj - 1].clicked)
                    list.Add(playerBoard.WarTile[lasti, lastj - 1]);
            if (lasti < 9)
                if (!playerBoard.WarTile[lasti + 1, lastj].clicked)
                    list.Add(playerBoard.WarTile[lasti + 1, lastj]);
            if (lastj < 9)
                if (!playerBoard.WarTile[lasti, lastj + 1].clicked)
                    list.Add(playerBoard.WarTile[lasti, lastj + 1]);
            Shufle(ref list);
        }
        private void Shufle(ref List<Tile> list)
        {
            Random r = new Random();
            while(list.Count > 0)
            {
                int n = r.Next(0, list.Count);
                possibleClicks.Push(list[n]);
                list.RemoveAt(n);
            }
        }
        public int calcI()
        {
               Random ri = new Random();
        int i = ri.Next(10);
            return i;

        }
        public int calcJ(int i)
        {
            Random rj = new Random();
            int j = rj.Next(10);
            return j;
        }
       
        public void startNewGame()
        {
            this.Dispose();
            bt.startSetup();
        }
        public void setPlayerControls()
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (opponentBoard.WarTile[i, j].boatHere)
                    {
                        opponentBoard.WarTile[i, j].Click += ShipTile_Click;
                    }
                    else
                    {
                        opponentBoard.WarTile[i, j].Click += Tile_Click;
                    }
                }
            }
        }
        private void Tile_Click(object sender, EventArgs e)
        {
            string s1 = "That was a miss";
            string s2 = "We hit nothing but water, captain";
            string s3 = "No enemy ships on that position";
            int n = r.Next(0, 3);
            switch (n)
            {
                case 0:
                    l.Text = s1;
                    break;
                case 1:
                    l.Text = s2;
                    break;
                case 2:
                    l.Text = s3;
                    break;
            }
            huntIsOn = true;
            opponentBoard.Enabled = false;
        }
        private void ShipTile_Click(object sender, EventArgs e)
        {

            sTilesHit++;
            if (sTilesHit < 18)
            {
                l.Text = "We hit an enemy ship! Fire again, captain!";
                Tile t = (Tile)sender;
                foreach (HashSet<Index> boat in opponentListBoats)
                {
                    Index ind = new Index(t.i, t.j);
                    boat.Remove(ind);
                    if (boat.Count == 0)
                    {
                        l.Text = "Great job, you destroyed an enemy battleship! Fire again!";
                        opponentListBoats.Remove(boat);
                        break;
                    }

                }
            }
            else
            {
                t.Stop();
                l.Text = "You W I N";
                DialogResult res = MessageBox.Show("You are victorious, captain! How about another round?", "V I C T O R Y",
                    MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    startNewGame();
                }
                else
                    Application.Exit();
            }

        }
    }
}
