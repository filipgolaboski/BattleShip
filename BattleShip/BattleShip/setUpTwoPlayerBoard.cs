using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public setUpTwoPlayerBoard(int numBoats) {
            playerBoard = new PlayerSetupBoard(numBoats, false);
            opponentBoard = new OpponentSetupBoard();
            Height = 2 * 337 + 200;
            Width = 1024;
            opponentBoard.Location = new System.Drawing.Point(5, 5);
            opponentBoard.setBoats();
            this.Controls.Add(opponentBoard);
            playerBoard.Location =new System.Drawing.Point (5,365);
            this.Controls.Add(playerBoard);
              
        }

       

        }
    }

