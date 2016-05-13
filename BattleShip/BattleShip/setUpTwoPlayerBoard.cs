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
        public Label titleLabel = new Label();
        public Label oLabel = new Label();
        public Label pLabel= new Label();
        private int[] numBoats = { 3, 1, 1};
        public Label boatsLabel = new Label();
        public setUpTwoPlayerBoard() {
            titleLabel.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.AntiqueWhite;
            titleLabel.Size = new Size(550, 30);
            titleLabel.Location = new Point(180, 10);
            titleLabel.Text = "Welcome captain, prepare for battle";
            titleLabel.BackColor = Color.Transparent;
            this.Controls.Add(titleLabel);
            playerBoard = new PlayerSetupBoard(3, 0);
            playerBoard.Location = new Point(5, 100);
            playerBoard.Click += board_click;
            this.Controls.Add(playerBoard);
            opponentBoard = new OpponentSetupBoard();
            opponentBoard.Location = new Point(500, 100);
            this.Controls.Add(opponentBoard);
            oLabel.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Italic | FontStyle.Bold);
            oLabel.ForeColor = Color.AntiqueWhite;
            oLabel.Size = new Size(150, 30);
            oLabel.Location = new Point(140, 70);
            oLabel.Text = "Your sea";
            this.Controls.Add(oLabel);
            pLabel.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Italic | FontStyle.Bold);
            pLabel.ForeColor = Color.AntiqueWhite;
            pLabel.Size = new Size(250, 30);
            pLabel.Location = new Point(opponentBoard.Location.X + 110, 70);
            pLabel.Text = "Opponent sea";
            this.Controls.Add(pLabel);
            threeBoats.Text = "Destroyer";
            threeBoats.MouseClick += threeBoats_click;
            threeBoats.Location = new Point(5, playerBoard.Height + 105);
            threeBoats.Size = new Size(100, 50);
            threeBoats.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            this.Controls.Add(threeBoats);
            fourBoats.Text = "Battleship";
            fourBoats.MouseClick += fourBoats_click;
            fourBoats.Location = new Point(threeBoats.Width + 5, playerBoard.Height + 105);
            fourBoats.Size = new Size(100, 50);
            fourBoats.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            this.Controls.Add(fourBoats);
            fiveBoats.Text = "Aircraft carier";
            fiveBoats.MouseClick += fiveBoats_click;
            fiveBoats.Location = new Point(threeBoats.Width + fourBoats.Width + 5, playerBoard.Height + 105);
            fiveBoats.Size = new Size(100, 50);
            fiveBoats.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            this.Controls.Add(fiveBoats);
            changeDir.Text = "Change orientation";
            changeDir.MouseClick += changeDir_click;
            changeDir.Location = new Point(threeBoats.Width + fourBoats.Width + fiveBoats.Width + 5, playerBoard.Height + 105);
            changeDir.Size = new Size(100, 50);
            changeDir.Font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
            this.Controls.Add(changeDir);
            boatsLabel.Text = "Ships left \n"+ playerBoard.numBoatsOnTile[0];
            boatsLabel.ForeColor= Color.AntiqueWhite;
            boatsLabel.Location = new Point(changeDir.Location.X+105,changeDir.Location.Y+10);
            boatsLabel.Font= new Font(new FontFamily("Arial"), 10, FontStyle.Bold); 
            boatsLabel.Size = new Size(90, 50);
            Controls.Add(boatsLabel);
            startGame.Size = new Size(400, 50);
            startGame.Location =new Point(opponentBoard.Location.X, playerBoard.Height + 105);
            startGame.Font = new Font(new FontFamily("Arial"), 15, FontStyle.Bold | FontStyle.Italic);
            startGame.Text = "START THE BATTLE";
     
            this.Controls.Add(startGame);

        }
       
        private void fourBoats_click(object sender, EventArgs e)
        {
            playerBoard.numBoats = 4;
            if (playerBoard.numBoatsOnTile[1] > 0)
            {
                boatsLabel.Text = "Ships left \n" + playerBoard.numBoatsOnTile[1];
                playerBoard.Enabled = true;
            }
            else
            {
                playerBoard.Enabled = false;
            }
        }
        private void threeBoats_click(object sender, EventArgs e)
        {
            playerBoard.numBoats = 3;
            if (playerBoard.numBoatsOnTile[0] > 0)
            {
                boatsLabel.Text = "Ships left \n" + playerBoard.numBoatsOnTile[0];
                playerBoard.Enabled = true;
            }
            else
            {
                playerBoard.Enabled = false;
            }
            
        }
        private void fiveBoats_click(object sender, EventArgs e)

        {
            playerBoard.numBoats = 5;
            if (playerBoard.numBoatsOnTile[2] > 0)
            {
                boatsLabel.Text = "Ships left \n" + playerBoard.numBoatsOnTile[2];
                playerBoard.Enabled = true;
             }
            else
            {
                playerBoard.Enabled = false;
            }
            
        }
        private void board_click(object sender, EventArgs e)
        {
            if (playerBoard.numBoats == 3)
                boatsLabel.Text = "Ships left \n" + (playerBoard.numBoatsOnTile[0]-1);
            if (playerBoard.numBoats == 4)
                boatsLabel.Text = "Ships left \n" + (playerBoard.numBoatsOnTile[1]-1);
            if (playerBoard.numBoats == 5)
                boatsLabel.Text = "Ships left \n" + (playerBoard.numBoatsOnTile[2]-1);
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
      
        public bool gameReady()
        {
            return playerBoard.numBoatsOnTile[0] == 0 && playerBoard.numBoatsOnTile[1] == 0 && playerBoard.numBoatsOnTile[2] == 0;
        }
        

    }
    }

