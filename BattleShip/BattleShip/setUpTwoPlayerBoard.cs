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
        public setUpTwoPlayerBoard(int numBoats) {
            playerBoard = new PlayerSetupBoard(numBoats, 0);
            opponentBoard = new OpponentSetupBoard();
            opponentBoard.Location = new System.Drawing.Point(5, 5);
            opponentBoard.setBoats();
            this.Controls.Add(opponentBoard);
            playerBoard.Location =new System.Drawing.Point (5,365);
            this.Controls.Add(playerBoard);
            threeBoats.Text = "3boat";
            threeBoats.MouseClick += threeBoats_click;
            threeBoats.Location = new System.Drawing.Point(1250, 550);
            threeBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(threeBoats);
            fourBoats.Text = "4boat";
            fourBoats.MouseClick += fourBoats_click;
            fourBoats.Location = new System.Drawing.Point(1250, 600);
            fourBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(fourBoats);
            fiveBoats.Text = "5boat";
            fiveBoats.MouseClick += fiveBoats_click;
            fiveBoats.Location = new System.Drawing.Point(1250, 650);
            fiveBoats.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(fiveBoats);
            changeDir.Text = "Change Direction";
            changeDir.MouseClick += changeDir_click;
            changeDir.Location = new System.Drawing.Point(1150, 550);
            changeDir.Size = new System.Drawing.Size(100, 150);
            this.Controls.Add(changeDir);
            startGame.Location = new System.Drawing.Point(1150, 500);
            startGame.Size = new System.Drawing.Size(200, 50);
            startGame.Text = "Start";
            startGame.MouseClick += startGame_click;
            this.Controls.Add(startGame);

        }
        private void fourBoats_click(object sender, EventArgs e)
        {
            playerBoard.numBoats = 4;
        }
        private void threeBoats_click(object sender, EventArgs e)
        {
            playerBoard.numBoats = 3;
        }
        private void fiveBoats_click(object sender, EventArgs e)
        {
            playerBoard.numBoats = 5;
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
            this.Dispose();
        }

    }
    }

