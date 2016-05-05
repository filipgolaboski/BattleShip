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
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        private int c = 0;
        public Label l = new Label();
        public List<HashSet<Index>> playerListBoats;
        private Random rj = new Random();
        private Random ri = new Random();
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
                            l.Text = "BOAT DESTROYED";
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
                }else
                {

                    Tile t = possibleClicks.Pop();
                    playerBoard.WarTile[t.i, t.j].click();

                    if (playerBoard.WarTile[t.i, t.j].boatHere)
                    {
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
                            playerListBoats.Remove(boat);
                            break;
                        }
                       
                    }
                    changePossible();
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
       
        public void changePossible()
        {
           
            if (lasti != 0 && lasti!=9)
            {
                
                if (!playerBoard.WarTile[lasti + 1, lastj].clicked && playerBoard.WarTile[lasti-1, lastj].clicked && playerBoard.WarTile[lasti, lastj].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                }
                
                if (!playerBoard.WarTile[lasti - 1, lastj].clicked && playerBoard.WarTile[lasti+1, lastj].clicked && playerBoard.WarTile[lasti, lastj].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                }
            }
            if (lastj != 0 && lastj != 9)
            {
                if (!playerBoard.WarTile[lasti, lastj-1].clicked && playerBoard.WarTile[lasti, lastj+1].clicked&& playerBoard.WarTile[lasti, lastj].clicked && playerBoard.WarTile[lasti , lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti, lastj-1]);
                }
               
                if (!playerBoard.WarTile[lasti, lastj+1].clicked && playerBoard.WarTile[lasti, lastj-1].clicked && playerBoard.WarTile[lasti, lastj].clicked && playerBoard.WarTile[lasti , lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti , lastj+1]);
                }
                
            }
            if (lasti == 0)
            {
                if (!playerBoard.WarTile[lasti + 1, lastj].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti + 1, lastj]);
                }
            }
            if (lasti == 9)
            {
                if (!playerBoard.WarTile[lasti - 1, lastj].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti - 1, lastj]);
                }
            }

            if (lastj == 0)
            {
                if (!playerBoard.WarTile[lasti, lastj + 1].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti, lastj + 1]);
                }
               
            }
            if (lastj == 9)
            {
                if (!playerBoard.WarTile[lasti, lastj - 1].clicked && playerBoard.WarTile[lasti, lastj].boatHere)
                {
                    possibleClicks.Push(playerBoard.WarTile[lasti, lastj - 1]);
                }

            }


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
            
            int i = ri.Next(10);
            return i;

        }
        public int calcJ(int i)
        {
            
            
            int j = 0;
            if (i % 2 == 0)
            {
                j = rj.Next(10);
                if (j % 2 != 0)
                {
                    if (j != 9)
                    {
                        j += 1;
                    }
                    else
                    {
                        j -= 1;
                    }
                }
            }
            else
            {
                j = rj.Next(10);
                if (j % 2 == 0)
                {
                    if (j != 9)
                    {
                        j += 1;
                    }
                    else
                    {
                        j -= 1;
                    }
                }

            }
            
            return j;
        }
    }
}
