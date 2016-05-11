using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private bool firstTime = true;
        public Button newGame = new Button();
        private BattleContainer bt = null;
        public BattleBoard(PlayerSetupBoard pt, OpponentSetupBoard st , List<HashSet<Index>> playerBoatList, List<HashSet<Index>> opponentBoatList)
        {
            playerBoard = new PlayerWarBoard(pt.Tiles);
            opponentBoard = new OpponentWarBoard(st.Tiles);
            playerListBoats = playerBoatList;
            opponentBoard.setListOfBoats(opponentBoatList);
            opponentBoard.Location = st.Location;
            playerBoard.Location = pt.Location;
            playerBoard.Enabled = false;
            this.Controls.Add(playerBoard);
            this.Controls.Add(opponentBoard);
            l.Size = new System.Drawing.Size(500, 30);
            l.Location = new System.Drawing.Point(10, playerBoard.Height + 30);
            this.Controls.Add(l);
            playerBoard.setBattleBoard(this);
            opponentBoard.setBattleBoard(this);
            t.Interval = 250;
            t.Start();
            t.Tick += tick;
            newGame.Size = new System.Drawing.Size(100, 50);
            newGame.Location = new System.Drawing.Point(opponentBoard.Width + opponentBoard.Location.X - newGame.Width, playerBoard.Height); ;
            newGame.Text = "New Game";
            newGame.MouseClick += newGame_click;
            this.Controls.Add(newGame);
        }
        public void setBattleContainer(BattleContainer bt)
        {
            this.bt = bt;
        }
        private void tick(object sender, EventArgs e)
        {
           // opponentBoard.Enabled = false;
            
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
                            l.Text = "Opponent Is thinking.";
                        }
                        else {

                            l.Text = "Opponent Is thinking..";
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
                //opponentBoard.Enabled = false;
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
                    l.Text = "Opponent Hit!";
                    addToPossible();
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            possibleClicks.Clear();
                            l.Text = "BOAT DESTROYED";
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }
                    }
                }
                else
                {
                    l.Text = "Opponent Miss!";
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
                        if (lasti - 1 == t.i)
                            if (lasti + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                        if (lasti + 1 == t.i)
                            if (lasti - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                        if (lastj - 1 == t.j)
                            if (lastj + 1 < 10)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj + 1]);
                        if (lastj + 1 == t.j)
                            if (lastj - 1 >= 0)
                                possibleClicks.Push(playerBoard.WarTile[lasti, lastj - 1]);
                    }
                    changePossible(t);
                    firstTime = false;      
                    shipHit++;
                    l.Text = "Opponent Hit!";
                    lasti = t.i;
                    lastj = t.j;
                    
                    foreach (HashSet<Index> boat in playerListBoats)
                    {
                        Index ind = new Index(lasti, lastj);
                        boat.Remove(ind);
                        if (boat.Count == 0)
                        {
                            l.Text = "BOAT DESTROYED";
                            possibleClicks.Clear();
                            firstTime = true;
                            playerListBoats.Remove(boat);
                            break;
                        }

                    }

                }
                else
                {
                    l.Text = "Opponent Miss!";
                    opponentBoard.Enabled = true;
                    huntIsOn = false;
                    return;
                }

            }


                
            if (shipHit == 18)
            {
                l.Text = "Opponent wins";
                t.Stop();
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
            if (lasti > 0)
            {
                if (!playerBoard.WarTile[lasti - 1, lastj].clicked)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                }

            }
            if (lastj > 0)
            {
                if (!playerBoard.WarTile[lasti, lastj - 1].clicked)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti, lastj - 1]);
                }
            }
            if (lasti < 9)
            {
                if (!playerBoard.WarTile[lasti + 1, lastj].clicked)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                }

            }
           
            if (lastj < 9)
            {
                if (!playerBoard.WarTile[lasti, lastj + 1].clicked)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti, lastj + 1]);
                }
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
        private void newGame_click(object sender, EventArgs e)
        {
            this.Hide();
            bt.startSetup();
        }
    }
}
