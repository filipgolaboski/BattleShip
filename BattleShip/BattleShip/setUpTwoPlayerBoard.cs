using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
   
    public class setUpTwoPlayerBoard : Panel
    {
        //panel kade shto se setiraat brodovite i se podeluva mapata na dvajca igrachi
        public PlayerSetupBoard playerBoard; // 3 e dolzhinata na brod false nasokata na brodot a true
        public OpponentSetupBoard opponentBoard ;// dali se setira mapata ili se igra (podocna ke se podelat na mapa shto se igra ili se setira imam idea samo ne vo 3 na sabajle xD)
        public Button fourBoats = new Button();
        public Button threeBoats = new Button();
        public Button fiveBoats = new Button();
        public Button changeDir = new Button();
        public Button startGame = new Button();
        public BattleContainer bt;
        private int[] numBoats = { 3, 1, 1};
        public setUpTwoPlayerBoard() {
            playerBoard = new PlayerSetupBoard(3, 0);
            opponentBoard = new OpponentSetupBoard();
            opponentBoard.Location = new System.Drawing.Point(500, 0);
            opponentBoard.setBoats();
            this.Controls.Add(opponentBoard);
            playerBoard.Location =new System.Drawing.Point (0,0);
            playerBoard.Click += board_click;
            this.Controls.Add(playerBoard);
            threeBoats.Text = "3boat";
            threeBoats.MouseClick += threeBoats_click;
            threeBoats.Location = new System.Drawing.Point(0, playerBoard.Height);
            threeBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(threeBoats);
            fourBoats.Text = "4boat";
            fourBoats.MouseClick += fourBoats_click;
            fourBoats.Location = new System.Drawing.Point(threeBoats.Width, playerBoard.Height);
            fourBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(fourBoats);
            fiveBoats.Text = "5boat";
            fiveBoats.MouseClick += fiveBoats_click;
            fiveBoats.Location = new System.Drawing.Point(threeBoats.Width + fourBoats.Width, playerBoard.Height);
            fiveBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(fiveBoats);
            changeDir.Text = "Change Direction";
            changeDir.MouseClick += changeDir_click;
            changeDir.Location = new System.Drawing.Point(threeBoats.Width + fourBoats.Width + fiveBoats.Width, playerBoard.Height);
            changeDir.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(changeDir);
            startGame.Size = new System.Drawing.Size(100, 50);
            startGame.Location =new System.Drawing.Point(opponentBoard.Width + opponentBoard.Location.X - startGame.Width, playerBoard.Height); ;
            startGame.Text = "Start";
            startGame.MouseClick += startGame_click;
            this.Controls.Add(startGame);

        }
        public void setBattleContainer(BattleContainer bt)
        {
            this.bt = bt;
        }
        private void fourBoats_click(object sender, EventArgs e)
        {
            if (numBoats[1] > 0)
            {
                playerBoard.Enabled = true;
                playerBoard.numBoats = 4;
            }
            else
            {
                
                playerBoard.Enabled = false;
            }
        }
        private void threeBoats_click(object sender, EventArgs e)
        {
            if (numBoats[0] > 0)
            {
                playerBoard.Enabled = true;
                playerBoard.numBoats = 3;
            }
            else
            {
                playerBoard.Enabled = false;
            }
            
        }
        private void fiveBoats_click(object sender, EventArgs e)
        {
            if (numBoats[2] > 0)
            {
                playerBoard.Enabled = true;
                playerBoard.numBoats = 5;
            }
            else
            {
                playerBoard.Enabled = false;
            }
            
        }
        private void board_click(object sender, EventArgs e)
        {
            if (playerBoard.numBoats == 3)
            {
                numBoats[0]--;
                if (numBoats[0] <= 0)
                {
                    playerBoard.Enabled = false;
                }
            }
            if (playerBoard.numBoats == 4)
            {
                numBoats[1]--;
                if (numBoats[1] <= 0)
                {
                    playerBoard.Enabled = false;
                }
            }
            if (playerBoard.numBoats == 5)
            {
                numBoats[2]--;
                if (numBoats[2] <= 0)
                {
                    playerBoard.Enabled = false;
                }
            }
        }
        private void changeDir_click(object sender, EventArgs e)
        {
            if (playerBoard.shipRotation == 1)
            {
                playerBoard.shipRotation = 0;
            }
            else
            {
                playerBoard.shipRotation = 1;
            }
        }
        private void startGame_click(object sender, EventArgs e)
        {
            this.Hide();
            bt.startGame();
            
        }

    }
    }

